using Custodian.Models;
using Custodian.ViewModels;

namespace Custodian.Pages;

public partial class Facility : ContentPage, IQueryAttributable
{
	public Facility(FacilityViewModel vm)
	{
		InitializeComponent();
        this.BindingContext = vm;
             ongoingAssigments.ItemsSource = new Assignment[]
                {
                    new Assignment { Title = "Route 006", IsStarted = false },
                    new Assignment { Title = "Route 007", IsStarted = false },
                    new Assignment { Title = "Route 008", IsStarted = true },
                    new Assignment { Title = "Route 009" , IsStarted = false },
                };
            completedAssigments.ItemsSource = new CompletedAssignment[]
                {
                    new CompletedAssignment { Title = "Route 001", IsOverTime = true },
                    new CompletedAssignment { Title = "Route 002", IsOverTime = false },
                    new CompletedAssignment { Title = "Route 003", IsOverTime = false },
                    new CompletedAssignment { Title = "Route 004" , IsOverTime = true },
                };
            ongoingWorkOrders.ItemsSource = new Assignment[]
                {
                    new Assignment { Title = "WO# 1", IsStarted = false },
                    new Assignment { Title = "WO# 2", IsStarted = false },
                    new Assignment { Title = "WO# 3", IsStarted = true },
                    new Assignment { Title = "WO# 4" , IsStarted = false },
                };
            completedWorkorders.ItemsSource = new CompletedAssignment[]
               {
                    new CompletedAssignment { Title = "WO# 004", IsOverTime = true },
                    new CompletedAssignment { Title = "WO# 005", IsOverTime = false },
                    new CompletedAssignment { Title = "WO# 006", IsOverTime = false },
                    new CompletedAssignment { Title = "WO# 007" , IsOverTime = true },
               };
    }
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Models.Task obj = query["param"] as Models.Task;
        lblTitle.Text = "Facility - " +obj.Title;
        lblFacility.Text = obj.Title;
    }
    
    void btnOngoing_Clicked(System.Object sender, System.EventArgs e)
    {
        ongoingAssigments.IsVisible = true;
        completedAssigments.IsVisible = false;
        ongoingWorkOrders.IsVisible = true;
        completedWorkorders.IsVisible = false;
        frmCompleted.BackgroundColor = Color.FromArgb("#00FFFFFF");
        frmOngoing.BackgroundColor = Color.FromArgb("#FFFFFF");
        lblCompleted.TextColor= Color.FromArgb("#000000");
        lblOngoing.TextColor = Color.FromArgb("#005F9D");
    }
    void btnCompleted_Clicked(System.Object sender, System.EventArgs e)
    {
        ongoingAssigments.IsVisible = false;
        completedAssigments.IsVisible = true;
        ongoingWorkOrders.IsVisible = false;
        completedWorkorders.IsVisible = true;
        frmOngoing.BackgroundColor = Color.FromArgb("#00FFFFFF");
        frmCompleted.BackgroundColor = Color.FromArgb("#FFFFFF");
        lblCompleted.TextColor = Color.FromArgb("#005F9D");
        lblOngoing.TextColor = Color.FromArgb("#000000");
    }
    private async void NavigateBack(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}