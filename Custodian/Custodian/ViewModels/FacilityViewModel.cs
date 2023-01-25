using CommunityToolkit.Mvvm.Input;
using Custodian.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodian.ViewModels
{
    public partial class FacilityViewModel
    {
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
