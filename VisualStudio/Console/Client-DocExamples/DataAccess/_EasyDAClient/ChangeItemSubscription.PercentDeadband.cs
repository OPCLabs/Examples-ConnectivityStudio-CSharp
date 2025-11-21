// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how change the percent deadband of an existing subscription.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Threading;
using OpcLabs.BaseLib.ComInterop;
using OpcLabs.EasyOpc.DataAccess.OperationModel;
using OpcLabs.EasyOpc.DataAccess;

namespace DocExamples.DataAccess._EasyDAClient
{
    partial class ChangeItemSubscription
    {
        public static void PercentDeadband()
        {
            // Instantiate the client object.
            using (var client = new EasyDAClient())
            {
                client.ItemChanged += client_ItemChanged_PercentDeadband;

                Console.WriteLine("Subscribing with 10% deadband...");
                int handle = client.SubscribeItem("", "OPCLabs.KitServer.2", "Simulation.Ramp 0:100 (10 s)",
                    VarTypes.Empty, requestedUpdateRate:100, percentDeadband:10.0f, state:null);

                Console.WriteLine("Waiting for 10 seconds...");
                Thread.Sleep(10 * 1000);

                Console.WriteLine("Changing subscription to 0% deadband...");
                client.ChangeItemSubscription(handle, new DAGroupParameters(100, 0.0f));

                Console.WriteLine("Waiting for 10 seconds...");
                Thread.Sleep(10 * 1000);

                Console.WriteLine("Unsubscribing...");
                client.UnsubscribeAllItems();

                Console.WriteLine("Waiting for 10 seconds...");
                Thread.Sleep(10 * 1000);
            }
        }

        // Item changed event handler
        static void client_ItemChanged_PercentDeadband(object sender, EasyDAItemChangedEventArgs e)
        {
            if (e.Succeeded)
                Console.WriteLine(e.Vtq);
            else
                Console.WriteLine("*** Failure: {0}", e.ErrorMessageBrief);
        }
    }
}
#endregion
