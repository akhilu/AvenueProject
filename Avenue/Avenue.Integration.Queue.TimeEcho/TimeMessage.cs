using System;
using System.Xml.Serialization;

namespace Avenue.Integration.Queue.TimeEcho
{

        [XmlRoot("Message")]
        public class TimeMessage
        {
            [XmlElement("Time")]
            public DateTime Time { get; set; }

            [XmlElement("Text")]
            public String Test { get; set; }
        }

}
