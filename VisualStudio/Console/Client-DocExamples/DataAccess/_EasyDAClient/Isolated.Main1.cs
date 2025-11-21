// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
#region Example
// This example shows how to create and use two isolated client objects, resulting in two separate connections to the target
// OPC DA server.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Diagnostics;
using System.Threading;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.OperationModel;

namespace DocExamples.DataAccess._EasyDAClient
{
    class Isolated
    {
        public static void Main1()
        {
            // Instantiate the client objects and make them isolated
            var client1 = new EasyDAClient { Isolated = true };
            var client2 = new EasyDAClient { Isolated = true };

            // The callback is a local method the displays the value
            void ItemChangedCallback(object sender, EasyDAItemChangedEventArgs eventArgs)
            {
                Debug.Assert(!(eventArgs is null));

                string displayPrefix = $"[{eventArgs.Arguments.State}]";
                if (eventArgs.Succeeded)
                {
                    Debug.Assert(!(eventArgs.Vtq is null));
                    Console.WriteLine($"{displayPrefix} {eventArgs.Vtq}");
                }
                else
                    Console.WriteLine($"{displayPrefix} *** Failure: {eventArgs.ErrorMessageBrief}");
            }

            Console.WriteLine("Subscribing...");
            client1.SubscribeItem("", "OPCLabs.KitServer.2", "Simulation.Random", 1000, ItemChangedCallback, state: 1);
            client2.SubscribeItem("", "OPCLabs.KitServer.2", "Simulation.Random", 1000, ItemChangedCallback, state: 2);

            Console.WriteLine("Processing item changed events for 10 seconds...");
            Thread.Sleep(10 * 1000);

            Console.WriteLine("Unsubscribing...");
            client1.UnsubscribeAllItems();
            client2.UnsubscribeAllItems();

            Console.WriteLine("Waiting for 2 seconds...");
            Thread.Sleep(2 * 1000);
        }
    }
}
#endregion
