using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Avenue.Integration.Configuration;
using Avenue.Integration;
using Avenue.Integration.Queue.TimeEcho;
using Topshelf;


namespace Avenue.Integration.Sample
{
    class Program
    {
        static void Main(string[] args)
        {

            var configuration = ServiceBusFactory.New(x =>
            {
                //x.UserServiceLocator(locator);
                //x.LogTo<String>();
                //x.UseDataprovider<Type>();
                //x.what else

                x.AddEndPoint(e =>
                {
                    e.UseQueueClient<TimeEcoQueueClient>("End Point 1");
                    e.DeSerializeTo<TimeMessage>();
                    e.UseSerializer<EndPoint.XmlDeserializer>();
                    // e.UseObjectRouter<String>();
                });

                x.AddEndPoint(e =>
                {
                    e.UseQueueClient<TimeEcoQueueClient>("End Point 2");
                    e.DeSerializeTo<TimeMessage>();
                    e.UseSerializer<EndPoint.XmlDeserializer>();
                    // e.UseObjectRouter<String>();
                });

                x.AddEndPoint(e =>
                {
                    e.UseQueueClient<TimeEcoQueueClient>("End Point 3");
                    e.DeSerializeTo<TimeMessage>();
                    e.UseSerializer<EndPoint.XmlDeserializer>();
                    // e.UseObjectRouter<String>();
                });

                x.AddEndPoint(e =>
                {
                    e.UseQueueClient<TimeEcoQueueClient>("End Point 4");
                    e.DeSerializeTo<TimeMessage>();
                    e.UseSerializer<EndPoint.XmlDeserializer>();
                    // e.UseObjectRouter<String>();
                });

                x.AddEndPoint(e =>
                {
                    e.UseQueueClient<TimeEcoQueueClient>("End Point 5");
                    e.DeSerializeTo<TimeMessage>();
                    e.UseSerializer<EndPoint.XmlDeserializer>();
                    // e.UseObjectRouter<String>();
                });

                x.AddEndPoint(e =>
                {
                    e.UseQueueClient<TimeEcoQueueClient>("End Point 6");
                    e.DeSerializeTo<TimeMessage>();
                    e.UseSerializer<EndPoint.XmlDeserializer>();
                    // e.UseObjectRouter<String>();
                });
            });

            ApplicationBus.Bus.RegisterHandlerForCommand<TimeMessage, TimeHandler>();

            var host = HostFactory.New(x =>
            {
                x.Service<ServiceBusHost>(s =>
                {
                    s.SetServiceName("Service Bus Host");

                    s.ConstructUsing(name => ServiceBusFactory.CreateHost(configuration));
                    s.WhenStarted(svc => svc.Start());
                    s.WhenStopped(svc => svc.Stop());
                });
                x.RunAsLocalSystem();
                x.SetDescription("Service Bus Host");
                x.SetDisplayName("Service Bus Host");
                x.SetServiceName("Service Bus Host");
            });



            host.Run();

        }


    }

}
