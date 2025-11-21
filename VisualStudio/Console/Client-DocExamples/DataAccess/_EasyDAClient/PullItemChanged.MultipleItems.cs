// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
#region Example
// This example shows how to subscribe to changes of multiple items and obtain the item changed events by pulling them.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.OperationModel;

namespace DocExamples.DataAccess._EasyDAClient
{
    partial class PullItemChanged
    {
        public static void MultipleItems()
        {
            // Instantiate the client object.
            // In order to use event pull, you must set a non-zero queue capacity upfront.
            var client = new EasyDAClient { PullItemChangedQueueCapacity = 1000 };

            Console.WriteLine("Subscribing item changes...");
            client.SubscribeMultipleItems(
                new[] {
                    new DAItemGroupArguments("", "OPCLabs.KitServer.2", "Simulation.Random", 1000, null),
                    new DAItemGroupArguments("", "OPCLabs.KitServer.2", "Trends.Ramp (1 min)", 1000, null),
                    new DAItemGroupArguments("", "OPCLabs.KitServer.2", "Trends.Sine (1 min)", 1000, null),
                    // Intentionally specifying an unknown item here, to demonstrate its behavior.
                    new DAItemGroupArguments("", "OPCLabs.KitServer.2", "SomeUnknownItem", 1000, null)
                });

            Console.WriteLine("Processing item changes for 1 minute...");
            int endTick = Environment.TickCount + 60 * 1000;
            do
            {
                EasyDAItemChangedEventArgs eventArgs = client.PullItemChanged(2 * 1000);
                if (!(eventArgs is null))
                    // Handle the notification event
                    if (eventArgs.Succeeded)
                        Console.WriteLine($"{eventArgs.Arguments.ItemDescriptor.ItemId}: {eventArgs.Vtq}");
                    else
                        Console.WriteLine($"{eventArgs.Arguments.ItemDescriptor.ItemId} *** Failure: {eventArgs.ErrorMessageBrief}");
            } while (Environment.TickCount < endTick);

            Console.WriteLine("Unsubscribing item changes...");
            client.UnsubscribeAllItems();

            Console.WriteLine("Finished.");
        }
    }
}
#endregion
