using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Avenue.Integration.Sample
{
    class TimeHandler : Avenue.ApplicationBus.HandlesCommand<TimeMessage>
    {
        public void Handle(TimeMessage message)
        {
            System.Threading.Thread.Sleep(200);
            System.Diagnostics.Debug.WriteLine(message.Time.ToLongTimeString());
            System.Diagnostics.Debug.WriteLine(message.Test + "Running on thread " + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString());
            Console.WriteLine(message.Time.ToLongTimeString());
            Console.WriteLine(message.Test + "Running on thread " + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString());
        }
    }
}
