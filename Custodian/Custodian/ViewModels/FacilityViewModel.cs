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
        private IConfiguration conf;
        public bool IsButtonsVisible
        {
            get => _IsButtonsVisible;
            set => SetProperty(ref _IsButtonsVisible, value);
        }
        
        public FacilityViewModel(IConfiguration configuration) 
        {           
             IsButtonsVisible =  bool.Parse(configuration["IsButtonEnabled"]);
            WeakReferenceMessenger.Default.Register<StartRouteMessage>(this, (sender, args) =>
            { 
                try
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        int totalMin = 0;
                        var jsonString = args.Value.ToString();
                        DatabaseService.write(jsonString);
                        var route = JsonSerializer.Deserialize<Route>(jsonString);
                        route.steps = new List<Step>();
                        foreach (var task in route.tasks)
                        {
                           var strings= task.Split('|');
                           route.steps.Add(new Step { Description= strings[0], TimeInMins = strings[1] });
                           totalMin = totalMin + int.Parse(strings[1]);
                        }
                        TimeSpan timeSpan = TimeSpan.FromMinutes(totalMin);
                        route.plannedTime = timeSpan.ToString("t");
                        Utils.ongoingRoutes.Add(route);
                        _ = NavigateAssignment(route);
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
            Utils.activeAssigment = (Models.Route)arg;
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
