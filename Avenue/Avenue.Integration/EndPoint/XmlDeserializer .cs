using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Avenue.Integration.EndPoint
{
    public class XmlDeserializer : IQueueMessageDeSerializer
    {
        public T Deserialize<T>(Avenue.Integration.EndPoint.EndPointMessage message)
        {
            var serializer = new XmlSerializer(typeof(T));
            var stringReader = new StringReader(message.Body);
            var result = (T)serializer.Deserialize(stringReader);

            return result;
        }
    }
}
