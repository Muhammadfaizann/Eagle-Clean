using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Custodian.Helpers;
using Custodian.Messages;
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
        public FacilityViewModel() 
        {
            WeakReferenceMessenger.Default.Register<StartRouteMessage>(this, (sender, args) =>
            { 
                try
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await NavigateAssignment(new Models.Assignment() { Title = "Route 006" });
                    });

                }
                catch (Exception ex)
                {

                }
            });
        }
        [RelayCommand]
        async Task NavigateAssignment(object arg)
        {
            Utils.activeAssigment = (Models.Assignment)arg;
            var navigationParameter = new Dictionary<string, object>
            {
                { "param", arg }
            };
            await Shell.Current.GoToAsync(nameof(ActiveCleaningRoutePage), navigationParameter);
        } 
        [RelayCommand]
        async Task NavigateWorkorder(object arg)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "param", arg }
            };
            await Shell.Current.GoToAsync(nameof(WorkOrderPage), navigationParameter);
        }
    }
}
