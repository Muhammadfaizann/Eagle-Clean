using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Messaging;
using Custodian.ActivityLog;
using Custodian.Helpers;
using Custodian.Messages;
using Custodian.ViewModels;
using Kotlin.Text;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace Custodian.Pages;

public partial class Login : ContentPage
{
    bool IsScanned = false;
    string badgeID = string.Empty;

    public Login(LoginViewModel vm) 
    {


        InitializeComponent();
        BindingContext = vm;
        WeakReferenceMessenger.Default.Register<BarcodeScanMessage>(this, (sender, args) =>
        { 
            try
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                   
                    IsScanned = true;
                    badgeID = args.Value.ToString();
                    if (args.Value.Length == 12)
                    {
                        entryId1.Text = args.Value[0].ToString();
                        entryId2.Text = args.Value[1].ToString();
                        entryId3.Text = args.Value[2].ToString();
                        entryId4.Text = args.Value[3].ToString();
                        entryId5.Text = args.Value[4].ToString();
                        entryId6.Text = args.Value[5].ToString();
                        entryId7.Text = args.Value[6].ToString();
                        entryId8.Text = args.Value[7].ToString();
                        entryId9.Text = args.Value[8].ToString();
                        entryId10.Text = args.Value[9].ToString();
                        entryId11.Text = args.Value[10].ToString();
                        entryId12.Text = args.Value[11].ToString();
                        VerifyLogin(badgeID);
                    }
                    else
                    {
                        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                        string text = "Invalid Barcode!!";
                        ToastDuration duration = ToastDuration.Short;
                        double fontSize = 12;
                        var toast = Toast.Make(text, duration, fontSize);
                        await toast.Show(cancellationTokenSource.Token);
                    }
                   
                });

            }
            catch (Exception ex)
            {
                Logger.Log("1", "Exception", ex.Message);
            }
        });
    }

    private void entryId1_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!IsScanned)
        {
            badgeID = string.Empty;
            badgeID = entryId1.Text;
            entryId2.Focus();
        }
    }
    private void entryId2_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!IsScanned)
        {
            badgeID = badgeID + entryId2.Text;
            entryId3.Focus();
        }
    }
    private void entryId3_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!IsScanned)
        {
            badgeID = badgeID + entryId3.Text;
            entryId4.Focus();
        }
    }
    private void entryId4_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!IsScanned)
        {
            badgeID = badgeID + entryId4.Text;
            entryId5.Focus();
        }
    }
    private void entryId5_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!IsScanned)
        {
            badgeID = badgeID + entryId5.Text;
            entryId6.Focus();
        }
    }
    private void entryId6_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!IsScanned)
        {
            badgeID = badgeID + entryId6.Text;
            entryId7.Focus();
        }
    }
    private void entryId7_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!IsScanned)
        {
            badgeID = badgeID + entryId7.Text;
            entryId8.Focus();
        }
    }
    private void entryId8_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!IsScanned)
        {
            badgeID = badgeID + entryId8.Text;
            entryId9.Focus();
        }
    }
    private void entryId9_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!IsScanned)
        {
            badgeID = badgeID + entryId9.Text;
            entryId10.Focus();
        }
    }
    private void entryId10_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!IsScanned)
        {
            badgeID = badgeID + entryId10.Text;
            entryId11.Focus();
        }
    }
    private void entryId11_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!IsScanned)
        {
            badgeID = badgeID + entryId11.Text;
            entryId12.Focus();
        }
    }
    private void entryId12_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!IsScanned)
        {
            badgeID = badgeID + entryId12.Text;
            Login_Clicked(null, null);
        }
        IsScanned = false;
    }

    private void Login_Clicked(object sender, EventArgs e)
    {
       
        Button btn = sender as Button;
        btn.IsEnabled = false;
        VerifyLogin(badgeID);
    }

    private async void VerifyLogin(string badgeId)
    {
        try
        {
            Logger.Log("2", "Info", $"Barcode scanned with Badge ID: {badgeID} , Length: {badgeID.Length}.");
           
            if (await Utils.IsBadgeValid(badgeID))
            {
                Utils.BadgeID = badgeID;
                var vm = BindingContext as LoginViewModel;
                vm.LoginCommand.Execute(null);
            }
            else
            {
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                string text = "Invalid badge ID";
                ToastDuration duration = ToastDuration.Short;
                double fontSize = 12;
                var toast = Toast.Make(text, duration, fontSize);
                await toast.Show(cancellationTokenSource.Token);
            }

           
        }
        catch (Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        WeakReferenceMessenger.Default.Unregister<BarcodeScanMessage>(this);
    }


}
