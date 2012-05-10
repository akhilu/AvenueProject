using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Avenue.Integration.EndPoint
{
    public interface IQueueMessageDeSerializer
    {
        T Deserialize<T>(Avenue.Integration.EndPoint.EndPointMessage message);
    }
}
