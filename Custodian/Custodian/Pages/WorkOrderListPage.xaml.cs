using Custodian.Models;
using Custodian.ViewModels;

namespace Custodian.Pages;

public partial class WorkOrderListPage : ContentPage
{
	public WorkOrderListPage(WorkOrderListViewModel vm)
	{
		InitializeComponent();
        BindingContext= vm;
        workOrderList.ItemsSource = new Workorder[]
        {
            new Workorder { Title = "WO# 1", Subject = "Mowing - 26 times / year" },
            new Workorder { Title = "WO# 2", Subject = "Snow Removal" },
            new Workorder { Title = "WO# 3", Subject = "Replace light Bulb" },
            new Workorder { Title = "WO# 4" , Subject = "High Dusting" },
        };
    }

    
}