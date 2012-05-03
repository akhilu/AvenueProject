using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Avenue.Integration.EndPoint;

namespace Avenue.Integration.Configuration
{
    public class ServiceBusFactory
    {
        public static HostConfiguration New(Action<HostConfigurator> configure)
        {
            if (configure == null)
                throw new ArgumentNullException("configure");

            var configurator = new HostConfigurator();

            configure(configurator);

            //returns a configurationconfigurator.GetConfig()
            return configurator.GetConfig();

            //uses configuration to build instance of host;

        }

        public static ServiceBusHost CreateHost(HostConfiguration config)
        {
            ServiceBusHost host = new ServiceBusHost();
            //  host.

            foreach (var endPointConfig in config.EndPointConfigurations)
            {


                var d1 = typeof(EndPoint<>);
                Type[] typeArgs = { endPointConfig.DeSerializeTo };
                var makeme = d1.MakeGenericType(typeArgs);


                var newEndPoint = (IEndPoint)Activator.CreateInstance(makeme);

                //new End
                //var newEndPoint = new EndPoint<x>();
                newEndPoint.ConnectionString = endPointConfig.ConnectionString;
                newEndPoint.QueueClient = (IQueueClient)System.Activator.CreateInstance(endPointConfig.QueueClient);
                newEndPoint.DeSerializer = (IQueueMessageDeSerializer)System.Activator.CreateInstance(endPointConfig.UseSerializer);


                host.AddEndPoint(newEndPoint);
            }




            return host;
        }


    }
}
