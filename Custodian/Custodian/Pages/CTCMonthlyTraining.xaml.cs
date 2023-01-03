using Custodian.Models;
using System.Collections.ObjectModel;

namespace Custodian.Pages;

public partial class CTCMonthlyTraining : ContentPage
{
	public CTCMonthlyTraining()
	{
		InitializeComponent();
		
		collection.ItemsSource = new MonthlyTraining[]
		{
			new MonthlyTraining { Title = "CTC Recuring Session - July", Subject = "Recycle" },
			new MonthlyTraining { Title = "CTC Recuring Session - June", Subject = "Emergency Response and Spill Response" },
			new MonthlyTraining { Title = "CTC Recurring Session - May", Subject = "Vacuum Specialist" },
			new MonthlyTraining { Title = "CTC Recurring Session - April", Subject = "Utility Specialist" },
			new MonthlyTraining { Title = "CTC Recurring Session - March", Subject = "Utility Specialist" },
		};
		
		

    }
}