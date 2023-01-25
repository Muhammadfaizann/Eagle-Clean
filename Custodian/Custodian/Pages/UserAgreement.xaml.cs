namespace Custodian.Pages;

public partial class UserAgreement : ContentPage
{
	public UserAgreement()
	{
		InitializeComponent();
		lbl1.Text = "This Mutual Non-Disclosure Agreement (\"Agreement\"), dated as of ________ (\"Effective Date\"), is agreed by and between [COMPANY 1], with primary offices located at [STREET ADDRESS] (“Company 1”), and [COMPANY 2], with primary offices located at [STREET ADDRESS] (\"Company 2\").";
		lbl2.Text = "Company 1 and Company 2 are individually referred to herein as, the “Party”, and collectively as, the “Parties”.";
		lbl3.Text = "RECITALS";
        lbl4.Text = "WHEREAS, the Parties desire to explore and engage in discussions regarding a potential business opportunity of mutual interest (“Business Discussion”);";

    }

    private async void btnIAgree_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Login());
    }
   
}