﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Custodian.Helpers;
using Custodian.Helpers.LocationService;
using Custodian.Messages;
using Custodian.Models;
using Custodian.Pages;
using Custodian.Popups;
using Custodian.Services.ProofOfWork;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Custodian.ViewModels
{
    public partial class ProofOfWorkViewModel : ObservableObject
    {
        #region Properties
        private IProofOfWorkService _proofOfWorkService;
        ILocationService _locationService;
        private TimeSpan prevTime;
        private Step currentStep;
        private ObservableCollection<Step> _CleaningPlanList = new ObservableCollection<Step>();
        public ObservableCollection<Step> CleaningPlanList
        {
            get => _CleaningPlanList;
            set => SetProperty(ref _CleaningPlanList, value);
        }
        private string _ButtonText = "End Route";
        public string ButtonText {
            get => _ButtonText;
            set => SetProperty(ref _ButtonText, value);
        }
        private string _TimerText;
        public string TimerText
        {
            get => _TimerText;
            set => SetProperty(ref _TimerText, value);
        }
        private string _ActualTimeText;
        public string ActualTimeText
        {
            get => _ActualTimeText;
            set => SetProperty(ref _ActualTimeText, value);
        }
        #endregion

        public ProofOfWorkViewModel(ILocationService locationService, IProofOfWorkService proofOfWorkService)
        {
            ButtonText = "End Route";
            prevTime = TimeSpan.Zero;
            ActualTimeText = "--------";
            _locationService=locationService;
            _proofOfWorkService=proofOfWorkService;
            WeakReferenceMessenger.Default.Register<TaskCompletedMessage>(this, TaskCompletedMessageReceived);
        }

       


        #region Events
        private async void TaskCompletedMessageReceived(object recipient, TaskCompletedMessage message)
        {
            string task = currentStep.Description + "|" + currentStep.PlannedTimeInMint;
            Utils.activeRouteRecord.tasksIncomplete.Remove(task);

            TimeSpan currentTime = TimeSpan.Parse(_TimerText);
            TimeSpan difference = currentTime.Subtract(prevTime);
            prevTime = currentTime;

            if (Utils.activeRouteRecord.actualTime != null)
            {
                int actualTime = int.Parse(Utils.activeRouteRecord.actualTime) + difference.Seconds;
                Utils.activeRouteRecord.actualTime = actualTime.ToString();
            }
            else
            {
                Utils.activeRouteRecord.actualTime = difference.Seconds.ToString();
            }

            Utils.activeRouteRecord.tasksComplete.Add(task + "|" + difference.Seconds);
            _CleaningPlanList.Remove(currentStep);
            if (_CleaningPlanList.Count == 0)
            {
                WeakReferenceMessenger.Default.Send(new StopTimerMessage("Stop Timer"));
                ButtonText = "Complete Route";
                ActualTimeText = _TimerText;
            }
            CleaningPlanList = _CleaningPlanList;

            Utils.activeRouteRecord.seq = "2";
            Utils.activeRouteRecord.endDate = DateTime.Now.ToString("MM/dd/yyyy");
            Utils.activeRouteRecord.endTime = DateTime.Now.ToString("HH:mm:ss");
            Location currentLocation = await _locationService.GetCurrentLocation();
            Utils.activeRouteRecord.startLatitude = currentLocation.Latitude.ToString();
            Utils.activeRouteRecord.startLongitude = currentLocation.Longitude.ToString();
            Utils.activeRouteRecord.status = "In-Progress";

            string jsonRecord = JsonSerializer.Serialize<MergeRecord>(Utils.activeRouteRecord);
            await DatabaseService.write(jsonRecord);

        }

        #endregion

        #region Commands
        [RelayCommand]
        Task TaskCompleted(object arg)
        {

            currentStep = arg as Step;
            return Task.CompletedTask;

        }
        #endregion


    }
}