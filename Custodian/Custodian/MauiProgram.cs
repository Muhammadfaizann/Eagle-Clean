
using Microsoft.Maui.Controls.Compatibility.Hosting;
using Microsoft.Maui.Hosting;
using Syncfusion.Maui.Core.Hosting;

namespace Custodian;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureSyncfusionCore()
            .UseMauiCompatibility()
            .ConfigureMauiHandlers(handlers =>
			{
			
			
			
            })
            .ConfigureFonts(fonts =>
			{
			fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			fonts.AddFont("HelveticaNowDisplay.ttf", "Helvetica-fonts");

            });

		return builder.Build();
	}
}
