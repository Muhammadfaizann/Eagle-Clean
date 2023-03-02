using Custodian.Helpers;
using Custodian.ViewModels;

namespace Custodian.Pages;

public partial class MyWork : ContentPage
{
	public MyWork(MyWorkViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        ongoingAssigments.ItemsSource = Utils.partialRoutes;
        completedAssigments.ItemsSource = Utils.completedRoutes;
        
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
        lblCompleted.TextColor = Color.FromArgb("#000000");
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