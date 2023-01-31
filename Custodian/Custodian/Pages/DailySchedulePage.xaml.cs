using Custodian.Models;
using Custodian.ViewModels;

namespace Custodian.Pages;

public partial class DailySchedulePage : ContentPage
{
	public DailySchedulePage(DailyScheduleViewModel vm)
	{
		InitializeComponent();
        BindingContext= vm;
        collection.ItemsSource = new Models.Task[]
        {
            new Models.Task { Title = "Anytown PO", Subject = "6 Routes & 1 Work Order"},
            new Models.Task { Title = "ALMA MI Post Office", Subject = "3 Routes"},
            new Models.Task { Title = "ITHICA MI Post Office", Subject = "2 Routes & 2 Work Order"},
            new Models.Task { Title = "ITHICA MI Post Office", Subject = "1 Work Order"},

        };
       
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        loader.IsRunning = loader.IsVisible = true;
        var navigationParameter = new Dictionary<string, object>
            {
                { "param", e.Parameter }
            };
        await Shell.Current.GoToAsync(nameof(Facility), navigationParameter);
        loader.IsRunning = loader.IsVisible = false;
    }
}