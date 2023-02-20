using Bumptech.Glide.Util;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using Custodian.Messages;
using Custodian.Models;
using Custodian.Popups;

namespace Custodian.Pages;

public partial class ActiveCleaningRoutePage : ContentPage, IQueryAttributable
{
    IDispatcherTimer timer;
    public ActiveCleaningRoutePage()
	{
		InitializeComponent();
        WeakReferenceMessenger.Default.Register<EndRouteMessage>(this, OnMessageReceived);
    }

    private void OnMessageReceived(object recipient, EndRouteMessage message)
    {

        try
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
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
        cleaningPlan.ItemsSource = obj.tasks;
        lblPlannedTime.Text= obj.plannedTime;
        StartTimerUpword();
    }
    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        
        var args = e as TappedEventArgs;
        var label = args.Parameter as Label;
        var image = sender as Image;
        image.Source = "green_tick.png";
        label.TextDecorations = TextDecorations.Strikethrough;
        label.Opacity = 0.5;
        btnEndRoute.IsVisible = true;
        if (label.Text == "Clean Furniture - 20 Minutes")
            btnEndRoute.Text = "Complete Route";
    }

    private void AddPictures_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddPicturesPage());
    }

    private void btnEndRoute_Clicked(object sender, EventArgs e)
    {

        if (btnEndRoute.Text == "Complete Route")
        {
            var popup = new EndRoutePopup(true);
            this.ShowPopup(popup);
        }
        else
        {
            var popup = new EndRoutePopup(false);
            this.ShowPopup(popup);
        }

    }
    private void StartTimerUpword()
    {
        try
        {
            TimeSpan dateTime = TimeSpan.ParseExact(lblPlannedTime.Text, "t", null);
            var seconds = dateTime.TotalSeconds;
            var progressPerSec = (1 / seconds) * 100;
            timer = Dispatcher.CreateTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            lblTime.IsVisible = true;
            TimeSpan timer_date_time = new TimeSpan(0,0,0);
            timer.Tick += (s, e) =>
            {
                lblTime.Dispatcher.Dispatch(() =>
                {

                    lblTime.Text = timer_date_time.ToString("t");
                    timer_date_time = timer_date_time.Add(new TimeSpan(0,0,1));
                    timerProgressBar.Progress = timerProgressBar.Progress + progressPerSec;
                });

            };
            timer.Start();
        }
        catch(Exception ex)
        {

        }

    }
}