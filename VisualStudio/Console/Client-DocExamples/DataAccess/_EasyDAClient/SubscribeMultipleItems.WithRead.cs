// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
#region Example
// This example shows how to read items while subscribed.
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
    partial class SubscribeMultipleItems
    {
        public static void WithRead()
        {
            // Instantiate the client object.
            using (var client = new EasyDAClient())
            {
                client.ItemChanged += client_Read_ItemChanged;

                Console.WriteLine("Subscribing item changes...");
                client.SubscribeMultipleItems(
                    new[] {
                            new DAItemGroupArguments("", "OPCLabs.KitServer.2", "Simulation.Random", 1000, null), 
                            new DAItemGroupArguments("", "OPCLabs.KitServer.2", "Trends.Sine (1 min)", 1000, null),  
                        });

                Console.WriteLine("Waiting for 10 seconds...");
                Thread.Sleep(10 * 1000);

                Console.WriteLine("Reading items...");
                DAVtqResult[] vtqResults = client.ReadMultipleItems("OPCLabs.KitServer.2",
                    new DAItemDescriptor[]
                    {
                        "Trends.Ramp (1 min)", "Trends.Sine (1 min)"
                    });

                for (int i = 0; i < vtqResults.Length; i++)
                {
                    Debug.Assert(vtqResults[i] != null);

                    if (vtqResults[i].Succeeded)
                        Console.WriteLine("vtqResults[{0}].Vtq: {1}", i, vtqResults[i].Vtq);
                    else
                        Console.WriteLine("vtqResults[{0}] *** Failure: {1}", i, vtqResults[i].ErrorMessageBrief);
                }

                Console.WriteLine("Waiting for 10 seconds...");
                Thread.Sleep(10 * 1000);

                Console.WriteLine("Unsubscribing item changes...");
            }

            Console.WriteLine("Finished.");
        }

        // Item changed event handler
        static void client_Read_ItemChanged(object sender, EasyDAItemChangedEventArgs e)
        {
            if (e.Succeeded)
                Console.WriteLine($"< {e.Arguments.ItemDescriptor.ItemId}: {e.Vtq}");
            else
                Console.WriteLine($"< {e.Arguments.ItemDescriptor.ItemId} *** Failure: {e.ErrorMessageBrief}");
        }
    }
}
#endregion
