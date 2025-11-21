// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

#region Example
// This example shows how to subscribe to changes of a single monitored item, pull events, and display each change.
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
    class PullDataChangeNotification
    {
        public static void Main1()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client object
            // In order to use event pull, you must set a non-zero queue capacity upfront.
            var client = new EasyUAClient { PullDataChangeNotificationQueueCapacity = 1000 };

            Console.WriteLine("Subscribing...");
            client.SubscribeDataChange(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10853", 1000);

            Console.WriteLine("Processing data change events for 1 minute...");
            int endTick = Environment.TickCount + 60 * 1000;
            do
            {
                EasyUADataChangeNotificationEventArgs eventArgs = client.PullDataChangeNotification(2 * 1000);
                if (!(eventArgs is null))
                    // Handle the notification event
                    Console.WriteLine(eventArgs);
            } while (Environment.TickCount < endTick);

            Console.WriteLine("Unsubscribing...");
            client.UnsubscribeAllMonitoredItems();
        }
    }
}
#endregion
