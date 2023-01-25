using CommunityToolkit.Maui;
using Custodian.Pages;

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
            .UseMauiMaps()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("Helvetica-fonts.ttf", "Helvetica-fonts");
                fonts.AddFont("FontAwesome.otf", "Awesome-fonts");
            });
		builder.Services.AddSingleton<DailyScheduleViewModel>();
        builder.Services.AddSingleton<WorkOrderListViewModel>();
        builder.Services.AddSingleton<FacilityViewModel>();

        builder.Services.AddSingleton<DailySchedulePage>();
        builder.Services.AddSingleton<WorkOrderListPage>();
        builder.Services.AddSingleton<Facility>();


        return builder.Build();
	}
}
