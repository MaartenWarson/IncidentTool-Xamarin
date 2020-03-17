using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IncidentTool.Services.General
{
    public class ModelViewLocator
    {
        public static void NavigateTo(Page page, INavigation navigation)
        {
            navigation.PushAsync(page);
        }
    }
}
