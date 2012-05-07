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
            // Create the dashboard
        }

        internal void AddEndPoint(EndPoint.IEndPoint endPoint)
        {
            endPoints.Add(endPoint);
        }
        
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
            }

        }

        private void StopEndPoints()
        {
            foreach (var endpoint in endPoints)
            {
                endpoint.Stop();
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
