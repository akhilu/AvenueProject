using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Avenue.Integration
{
    public class ServiceBusHost
    {
        //private ServiceBus.Admin.Host host = new ServiceBus.Admin.Host();
        private List<EndPoint.IEndPoint> endPoints = new List<EndPoint.IEndPoint>();

        public ServiceBusHost()
        {
            //foreach (var item in serviceBusConfiguration.MonitorConfigurations)
            //    BuildQueueMonitor(item);         


            // Create the dashboard

        }

        internal void AddEndPoint(EndPoint.IEndPoint endPoint)
        {
            //verify

            // make sure onely one end point to location
            endPoints.Add(endPoint);

        }

        //private void BuildQueueMonitor(MonitorConfiguration config)
        //{
        //   // QueueCordinator.Instance.AddQueueMonitor(config.BuildMonitor());

        //    var monitor = new QueueMonitor();



        //    monitor.ConnectionString = config.ConnectionString;
        //    monitor.QueueClient = config.QueueClient;
        //    monitor.MessageHandler = config.Handler;

        //  //  monitor.QueueClient.Configure(new Func<Message, bool>(T => config.Handler.Handle(T)), config.ConnectionString);



        //    QueueCordinator.Instance.AddQueueMonitor(monitor);
        //}

        public void Start()
        {
            try
            {
                Console.WriteLine("Starting Queue Corrdinator");
                StartEndPoints();

                // host.Start();
                Console.WriteLine("Listening");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        private void StartEndPoints()
        {
            foreach (var endpoint in endPoints)
            {
                endpoint.Start();
                //start end point
                //   endpoint.

            }

        }

        private void StopEndPoints()
        {

            foreach (var endpoint in endPoints)
            {
                endpoint.Stop();
                //start end point
                //   endpoint.

            }
        }

        public void Stop()
        {
            Console.WriteLine("Stoppint Queue Corrdinator");
            StopEndPoints();

       //     host.Stop();
        }
    }
}
