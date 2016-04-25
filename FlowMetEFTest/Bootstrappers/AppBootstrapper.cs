using Caliburn.Micro;
using FlowMetEFTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FlowMetEFTest.Bootstrappers
{
    class AppBootstrapper : BootstrapperBase
    {
        private readonly SimpleContainer _container =
            new SimpleContainer();
        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override void Configure()
        {
            _container.Instance<IWindowManager>(new WindowManager());
            _container.Singleton<IEventAggregator, EventAggregator>();            
            _container.PerRequest<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            var instance = _container.GetInstance(service, key);
            if (instance != null)
                return instance;
            throw new InvalidOperationException("Could not locate any instances.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
