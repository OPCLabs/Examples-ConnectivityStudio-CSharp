// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to subscribe to changes of a single monitored item, and display the value of the item with each change
// using a callback method that is provided as lambda expression.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;

namespace UADocExamples._EasyUAClient
{
    partial class SubscribeDataChange
    {
        public static void CallbackLambda()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client object
            var client = new EasyUAClient();

            Console.WriteLine("Subscribing...");
            // The callback is a lambda expression the displays the value
            client.SubscribeDataChange(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10853", 1000,
                (sender, eventArgs) =>
                {
                    if (eventArgs.Succeeded)
                        Console.WriteLine("Value: {0}", eventArgs.AttributeData.Value);
                    else
                        Console.WriteLine("*** Failure: {0}", eventArgs.ErrorMessageBrief);
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
