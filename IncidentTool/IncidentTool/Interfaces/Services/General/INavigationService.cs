using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IncidentTool.Interfaces.Services.General
{
    public interface INavigationService
    {
        void NavigateTo(Page page, INavigation navigation);
    }
}
