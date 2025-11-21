// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
#region Example
// This example shows how to store current state of the subscribed items in a dictionary.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Collections.Generic;
using System.Threading;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.OperationModel;

namespace DocExamples.DataAccess._EasyDAClient
{
    partial class SubscribeMultipleItems
    {
        public static void StoreInDictionary()
        {
            // Instantiate the client object.
            using (var client = new EasyDAClient())
            {
                client.ItemChanged += client_ItemChanged_StoreInDictionary;

                Console.WriteLine("Subscribing item changes...");
                client.SubscribeMultipleItems(
                    new[] {
                            new DAItemGroupArguments("", "OPCLabs.KitServer.2", "Simulation.Random", 1000, null), 
                            new DAItemGroupArguments("", "OPCLabs.KitServer.2", "Trends.Ramp (1 min)", 1000, null), 
                            new DAItemGroupArguments("", "OPCLabs.KitServer.2", "Trends.Sine (1 min)", 1000, null),  
                            new DAItemGroupArguments("", "OPCLabs.KitServer.2", "Simulation.Register_I4", 1000, null)
                        });

                Console.WriteLine("Processing item changed events for 1 minute...");
                int startTickCount = Environment.TickCount;
                do
                {
                    Thread.Sleep(5*1000);

                    // Each 5 seconds, display the current state of the items we have subscribed to.
                    lock (_serialize)
                    {
                        Console.WriteLine();
                        foreach (KeyValuePair<DAItemDescriptor, DAVtqResult> pair in _vtqResultDictionary)
                        {
                            DAItemDescriptor itemDescriptor = pair.Key;
                            DAVtqResult vtqResult = pair.Value;
                            Console.WriteLine($"{itemDescriptor}: {vtqResult}");
                        }

                        // The code above shows how you can process the complete contents of the dictionary. In other
                        // scenarios, you may want to access just a specific entry in the dictionary. You can achieve that
                        // by indexing the dictionary by the item descriptor of the item you are interested in.
                    }
                } while (Environment.TickCount < startTickCount + 60*1000);

                Console.WriteLine("Unsubscribing item changes...");
            }

            Console.WriteLine("Finished.");
        }

        // Item changed event handler
        static void client_ItemChanged_StoreInDictionary(object sender, EasyDAItemChangedEventArgs e)
        {
            lock (_serialize)
                // Convert the event arguments to a DAVtq result object, and store it in the dictionary under the key which
                // is the item descriptor of the item this item changed event is for.
                _vtqResultDictionary[e.Arguments.ItemDescriptor] = (DAVtqResult)e;
        }

        // Holds last known state of each subscribed item.
        private static readonly Dictionary<DAItemDescriptor, DAVtqResult> _vtqResultDictionary = 
            new Dictionary<DAItemDescriptor, DAVtqResult>();

        // Synchronization object used to prevent simultaneous access to the dictionary.
        private static readonly object _serialize = new object();
    }
}
#endregion
