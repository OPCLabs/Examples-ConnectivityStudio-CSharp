// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to subscribe to range of values from an array.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UACommonDocExamples._UAIndexRangeList
{
    partial class Usage
    {
        public static void Subscribe()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client object
            var client = new EasyUAClient();

            Console.WriteLine("Subscribing to range...");
            var attributeArguments = new UAAttributeArguments(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10933")
                {
                    IndexRangeList = UAIndexRangeList.OneDimension(2, 4)
                };
            var monitoredItemArguments = new UAMonitoredItemArguments(attributeArguments, monitoringParameters:1000);
            // The callback is a lambda expression the displays the value
            client.SubscribeMonitoredItem(monitoredItemArguments,
                (sender, eventArgs) =>
                {
                    if (eventArgs.Succeeded)
                    {
                        var arrayValue = eventArgs.AttributeData.Value as Int32[];
                        if (!(arrayValue is null))
                            Console.WriteLine($"Value: {{{String.Join(",", arrayValue)}}}");
                    }
                    else
                        Console.WriteLine($"*** Failure: {eventArgs.ErrorMessageBrief}");
                });

            Console.WriteLine("Processing data change events for 10 seconds...");
            System.Threading.Thread.Sleep(10 * 1000);

            Console.WriteLine("Unsubscribing...");
            client.UnsubscribeAllMonitoredItems();

            Console.WriteLine("Waiting for 2 seconds...");
            System.Threading.Thread.Sleep(2 * 1000);
        }
    }
}
#endregion
