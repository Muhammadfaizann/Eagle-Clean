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
    TimeSpan dateTime;
    double progressPerSec;
    TimeSpan timer_date_time;
    string LastCleaningPlan=string.Empty;
    public ActiveCleaningRoutePage()
	{
		InitializeComponent();
        WeakReferenceMessenger.Default.Register<EndRouteMessage>(this, OnMessageReceived);

        timer = Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += (s, e) =>
        {
            lblTime.Text = timer_date_time.ToString("t");
            timer_date_time = timer_date_time.Add(new TimeSpan(0, 0, 1));
            timerProgressBar.Progress = timerProgressBar.Progress + progressPerSec;
        };


    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        timer_date_time = new TimeSpan(0, 0, 0);
        timerProgressBar.Progress = 0;
        timer.Start();
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
        cleaningPlan.ItemsSource = obj.steps;
        LastCleaningPlan = obj.steps.Last().Description;
        lblPlannedTime.Text= obj.plannedTime;
        dateTime = TimeSpan.ParseExact(lblPlannedTime.Text, "t", null);
        var seconds = dateTime.TotalSeconds;
        progressPerSec = (1 / seconds) * 100;
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
        if (label.FormattedText.Spans[0].Text == LastCleaningPlan)
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
    

}

   
    
