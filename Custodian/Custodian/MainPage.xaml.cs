using System.Collections.ObjectModel;

namespace Custodian;

public partial class MainPage : ContentPage
{
    ObservableCollection<string> barcodes = new ObservableCollection<string>();
    public MainPage()
	{
		InitializeComponent();
        MessagingCenter.Subscribe<App, string>(this, "ScanBarcode", (sender, arg) => {
            ScanBarcode(arg);
        });
        list.ItemsSource = barcodes;
    }
    private void ScanBarcode(string barcode)
    {
        barcodes.Add(barcode);
    }

}

