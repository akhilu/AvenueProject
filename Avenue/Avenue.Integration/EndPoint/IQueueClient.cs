using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Avenue.Integration.EndPoint
{
    public interface IQueueClient
    {
        void Configure(Func<Avenue.Integration.EndPoint.EndPointMessage, bool> recieveMessage, string connectionString);
        void Start();
        void Stop();
    }
}
