using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorM.Interfaces
{
    public interface INavigationService
    {
        Task NavigateToPage(string pageName, object parameter = null);
        Task NavigateBack();
    }
}
