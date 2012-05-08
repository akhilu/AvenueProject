using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Avenue.Integration.Queue.TimeEcho;
using NUnit.Framework;
using Avenue.Integration.Configuration;
using Avenue.Integration.EndPoint;

namespace Avenue.Integration.Tests.Configuration
{
    [TestFixture]
    public class ServiceBusFactoryTests
    {
        [Test]
        public void CreateHost_ShouldCreateConfig()
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
                        e.UseSerializer<XmlDeserializer>();
                        // e.UseObjectRouter<String>();
                    });

                    x.AddEndPoint(e =>
                    {
                        e.UseQueueClient<TimeEcoQueueClient>("End Point 1");
                        e.DeSerializeTo<TimeMessage>();
                        e.UseSerializer<XmlDeserializer>();
                        // e.UseObjectRouter<String>();
                    });
                });

            Assert.IsTrue(configuration.EndPointConfigurations.Count == 2);
            Assert.IsTrue(configuration.EndPointConfigurations.First().QueueClient == typeof(TimeEcoQueueClient));
            Assert.IsTrue(configuration.EndPointConfigurations.First().DeSerializeTo == typeof(TimeMessage));
            Assert.IsTrue(configuration.EndPointConfigurations.First().UseSerializer == typeof(XmlDeserializer));
        }

        [Test]
        public void CreatHost_ShouldCreateHost()
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
                    e.UseSerializer<XmlDeserializer>();
                    // e.UseObjectRouter<String>();
                });

                x.AddEndPoint(e =>
                {
                    e.UseQueueClient<TimeEcoQueueClient>("End Point 1");
                    e.DeSerializeTo<TimeMessage>();
                    e.UseSerializer<XmlDeserializer>();
                    // e.UseObjectRouter<String>();
                });
            });

            var serviceHost = ServiceBusFactory.CreateHost(configuration);

            Assert.IsNotNull(serviceHost);


        }
    }
}
