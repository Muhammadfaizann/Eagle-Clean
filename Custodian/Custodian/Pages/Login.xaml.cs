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

    private async void Login_Clicked(object sender, EventArgs e)
    {
        try
        {
            Logger.Log("2", "Info", $"Barcode scanned with Badge ID: {badgeID} , Length: {badgeID.Length}.");
            

                if (Utils.IsBadgeValid(badgeID))
                {
                    Utils.BadgeID = badgeID;
                    Utils.ImportConfigurations();
                    Utils.LoadRoutes();
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
        catch(Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    }

    

    /*
    private int GetCheckDigit(string barcode)
    {
        List<int> serialNumberArray = new List<int>();
        var modCheck = barcode.Length % 2;
        var loopCounter = 0;
        while (loopCounter < barcode.Length)
        {
            var digit = Convert.ToInt32(barcode.Substring(loopCounter, 1));
            serialNumberArray.Add(digit);
            loopCounter++;
        }

        for (var i = 0; i < serialNumberArray.Count; i++)
        {
            var check = i + 1;
            if ((check % 2) == modCheck)
            {
                serialNumberArray[i] = serialNumberArray[i] * 2;
            }
        }

        loopCounter = 0;
        var additiveResult = 0;
        var arrayItemValue = 0;
        var checkSum = 0;

        while (loopCounter < barcode.Length)
        {
            arrayItemValue = serialNumberArray[loopCounter];
            if (arrayItemValue > 9)
            {
                additiveResult = 1 + (arrayItemValue % 10);
            }
            else
            {
                additiveResult = arrayItemValue;
            }
            checkSum = checkSum + additiveResult;
            loopCounter++;
        }

        var checkDigit = 0;
        if ((checkSum % 10) > 0)
        {
            checkDigit = 10 - (checkSum % 10);
        }

        return checkDigit;
    }
    private int Mod10Check(string creditCardNumber)
    {
        //// 1.	Starting with the check digit double the value of every other digit 
        //// 2.	If doubling of a number results in a two digits number, add up
        ///   the digits to get a single digit number. This will results in eight single digit numbers                    
        //// 3. Get the sum of the digits
        int sumOfDigits = creditCardNumber.Where((e) => e >= '0' && e <= '9')
                        .Reverse()
                        .Select((e, i) => ((int)e - 48) * (i % 2 == 0 ? 1 : 2))
                        .Sum((e) => e / 10 + e % 10);


        //// If the final sum is divisible by 10, then the credit card number
        //   is valid. If it is not divisible by 10, the number is invalid.            
        return sumOfDigits % 10;
    }*/
    public int CalculateMod10CheckDigit(string number)
    {
        int sum = 0;
        bool isEvenPosition = false;

        // Iterate over the digits of the number from right to left
        for (int i = number.Length - 1; i >= 0; i--)
        {
            int digit = int.Parse(number[i].ToString());

            // Double the value of every other digit starting from the second-to-last
            if (isEvenPosition)
            {
                digit *= 2;
                if (digit > 9)
                {
                    digit = digit % 10 + digit / 10;
                }
            }

            sum += digit;
            isEvenPosition = !isEvenPosition;
        }

        // Calculate the check digit as the number that must be added to the sum to make it a multiple of 10
        int checkDigit = (sum % 10 == 0) ? 0 : 10 - (sum % 10);
        return checkDigit;
    }
    
}
