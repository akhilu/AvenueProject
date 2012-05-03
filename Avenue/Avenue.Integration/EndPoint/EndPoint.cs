using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Avenue.ApplicationBus;

namespace Avenue.Integration.EndPoint
{
    public class EndPoint<T> : IEndPoint where T : ApplicationBus.Command
    {
        public EndPoint()
        {
            InstanceId = Guid.NewGuid();
            CurrentStatus = Status.Stopped;
        }

        public enum Status
        {
            Running,
            Stopping,
            Stopped
        }

        public Guid InstanceId { get; private set; }
        public IQueueClient QueueClient { get; set; }
        public string ConnectionString { get; set; }
        public IQueueMessageDeSerializer DeSerializer { get; set; }


        public Status CurrentStatus { get; private set; }

        public void Start()
        {
            Console.WriteLine("Starting InstanceId {0}", InstanceId);

            Func<Message, bool> MessageHandler = delegate(Message s)
            {
                var message = DeSerializer.Deserialize<T>(s);
                ApplicationBus.Bus.SendCommand(message);
                return true;
            };

            QueueClient.Configure(MessageHandler, ConnectionString);
            QueueClient.Start();
            CurrentStatus = Status.Running;
        }





        public void Stop()
        {
            Console.WriteLine("Stoppint InstanceId {0}", InstanceId);
            CurrentStatus = Status.Stopping;

            CurrentStatus = Status.Stopped;
            Console.WriteLine("Stopped InstanceId {0}", InstanceId);
        }


        public object SomeMethod(object value)
        {
            throw new NotImplementedException();
        }
    }
}
