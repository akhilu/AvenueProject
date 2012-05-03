using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Avenue.ApplicationBus;
using System.Threading;

namespace Avenue.Integration.EndPoint
{
    public class EndPoint<T> : IEndPoint where T : ApplicationBus.Command
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

            _pool = new Semaphore(5, 5);

            Func<Message, bool> MessageHandler = delegate(Message s)
            {
                try
                {
                    _pool.WaitOne();
                    var message = DeSerializer.Deserialize<T>(s);
                    ApplicationBus.Bus.SendCommand(message);
                    
                }
                catch (Exception ex)
                {


                }
                finally
                {
                    _pool.Release();
                }
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
