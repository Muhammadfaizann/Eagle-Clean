
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using Custodian.ActivityLog;
using Custodian.Helpers;
using Custodian.Helpers.LocationService;
using Custodian.Messages;
using Custodian.Models;
using Custodian.Models.ServerModels;
using Custodian.Popups;
using Custodian.Services.ProofOfWork;
using Custodian.ViewModels;
using Microsoft.Maui.Controls;
using System.Text.Json;

namespace Custodian.Pages;

public partial class ProofOfWork : ContentPage, IQueryAttributable
{
    IDispatcherTimer timer;
    TimeSpan plannedTime;
    private TimeSpan prevTime;
    double progressPerSec;
    TimeSpan timer_date_time;
    ILocationService _locationService;
    IProofOfWorkService _proofOfWorkService;
    bool IsBackgroundThreadRunning = true;
    ProofOfWorkViewModel _viewmodel;
    public ProofOfWork(ProofOfWorkViewModel viewModel, ILocationService locationService, IProofOfWorkService proofOfWorkService)
	{
        try
        {
            InitializeComponent();
            _locationService = locationService;
            _proofOfWorkService = proofOfWorkService;
             this.BindingContext = _viewmodel  = viewModel;
            
            WeakReferenceMessenger.Default.Register<EndRouteMessage>(this, OnEndRouteMessageReceived);
            WeakReferenceMessenger.Default.Register<StopTimerMessage>(this, OnStopTimerMessageReceived);
            timer = Dispatcher.CreateTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) =>
            {
                if (timer_date_time.Equals(plannedTime))  // if timer exceed planned time 
                {
                    Color blueColor = Color.FromArgb("#224BA9");
                    Brush blueBrush = new SolidColorBrush(blueColor);
                    timerProgressBar.TrackFill = blueBrush;

                    Color redColor = Color.FromArgb("#F44336");
                    Brush redBrush = new SolidColorBrush(redColor);
                    timerProgressBar.ProgressFill = redBrush;

                    timerProgressBar.Progress = 0;


                }
                lblTime.Text = timer_date_time.ToString("t");
                timer_date_time = timer_date_time.Add(new TimeSpan(0, 0, 1));
                timerProgressBar.Progress = timerProgressBar.Progress + progressPerSec;

            };
            prevTime = TimeSpan.Zero;
        }
        catch(Exception ex)
        {
             Logger.Log("1", "Exception", ex.Message);
        }
    } 

    private void OnStopTimerMessageReceived(object recipient, StopTimerMessage message)
    {
       timer.Stop();
       IsBackgroundThreadRunning = false;
    }

    protected override void OnAppearing()
    {
        try
        {

            base.OnAppearing();
            btnEndRoute.IsEnabled = true;
            btnAddPics.IsEnabled = true;
            

            if (_viewmodel.CleaningPlanList.Count == 0)
                btnEndRoute.Text = "Complete Route";

            DeviceDisplay.Current.KeepScreenOn = true;
        }
        catch(Exception ex )
        {
            Logger.Log("1", "Exception", ex.Message);
        }
        //RuntheBackGroundThread();
    }
    private async void RuntheBackGroundThread()
    {
        await System.Threading.Tasks.Task.Run(async() =>
        {
            var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));

            while (IsBackgroundThreadRunning)
            {
                Utils.activeRouteRecord.seq = "2";
                Utils.activeRouteRecord.endDate = DateTime.Now.ToString("MM/dd/yyyy");
                Utils.activeRouteRecord.endTime = DateTime.Now.ToString("HH:mm:ss");
                Location currentLocation = await _locationService.GetCurrentLocation();
                Utils.activeRouteRecord.startLatitude = currentLocation.Latitude.ToString();
                Utils.activeRouteRecord.startLongitude = currentLocation.Longitude.ToString();
                Utils.activeRouteRecord.status = "InProgress";

                string jsonRecord = JsonSerializer.Serialize<MergeRecord>(Utils.activeRouteRecord);
                Utils.activeRouteFileName = await FileSystemService.Write(jsonRecord);
                
                string guid = Utils.activeRouteFileName.Split("_")[0];
                Utils.OfflineRecords.Add(new WorkRecord() { id = Guid.Parse(guid), filename = Utils.activeRouteFileName, json = jsonRecord });


            }
        });
    }
    private void OnEndRouteMessageReceived(object recipient, EndRouteMessage message)
    {
        try
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Helpers.Utils.activeRouteRecord.endBarcode = message.Value;
                btnEndRoute_Clicked(null, null);
            });

        }
        catch (Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
     {
        try
        {
            MergeRecord record;
            Route route;
            if (query.Count == 0)
            {
                record = Utils.activeRouteRecord;
                route = Utils.activeAssigment;
                timer_date_time = new TimeSpan(0, 0, 0);
                timerProgressBar.Progress = 0;
                timer.Start();

            }
            else   // Continue as partial route
            {
                record = query["param"] as MergeRecord;
               
                timer_date_time = new TimeSpan(0,0, int.Parse(record.actualTime));
                Utils.activeRouteRecord = record;
                route = JsonSerializer.Deserialize<Route>(record.startBarcode);
            }



            TimeSpan estimatedTimeSpan = TimeSpan.FromSeconds(double.Parse(record.estimatedTime));
            route.plannedTime = estimatedTimeSpan.ToString("t");

            lblPlannedTime.Text = route.plannedTime;
            plannedTime = TimeSpan.ParseExact(lblPlannedTime.Text, "t", null);
            var seconds = plannedTime.TotalSeconds;
            progressPerSec = (1 / seconds) * 100;

            if (query.Count != 0)   // Continue as partial route
            {
                timerProgressBar.Progress = int.Parse(record.actualTime)*progressPerSec;
                timer.Start();
            }

            // populate Cleaning plan
            route.taskList = new List<Models.Task>();

            foreach (var task in record.tasksIncomplete)
            {
                var strings = task.Split('|');
                route.taskList.Add(new Models.Task { Description = strings[0], PlannedTimeInMint = (int.Parse(strings[1]) / 60).ToString() });

            }

            routeTitle.Text = route.rte;
            description.Text = route.desc;
            _viewmodel.CleaningPlanList = route.taskList.ToObservableCollection();

           
            
        }
        catch(Exception ex)
        {
             Logger.Log("1", "Exception", ex.Message);
        }
    }

    private void AddPictures_Clicked(object sender, EventArgs e)
    {
        try
        {
            loader.IsRunning = loader.IsVisible = true;
            btnAddPics.IsEnabled = false;
            Navigation.PushAsync(new AddPicturesPage());
            loader.IsRunning = loader.IsVisible = false;
        }
        catch (Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    }

    private void btnEndRoute_Clicked(object sender, EventArgs e)
    {
        try
        {
            btnEndRoute.IsEnabled = false;
            if (btnEndRoute.Text == "Complete Route")
            {
                var popup = new EndRoutePopup(true, _locationService);
                this.ShowPopup(popup);
            }
            else
            {
                var popup = new EndRoutePopup(false, _locationService);
                this.ShowPopup(popup);
            }
           
        }
        catch(Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }

    }

    protected override void OnDisappearing()
    {
        try
        {
            base.OnDisappearing();
            WeakReferenceMessenger.Default.Unregister<EndRouteMessage>(this);
            WeakReferenceMessenger.Default.Unregister<StopTimerMessage>(this);
            WeakReferenceMessenger.Default.Unregister<TaskCompletedMessage>(this.BindingContext);
            DeviceDisplay.Current.KeepScreenOn = false;
        }
        catch(Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            loader.IsRunning = loader.IsVisible = true;
            Models.Task step = e.Parameter as Models.Task;
            TimeSpan currentTimer = TimeSpan.Parse(lblTime.Text);
            var popup = new TaskCompletedPopup();
            this.ShowPopup(popup);
            loader.IsRunning = loader.IsVisible = false;

        }
        catch(Exception ex )
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    }
}



