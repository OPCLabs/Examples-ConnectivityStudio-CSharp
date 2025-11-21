// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable LocalizableElement
// ReSharper disable InconsistentNaming
// ReSharper disable PossibleNullReferenceException
// ReSharper disable ConvertIfStatementToConditionalTernaryExpression
#region Example
// This example shows how to subscribe to changes of multiple monitored items and use a data change filter.
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
    partial class SubscribeMultipleMonitoredItems
    {
        public static void Filter()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client object and hook events.
            var client = new EasyUAClient();
            client.DataChangeNotification += client_DataChangeNotification_Filter;

            Console.WriteLine("Subscribing...");
            // Report a notification if either the StatusCode or the value change. 
            // The UADataChangeTrigger has an implicit conversion to UADataChangeFilter and can thus be used in its place.
            client.SubscribeMultipleMonitoredItems(new[]
                {
                    new EasyUAMonitoredItemArguments(null, endpointDescriptor, 
                        "nsu=http://test.org/UA/Data/ ;i=10845", 
                        new UAMonitoringParameters(1000, UADataChangeTrigger.StatusValue)),
                    new EasyUAMonitoredItemArguments(null, endpointDescriptor, 
                        "nsu=http://test.org/UA/Data/ ;i=10853", 
                        new UAMonitoringParameters(1000, UADataChangeTrigger.StatusValue)),
                    new EasyUAMonitoredItemArguments(null, endpointDescriptor, 
                        "nsu=http://test.org/UA/Data/ ;i=10855", 
                        new UAMonitoringParameters(1000, UADataChangeTrigger.StatusValue))
                });

            Console.WriteLine("Processing monitored item changed events for 10 seconds...");
            System.Threading.Thread.Sleep(10 * 1000);

            Console.WriteLine("Unsubscribing...");
            client.UnsubscribeAllMonitoredItems();

            Console.WriteLine("Waiting for 5 seconds...");
            System.Threading.Thread.Sleep(5 * 1000);

            Console.WriteLine("Finished.");
        }

        static void client_DataChangeNotification_Filter(object sender, EasyUADataChangeNotificationEventArgs e)
        {
            // Display value.
            if (e.Succeeded)
                Console.WriteLine($"{e.Arguments.NodeDescriptor}: {e.AttributeData.Value}");
            else
                Console.WriteLine($"{e.Arguments.NodeDescriptor} *** Failure: {e.ErrorMessageBrief}");
        }
    }
}
#endregion
