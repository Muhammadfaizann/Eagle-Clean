namespace Custodian.Pages;

public partial class DailySchedulePage : ContentPage
{
	public DailySchedulePage()
	{
		InitializeComponent();
        ongoingAssigments.ItemsSource = new object[] { "", "", "", "", "" };
        completedAssigments.ItemsSource = new object[] { "", "", "", "", "" };
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

    private void btnStart_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ActiveCleaningRoutePage());
    }
}