using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Avenue.Integration.EndPoint
{
    interface IEndPoint
    {
        IQueueClient QueueClient { get; set; }

        string ConnectionString { get; set; }

        IQueueMessageDeSerializer DeSerializer { get; set; }

        void Start();
        void Stop();
    }

    interface IEndPoint<T> : IEndPoint where T : ApplicationBus.Command
    {
       
    }
}
