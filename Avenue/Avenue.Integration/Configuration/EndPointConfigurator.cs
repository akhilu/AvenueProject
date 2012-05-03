using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Avenue.Integration.Configuration
{
    public class EndPointConfigurator
    {
        public EndPointConfiguration EndPointConfiguration { get; set; }

        public EndPointConfigurator()
        {
            EndPointConfiguration = new EndPointConfiguration();
        }

        public void UseQueueClient<T>(string connectionString)
        {
            EndPointConfiguration.QueueClient = typeof(T);
            EndPointConfiguration.ConnectionString = connectionString;
        }

        public void DeSerializeTo<T>()  where T : Avenue.ApplicationBus.Message
        {
            EndPointConfiguration.DeSerializeTo = typeof(T);
        }

        public void UseSerializer<T>() where T : Avenue.Integration.EndPoint.IQueueMessageDeSerializer
        {
            EndPointConfiguration.UseSerializer = typeof(T);
        }

    }
}
