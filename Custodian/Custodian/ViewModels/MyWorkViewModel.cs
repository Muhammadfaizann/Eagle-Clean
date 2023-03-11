using CommunityToolkit.Mvvm.Input;
using Custodian.Helpers;
using Custodian.Models;
using Custodian.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodian.ViewModels
{
    public partial class MyWorkViewModel
    {
        public MyWorkViewModel()
        {
           
        }

        [RelayCommand]
        async System.Threading.Tasks.Task NavigateAssignment(object arg)
        {
            try
            {
                Utils.activeRouteFileName = (arg as MergeRecord).filename;
                var navigationParameter = new Dictionary<string, object>
               {
                    { "param", arg }
               };
               await Shell.Current.GoToAsync(nameof(ProofOfWork), navigationParameter);
            }
            catch(Exception ex)
            {

            }
           
        }
    }
}
