using IncidentTool.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace IncidentTool.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanQRView : ContentPage
    {
        public ScanQRView()
        {
            InitializeComponent();

            BindingContext = new ScanQRViewModel(Navigation);
        }
    }
}