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
        btnCompleted.Style = (Style)Application.Current.Resources["SegmentedButtonUnclicked"];
        btnOngoing.Style = (Style)Application.Current.Resources["SegmentedButtonClicked"];
    }
    void btnCompleted_Clicked(System.Object sender, System.EventArgs e)
    {
        ongoingAssigments.IsVisible = false;
        completedAssigments.IsVisible = true;
        btnOngoing.Style = (Style)Application.Current.Resources["SegmentedButtonUnclicked"];
        btnCompleted.Style = (Style)Application.Current.Resources["SegmentedButtonClicked"];
    }

    private void btnStart_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ActiveCleaningRoutePage());
    }
}