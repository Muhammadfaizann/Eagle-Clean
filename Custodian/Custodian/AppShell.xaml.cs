using Custodian.Pages;

namespace Custodian;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(ProofOfWork), typeof(ProofOfWork));
        Routing.RegisterRoute(nameof(WorkOrderPage), typeof(WorkOrderPage)); 
        Routing.RegisterRoute(nameof(DailySchedulePage), typeof(DailySchedulePage));

        Routing.RegisterRoute(nameof(Facility), typeof(Facility));
        Routing.RegisterRoute(nameof(MyWork), typeof(MyWork));
        Routing.RegisterRoute("Login", typeof(Login));

        Routing.RegisterRoute(nameof(FacilityList), typeof(FacilityList));

        Routing.RegisterRoute(nameof(ScanJob), typeof(ScanJob));
        Routing.RegisterRoute(nameof(CTCTrainingVideo), typeof(CTCTrainingVideo));
        Routing.RegisterRoute(nameof(CTCMonthlyTraining), typeof(CTCMonthlyTraining));
        Routing.RegisterRoute(nameof(CTCJobAidsPage), typeof(CTCJobAidsPage));
    }
}
