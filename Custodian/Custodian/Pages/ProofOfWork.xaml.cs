
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
    public ProofOfWork(ProofOfWorkViewModel viewModel, ILocationService locationService, IProofOfWorkService proofOfWorkService)
	{
        try
        {
            InitializeComponent();
            _locationService = locationService;
            _proofOfWorkService = proofOfWorkService;
            this.BindingContext = viewModel;
            WeakReferenceMessenger.Default.Register<EndRouteMessage>(this, OnEndRouteMessageReceived);
            WeakReferenceMessenger.Default.Register<StopTimerMessage>(this, OnStopTimerMessageReceived);
            timer = Dispatcher.CreateTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) =>
            {
                if (timer_date_time.Equals(plannedTime))
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
        base.OnAppearing();
        timer_date_time = new TimeSpan(0, 0, 0);
        timerProgressBar.Progress = 0;
        timer.Start();
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
                await DatabaseService.Write(jsonRecord);

                
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

        }
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        try
        {
            var obj = query["param"] as Route;
            Utils.activeAssigment = obj;
            routeTitle.Text = obj.rte;
            description.Text = obj.desc;
            var viewmodel = this.BindingContext as ProofOfWorkViewModel;
            viewmodel.CleaningPlanList = obj.taskList.ToObservableCollection();
            lblPlannedTime.Text = obj.plannedTime;
            plannedTime = TimeSpan.ParseExact(lblPlannedTime.Text, "t", null);
            var seconds = plannedTime.TotalSeconds;
            progressPerSec = (1 / seconds) * 100;
        }
        catch(Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    }

    private void AddPictures_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddPicturesPage());
    }

    private void btnEndRoute_Clicked(object sender, EventArgs e)
    {

        if (btnEndRoute.Text == "Complete Route")
        {
            var popup = new EndRoutePopup(true, _locationService, _proofOfWorkService);
            this.ShowPopup(popup);
        }
        else
        {
            var popup = new EndRoutePopup(false, _locationService, _proofOfWorkService);
            this.ShowPopup(popup);
        }

    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        WeakReferenceMessenger.Default.Unregister<EndRouteMessage>(this);
        WeakReferenceMessenger.Default.Unregister<StopTimerMessage>(this);
        WeakReferenceMessenger.Default.Unregister<TaskCompletedMessage>(this.BindingContext);
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Models.Task step = e.Parameter as Models.Task;
        TimeSpan currentTimer = TimeSpan.Parse(lblTime.Text);
        var popup = new TaskCompletedPopup();
        this.ShowPopup(popup);
    }
}



