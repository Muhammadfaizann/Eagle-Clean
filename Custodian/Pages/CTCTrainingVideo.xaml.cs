namespace Custodian.Pages;

public partial class CTCTrainingVideo : ContentPage
{
	public CTCTrainingVideo()
	{
		InitializeComponent();
        collection.ItemsSource = new string[] { "CTC Recuring Session - July", "CTC Recuring Session - June", "CTC Recurring Session - May", "CTC Recurring Session - April", "CTC Recurring Session - March" };
    }
   
}