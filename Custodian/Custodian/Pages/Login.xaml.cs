using CommunityToolkit.Mvvm.Messaging;
using Custodian.ViewModels;
using System;

namespace Custodian.Pages;

public partial class Login : ContentPage
{
    public Login(LoginViewModel vm)
    {  
        InitializeComponent();
        BindingContext = vm;
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
    
    private async void btnLogin_Clicked(object sender, EventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new string("loadlogin"));
        await Navigation.PopToRootAsync();
    }
}