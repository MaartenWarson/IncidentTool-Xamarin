using Autofac;
using IncidentTool.Interfaces.Repositories;
using IncidentTool.Interfaces.Services.Data;
using IncidentTool.Interfaces.Services.General;
using IncidentTool.Repositories;
using IncidentTool.Services.Data;
using IncidentTool.Services.General;
using IncidentTool.ViewModels;
using IncidentTool.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace IncidentTool.Container
{
    public class AppContainer : IDependencyResolver
    {
        private IContainer _container; // Autofac container
        private static AppContainer _instance;
        public static AppContainer Instance => _instance ?? (_instance = new AppContainer()); // Instantie van de container

        private AppContainer()
        {
            RegisterDependencies();
        }

        private void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            // ViewModels
            builder.RegisterType<LoginViewModel>();
            builder.RegisterType<HomeViewModel>();
            builder.RegisterType<ReportIncidentViewModel>().SingleInstance(); // 1 object voor de volledige app gebruiken (ivm Messaging)
            builder.RegisterType<ReportedIncidentsViewModel>().SingleInstance(); // 1 object voor de volledige app gebruiken (ivm Messaging)
            builder.RegisterType<ScanQRViewModel>();

            // Services
            builder.Register(c => Instance).As<IDependencyResolver>();
            builder.RegisterType<UserDataService>().As<IUserDataService>();
            builder.RegisterType<NavigationService>().As<INavigationService>();
            builder.RegisterType<OccurredIncidentDataService>().As<IOccurredIncidentDataService>();
            builder.RegisterType<DeviceDataService>().As<IDeviceDataService>();
            builder.RegisterType<IncidentDataService>().As<IIncidentDataService>();

            // Repository
            builder.RegisterType<GenericRepository>().As<IGenericRepository>();

            // Container builden
            _container = builder.Build();
        }

        public object Resolve(Type typeName)
        {
            return _container.Resolve(typeName);
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
