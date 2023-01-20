namespace Custodian.Pages;

public partial class ViewSDSSheetPage : ContentPage
{
	public ViewSDSSheetPage()
	{
		InitializeComponent();
	}
    private void OpenFlyoutMenu(object sender, TappedEventArgs e)
    {
        Shell.Current.FlyoutBehavior = FlyoutBehavior.Locked;
        Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
        Shell.Current.FlyoutIsPresented = true;
    }
}