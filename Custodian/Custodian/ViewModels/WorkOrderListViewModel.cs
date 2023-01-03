using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Custodian.Pages;


namespace Custodian.ViewModels
{
    public partial class WorkOrderListViewModel : ObservableObject
    {

        [RelayCommand]
        async Task Navigate(object arg)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "param", arg }
            };
            await Shell.Current.GoToAsync(nameof(WorkOrderPage),navigationParameter);
        }



    }
}
