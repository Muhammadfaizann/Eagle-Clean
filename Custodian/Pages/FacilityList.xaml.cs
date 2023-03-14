using Android.Graphics.Drawables;
using Custodian.ActivityLog;
using Custodian.Helpers;
using Custodian.Helpers.LocationService;
using Custodian.Services.Facility;
using Custodian.Services.Server;

namespace Custodian.Pages;

public partial class FacilityList : ContentPage
{
    IFacilityService _facilityService;
    ILocationService _locationService;
    static List<Models.Facility> facilities = new List<Models.Facility>();

    public FacilityList(IFacilityService facilityService, ILocationService locationService)
	{
        try
        {
            InitializeComponent();
            _facilityService = facilityService;
            _locationService = locationService;

            if (Utils.IsFromHomePage)
                navBar.Icon = "Navigation";
            else
                navBar.Icon = "Menu";
        }
        catch(Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    }
    protected override void OnAppearing()
    {
        try
        {
            base.OnAppearing();
            LoadUpAllFacilities();
        }
        catch (Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    }

    private async void LoadUpAllFacilities()
    {
        try
        {
            loader.IsRunning = loader.IsVisible = true;
            
            if (!facilities.Any())
            {
                Location location = await _locationService.GetCurrentLocation();
                if (location == null)
                {
                    location = await _locationService.GetLastKnownLocation();
                }
                facilities = await _facilityService.GetAllFacilities(location.Latitude, location.Longitude, Utils.config.Radius);
                
            }
            collection.ItemsSource = facilities;
            loader.IsRunning = loader.IsVisible = false;
        }
        catch (Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            loader.IsRunning = loader.IsVisible = true;
            var navigationParameter = new Dictionary<string, object>
            {
                { "param", e.Parameter }
            };
            await Shell.Current.GoToAsync(nameof(Facility), navigationParameter);
            loader.IsRunning = loader.IsVisible = false;
        }
        catch(Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    }

    private void SearchBar_Focused(object sender, FocusEventArgs e)
    {
        try
        {

        }
        catch(Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    }

    private void SearchBar_Unfocused(object sender, FocusEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    }
}