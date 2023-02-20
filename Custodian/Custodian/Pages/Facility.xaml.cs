using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Messaging;
using Custodian.Helpers;
using Custodian.Messages;
using Custodian.Models;
using Custodian.ViewModels;
using Microsoft.Extensions.Configuration;

namespace Custodian.Pages;

public partial class Facility : ContentPage, IQueryAttributable
{
	public Facility(FacilityViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
        ongoingAssigments.ItemsSource = Utils.ongoingRoutes;
        completedAssigments.ItemsSource =Utils.completedRoutes;
                ongoingWorkOrders.ItemsSource = new Workorder[]
                {
                    new Workorder { Title = "WO# 1", Subject = "Mowing - 26 times / year" , IsStarted = false },
                    new Workorder { Title = "WO# 2", Subject = "Mowing - 26 times / year" , IsStarted = false },
                    new Workorder { Title = "WO# 3", Subject = "Mowing - 26 times / year" , IsStarted = true },
                    new Workorder { Title = "WO# 4" , Subject = "Mowing - 26 times / year", IsStarted = false },
                };
                completedWorkorders.ItemsSource = new CompletedRoute[]
                {
                    new CompletedRoute { Title = "WO# 004", IsOverTime = true },
                    new CompletedRoute { Title = "WO# 005", IsOverTime = false },
                    new CompletedRoute { Title = "WO# 006", IsOverTime = false },
                    new CompletedRoute { Title = "WO# 007" , IsOverTime = true },
                };
    }
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Models.Schedual obj = query["param"] as Models.Schedual;
        navBar.Title = "Facility - " +obj.Title;
        lblFacility.Text = obj.Title;
    }
    
    void btnOngoing_Clicked(System.Object sender, System.EventArgs e)
    {
        loader.IsRunning = loader.IsVisible = true;
        ongoingAssigments.IsVisible = true;
        completedAssigments.IsVisible = false;
        ongoingWorkOrders.IsVisible = true;
        completedWorkorders.IsVisible = false;
        frmCompleted.BackgroundColor = Color.FromArgb("#00FFFFFF");
        frmOngoing.BackgroundColor = Color.FromArgb("#FFFFFF");
        lblCompleted.TextColor= Color.FromArgb("#000000");
        lblOngoing.TextColor = Color.FromArgb("#005F9D");
        loader.IsRunning = loader.IsVisible = false;
    }
    void btnCompleted_Clicked(System.Object sender, System.EventArgs e)
    {
        loader.IsRunning = loader.IsVisible = true;
        ongoingAssigments.IsVisible = false;
        completedAssigments.IsVisible = true;
        ongoingWorkOrders.IsVisible = false;
        completedWorkorders.IsVisible = true;
        frmOngoing.BackgroundColor = Color.FromArgb("#00FFFFFF");
        frmCompleted.BackgroundColor = Color.FromArgb("#FFFFFF");
        lblCompleted.TextColor = Color.FromArgb("#005F9D");
        lblOngoing.TextColor = Color.FromArgb("#000000");
        loader.IsRunning = loader.IsVisible = false;
    }
   
}