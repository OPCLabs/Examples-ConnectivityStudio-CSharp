// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable PossibleNullReferenceException
#region Example
// Shows an OPC UA data change observable with specified timeouts.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using OpcLabs.EasyOpc.UA.Reactive;
using System;
using System.Threading;
using OpcLabs.EasyOpc.UA;

namespace ReactiveDocExamples
{
    namespace _UADataChangeNotificationObservable
    {
        partial class Subscribe
        {
            public static void Timeouts()
            {
                // Define which server we will work with.
                UAEndpointDescriptor endpointDescriptor =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
                // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
                // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

                Console.WriteLine("Creating observable...");
                UADataChangeNotificationObservable<float> observable =
                    UADataChangeNotificationObservable.Create<float>(
                        endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10853", 1000);
                // Set fairly short timeouts (failure can thus occur).
                observable.ClientSelector.Isolated = true;
                observable.ClientSelector.IsolatedParameters.SessionParameters.EndpointSelectionTimeout = 1500;
                observable.ClientSelector.IsolatedParameters.SessionParameters.SessionConnectTimeout = 3000;
                observable.ClientSelector.IsolatedParameters.SessionParameters.SessionTimeout = 3000;
                observable.ClientSelector.IsolatedParameters.SessionParameters.SessionTimeoutDebug = 3000;

                Console.WriteLine("Subscribing...");
                using (observable.Subscribe(e => Console.WriteLine(
                    (e.Exception is null) ? e.AttributeData.ToString() : e.Exception.GetBaseException().ToString())))
                {
                    Console.WriteLine("Waiting for 10 seconds...");
                    Thread.Sleep(10*1000);

                    Console.WriteLine("Unsubscribing...");
                }

                Console.WriteLine("Waiting for 2 seconds...");
                Thread.Sleep(2 * 1000);
            }
        }
    }
}
#endregion
