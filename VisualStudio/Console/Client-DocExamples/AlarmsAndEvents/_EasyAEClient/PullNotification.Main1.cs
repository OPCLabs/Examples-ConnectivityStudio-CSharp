// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
#region Example
// This example shows how to subscribe to events and obtain the notification events by pulling them.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.AlarmsAndEvents;
using OpcLabs.EasyOpc.AlarmsAndEvents.OperationModel;

namespace DocExamples.AlarmsAndEvents._EasyAEClient
{
    class PullNotification
    {
        public static void Main1()
        {
            // Instantiate the client object.
            // In order to use event pull, you must set a non-zero queue capacity upfront.
            using (var client = new EasyAEClient { PullNotificationQueueCapacity = 1000 })
            {
                Console.WriteLine("Subscribing events...");
                int handle = client.SubscribeEvents("", "OPCLabs.KitEventServer.2", 1000);

                Console.WriteLine("Processing event notifications for 1 minute...");
                int endTick = Environment.TickCount + 60 * 1000;
                do
                {
                    EasyAENotificationEventArgs eventArgs = client.PullNotification(2 * 1000);
                    if (!(eventArgs is null))
                        // Handle the notification event
                        Console.WriteLine(eventArgs);
                } while (Environment.TickCount < endTick);

                Console.WriteLine("Unsubscribing events...");
                client.UnsubscribeEvents(handle);

                Console.WriteLine("Finished.");
            }
        }
    }
}
#endregion
