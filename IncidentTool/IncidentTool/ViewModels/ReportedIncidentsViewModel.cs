using IncidentTool.Constants;
using IncidentTool.Container;
using IncidentTool.Interfaces.Services.Data;
using IncidentTool.Interfaces.Services.General;
using IncidentTool.Models;
using IncidentTool.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Device = IncidentTool.Models.Device;

namespace IncidentTool.ViewModels
{
    public class ReportedIncidentsViewModel : ViewModelBase
    {
        // Membervariabelen
        private IList<OccurredIncident> _occurredIncidents;
        private Device _tempDevice;
        private readonly IOccurredIncidentDataService _occurredIncidentDataService;
        private readonly IDeviceDataService _deviceDataService;
        private User _user;


        // Properties
        public IList<OccurredIncident> OccurredIncidents
        {
            get { return _occurredIncidents; }
            set
            {
                _occurredIncidents = value;
                OnPropertyChanged();
            }
        }

        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }


        // Commands
        public ICommand ShowIncidentsCommand => new Command(OnShowIncidents);


        // Constructor
        public ReportedIncidentsViewModel() : base()
        {
            // Services initialiseren
            _occurredIncidentDataService = (IOccurredIncidentDataService)AppContainer.Instance.Resolve(typeof(IOccurredIncidentDataService));
            _deviceDataService = (IDeviceDataService)AppContainer.Instance.Resolve(typeof(IDeviceDataService));
        }


        // Methods
        // Subscriben op messages van LoginViewModel (waaraan user wordt meegegeven)
        public void InitializeMessenger()
        {
            MessagingCenter.Subscribe<LoginViewModel, User>(this, MessagingConstants.PassUser,
                (loginViewModel, user) => OnPassUserReceived(user)); // Wanneer er een bericht is met de opgegeven naam, wordt de OnPassUserReceived-methode uitgevoerd
        }

        private void OnPassUserReceived(User user)
        {
            User = user;
        }

        private async void OnShowIncidents()
        {
            // Alle occured incidents opvragen
            int id = _user.UserId;
            var occurredIncidentsOriginal = (await _occurredIncidentDataService.GetAllOccurredIncidentsByUserIdAsync(id)).ToList();
            IList<OccurredIncident> tempOccurredIncidents = new List<OccurredIncident>();

            // Voor ieder verkregen occurred incident wordt een nieuw (iets verschillend) object gemaakt
            foreach (OccurredIncident incident in occurredIncidentsOriginal)
            {
                await InitDeviceById(incident.DeviceId);

                OccurredIncident occurredIncident = new OccurredIncident
                {
                    OccurredIncidentId = incident.OccurredIncidentId,
                    DeviceId = incident.DeviceId,
                    DeviceName = _tempDevice.Name,
                    DeviceLocation = _tempDevice.Location,
                    IncidentDescription = incident.IncidentDescription,
                    CurrentUserId = incident.CurrentUserId,
                    Solved = incident.Solved,
                    SolvedString = "Niet opgelost"
                };

                if (occurredIncident.Solved)
                {
                    occurredIncident.SolvedString = "Opgelost";
                }

                tempOccurredIncidents.Add(occurredIncident);
            }

            OccurredIncidents = tempOccurredIncidents;
        }

        private async Task InitDeviceById(int deviceId)
        {
            _tempDevice = await _deviceDataService.GetDeviceByIdAsync(deviceId);
        }
    }
}
