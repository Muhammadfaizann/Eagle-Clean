using CommunityToolkit.Maui;
using Custodian.Pages;
using Custodian.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using Syncfusion.Maui.Core.Hosting;
using System.Reflection;

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



        var a = Assembly.GetExecutingAssembly();
        using var stream = a.GetManifestResourceStream("Custodian.appsettings.json");
        var config = new ConfigurationBuilder()
             .AddJsonStream(stream)
             .Build();


        builder.Configuration.AddConfiguration(config);



        builder.Services.AddSingleton<DailyScheduleViewModel>();
        builder.Services.AddSingleton<WorkOrderListViewModel>();
        builder.Services.AddSingleton<FacilityViewModel>();
        builder.Services.AddSingleton<LoginViewModel>();

        builder.Services.AddSingleton<DailySchedulePage>();
        builder.Services.AddSingleton<ActiveCleaningRoutePage>();
        builder.Services.AddSingleton<Facility>();
        builder.Services.AddTransient<Login>();

        
        return builder.Build();
	}
}
