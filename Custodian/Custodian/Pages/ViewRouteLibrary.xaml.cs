namespace Custodian.Pages;

public partial class ViewRouteLibrary : ContentPage
{
	public ViewRouteLibrary()
	{
		InitializeComponent();
        collection.ItemsSource = new string[] { "ALMA MI Post Office", "ITHICA MI Post Office", "SHEPHERD MI Post Office", "MT PLEASANT MI Post Office", "ALMA MI Post Office" };

    }
   
}