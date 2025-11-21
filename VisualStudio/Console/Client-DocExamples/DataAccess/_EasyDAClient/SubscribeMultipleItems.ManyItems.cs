// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
#region Example
// This example shows how subscribe to large number of items.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Threading;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.OperationModel;

namespace DocExamples.DataAccess._EasyDAClient
{
    partial class SubscribeMultipleItems
    {
        public static void ManyItems()
        {
            // Instantiate the client object.
            using (var client = new EasyDAClient())
            {
                client.ItemChanged += client_ItemChanged_ManyItems;

                const int numberOfItems = 1000;

                Console.WriteLine("Preparing arguments...");
                var argumentArray = new DAItemGroupArguments[numberOfItems];
                for (int i = 0; i < numberOfItems; i++)
                {
                    int copy = (i / 100) + 1;
                    int phase = (i % 100) + 1;
                    string itemId = String.Format("Simulation.Incrementing.Copy_{0}.Phase_{1}", copy, phase);
                    argumentArray[i] = new DAItemGroupArguments("", "OPCLabs.KitServer.2", itemId, 50, null);
                }

                Console.WriteLine("Subscribing to {0} items...", numberOfItems);
                client.SubscribeMultipleItems(argumentArray);

                Console.WriteLine("Processing item changed events for 1 minute...");
                Thread.Sleep(60 * 1000);
            }
        }

        // Item changed event handler
        static void client_ItemChanged_ManyItems(object sender, EasyDAItemChangedEventArgs e)
        {
            if (e.Succeeded)
                Console.WriteLine("{0}: {1}", e.Arguments.ItemDescriptor.ItemId, e.Vtq);
            else
                Console.WriteLine("{0} *** Failure: {1}", e.Arguments.ItemDescriptor.ItemId, e.ErrorMessageBrief);
        }
    }
}
#endregion
