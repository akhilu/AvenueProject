using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Avenue.Integration.Configuration
{
    public class HostConfigurator
    {
        private readonly HostConfiguration _hostConfiguration = new HostConfiguration();


        public void AddEndPoint(Action<EndPointConfigurator> configure)
        {
            if (configure == null)
                throw new ArgumentNullException("configure");

            var configurator = new EndPointConfigurator();

            configure(configurator);

            _hostConfiguration.EndPointConfigurations.Add(configurator.EndPointConfiguration);
        }

        //public void ServiceLocator(Microsoft.Practices.ServiceLocation.IServiceLocator locator)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Logger<T>()
        //{
        //    throw new NotImplementedException();
        //}

        //public void DataProvider<T>()
        //{
        //    throw new NotImplementedException();
        //}

        public HostConfiguration GetConfig()
        {
            return _hostConfiguration;

        }
    }
}
