using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Avenue.Integration.Configuration;
using Avenue.Integration.EndPoint;
using Avenue.Integration.Queue.TimeEcho;
using NUnit.Framework;

namespace Avenue.Integration.Tests.EndPoint
{
    [TestFixture]
    public class EndPointTests
    {
        private ServiceBusHost serviceHost;
        [SetUp]
        public void Setup()
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

                
            });

            serviceHost = ServiceBusFactory.CreateHost(configuration);
        }


        [Test]
        public void StartStop_ShouldStartStopStopEndPoint()
        {
            serviceHost.Start();

            System.Threading.Thread.Sleep(2000);
            Assert.IsTrue(TimeMessageHandler.Called);
            serviceHost.Stop();
            
            System.Threading.Thread.Sleep(2000);
            TimeMessageHandler.Called = false;
            System.Threading.Thread.Sleep(2000);
            Assert.IsFalse(TimeMessageHandler.Called);

            
        }


    }
}
