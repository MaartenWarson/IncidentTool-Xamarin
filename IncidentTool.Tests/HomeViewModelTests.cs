using IncidentTool.Interfaces.Services.General;
using IncidentTool.ViewModels;
using IncidentTool.Views;
using Moq;
using NUnit.Framework;
using Xamarin.Forms;

namespace IncidentTool.Tests
{
    [TestFixture]
    public class HomeViewModelTests
    {
        private HomeViewModel _model;
        private Mock<INavigation> _navigationMock;
        private Mock<INavigationService> _navigationServiceMock;

        [SetUp]
        public void Setup()
        {
            _navigationMock = new Mock<INavigation>();
            _navigationServiceMock = new Mock<INavigationService>();
            _model = new HomeViewModel(_navigationMock.Object);
        }

        [Test]
        public void OpenReportIncidentViewCommand_ShouldOpenScanView()
        {
            // Arrange
            var scanQRViewMock = new Mock<ScanQRView>();

            // Act
            _model.OpenReportIncidentViewCommand.Execute(null);

            // Assert
            _navigationServiceMock.Verify(service => service.NavigateTo(scanQRViewMock.Object, _model.Navigation), Times.Once);
        }


        [Test]
        public void OpenReportedIncidentsViewCommand_ShouldOpenReportedIncidentsView()
        {
            // Arrange
            var reportedIncidentsViewMock = new Mock<ReportedIncidentsView>();

            // Act
            _model.OpenReportedIncidentsViewCommand.Execute(null);

            // Assert
            _navigationServiceMock.Verify(service => service.NavigateTo(reportedIncidentsViewMock.Object, _model.Navigation), Times.Once);
        }

        [Test]
        public void LogoutCommand_ShouldOpenLoginView()
        {
            // Arrange
            var loginViewMock = new Mock<LoginView>();

            // Act
            _model.LogoutCommand.Execute(null);

            // Assert
            _navigationServiceMock.Verify(service => service.NavigateTo(loginViewMock.Object, _model.Navigation), Times.Once);
        }
    }
}