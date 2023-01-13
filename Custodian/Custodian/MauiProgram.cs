using CommunityToolkit.Maui;
using Custodian.Pages;
using Custodian.Platforms.Android.Renderers;
using Custodian.ViewModels;
using Microsoft.Maui.Controls.Compatibility.Hosting;
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
            .UseMauiCommunityToolkit()
			.ConfigureMauiHandlers(handlers =>
			{
				handlers.AddHandler(typeof(Shell), typeof(CustomShellRenderer));
            })
            .UseMauiMaps()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("Helvetica-fonts.ttf", "Helvetica-fonts");
            });
		builder.Services.AddSingleton<DailyScheduleViewModel>();
        builder.Services.AddSingleton<WorkOrderListViewModel>();

        builder.Services.AddSingleton<DailySchedulePage>();
        builder.Services.AddSingleton<WorkOrderListPage>();


        return builder.Build();
	}
}
