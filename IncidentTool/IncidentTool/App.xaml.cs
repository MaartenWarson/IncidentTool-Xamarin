using IncidentTool.Container;
using IncidentTool.Interfaces.Services.General;
using IncidentTool.ViewModels;
using IncidentTool.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IncidentTool
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Bij opstarten wordt NavigationPage met startpagina LoginView getoond
            MainPage = new NavigationPage(new LoginView())
            {
                // Menubalk (in app)
                BarBackgroundColor = Color.FromHex("#FF007689"),
                BarTextColor = Color.White, 
            };

            InitializeMessenger();
        }

        private void InitializeMessenger()
        {
            // ReportedIncidentsViewModel
            var reportedIncidentsViewModel = AppContainer.Instance.Resolve<ReportedIncidentsViewModel>();
            reportedIncidentsViewModel.InitializeMessenger();

            // ReportIncidentViewModel
            var reportIncidentViewModel = AppContainer.Instance.Resolve<ReportIncidentViewModel>();
            reportIncidentViewModel.InitializeMessenger();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
