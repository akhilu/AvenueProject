using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Avenue.Integration.Configuration
{
    public class HostConfiguration
    {
        public HostConfiguration()
        {
            EndPointConfigurations = new List<EndPointConfiguration>();
        }
        public List<EndPointConfiguration> EndPointConfigurations { get; set; }
    }
}
