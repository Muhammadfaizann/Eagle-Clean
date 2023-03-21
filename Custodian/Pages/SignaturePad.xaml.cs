using Android.Service.Voice;
using CommunityToolkit.Mvvm.Messaging;
using Custodian.ActivityLog;
using Custodian.Messages;
using Custodian.ViewModels;

namespace Custodian.Pages;

public partial class SignaturePad : ContentPage
{
    public SignaturePad()
    {
        InitializeComponent();
    }
    private void clear_Clicked(object sender, EventArgs e)
    {
        pad.BorderColor = Color.Parse("Red");
        PadView.Clear();
    }
    private void confirm_Clicked(object sender, EventArgs e)
    {       
        
        //Stream image = await PadView.GetImageStreamAsync(SignatureImageFormat.Jpeg);    
        loader.IsRunning = loader.IsVisible = true;
        Button btn = sender as Button;
        btn.IsEnabled = false;
        WeakReferenceMessenger.Default.Send(new LoginMessage("Login"));
        Logger.Log("2", "Info", "Logged in successful!");
        loader.IsRunning = loader.IsVisible = false;
    }

    private void PadView_DrawCompleted(object sender, EventArgs e)
    {
        pad.BorderColor = Color.Parse("Green");
    }
}