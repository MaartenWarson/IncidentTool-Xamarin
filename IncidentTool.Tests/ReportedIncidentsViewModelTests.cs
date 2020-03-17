using IncidentTool.Interfaces.Services.General;
using IncidentTool.ViewModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IncidentTool.Tests
{
    [TestFixture]
    public class ReportedIncidentsViewModelTests
    {

        private ReportedIncidentsViewModel _model;

        [SetUp]
        public void Setup()
        {
            _model = new ReportedIncidentsViewModel();
        }

        [Test]
        public void ShowIncidentsCommand_ShouldFillOccurredIncidentsList()
        {
            // Act
            _model.ShowIncidentsCommand.Execute(null);

            // Assert
            Assert.NotNull(_model.OccurredIncidents);
        }
    }
}
