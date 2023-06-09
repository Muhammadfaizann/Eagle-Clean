
using Custodian.ActivityLog;
using Custodian.Helpers;
using Custodian.ViewModels;
using Java.Lang.Reflect;
using Microsoft.Extensions.Logging;

namespace Custodian.Pages;

public partial class HomePage : ContentPage
{
	public HomePage(UploadThread uploadThread)
	{
        try
        {
            InitializeComponent();
        }
        catch (Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
	}

    protected override void OnAppearing()
    {
        try
        {
            base.OnAppearing();
            InitLocalData();
            Logger.Log("2", "INFO", "Home Page Loaded!"); 
        }
        catch (Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    }

    private void InitMyWorkValues()
    {
        try
        {
            if (Utils.completedRoutes.Count == 1 || Utils.completedRoutes.Count == 0)
                lblCompletedRoutes.Text = Utils.completedRoutes.Count.ToString() + " Route";
            else
                lblCompletedRoutes.Text = Utils.completedRoutes.Count.ToString() + " Routes";

            if (Utils.partialRoutes.Count == 1 || Utils.partialRoutes.Count == 0)
                lblPartialRoutes.Text = Utils.partialRoutes.Count.ToString() + " Route";
            else
                lblPartialRoutes.Text = Utils.partialRoutes.Count.ToString() + " Routes";
        }
        catch(Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    }
    private async void InitLocalData()
    {
        try 
        {
            loader.IsRunning = loader.IsVisible = true;
            await LoadUpLocalDataAysnc();
            InitMyWorkValues();
            loader.IsRunning = loader.IsVisible = false;
        }
        catch(Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    }
    private async Task LoadUpLocalDataAysnc()
    {
        try
        {
            await Utils.ImportConfigurations();
            await Utils.LoadRoutes();
        }
        catch(Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    }
    private async void btnJobScanningClicked(object sender, TappedEventArgs e)
    {
        try
        {
            loader.IsRunning = loader.IsVisible = true;
            await Shell.Current.GoToAsync(nameof(ScanJob));
            loader.IsRunning = loader.IsVisible = false;
        }
        catch(Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    } 
    private async void btnFacilityClicked(object sender, TappedEventArgs e)
    {
        try
        {
            loader.IsRunning = loader.IsVisible = true;
            //await Navigation.PushAsync(new FacilityList());
            Utils.IsFromHomePage = true;
            await Shell.Current.GoToAsync(nameof(FacilityList));
            Utils.IsFromHomePage = false;
            loader.IsRunning = loader.IsVisible = false;
        }
        catch(Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    } 
    private async void btnTimeClockClicked(object sender, TappedEventArgs e)
    {
        try
        {
            loader.IsRunning = loader.IsVisible = true;
            await Shell.Current.GoToAsync(nameof(TimeClock));
            loader.IsRunning = loader.IsVisible = false;
        }
        catch (Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    } 
    private async void btnMyWorkClicked(object sender, TappedEventArgs e)
    {
        try
        {
            loader.IsRunning = loader.IsVisible = true;
            await Shell.Current.GoToAsync(nameof(MyWork));
            loader.IsRunning = loader.IsVisible = false;
        }
        catch(Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    }
    private async void btnLogoutClicked(object sender, TappedEventArgs e)
    {
        try
        {
            loader.IsRunning = loader.IsVisible = true;
            Logger.Log("2", "INFO", "Logged out!!");
            await Navigation.PushAsync(new Login(new LoginViewModel()));
            //await Shell.Current.GoToAsync(nameof(Login));
            loader.IsRunning = loader.IsVisible = false;
        }
        catch(Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    }
}