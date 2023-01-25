using CommunityToolkit.Maui.Views;
using Custodian.Models;
using Custodian.Popups;

namespace Custodian.Pages;

public partial class ActiveCleaningRoutePage : ContentPage, IQueryAttributable
{
	public ActiveCleaningRoutePage()
	{
		InitializeComponent();
        cleaningPlan.ItemsSource = new object[] { "Mop Floor 2 - 20 Minutes", "Restock - 25 Minutes", "Clean Furniture - 20 Minutes" };
    }
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var obj = query["param"] as Assignment;
        routeTitle.Text =obj.Title;
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
        if (label.Text == "Clean Furniture - 20 Minutes")
            btnEndRoute.Text = "Complete Route";
    }

    private void AddPictures_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddPicturesPage());
    }

    private void btnEndRoute_Clicked(object sender, EventArgs e)
    {

        if (btnEndRoute.Text == "Complete Route")
        {
            var popup = new EndRoutePopup(true);
            this.ShowPopup(popup);
        }
        else
        {
            var popup = new EndRoutePopup(false);
            this.ShowPopup(popup);
        }
    }
    private async void NavigateBack(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

}