// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable PossibleNullReferenceException
#region Example
// Shows how to create an observable for OPC-A&E notifications, and subscribe to it.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Threading;
using OpcLabs.EasyOpc.AlarmsAndEvents.Reactive;

namespace ReactiveDocExamples
{
    namespace _AENotificationObservable
    {
        class Subscribe
        {
            public static void Main1()
            {
                Console.WriteLine("Creating observable...");
                AENotificationObservable events =
                    AENotificationObservable.Create("", "OPCLabs.KitEventServer.2", 1000);

                Console.WriteLine("Subscribing...");
                using (events.Subscribe(e => Console.WriteLine(
                    (e.Exception is null) ? (e.EventData?.Message ?? "") : e.Exception.GetBaseException().ToString())))
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
