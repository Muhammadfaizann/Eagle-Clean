using Android.Service.Voice;
using Custodian.ActivityLog;
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
        PadView.Clear();
    }
    private async void confirm_Clicked(object sender, EventArgs e)
    {       
        //Stream image = await PadView.GetImageStreamAsync(SignatureImageFormat.Jpeg);    
        loader.IsRunning = loader.IsVisible = true;
        Button btn = sender as Button;
        btn.IsEnabled = false;
        await Navigation.PushAsync(new Login(new ViewModels.LoginViewModel()));
        loader.IsRunning = loader.IsVisible = false;
    }
}