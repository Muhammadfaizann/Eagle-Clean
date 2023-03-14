using CommunityToolkit.Maui.Core.Extensions;
using Custodian.Helpers;
using Custodian.Models;
using Custodian.ViewModels;
using System.Text.Json;

namespace Custodian.Pages;

public partial class MyWork : ContentPage
{
	public MyWork(MyWorkViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        
        completedAssigments.ItemsSource = Utils.completedRoutes;
        
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        List<MergeRecord> mergeRecords = new List<MergeRecord>();
        foreach(var route in Utils.partialRoutes)
        {

            MergeRecord record = JsonSerializer.Deserialize<MergeRecord>(route.json);
            record.filename = route.filename;
            mergeRecords.Add(record);
        }
        ongoingAssigments.ItemsSource = mergeRecords.ToObservableCollection();
    }

    void btnOngoing_Clicked(System.Object sender, System.EventArgs e)
    {
        loader.IsRunning = loader.IsVisible = true;
        ongoingAssigments.IsVisible = true;
        completedAssigments.IsVisible = false;
       
        frmCompleted.BackgroundColor = Color.FromArgb("#00FFFFFF");
        frmOngoing.BackgroundColor = Color.FromArgb("#FFFFFF");
        lblCompleted.TextColor = Color.FromArgb("#000000");
        lblOngoing.TextColor = Color.FromArgb("#005F9D");
        loader.IsRunning = loader.IsVisible = false;
    }
    void btnCompleted_Clicked(System.Object sender, System.EventArgs e)
    {
        loader.IsRunning = loader.IsVisible = true;
        ongoingAssigments.IsVisible = false;
        completedAssigments.IsVisible = true;
       
        frmOngoing.BackgroundColor = Color.FromArgb("#00FFFFFF");
        frmCompleted.BackgroundColor = Color.FromArgb("#FFFFFF");
        lblCompleted.TextColor = Color.FromArgb("#005F9D");
        lblOngoing.TextColor = Color.FromArgb("#000000");
        loader.IsRunning = loader.IsVisible = false;
    }

    private void ContinueBtn_Clicked(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        btn.IsEnabled = false;
    }
}