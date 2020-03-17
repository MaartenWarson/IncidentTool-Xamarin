using IncidentTool.Interfaces.Services.Data;
using IncidentTool.Interfaces.Services.General;
using IncidentTool.Models;
using IncidentTool.ViewModels;
using IncidentTool.Views;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IncidentTool.Tests
{
    [TestFixture]
    public class ReportIncidentViewModelTests
    {
        private ReportIncidentViewModel _model;
        private Mock<IOccurredIncidentDataService> _occurredIncidentDataServiceMock;
        private Mock<INavigationService> _navigationServiceMock;

        [SetUp]
        public void Setup()
        {
            _model = new ReportIncidentViewModel();
            _occurredIncidentDataServiceMock = new Mock<IOccurredIncidentDataService>();
            _navigationServiceMock = new Mock<INavigationService>();
        }

        [Test]
        public void ReportIncidentCommand_ShouldReportIncidentIfIncidentIsNotNull()
        {
            // Arrange
            var incidentMock = new Mock<Incident>();
            _model.SelectedIncident = incidentMock.Object;
            string description = "Incident";
            int userId = 1;
            int deviceId = 1;

            // Act
            _model.ReportIncidentCommand.Execute(null);

            // Assert
            _occurredIncidentDataServiceMock.Verify(service => service.CreateOccurredIncidentAsync(description, userId, deviceId), Times.Once);
        }

        [Test]
        public void ShowIncidentsCommand_ShouldFillIncidentsList()
        {
            // Act
            _model.ShowIncidentsCommand.Execute(null);

            // Assert
            Assert.NotNull(_model.Incidents);
        }

        [Test]
        public void GoHomeCommand_ShouldGoToHomeView()
        {
            // Arrange
            var homeViewMock = new Mock<HomeView>();

            // Act
            _model.GoHomeCommand.Execute(null);

            // Assert
            _navigationServiceMock.Verify(service => service.NavigateTo(homeViewMock.Object, _model.Navigation));
        }
    }
}
