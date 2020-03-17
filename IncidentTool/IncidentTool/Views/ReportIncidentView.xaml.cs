using IncidentTool.Container;
using IncidentTool.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IncidentTool.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportIncidentView : ContentPage
    {
        public ReportIncidentView()
        {
            InitializeComponent();

            // ViewModel uit container nemen (want wordt maar 1 ViewModel gemaakt voor volledige app)
            var reportIncidentViewModel = AppContainer.Instance.Resolve<ReportIncidentViewModel>();
            reportIncidentViewModel.Navigation = Navigation;
            BindingContext = reportIncidentViewModel;
        }
    }
}