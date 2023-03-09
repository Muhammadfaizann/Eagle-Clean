using Custodian.ActivityLog;
using Custodian.Helpers;
using Custodian.Helpers.LocationService;
using Custodian.Models;
using Custodian.Models.ServerModels;
using Custodian.Services.Server;
using Custodian.ViewModels;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Custodian.Pages;

public partial class Facility : ContentPage, IQueryAttributable
{
    IApiClientService _client;
    ILocationService _locationService;
    Models.Facility facility;
    public Facility(FacilityViewModel vm, IApiClientService client, ILocationService locationService)
    {
        try 
        {
            InitializeComponent();
            BindingContext = vm;
            _client = client;
            _locationService = locationService;
            routesCollection.ItemsSource = new List<string>() { "Route 001", "Route 002", "Route 003" };
        }
        catch (Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    }
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        try
        {
            facility = query["param"] as Models.Facility;

            navBar.Title = "Facility - " + facility.LocaleName;
            lblFacility.Text = facility.LocaleName;
            lblAddress.Text = facility.Address + " " + facility.City + " " + facility.DistrictName;
        }
        catch (Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadAllRoutes();
    }

    private async void LoadAllRoutes()
    {
        try
        {
            loader.IsRunning = loader.IsVisible = true;
            List<RouteModel> response = await _client.GetAsync<List<RouteModel>>("routes/" + facility.FacilityId);
            if (response != null)
            {
                routesCollection.ItemsSource = response;
            }
            loader.IsRunning = loader.IsVisible = false;
        }
        catch(Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            Button btn = sender as Button;
            RouteModel routeDetails = btn.CommandParameter as RouteModel;

            Location currentLocation = await _locationService.GetCurrentLocation();
            var route = await Utils.StartRoute(routeDetails.json, currentLocation.Latitude, currentLocation.Longitude, false);
            var navigationParameter = new Dictionary<string, object>
            {
               { "param", route }
            };
            await Shell.Current.GoToAsync(nameof(ProofOfWork), navigationParameter);
        }
        catch(Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    }

    
}