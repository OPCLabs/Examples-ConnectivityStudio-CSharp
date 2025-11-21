// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable PossibleNullReferenceException
#region Example
// Shows how to create an observer that writes values to OPC-DA item, and subscribe it to a generated sequence of values.
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
    namespace _DAWriteItemValueObserver
    {
        class OnNext
        {
            public static void Main1()
            {
                Console.WriteLine("Creating source observable, 0..9 in 1 second intervals...");
                IObservable<int> source = Observable.Generate(0, i => i < 10, i => i + 1, i => i, i => TimeSpan.FromSeconds(1));

                Console.WriteLine("Creating observer to write values into OPC item...");
                DAWriteItemValueObserver<int> observer =
                    DAWriteItemValueObserver.Create<int>("", "OPCLabs.KitServer.2", "Simulation.Register_I4");

                Console.WriteLine("Monitoring changes of the target OPC item using traditional means...");
                int handle = EasyDAClient.SharedInstance.SubscribeItem("", "OPCLabs.KitServer.2", "Simulation.Register_I4", 
                    100, (_, e) => Console.WriteLine(e.Vtq));

                Console.WriteLine("Subscribing the observer to source observable...");
                source.Subscribe(observer);

                Console.WriteLine("Waiting for 10 seconds...");
                Thread.Sleep(10 * 1000);

                Console.WriteLine("Finalizing monitoring...");
                EasyDAClient.SharedInstance.UnsubscribeItem(handle);

                Console.WriteLine("Waiting for 2 seconds...");
                Thread.Sleep(2 * 1000);
            }
        }
    }
}
#endregion
