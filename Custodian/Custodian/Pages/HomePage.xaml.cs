
using Custodian.Helpers;

namespace Custodian.Pages;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (Utils.completedRoutes.Count==1 || Utils.completedRoutes.Count == 0)
        lblCompletedRoutes.Text = Utils.completedRoutes.Count.ToString()+ " Route";
        else
        lblCompletedRoutes.Text = Utils.completedRoutes.Count.ToString() + " Routes";
        
        if(Utils.partialRoutes.Count==1 || Utils.partialRoutes.Count == 0)
            lblPartialRoutes.Text = Utils.partialRoutes.Count.ToString()+ " Route";
        else
            lblPartialRoutes.Text = Utils.partialRoutes.Count.ToString() + " Routes";
    }


    private async void btnJobScanningClicked(object sender, TappedEventArgs e)
    {
        loader.IsRunning = loader.IsVisible = true;
        await Shell.Current.GoToAsync(nameof(ScanJob));
        loader.IsRunning = loader.IsVisible = false;
    } 
    private async void btnFacilityClicked(object sender, TappedEventArgs e)
    {
        loader.IsRunning = loader.IsVisible = true;
        //await Navigation.PushAsync(new FacilityList());
         await Shell.Current.GoToAsync(nameof(FacilityList));
        loader.IsRunning = loader.IsVisible = false;
    } 
    private void btnTimeClockClicked(object sender, TappedEventArgs e)
    {

    } 
    private async void btnMyWorkClicked(object sender, TappedEventArgs e)
    {
        loader.IsRunning = loader.IsVisible = true;
        await Shell.Current.GoToAsync(nameof(MyWork));
        loader.IsRunning = loader.IsVisible = false;
    }
    private async void btnLogoutClicked(object sender, TappedEventArgs e)
    {
        loader.IsRunning = loader.IsVisible = true;
        await Shell.Current.GoToAsync(nameof(Login));
        loader.IsRunning = loader.IsVisible = false;
    }
}