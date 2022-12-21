namespace Custodian.Pages;

public partial class ActiveCleaningRoutePage : ContentPage
{
	public ActiveCleaningRoutePage()
	{
		InitializeComponent();
        cleaningPlan.ItemsSource = new object[] { "", "" , "" , "" };
    }
}