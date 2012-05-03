using System;
using System.Xml.Serialization;

namespace Avenue.Integration.Sample
{

        [XmlRoot("Message")]
        public class TimeMessage : Avenue.ApplicationBus.Command
        {
            [XmlElement("Time")]
            public DateTime Time { get; set; }

            [XmlElement("Text")]
            public String Test { get; set; }
        }



}
