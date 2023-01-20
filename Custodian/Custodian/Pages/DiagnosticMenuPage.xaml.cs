namespace Custodian.Pages;

public partial class DiagnosticMenuPage : ContentPage
{
	public DiagnosticMenuPage()
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