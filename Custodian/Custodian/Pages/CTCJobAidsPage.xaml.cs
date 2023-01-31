namespace Custodian.Pages;

public partial class CTCJobAidsPage : ContentPage
{
	public CTCJobAidsPage()
	{
		InitializeComponent();
		collection.ItemsSource = new string[] { "Light Duty Specialist Job Aid", "Vacuum Specialist Job Aid", "Utility Specialist Job Aid", "Rest Room Specialist Job Aid", "Harness Fitment Specialist Job Aid", "Vacuum Specialist Job Aid" };
	}
}