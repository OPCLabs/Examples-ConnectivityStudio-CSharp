// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable LocalizableElement
// ReSharper disable InconsistentNaming
// ReSharper disable PossibleNullReferenceException
// ReSharper disable ConvertIfStatementToConditionalTernaryExpression
#region Example
// This example shows how to store current state of the subscribed monitored items in a dictionary.
// each change.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Collections.Generic;
using System.Threading;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples._EasyUAClient
{
    partial class SubscribeMultipleMonitoredItems
    {
        public static void StoreInDictionary()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client object and hook events.
            using (var client = new EasyUAClient())
            {
                client.DataChangeNotification += client_DataChangeNotification_StoreInDictionary;

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

                Console.WriteLine("Processing monitored item changed events for 1 minute...");
                int startTickCount = Environment.TickCount;
                do
                {
                    Thread.Sleep(5 * 1000);

                    // Each 5 seconds, display the current state of the monitored items we have subscribed to.
                    lock (_serialize)
                    {
                        Console.WriteLine();
                        foreach (KeyValuePair<UANodeDescriptor, UAAttributeDataResult> pair in _attributeDataResultDictionary)
                        {
                            UANodeDescriptor nodeDescriptor = pair.Key;
                            UAAttributeDataResult attributeDataResult = pair.Value;
                            Console.WriteLine($"{nodeDescriptor}: {attributeDataResult}");
                        }

                        // The code above shows how you can process the complete contents of the dictionary. In other
                        // scenarios, you may want to access just a specific entry in the dictionary. You can achieve that
                        // by indexing the dictionary by the node descriptor of the monitored item you are interested in.
                    }
                } while (Environment.TickCount < startTickCount + 60 * 1000);

                Console.WriteLine("Unsubscribing...");
            }

            Console.WriteLine("Finished.");
        }

        static void client_DataChangeNotification_StoreInDictionary(object sender, EasyUADataChangeNotificationEventArgs e)
        {
            lock (_serialize)
                // Convert the event arguments to a UAAttributeData result object, and store it in the dictionary under the
                // key which is the node descriptor of the monitored item this data change notification is for.
                _attributeDataResultDictionary[e.Arguments.NodeDescriptor] = (UAAttributeDataResult)e;
        }

        // Holds last known state of each subscribed monitored item.
        private static readonly Dictionary<UANodeDescriptor, UAAttributeDataResult> _attributeDataResultDictionary =
            new Dictionary<UANodeDescriptor, UAAttributeDataResult>();

        // Synchronization object used to prevent simultaneous access to the dictionary.
        private static readonly object _serialize = new object();
    }
}
#endregion
