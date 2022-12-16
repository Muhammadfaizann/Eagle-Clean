using Microsoft.Maui;

namespace Custodian;

public partial class SplashScreen : ContentPage
{
	public SplashScreen()
	{
		InitializeComponent();
        _ = RunAnimationAsync();

    }
    private async Task RunAnimationAsync()
    {
        for (int i = 1; i <= 15; i++)
        {
            splashPlaceHolder.Source = "frame" + i + ".png";
            await Task.Delay(112);
        }
    }
}