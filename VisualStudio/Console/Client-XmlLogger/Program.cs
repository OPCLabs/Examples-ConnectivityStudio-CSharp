// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

#region Example
// Logs OPC Data Access item changes into an XML file.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;
using OpcLabs.BaseLib.Runtime.InteropServices;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.OperationModel;

namespace XmlLogger
{
    class Program
    {
        static void Main()
        {
            ComManagement.Instance.AssureSecurityInitialization();

            Console.WriteLine("Starting up...");
            var xmlSerializer = new XmlSerializer(typeof(EasyDAItemChangedEventArgs));
            var xmlWriter = XmlWriter.Create("OpcData.xml", new XmlWriterSettings
                                        {
                                            Indent = true,
                                            CloseOutput = true
                                        });
            // The root element can have any name you need, but the name below also allows reading the log back as .NET array
            xmlWriter.WriteStartElement("ArrayOfEasyDAItemChangedEventArgs");

            Console.WriteLine("Logging data for 30 seconds...");
            int handle = EasyDAClient.SharedInstance.SubscribeItem("", "OPCLabs.KitServer.2", 
                "Simulation.Incrementing (1 s)", 100,
               (_, eventArgs) =>
                   {
                       Debug.Assert(!(eventArgs is null));
                       Console.Write(".");
                       xmlSerializer.Serialize(xmlWriter, eventArgs);
                   });
            System.Threading.Thread.Sleep(30 * 1000);

            Console.WriteLine();
            Console.WriteLine("Shutting down...");
            EasyDAClient.SharedInstance.UnsubscribeItem(handle);
            xmlWriter.WriteEndElement();    // not really necessary - XmlWriter would write the end tag for us anyway
            xmlWriter.Close();

            Console.WriteLine("Finished.");
        }
    }
}
#endregion
