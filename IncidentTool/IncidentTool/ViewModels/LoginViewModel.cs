using IncidentTool.Constants;
using IncidentTool.Container;
using IncidentTool.Interfaces.Services.Data;
using IncidentTool.Interfaces.Services.General;
using IncidentTool.Models;
using IncidentTool.ViewModels.Base;
using IncidentTool.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace IncidentTool.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        // Membervariabelen
        private string _userName;
        private string _password;
        private string _message;
        private readonly IUserDataService _userDataService;
        private readonly INavigationService _navigationService;


        // Properties
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public INavigation Navigation { get; set; }  // Nodig voor NavigationPage


        // Commands
        public ICommand LoginCommand => new Command(OnLoginAsync);


        // Constructor
        public LoginViewModel(INavigation navigation) : base()
        {
            Navigation = navigation;

            // Services initialiseren
            _userDataService = (IUserDataService)AppContainer.Instance.Resolve(typeof(IUserDataService));
            _navigationService = (INavigationService)AppContainer.Instance.Resolve(typeof(INavigationService));
        }


        // Command methods
        private async void OnLoginAsync()
        {
            Message = "";

            if (_userName == null || _password == null)
            {
                Message = "Vul een gebruikersnaam en wachtwoord in";
            }
            else
            {
                User user = await _userDataService.GetUserByNameAsync(_userName.ToLower());

                if (user == null)
                {
                    Message = "Gebruiker bestaat niet";
                }
                else if (user.Password != _password)
                {
                    Message = "Wachtwoord is niet correct";
                }
                else
                {
                    MessagingCenter.Send(this, MessagingConstants.PassUser, user); // Bericht sturen naar eender wie erop gesubscribed heeft. Hierin wordt de gebruiker meegestuurd die aangemeld wordt.
                    _navigationService.NavigateTo(new HomeView(), Navigation);
                }
            }
        }
    }
}
