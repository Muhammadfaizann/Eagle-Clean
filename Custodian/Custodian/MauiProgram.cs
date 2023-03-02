using CommunityToolkit.Maui;
using Custodian.Helpers;
using Custodian.Helpers.LocationService;
using Custodian.Pages;
using Custodian.Services.Facility;
using Custodian.Services.ProofOfWork;
using Custodian.Services.Server;
using Custodian.ViewModels;
using MetroLog.MicrosoftExtensions;
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

        builder.Logging.AddStreamingFileLogger(
                options =>
                {
                    options.RetainDays = 2;
                    options.FolderPath = Path.Combine(
                        "/storage/emulated/0/",
                        "MetroLogs");
                });

        builder.Services.AddSingleton<IApiClientService, ApiClientService>();
        builder.Services.AddSingleton<ILocationService, LocationService>();


        builder.Services.AddSingleton<IProofOfWorkService, ProofOfWorkSerive>();
        builder.Services.AddSingleton<IFacilityService, FacilityService>();


        builder.Services.AddSingleton<DailyScheduleViewModel>();
        builder.Services.AddSingleton<WorkOrderListViewModel>();
        builder.Services.AddSingleton<FacilityViewModel>();
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<MyWorkViewModel>();
        builder.Services.AddTransient<ProofOfWorkViewModel>();

        builder.Services.AddSingleton<DailySchedulePage>();
        builder.Services.AddTransient<ProofOfWork>();
        builder.Services.AddSingleton<Facility>();
        builder.Services.AddSingleton<FacilityList>();
        builder.Services.AddSingleton<ScanJob>();
        builder.Services.AddSingleton<MyWork>();
        builder.Services.AddTransient<Login>();

        
        return builder.Build();
	}
}
