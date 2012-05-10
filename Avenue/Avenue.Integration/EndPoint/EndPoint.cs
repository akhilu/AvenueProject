using System;
using Avenue.ApplicationBus;
using System.Threading;

namespace Avenue.Integration.EndPoint
{
    public class EndPoint<T> : IEndPoint where T : Command
    {
        private static Semaphore _pool;

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

            _pool = new Semaphore(1, 1);

            Func<Avenue.Integration.EndPoint.EndPointMessage, bool> messageHandler = delegate(Avenue.Integration.EndPoint.EndPointMessage s)
            {
                try
                {
                    _pool.WaitOne();
                    var message = DeSerializer.Deserialize<T>(s);
                    message.LocalId = s.LocalId;
                    Bus.SendCommand(message);
                    
                }
                finally
                {
                    _pool.Release();
                }
                return true;
            };

            QueueClient.Configure(messageHandler, ConnectionString);
            QueueClient.Start();
            CurrentStatus = Status.Running;
        }

        public void Stop()
        {
            Console.WriteLine("Stoppint InstanceId {0}", InstanceId);
            CurrentStatus = Status.Stopping;
            QueueClient.Stop();
            CurrentStatus = Status.Stopped;
            Console.WriteLine("Stopped InstanceId {0}", InstanceId);
        }

    }
}
