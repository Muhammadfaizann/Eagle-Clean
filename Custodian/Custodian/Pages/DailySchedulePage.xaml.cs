using Custodian.Models;
using Custodian.ViewModels;

namespace Custodian.Pages;

public partial class DailySchedulePage : ContentPage
{
	public DailySchedulePage(DailyScheduleViewModel vm)
	{
		InitializeComponent();
        BindingContext= vm;
        ongoingAssigments.ItemsSource = new Assignment[]
        {
            new Assignment { Title = "Anytown PO - Route 006", IsStarted = false },
            new Assignment { Title = "Anytown PO - Route 007", IsStarted = false },
            new Assignment { Title = "Anytown PO - Route 008", IsStarted = true },
            new Assignment { Title = "Anytown PO - Route 009" , IsStarted = false },
        };
        completedAssigments.ItemsSource = new CompletedAssignment[]
        {
            new CompletedAssignment { Title = "Anytown PO - Route 001", IsOverTime = true },
            new CompletedAssignment { Title = "Anytown PO - Route 002", IsOverTime = false },
            new CompletedAssignment { Title = "Anytown PO - Route 003", IsOverTime = false },
            new CompletedAssignment { Title = "Anytown PO - Route 004" , IsOverTime = true },
        };
    }
    void btnOngoing_Clicked(System.Object sender, System.EventArgs e)
    {
        ongoingAssigments.IsVisible = true;
        completedAssigments.IsVisible = false;
        frmCompleted.BackgroundColor = Color.FromArgb("#00FFFFFF");
        frmOngoing.BackgroundColor = Color.FromArgb("#FFFFFF");
        lblCompleted.TextColor= Color.FromArgb("#000000");
        lblOngoing.TextColor = Color.FromArgb("#005F9D");
    }
    void btnCompleted_Clicked(System.Object sender, System.EventArgs e)
    {
        ongoingAssigments.IsVisible = false;
        completedAssigments.IsVisible = true;
        frmOngoing.BackgroundColor = Color.FromArgb("#00FFFFFF");
        frmCompleted.BackgroundColor = Color.FromArgb("#FFFFFF");
        lblCompleted.TextColor = Color.FromArgb("#005F9D");
        lblOngoing.TextColor = Color.FromArgb("#000000");
    }

   
}