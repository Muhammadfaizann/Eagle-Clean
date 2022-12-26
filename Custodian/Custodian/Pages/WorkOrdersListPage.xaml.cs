namespace Custodian.Pages;

public partial class WorkOrdersListPage : ContentPage
{
	public WorkOrdersListPage()
	{
		InitializeComponent();
        workOrderList.ItemsSource = new object[] { "", "", "", "" };

    }

    private void btnStart_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new WorkOrderPage());
    }
}