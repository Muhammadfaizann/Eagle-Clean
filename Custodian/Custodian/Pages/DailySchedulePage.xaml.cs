namespace Custodian.Pages;

public partial class DailySchedulePage : ContentPage
{
	public DailySchedulePage()
	{
		InitializeComponent();
        ongoingAssigments.ItemsSource = new object[] { "Anytown PO - Route 006", "Anytown PO - Route 007", "Anytown PO - Route 008", "Anytown PO - Route 009" };
        completedAssigments.ItemsSource = new object[] { "Anytown PO - Route 001", "Anytown PO - Route 002", "Anytown PO - Route 003", "Anytown PO - Route 004", "Anytown PO - Route 005" };
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