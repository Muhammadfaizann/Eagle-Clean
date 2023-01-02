using CommunityToolkit.Maui.Views;
using Custodian.Popups;

namespace Custodian.Pages;

public partial class ActiveCleaningRoutePage : ContentPage
{
	public ActiveCleaningRoutePage(string route)
	{
		InitializeComponent();
        routeTitle.Text = route;
        cleaningPlan.ItemsSource = new object[] { "Mop Floor 2 - 20 Minutes", "Restock - 25 Minutes", "Clean Furniture - 20 Minutes" };
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        var args = e as TappedEventArgs;
        var label = args.Parameter as Label;
        var image = sender as Image;
        image.Source = "green_tick.png";
        label.TextDecorations = TextDecorations.Strikethrough;
        label.Opacity = 0.5;
        btnEndRoute.IsVisible = true;
    }

    private void AddPictures_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddPicturesPage());
    }

    private void btnEndRoute_Clicked(object sender, EventArgs e)
    {
        var popup = new EndRoutePopup();
        this.ShowPopup(popup);
    }
}