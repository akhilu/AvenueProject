﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Avenue.Integration.EndPoint;

using System.Timers;

namespace Avenue.Integration.Queue.TimeEcho
{
    public class TimeEcoQueueClient : IQueueClient
    {
        private Func<Avenue.Integration.EndPoint.EndPointMessage, bool> _messageSender;
        public String EchoMessage { get; set; }

        readonly Timer _timer;

        public TimeEcoQueueClient()
        {
            EchoMessage = string.Empty;
            _timer = new Timer(100) { AutoReset = true };
           // string messageBody = String.Format("<Message><Time>{0}</Time></Message>", DateTime.Now.ToString());
            _timer.Elapsed += (sender, eventArgs) => _messageSender(







                new EndPointMessage() { Body = GetMessage() }
                
                
                
                
                
                );

        }

        private string GetMessage()
        {
            var obj = new TimeMessage { Time = DateTime.Now, Test = "Hello From " + EchoMessage };

            var settings = new XmlWriterSettings
        {
            Indent = true
        };
            var xml = new StringBuilder();
            using (var writer = XmlWriter.Create(xml, settings))
            {

                //writer.WriteDocType("APIRequest", null, "https://url", null);
                //writer.WriteStartElement("APIRequest");
                //writer.WriteStartElement("Head");
                //writer.WriteElementString("Key", "123");
                //writer.WriteEndElement(); // </Head>

                var nsSerializer = new XmlSerializerNamespaces();
                nsSerializer.Add("", "");

                var xmlSerializer = new XmlSerializer(obj.GetType(), "");
                xmlSerializer.Serialize(writer, obj, nsSerializer);

            //    writer.WriteEndElement(); // </APIRequest>
            }

            return xml.ToString();
        }

        public void Start()
        {
            _timer.Start();
           // throw new NotImplementedException();
        }

        public void Stop()
        {
            Console.WriteLine("Stopping Time");
            _timer.Stop();
           // throw new NotImplementedException();
        }

        public void Configure(Func<Avenue.Integration.EndPoint.EndPointMessage, bool> recieveMessage, string connectionString)
        {
            EchoMessage = connectionString;
            _messageSender = recieveMessage;

        }
    }
}
