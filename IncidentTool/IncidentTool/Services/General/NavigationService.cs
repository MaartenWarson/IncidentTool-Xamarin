using IncidentTool.Interfaces.Services.General;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IncidentTool.Services.General
{
    public class NavigationService : INavigationService
    {
        public void NavigateTo(Page page, INavigation navigation)
        {
            // Taak om te navigeren doorgeven aan ModelViewLocator
            ModelViewLocator.NavigateTo(page, navigation);
        }
    }
}
