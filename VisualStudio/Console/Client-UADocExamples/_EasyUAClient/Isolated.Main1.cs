// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to create and use two isolated client objects, resulting in two separate connections to the target
// OPC UA server.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Diagnostics;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples._EasyUAClient
{
    class Isolated
    {
        public static void Main1()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client objects and make them isolated
            var client1 = new EasyUAClient { Isolated = true };
            var client2 = new EasyUAClient { Isolated = true };

            // The callback is a local method the displays the value
            void DataChangeCallback(object sender, EasyUADataChangeNotificationEventArgs eventArgs)
            {
                Debug.Assert(!(eventArgs is null));

                string displayPrefix = $"[{eventArgs.Arguments.State}]";
                if (eventArgs.Succeeded)
                {
                    Debug.Assert(!(eventArgs.AttributeData is null));
                    Console.WriteLine($"{displayPrefix} {eventArgs.AttributeData}");
                }
                else
                    Console.WriteLine($"{displayPrefix} *** Failure: {eventArgs.ErrorMessageBrief}");
            }

            Console.WriteLine("Subscribing...");
            client1.SubscribeDataChange(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10853", 1000, 
                DataChangeCallback, state: 1);
            client2.SubscribeDataChange(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10853", 1000,
                DataChangeCallback, state: 2);

            Console.WriteLine("Processing data change events for 10 seconds...");
            System.Threading.Thread.Sleep(10 * 1000);

            Console.WriteLine("Unsubscribing...");
            client1.UnsubscribeAllMonitoredItems();
            client2.UnsubscribeAllMonitoredItems();

            Console.WriteLine("Waiting for 2 seconds...");
            System.Threading.Thread.Sleep(2 * 1000);
        }
    }
}
#endregion
