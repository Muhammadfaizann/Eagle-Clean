using System;

namespace Custodian.Controls;

public partial class CustomNavBar : Grid
{
    public static readonly BindableProperty TitleProperty =
             BindableProperty.Create(
                 nameof(Title),
                 typeof(string),
                 typeof(CustomNavBar), string.Empty, defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnTitleChanged);

    private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var custumView = bindable as CustomNavBar;
        custumView.title.Text = newValue.ToString();
    }

    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }
    public static readonly BindableProperty IconProperty =
             BindableProperty.Create(
                 nameof(Title),
                 typeof(string),
                 typeof(CustomNavBar), string.Empty, defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnIconChanged);

    private static void OnIconChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var custumView = bindable as CustomNavBar;
        if(newValue.ToString().Equals("Menu"))
        {
            custumView.menuTab.IsVisible = true;
           
        }
        else if(newValue.ToString().Equals("Navigation"))
        {
   
            custumView.navTab.IsVisible = true;
        }
        

    }

    public string Icon
    {
        get { return (string)GetValue(IconProperty); }
        set { SetValue(IconProperty, value); }
    }
    public CustomNavBar()
	{
		InitializeComponent();
    }
    private async void NavigateBack(object sender, TappedEventArgs e)
    {
            await Shell.Current.GoToAsync("..");
    }
    private void OpenFlyoutMenu(object sender, TappedEventArgs e)
    {
        Shell.Current.FlyoutBehavior = FlyoutBehavior.Locked;
        Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
        Shell.Current.FlyoutIsPresented = true;
    }
}