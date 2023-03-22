using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Extensions;
using Custodian.ActivityLog;
using Custodian.Helpers;
using Custodian.Helpers.LocationService;
using Custodian.Services.Facility;

namespace Custodian.Pages;

public partial class FacilityList : ContentPage
{
    IFacilityService _facilityService;
    ILocationService _locationService;
    static List<Models.Facility> facilities;

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
            Logger.Log("2", "Info", "FacilityList Page Loaded!");
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

            facilities = new List<Models.Facility>();
            Location userLocation = await _locationService.GetCurrentLocation();

            if (userLocation != null)
            {
                facilities = await _facilityService.GetAllFacilities(userLocation.Latitude, userLocation.Longitude, Utils.config.Radius);
                foreach (var facility in facilities)
                {

                    facility.DistanceInMiles = distance(userLocation.Latitude, userLocation.Longitude, facility.Latitude, facility.Longitude, 'M').ToString();
                }
            }
            else
            {
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                string text = "Location record is not available";
                ToastDuration duration = ToastDuration.Short;
                double fontSize = 12;
                var toast = Toast.Make(text, duration, fontSize);
                await toast.Show(cancellationTokenSource.Token);
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

    

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            if(e.NewTextValue.Length>=3)
            collection.ItemsSource = facilities.Where(x => x.LocaleName.ToLower().Contains(e.NewTextValue.ToLower()) ||
                x.FacilityId.ToLower().Contains(e.NewTextValue.ToLower()) || x.FacilityType.ToLower().Contains(e.NewTextValue.ToLower())).ToObservableCollection();
            else
                collection.ItemsSource = facilities;
        }
        catch (Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    }

   
    private double distance(double lat1, double lon1, double lat2, double lon2, char unit)
    {
        if ((lat1 == lat2) && (lon1 == lon2))
        {
            return 0;
        }
        else
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            if (unit == 'K')
            {
                dist = dist * 1.609344;
            }
            else if (unit == 'N')
            {
                dist = dist * 0.8684;
            }
            return Math.Round(dist, 2);
        }
    }
    //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    //::  This function converts decimal degrees to radians             :::
    //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    private double deg2rad(double deg)
    {
        return (deg * Math.PI / 180.0);
    }

    //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    //::  This function converts radians to decimal degrees             :::
    //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    private double rad2deg(double rad)
    {
        return (rad / Math.PI * 180.0);
    }

    private async void Map_Tapped(object sender, TappedEventArgs e)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        string text = "Map navigation coming soon!";
        ToastDuration duration = ToastDuration.Short;
        double fontSize = 12;
        var toast = Toast.Make(text, duration, fontSize);
        await toast.Show(cancellationTokenSource.Token);
    }
}