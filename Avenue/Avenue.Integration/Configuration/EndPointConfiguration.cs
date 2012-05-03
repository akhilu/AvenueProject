using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Avenue.Integration.Configuration
{
    public class EndPointConfiguration
    {
        public Type QueueClient { get; set; }
        public Type DeSerializeTo { get; set; }
        public Type UseSerializer { get; set; }
        public String ConnectionString { get; set; }
    }
}
