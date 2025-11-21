// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
#region Example
// This example shows how change the update rate of multiple existing subscriptions.
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
    class ChangeMultipleItemSubscriptions
    {
        public static void Main1()
        {
            // Instantiate the client object.
            using (var client = new EasyDAClient())
            {
                client.ItemChanged += client_ItemChanged;

                Console.WriteLine("Subscribing...");
                int[] handleArray = client.SubscribeMultipleItems(new[]
                {
                    new DAItemGroupArguments("", "OPCLabs.KitServer.2", "Simulation.Random", 1000, null),
                    new DAItemGroupArguments("", "OPCLabs.KitServer.2", "Trends.Ramp (1 min)", 1000, null),
                    new DAItemGroupArguments("", "OPCLabs.KitServer.2", "Trends.Sine (1 min)", 1000, null),
                    new DAItemGroupArguments("", "OPCLabs.KitServer.2", "Simulation.Register_I4", 1000, null)
                });

                Console.WriteLine("Waiting for 10 seconds...");
                Thread.Sleep(10 * 1000);

                Console.WriteLine("Changing subscriptions...");
                client.ChangeMultipleItemSubscriptions(new[]
                {
                    new DAHandleGroupArguments(handleArray[0], 100),
                    new DAHandleGroupArguments(handleArray[1], 100)
                });

                Console.WriteLine("Waiting for 10 seconds...");
                Thread.Sleep(10 * 1000);

                Console.WriteLine("Unsubscribing...");
                client.UnsubscribeAllItems();

                Console.WriteLine("Waiting for 10 seconds...");
                Thread.Sleep(10 * 1000);
            }
        }

        // Item changed event handler
        static void client_ItemChanged(object sender, EasyDAItemChangedEventArgs e)
        {
            if (e.Succeeded)
                Console.WriteLine(e.Vtq);
            else
                Console.WriteLine($"*** Failure: {e.ErrorMessageBrief}");
        }
    }
}
#endregion
