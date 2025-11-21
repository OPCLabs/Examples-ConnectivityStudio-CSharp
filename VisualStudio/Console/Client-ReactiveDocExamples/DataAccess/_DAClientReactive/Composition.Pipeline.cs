// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable PossibleNullReferenceException
#region Example
// Shows how to continuously transform values of OPC-DA item, and write the results into a second item.
// Requires Microsoft Reactive Extensions (Rx).
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Reactive.Linq;
using System.Threading;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.Reactive;

namespace ReactiveDocExamples
{
    namespace _DAClientReactive
    {
        class Composition
        {
            public static void Pipeline()
            {
                Console.WriteLine("Creating source observable...");
                DAItemChangedObservable<int> source =
                    DAItemChangedObservable.Create<int>("", "OPCLabs.KitServer.2", "Simulation.Incrementing (1 s)", 100);

                Console.WriteLine("Creating processed observable (takes valid input values and multiplies them by 1000)...");
                IObservable<int> processed = source
                    .Where(e => e.Exception is null)
                    .Select(e => e.TypedVtq.TypedValue * 1000);

                Console.WriteLine("Creating observer to write values into OPC item...");
                DAWriteItemValueObserver<int> observer = 
                    DAWriteItemValueObserver.Create<int>("", "OPCLabs.KitServer.2", "Simulation.Register_I4");

                Console.WriteLine("Monitoring changes of the target OPC item using traditional means...");
                int handle = EasyDAClient.SharedInstance.SubscribeItem("", "OPCLabs.KitServer.2", "Simulation.Register_I4",
                    100, (_, e) => Console.WriteLine(e.Vtq));

                Console.WriteLine("Subscribing the observer to the processed observable...");
                using (processed.Subscribe(observer))
                {
                    Console.WriteLine("Waiting for 10 seconds...");
                    Thread.Sleep(10 * 1000);

                    Console.WriteLine("Unsubscribing the observer from the processed observable...");
                }

                Console.WriteLine("Finalizing monitoring...");
                EasyDAClient.SharedInstance.UnsubscribeItem(handle);

                Console.WriteLine("Waiting for 2 seconds...");
                Thread.Sleep(2 * 1000);
            }
        }
    }
}
#endregion
