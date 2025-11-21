// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable LocalizableElement
// ReSharper disable InconsistentNaming
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to subscribe to changes of all data variables under a specified object in OPC UA address space.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Linq;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.AddressSpace;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples._EasyUAClient
{
    partial class SubscribeMultipleMonitoredItems
    {
        public static void AllInObject()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"
            
            // Instantiate the client object and hook events
            var client = new EasyUAClient();
            client.DataChangeNotification += client_DataChangeNotification_AllInObject;

            // Obtain variables under "Scalar" node
            Console.WriteLine("Browsing...");
            UANodeElementCollection nodeElementCollection;
            try
            {
                nodeElementCollection = client.BrowseDataVariables(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;ns=2;i=10787");
            }
            catch (UAException uaException)
            {
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
                return;
            }

            // Create array with monitored item arguments
            EasyUAMonitoredItemArguments[] monitoredItemArgumentsArray = nodeElementCollection
                .Select(element => new EasyUAMonitoredItemArguments(null, endpointDescriptor, element))
                .ToArray();

            Console.WriteLine("Subscribing...");
            client.SubscribeMultipleMonitoredItems(monitoredItemArgumentsArray);

            Console.WriteLine("Processing monitored item changed events for 20 seconds...");
            System.Threading.Thread.Sleep(20 * 1000);

            Console.WriteLine("Unsubscribing...");
            client.UnsubscribeAllMonitoredItems();

            Console.WriteLine("Waiting for 5 seconds...");
            System.Threading.Thread.Sleep(5 * 1000);
        }

        static void client_DataChangeNotification_AllInObject(object sender, EasyUADataChangeNotificationEventArgs e)
        {
            // Display value
            if (e.Succeeded)
                Console.WriteLine("{0}: {1}", e.Arguments.NodeDescriptor, e.AttributeData.Value);
            else
                Console.WriteLine("{0} *** Failure: {1}", e.Arguments.NodeDescriptor, e.ErrorMessageBrief);
        }
    }
}
#endregion
