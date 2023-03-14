using Custodian.Helpers;
using Custodian.Models;
using Custodian.ViewModels;

namespace Custodian.Pages;

public partial class DailySchedulePage : ContentPage
{
	public DailySchedulePage(DailyScheduleViewModel vm)
	{
		InitializeComponent();
        BindingContext= vm;
        completedAssigments.ItemsSource = Utils.completedRoutes;
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
    void btnOngoing_Clicked(System.Object sender, System.EventArgs e)
    {
        loader.IsRunning = loader.IsVisible = true;
        grid_QR.IsVisible = true;
        completedAssigments.IsVisible = false;
        frmCompleted.BackgroundColor = Color.FromArgb("#00FFFFFF");
        frmOngoing.BackgroundColor = Color.FromArgb("#FFFFFF");
        lblCompleted.TextColor = Color.FromArgb("#000000");
        lblOngoing.TextColor = Color.FromArgb("#005F9D");
        loader.IsRunning = loader.IsVisible = false;
    }
    void btnCompleted_Clicked(System.Object sender, System.EventArgs e)
    {
        loader.IsRunning = loader.IsVisible = true;
        grid_QR.IsVisible = false;
        completedAssigments.IsVisible = true;
        frmOngoing.BackgroundColor = Color.FromArgb("#00FFFFFF");
        frmCompleted.BackgroundColor = Color.FromArgb("#FFFFFF");
        lblCompleted.TextColor = Color.FromArgb("#005F9D");
        lblOngoing.TextColor = Color.FromArgb("#000000");
        loader.IsRunning = loader.IsVisible = false;
    }
}