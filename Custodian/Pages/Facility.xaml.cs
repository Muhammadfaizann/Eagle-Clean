using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
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
            loader.IsRunning = loader.IsVisible = true;
            Button btn = sender as Button;
            btn.IsEnabled = false;
            RouteModel routeDetails = btn.CommandParameter as RouteModel;

            Location currentLocation = await _locationService.GetCurrentLocation();
            if (currentLocation != null)
            {
                await Utils.StartRoute(routeDetails.json, currentLocation.Latitude, currentLocation.Longitude, false);

                await Shell.Current.GoToAsync(nameof(ProofOfWork));
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
            loader.IsRunning = loader.IsVisible = false;
        }
        catch(Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    }

    
}