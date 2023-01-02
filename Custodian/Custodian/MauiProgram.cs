#if ANDROID
using CommunityToolkit.Maui;
using Custodian.Platforms.Android.Renderers;
#endif
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
			#if ANDROID	
            .UseMauiCommunityToolkit()
			#endif
			.ConfigureMauiHandlers(handlers =>
			{
			#if ANDROID
				handlers.AddHandler(typeof(Shell), typeof(CustomShellRenderer));
			#endif
            })
            .UseMauiMaps()
            .ConfigureFonts(fonts =>
			{
			fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			fonts.AddFont("HelveticaNowDisplay.ttf", "Helvetica-fonts");
			
            });

		return builder.Build();
	}
}
