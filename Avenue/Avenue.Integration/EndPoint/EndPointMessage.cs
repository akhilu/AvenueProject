using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Avenue.Integration.EndPoint
{
    public class EndPointMessage
    {
        public EndPointMessage()
        {
            LocalId = Guid.NewGuid();
            TimeRecieved = DateTime.Now;
        }
        public DateTime TimeRecieved { get; set; }
        public Guid LocalId { get; set; }
        public string Body { get; set; }
    }
}
