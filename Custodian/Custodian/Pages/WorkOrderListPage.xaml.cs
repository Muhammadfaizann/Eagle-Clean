namespace Custodian.Pages;

public partial class WorkOrderListPage : ContentPage
{
	public WorkOrderListPage()
	{
		InitializeComponent();
        workOrderList.ItemsSource = new object[] { "", "", "", "" };

    }

    private void btnStart_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new WorkOrderPage());
    }
}