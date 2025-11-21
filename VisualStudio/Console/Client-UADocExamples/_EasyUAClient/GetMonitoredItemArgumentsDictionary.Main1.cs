// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable InconsistentNaming
// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to obtain dictionary of parameters of all monitored item subscriptions.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples._EasyUAClient
{
    class GetMonitoredItemArgumentsDictionary
    {
        public static void Main1()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client object and hook events.
            var client = new EasyUAClient();
            client.DataChangeNotification += client_DataChangeNotification;

            Console.WriteLine("Subscribing...");
            client.SubscribeMultipleMonitoredItems(new[]
                {
                    new EasyUAMonitoredItemArguments(null, endpointDescriptor, 
                        "nsu=http://test.org/UA/Data/ ;i=10845", 1000),
                    new EasyUAMonitoredItemArguments(null, endpointDescriptor, 
                        "nsu=http://test.org/UA/Data/ ;i=10853", 1000),
                    new EasyUAMonitoredItemArguments(null, endpointDescriptor, 
                        "nsu=http://test.org/UA/Data/ ;i=10855", 1000)
                });

            Console.WriteLine("Getting monitored item arguments dictionary...");
            EasyUAMonitoredItemArgumentsDictionary monitoredItemArgumentsDictionary =
                client.GetMonitoredItemArgumentsDictionary();

            foreach (EasyUAMonitoredItemArguments monitoredItemArguments in monitoredItemArgumentsDictionary.Values)
            {
                Console.WriteLine();
                Console.WriteLine($"NodeDescriptor: {monitoredItemArguments.NodeDescriptor}");
                Console.WriteLine($"SamplingInterval: {monitoredItemArguments.MonitoringParameters.SamplingInterval}");
                Console.WriteLine($"PublishingInterval: {monitoredItemArguments.SubscriptionParameters.PublishingInterval}");
            }

            Console.WriteLine();
            Console.WriteLine("Waiting for 5 seconds...");
            System.Threading.Thread.Sleep(5 * 1000);

            Console.WriteLine("Unsubscribing...");
            client.UnsubscribeAllMonitoredItems();

            Console.WriteLine("Waiting for 5 seconds...");
            System.Threading.Thread.Sleep(5 * 1000);

            Console.WriteLine("Finished.");
        }

        static void client_DataChangeNotification(object sender, EasyUADataChangeNotificationEventArgs e)
        {
            // Your code would do the processing here.
        }


        // Example output:
        //
        //Subscribing...
        //Getting monitored item arguments dictionary...
        //
        //NodeDescriptor: NodeId="nsu=http://test.org/UA/Data/ ;i=10845"
        //SamplingInterval: 1000
        //PublishingInterval: 0
        //
        //NodeDescriptor: NodeId="nsu=http://test.org/UA/Data/ ;i=10853"
        //SamplingInterval: 1000
        //PublishingInterval: 0
        //
        //NodeDescriptor: NodeId="nsu=http://test.org/UA/Data/ ;i=10855"
        //SamplingInterval: 1000
        //PublishingInterval: 0
        //
        //Waiting for 5 seconds...
        //Unsubscribing...
        //Waiting for 5 seconds...
        //Finished.
    }
}
#endregion
