using Custodian.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodian.Helpers
{
    public static class Utils
    {
        public static string ROOT_PATH = "/storage/emulated/0/";
        public static Route activeAssigment { get; set; }
        public static ObservableCollection<Route> ongoingRoutes = new ObservableCollection<Route>();
        public static ObservableCollection<CompletedRoute> completedRoutes = new ObservableCollection<CompletedRoute>
        {
            new CompletedRoute { Title = "Route 001", IsOverTime = true },
            new CompletedRoute { Title = "Route 002", IsOverTime = false },
            new CompletedRoute { Title = "Route 003", IsOverTime = false },
            new CompletedRoute { Title = "Route 004", IsOverTime = true },
        };
        public static Config config { get; set; }
    }
}
