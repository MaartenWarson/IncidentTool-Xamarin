using IncidentTool.Container;
using IncidentTool.Interfaces.Services.General;
using IncidentTool.Models;
using IncidentTool.ViewModels.Base;
using IncidentTool.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace IncidentTool.Library
{
    public class HomeViewModel : ViewModelBase
    {
        // Membervariabelen
        private readonly INavigationService _navigationService;
        private User _user;


        // Properties
        public INavigation Navigation { get; set; }

        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged("User");
            }
        }


        // Commands
        public ICommand OpenReportIncidentViewCommand => new Command(OnOpenReportIncidentView);
        public ICommand OpenReportedIncidentsViewCommand => new Command(OnOpenReportedIncidentsView);
        public ICommand LogoutCommand => new Command(OnLogout);


        // Constructor
        public HomeViewModel(INavigation navigation) : base()
        {
            Navigation = navigation;

            // Services initialiseren
            _navigationService = (INavigationService)AppContainer.Instance.Resolve(typeof(INavigationService));
        }


        // Mehods
        private void OnOpenReportIncidentView()
        {
            _navigationService.NavigateTo(new ScanQRView(), Navigation);
        }

        private void OnOpenReportedIncidentsView()
        {
            _navigationService.NavigateTo(new ReportedIncidentsView(), Navigation);
        }

        private void OnLogout()
        {
            _navigationService.NavigateTo(new LoginView(), Navigation);
        }
    }
}
