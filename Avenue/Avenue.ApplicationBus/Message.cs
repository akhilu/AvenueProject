using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Avenue.ApplicationBus
{

    public class Message
    {
        public Message()
        {
            LocalId = Guid.NewGuid();
            TimeRecieved = DateTime.Now;
        }
        public DateTime TimeRecieved { get; set; }
        public Guid LocalId { get; set; }
        public string Body { get; set; }

    }
    
}
