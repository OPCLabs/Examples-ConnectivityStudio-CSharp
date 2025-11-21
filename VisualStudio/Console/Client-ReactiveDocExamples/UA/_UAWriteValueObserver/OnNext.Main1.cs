// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable PossibleNullReferenceException
#region Example
// Shows how to create an observer that writes values to OPC-UA node, and subscribe it to a generated sequence of values.
// Requires Microsoft Reactive Extensions (Rx).
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Reactive.Linq;
using System.Threading;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.Reactive;

namespace ReactiveDocExamples
{
    namespace _UAWriteValueObserver
    {
        class OnNext
        {
            public static void Main1()
            {
                // Define which server we will work with.
                UAEndpointDescriptor endpointDescriptor =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
                // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
                // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

                Console.WriteLine("Creating source observable, 0..9 in 1 second intervals...");
                IObservable<int> source = Observable.Generate(0, i => i < 10, i => i + 1, i => i, i => TimeSpan.FromSeconds(1));

                Console.WriteLine("Creating observer to write values into OPC node...");
                UAWriteValueObserver<int> observer =
                    UAWriteValueObserver.Create<int>(
                        endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10389");

                Console.WriteLine("Monitoring changes of the target OPC node using traditional means...");
                int handle = EasyUAClient.SharedInstance.SubscribeDataChange(
                    endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10389", 
                    100, (_, e) => Console.WriteLine(e.AttributeData));

                Console.WriteLine("Subscribing the observer to source observable...");
                source.Subscribe(observer);

                Console.WriteLine("Waiting for 10 seconds...");
                Thread.Sleep(10 * 1000);

                Console.WriteLine("Finalizing monitoring...");
                EasyUAClient.SharedInstance.UnsubscribeMonitoredItem(handle);

                Console.WriteLine("Waiting for 2 seconds...");
                Thread.Sleep(2 * 1000);
            }
        }
    }
}
#endregion
