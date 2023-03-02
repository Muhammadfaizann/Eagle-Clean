using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Custodian.Helpers;
using Custodian.Messages;
using Custodian.Pages;
using Microsoft.Extensions.Configuration;
using CommunityToolkit.Mvvm.ComponentModel;
using Org.Json;
using Custodian.Models;
using System.Text.Json;
using Custodian.Services;
using GoogleGson;

namespace Custodian.ViewModels
{
    public partial class FacilityViewModel : ObservableObject
    {
        
        private bool _IsButtonsVisible = true;
        public bool IsButtonsVisible
        {
            get => _IsButtonsVisible;
            set => SetProperty(ref _IsButtonsVisible, value);
        }
        
        public FacilityViewModel() 
        {           
            
        }
        
        [RelayCommand]
        async Task NavigateAssignment(object arg)
        {
            Utils.activeAssigment = (Models.Route)arg;
            var navigationParameter = new Dictionary<string, object>
            {
                { "param", arg }
            };
            await Shell.Current.GoToAsync(nameof(ProofOfWork), navigationParameter);
            
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
