using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Messaging;
using Custodian.Messages;
using Custodian.ViewModels;
using System;
using System.Linq.Expressions;

namespace Custodian.Pages;

public partial class Login : ContentPage
{
    bool IsScanned = false;
    public Login(LoginViewModel vm) 
    {  
        InitializeComponent();
        BindingContext = vm;
        WeakReferenceMessenger.Default.Register<BarcodeScanMessage>(this, (sender, args) =>
        { //multiple calls are triggering
            try
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    IsScanned = true;
                    if (args.Value.Length == 14)
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
                
            }
        });
    }

    private void entryId1_TextChanged(object sender, TextChangedEventArgs e)
    {
		entryId2.Focus();
    }
    private void entryId2_TextChanged(object sender, TextChangedEventArgs e)
    {
        entryId3.Focus();
    }
    private void entryId3_TextChanged(object sender, TextChangedEventArgs e)
    {
        entryId4.Focus();
    }
    private void entryId4_TextChanged(object sender, TextChangedEventArgs e)
    {
        entryId5.Focus();
    }
    private void entryId5_TextChanged(object sender, TextChangedEventArgs e)
    {
        entryId6.Focus();
    }
    private void entryId6_TextChanged(object sender, TextChangedEventArgs e)
    {
        entryId7.Focus();
    }
    private void entryId7_TextChanged(object sender, TextChangedEventArgs e)
    {
        entryId8.Focus();
    }
    private void entryId8_TextChanged(object sender, TextChangedEventArgs e)
    {
        entryId9.Focus();
    }
    private void entryId9_TextChanged(object sender, TextChangedEventArgs e)
    {
        entryId10.Focus();
    }
    private void entryId10_TextChanged(object sender, TextChangedEventArgs e)
    {
        entryId11.Focus();
    }
    private void entryId11_TextChanged(object sender, TextChangedEventArgs e)
    {
        entryId12.Focus();
    }
    private void entryId12_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!IsScanned)
        {
            var vm = BindingContext as LoginViewModel;
            vm.LoginCommand.Execute(null);
        }
        IsScanned = false;
    } 
}