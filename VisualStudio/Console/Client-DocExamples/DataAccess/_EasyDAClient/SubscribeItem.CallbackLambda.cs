// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
#region Example
// This example shows how subscribe to changes of a single item and display the value of the item with each change,
// using a callback method specified using lambda expression.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Diagnostics;
using System.Threading;
using OpcLabs.EasyOpc.DataAccess;

namespace DocExamples.DataAccess._EasyDAClient
{
    partial class SubscribeItem
    {
        public static void CallbackLambda()
        {
            // Instantiate the client object.
            var client = new EasyDAClient();

            Console.WriteLine("Subscribing...");
            // The callback is a lambda expression the displays the value
            client.SubscribeItem("", "OPCLabs.KitServer.2", "Simulation.Random", 1000,
                (sender, eventArgs) =>
                {
                    Debug.Assert(eventArgs != null);

                    if (eventArgs.Succeeded)
                    {
                        Debug.Assert(eventArgs.Vtq != null);
                        Console.WriteLine(eventArgs.Vtq.ToString());
                    }
                    else
                        Console.WriteLine("*** Failure: {0}", eventArgs.ErrorMessageBrief);
                });

            Console.WriteLine("Processing item changed events for 10 seconds...");
            Thread.Sleep(10 * 1000);

            Console.WriteLine("Unsubscribing...");
            client.UnsubscribeAllItems();

            Console.WriteLine("Waiting for 2 seconds...");
            Thread.Sleep(2 * 1000);
        }
    }
}
#endregion
