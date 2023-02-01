using CommunityToolkit.Mvvm.Messaging;
using Custodian.Messages;
using Custodian.ViewModels;
using System;

namespace Custodian.Pages;

public partial class Login : ContentPage
{
    public Login(LoginViewModel vm)
    {  
        InitializeComponent();
        BindingContext = vm;

            WeakReferenceMessenger.Default.Register<BarcodeScanMessage>(this, (sender, args) => { 
                 if(args.Value.Length==14)
                 {
                    entryId1.Text=args.Value[0].ToString();
                    entryId2.Text=args.Value[1].ToString();
                    entryId3.Text=args.Value[2].ToString();
                    entryId4.Text=args.Value[3].ToString();
                    entryId5.Text=args.Value[4].ToString();
                    entryId6.Text=args.Value[5].ToString();
                    entryId7.Text=args.Value[6].ToString();
                    entryId8.Text=args.Value[7].ToString();
                    entryId9.Text=args.Value[8].ToString();
                    entryId10.Text=args.Value[9].ToString();
                    entryId11.Text=args.Value[10].ToString();
                    entryId12.Text=args.Value[11].ToString();
                    vm.LoginCommand.Execute(null);
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
        var vm = BindingContext as LoginViewModel;
        vm.LoginCommand.Execute(null);
    }
}