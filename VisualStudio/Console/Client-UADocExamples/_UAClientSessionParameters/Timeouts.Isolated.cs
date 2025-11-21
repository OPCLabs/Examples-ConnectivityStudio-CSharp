// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to set fairly short OPC UA timeouts (failures can thus occur).
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;

namespace UADocExamples._UAClientSessionParameters
{
    class Timeouts
    {
        public static void Isolated()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client object and set timeouts
            var client = new EasyUAClient
            {
                Isolated = true,
                IsolatedParameters =
                {
                    SessionParameters =
                    {
                        EndpointSelectionTimeout = 1500,
                        SessionConnectTimeout = 3000,
                        SessionTimeout = 3000,
                        SessionTimeoutDebug = 3000
                    }
                }
            };

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
