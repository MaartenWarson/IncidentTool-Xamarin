using IncidentTool.Constants;
using IncidentTool.Container;
using IncidentTool.Interfaces.Services.Data;
using IncidentTool.Interfaces.Services.General;
using IncidentTool.Models;
using IncidentTool.ViewModels.Base;
using IncidentTool.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IncidentTool.ViewModels
{
    public class ReportIncidentViewModel : ViewModelBase
    {
        // Membervariabelen (gegevens van gescanned device)
        private int _deviceId;
        private string _deviceName;
        private string _deviceLocation;
        private int _currentDeviceTypeId;

        // Membervariabelen (gegevens nodig om OccurredIncident te maken)
        private IList<Incident> _incidents;
        private Incident _selectedIncident; // Hier wordt description van genomen
        private string _otherIncidentDescription;
        private int _userId;
        private string _message;
        private readonly IIncidentDataService _incidentDataService;
        private readonly IOccurredIncidentDataService _occurredIncidentDataService;
        private readonly INavigationService _navigationService;
        private string _qrCodeResult;


        // Properties
        public int DeviceId
        {
            get { return _deviceId; }
            set
            {
                _deviceId = value;
                OnPropertyChanged();
            }
        }

        public string DeviceName
        {
            get { return _deviceName; }
            set
            {
                _deviceName = value;
                OnPropertyChanged();
            }
        }

        public string DeviceLocation
        {
            get { return _deviceLocation; }
            set
            {
                _deviceLocation = value;
                OnPropertyChanged();
            }
        }

        public int CurrentDeviceTypeId
        {
            get { return _currentDeviceTypeId; }
            set
            {
                _currentDeviceTypeId = value;
                OnPropertyChanged();
            }
        }

        public IList<Incident> Incidents
        {
            get { return _incidents; }
            set
            {
                _incidents = value;
                OnPropertyChanged();
            }
        }

        public Incident SelectedIncident
        {
            get { return _selectedIncident; }
            set
            {
                _selectedIncident = value;
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

        public string OtherIncidentDescription
        {
            get { return _otherIncidentDescription; }
            set
            {
                _otherIncidentDescription = value;
                OnPropertyChanged();
            }
        }

        public int UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                OnPropertyChanged();
            }
        }

        public string QrCodeResult
        {
            get { return _qrCodeResult; }
            set
            {
                _qrCodeResult = value;
                OnPropertyChanged();
            }
        }

        public INavigation Navigation { get; set; }

        // Commands
        public ICommand ReportIncidentCommand => new Command(OnReportIncident);
        public ICommand ShowIncidentsCommand => new Command(LoadIncidents);
        public ICommand GoHomeCommand => new Command(OnGoToHomeView);


        // Constructor
        public ReportIncidentViewModel() : base()
        {
            // Services initialiseren
            _incidentDataService = (IIncidentDataService)AppContainer.Instance.Resolve(typeof(IIncidentDataService));
            _occurredIncidentDataService = (IOccurredIncidentDataService)AppContainer.Instance.Resolve(typeof(IOccurredIncidentDataService));
            _navigationService = (INavigationService)AppContainer.Instance.Resolve(typeof(INavigationService));

            InitializeMessenger();
        }


        // Methods
        public void InitializeMessenger()
        {
            // User (subscriben op message met user in)
            MessagingCenter.Subscribe<LoginViewModel, User>(this, MessagingConstants.PassUser,
                (loginViewModel, user) => OnPassUserReceived(user));

            // QR-CodeString (subsriben op message met SQ-string in)
            MessagingCenter.Subscribe<ScanQRViewModel, string>(this, MessagingConstants.PassScannedQRString,
                (scanQRViewModel, qrCodeResult) => OnPassQRString(qrCodeResult));
        }

        private void OnPassUserReceived(User user)
        {
            UserId = user.UserId;
        }

        private async void OnPassQRString(string qrCodeResult)
        {
            DissectQrCodeString(qrCodeResult);
        }

        private void DissectQrCodeString(string qrCodeResult)
        {
            string[] devidedData = qrCodeResult.Split(',');

            DeviceId = Convert.ToInt32(devidedData[0]);
            DeviceName = devidedData[1];
            DeviceLocation = devidedData[2];
            CurrentDeviceTypeId = Convert.ToInt32(devidedData[3]);
        }

        private async void LoadIncidents()
        {
            Incidents = (await _incidentDataService.GetAllIncidentsByDeviceTypeId(_currentDeviceTypeId)).ToList();
        }

        private void OnGoToHomeView()
        {
            _navigationService.NavigateTo(new HomeView(), Navigation);
        }

        private void OnReportIncident()
        {
            Message = "";

            if (_selectedIncident == null && _otherIncidentDescription == null)
            {
                Message = "Selecteer een incident of vul een ander incident in";
            }
            else
            {
                string description;

                // SelectedIncident heeft voorrang
                if (_selectedIncident != null)
                {
                    description = _selectedIncident.Description;
                }
                else
                {
                    description = _otherIncidentDescription;
                }

                _occurredIncidentDataService.CreateOccurredIncidentAsync(description, _userId, _deviceId);

                _selectedIncident = null;
                Message = "Incident succesvol gemeld";
            }
        }
    }
}
