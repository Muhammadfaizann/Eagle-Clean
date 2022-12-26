using CommunityToolkit.Maui.Views;
using Custodian.Popups;

namespace Custodian.Pages;

public partial class ActiveCleaningRoutePage : ContentPage
{
	public ActiveCleaningRoutePage()
	{
		InitializeComponent();
        cleaningPlan.ItemsSource = new object[] { "", "" , "" , "" };
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