using Microsoft.Maui;

namespace Custodian.Screens;

public partial class SplashScreen : ContentPage
{
	public SplashScreen()
	{
		InitializeComponent();
        _ = RunAnimationAsync();

    }
    private async Task RunAnimationAsync()
    {
        for (int i = 1; i <= 90; i++)
        {
            placeholder.Source = "frame" + i + ".png";
            await Task.Delay(50);
        }
    }
}