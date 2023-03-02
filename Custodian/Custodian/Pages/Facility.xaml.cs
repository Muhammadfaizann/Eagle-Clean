using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Messaging;
using Custodian.Helpers;
using Custodian.Messages;
using Custodian.Models;
using Custodian.Models.ServerModels;
using Custodian.Services.Server;
using Custodian.ViewModels;
using Microsoft.Extensions.Configuration;

namespace Custodian.Pages;

public partial class Facility : ContentPage, IQueryAttributable
{
    IApiClientService _client;
    Models.Facility facility;
	public Facility(FacilityViewModel vm, IApiClientService client)
	{
		InitializeComponent();
        BindingContext = vm;
        _client = client;
        routesCollection.ItemsSource= new List<string>() {"Route 001", "Route 002", "Route 003" };
    }
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        facility = query["param"] as Models.Facility;

        navBar.Title = "Facility - "+ facility.LocaleName;
        lblFacility.Text = facility.LocaleName;
        lblAddress.Text = facility.Address +" "+ facility.City +" "+ facility.DistrictName;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadAllRoutes();
    }

    private async void LoadAllRoutes()
    {
        loader.IsRunning = loader.IsVisible = true;
        List<RouteModel> response = await _client.GetAsync<List<RouteModel>>("route/"+ facility.FacilityId + "/routes");
        if (response != null)
        {
            routesCollection.ItemsSource = response;
        }
        loader.IsRunning = loader.IsVisible = false;
    }
}