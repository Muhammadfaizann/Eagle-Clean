
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using Custodian.Helpers;
using Custodian.Helpers.LocationService;
using Custodian.Messages;
using Custodian.Models;
using Custodian.Models.ServerModels;
using Custodian.Popups;
using Custodian.Services.ProofOfWork;
using Custodian.ViewModels;
using System.Text.Json;

namespace Custodian.Pages;

public partial class ProofOfWork : ContentPage, IQueryAttributable
{
    IDispatcherTimer timer;
    TimeSpan dateTime;
    private TimeSpan prevTime;
    double progressPerSec;
    TimeSpan timer_date_time;
    ILocationService _locationService;
    IProofOfWorkService _proofOfWorkService;
    bool IsBackgroundThreadRunning = true;
    public ProofOfWork(ProofOfWorkViewModel viewModel, ILocationService locationService, IProofOfWorkService proofOfWorkService)
	{
		InitializeComponent();
        _locationService = locationService;
        _proofOfWorkService = proofOfWorkService;
        this.BindingContext=viewModel;
        WeakReferenceMessenger.Default.Register<EndRouteMessage>(this, OnEndRouteMessageReceived);
        WeakReferenceMessenger.Default.Register<StopTimerMessage>(this, OnStopTimerMessageReceived);
        timer = Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += (s, e) =>
        {
            lblTime.Text = timer_date_time.ToString("t");
            timer_date_time = timer_date_time.Add(new TimeSpan(0, 0, 1));
            timerProgressBar.Progress = timerProgressBar.Progress + progressPerSec;
        };
        prevTime = TimeSpan.Zero;

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
        await Task.Run(async() =>
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
                Utils.activeRouteRecord.status = "In-Progress";

                string jsonRecord = JsonSerializer.Serialize<MergeRecord>(Utils.activeRouteRecord);
                await DatabaseService.write(jsonRecord);

                
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
        var obj = query["param"] as Route;
        routeTitle.Text = obj.rte;
        description.Text = obj.desc;
        var viewmodel = this.BindingContext as ProofOfWorkViewModel;
        viewmodel.CleaningPlanList = obj.steps.ToObservableCollection();
        lblPlannedTime.Text= obj.plannedTime;
        dateTime = TimeSpan.ParseExact(lblPlannedTime.Text, "t", null);
        var seconds = dateTime.TotalSeconds;
        progressPerSec = (1 / seconds) * 100;
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
        Step step = e.Parameter as Step;


        TimeSpan currentTimer = TimeSpan.Parse(lblTime.Text);
        
        
        var popup = new TaskCompletedPopup(step, currentTimer);
        this.ShowPopup(popup);
    }
}



