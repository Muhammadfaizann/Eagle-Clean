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
		InitializeComponent();
        _facilityService = facilityService;
        _locationService=locationService;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadUpAllFacilities();    
    }

    private async void LoadUpAllFacilities()
    {
        try
        {
            loader.IsRunning = loader.IsVisible = true;
            if (!facilities.Any())
            {
                Location location = await _locationService.GetCurrentLocation();
                facilities = await _facilityService.GetAllFacilities(location.Latitude, location.Longitude, Utils.config.Radius);
                collection.ItemsSource = facilities;
            }
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
            var navigationParameter = new Dictionary<string, object>
            {
                { "param", e.Parameter }
            };
            await Shell.Current.GoToAsync(nameof(Facility), navigationParameter);
        }
        catch(Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    }
}