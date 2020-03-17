using IncidentTool.Constants;
using IncidentTool.Container;
using IncidentTool.Interfaces.Services.General;
using IncidentTool.ViewModels.Base;
using IncidentTool.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing;

namespace IncidentTool.ViewModels
{
    public class ScanQRViewModel : ViewModelBase
    {
        // Membervariabelen
        private string _qrCodeResult;
        private readonly INavigationService _navigationService;


        // Properties
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

        public Result Result { get; set; }


        // Commands
        public ICommand ScanCommand => new Command(OnScan);


        // Constructor
        public ScanQRViewModel(INavigation navigation) : base()
        {
            Navigation = navigation;

            // Services initialiseren
            _navigationService = (INavigationService)AppContainer.Instance.Resolve(typeof(INavigationService));
        }


        // Methods
        private void OnScan()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                QrCodeResult = Result.Text;

                // Bericht met QR-string wordt verzonden
                MessagingCenter.Send(this, MessagingConstants.PassScannedQRString, _qrCodeResult);
                // Navigeren naar de volgende view
                _navigationService.NavigateTo(new ReportIncidentView(), Navigation);
            });
        }
    }
}
