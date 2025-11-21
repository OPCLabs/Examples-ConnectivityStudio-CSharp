// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
#region Example
// This example shows how to subscribe to events and display the event message with each notification. It also shows how to 
// unsubscribe afterwards.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Threading;
using OpcLabs.EasyOpc.AlarmsAndEvents;
using OpcLabs.EasyOpc.AlarmsAndEvents.OperationModel;

namespace DocExamples.AlarmsAndEvents._EasyAEClient
{
    partial class SubscribeEvents
    {
        public static void Main1()
        {
            // Instantiate the client object.
            using (var client = new EasyAEClient())
            {
                var eventHandler = new EasyAENotificationEventHandler(client_Notification);
                client.Notification += eventHandler;

                int handle = client.SubscribeEvents("", "OPCLabs.KitEventServer.2", 1000);

                Console.WriteLine("Processing event notifications for 1 minute...");
                Thread.Sleep(60 * 1000);

                client.UnsubscribeEvents(handle);
            }
        }

        // Notification event handler
        static void client_Notification(object sender, EasyAENotificationEventArgs e)
        {
            if (!e.Succeeded)
            {
                Console.WriteLine("*** Failure: {0}", e.ErrorMessageBrief);
                return;
            }
            if (!(e.EventData is null))
                Console.WriteLine(e.EventData.Message);
        }
    }
}
#endregion
