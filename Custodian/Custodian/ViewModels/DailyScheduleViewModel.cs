using Custodian.Pages;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Custodian.ViewModels
{
    public partial class DailyScheduleViewModel : ObservableObject
    {
        [ObservableProperty]
        string activeRoute;

        [RelayCommand]
        async Task Navigate(object arg)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "param", arg }
            };
            await Shell.Current.GoToAsync(nameof(ActiveCleaningRoutePage), navigationParameter);
        }
    }
}
