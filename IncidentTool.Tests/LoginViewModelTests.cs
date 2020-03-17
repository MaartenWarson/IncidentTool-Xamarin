using IncidentTool.Interfaces.Services.General;
using IncidentTool.ViewModels;
using IncidentTool.Views;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IncidentTool.Tests
{
    [TestFixture]
    public class LoginViewModelTests
    {
        private LoginViewModel _model;
        private Mock<INavigation> _navigationMock;
        private Mock<INavigationService> _navigationServiceMock;

        [SetUp]
        public void Setup()
        {
            _navigationMock = new Mock<INavigation>();
            _navigationServiceMock = new Mock<INavigationService>();
            _model = new LoginViewModel(_navigationMock.Object);
        }

        [Test]
        public void LoginCommand_ShouldOpenHomeViewIfCredentialsAreRight()
        {
            // Arrange
            _model.UserName = "maarten";
            _model.Password = "pxl";
            var homeViewMock = new Mock<HomeView>();

            // Act
            _model.LoginCommand.Execute(null);

            // Assert
            _navigationServiceMock.Verify(service => service.NavigateTo(homeViewMock.Object, _model.Navigation), Times.Once);
        }

        [Test]
        public void LoginCommand_ShouldNotOpenHomeViewIfCredentialsAreWrong()
        {
            // Arrange
            _model.UserName = "maarten";
            _model.Password = "wrong";
            var homeViewMock = new Mock<HomeView>();

            // Act
            _model.LoginCommand.Execute(null);

            // Assert
            _navigationServiceMock.Verify(service => service.NavigateTo(homeViewMock.Object, _model.Navigation), Times.Never);
        }
    }
}
