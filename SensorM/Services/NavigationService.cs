using SensorM.Interfaces;
using SensorM.Pages;
using SensorM.ViewModels;
using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace SensorM.Services
{
    public class NavigationService : INavigationService
    {
        public Task NavigateToASync(string route, IDictionary<string, object> parameters)
        {
            if(parameters != null)
            {
                return Shell.Current.GoToAsync(route, parameters);
            }
            else
            {
                return Shell.Current.GoToAsync(route);
            }
        }
    }
}