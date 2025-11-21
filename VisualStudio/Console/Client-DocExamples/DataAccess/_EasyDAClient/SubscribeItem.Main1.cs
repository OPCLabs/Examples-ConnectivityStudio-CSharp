// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
#region Example
// Hooking up events and receiving OPC item changes.
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
    partial class SubscribeItem
    {
        public static void Main1()
        {
            // Instantiate the client object.
            using (var client = new EasyDAClient())
            {
                var eventHandler = new EasyDAItemChangedEventHandler(client_ItemChanged);
                client.ItemChanged += eventHandler;

                Console.WriteLine("Subscribing item...");
                client.SubscribeItem("", "OPCLabs.KitServer.2", "Demo.Ramp", 200);

                Console.WriteLine("Process item change notifications for 30 seconds...");
                Thread.Sleep(30 * 1000);

                Console.WriteLine("Unsubscribing all items...");
                client.UnsubscribeAllItems();

                client.ItemChanged -= eventHandler;
            }

            Console.WriteLine("Finished.");
        }

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
