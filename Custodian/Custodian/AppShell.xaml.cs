using Custodian.Pages;

namespace Custodian;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(ActiveCleaningRoutePage), typeof(ActiveCleaningRoutePage));
        Routing.RegisterRoute(nameof(WorkOrderPage), typeof(WorkOrderPage)); 
        Routing.RegisterRoute(nameof(DailySchedulePage), typeof(DailySchedulePage));

        Routing.RegisterRoute(nameof(Facility), typeof(Facility));

        Routing.RegisterRoute(nameof(CTCTrainingVideo), typeof(CTCTrainingVideo));
        Routing.RegisterRoute(nameof(CTCMonthlyTraining), typeof(CTCMonthlyTraining));
        Routing.RegisterRoute(nameof(CTCJobAidsPage), typeof(CTCJobAidsPage));
    }
}
