// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
#region Example
// This example shows how to filter the events by their category.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Threading;
using OpcLabs.EasyOpc.AlarmsAndEvents;
using OpcLabs.EasyOpc.AlarmsAndEvents.OperationModel;
using OpcLabs.EasyOpc.DataAccess;

namespace DocExamples.AlarmsAndEvents._EasyAEClient
{
    partial class SubscribeEvents
    {
        // Instantiate the OPC-A&E client object.
        static readonly EasyAEClient AEClient = new EasyAEClient();

        // Instantiate the OPC-DA client object.
        static readonly EasyDAClient DAClient = new EasyDAClient();

        public static void FilterByCategories()
        {
            var eventHandler = new EasyAENotificationEventHandler(AEClient_Notification_FilterByCategories);
            AEClient.Notification += eventHandler;

            Console.WriteLine("Processing event notifications...");
            var subscriptionFilter = new AESubscriptionFilter
            {
                Categories = new long[] { 15531778 }
            };
            // You can also filter using event types, severity, areas, and sources.
            int handle = AEClient.SubscribeEvents("", "OPCLabs.KitEventServer.2", 1000, null, subscriptionFilter);

            // Allow time for initial refresh
            Thread.Sleep(5 * 1000);

            // Set some events to active state.
            DAClient.WriteItemValue("", "OPCLabs.KitServer.2", "SimulateEvents.ConditionState1.Activate", true);
            DAClient.WriteItemValue("", "OPCLabs.KitServer.2", "SimulateEvents.ConditionState2.Activate", true);

            Thread.Sleep(10 * 1000);

            AEClient.UnsubscribeEvents(handle);
            AEClient.Notification -= eventHandler;
        }

        // Notification event handler
        static void AEClient_Notification_FilterByCategories(object sender, EasyAENotificationEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine(e);
            if (!e.Succeeded)
                return;

            Console.WriteLine("Refresh: {0}", e.Refresh);
            Console.WriteLine("RefreshComplete: {0}", e.RefreshComplete);
            AEEventData eventData = e.EventData;
            if (!(eventData is null))
            {
                Console.WriteLine("Event.CategoryId: {0}", eventData.CategoryId);
                Console.WriteLine("Event.QualifiedSourceName: {0}", eventData.QualifiedSourceName);
                Console.WriteLine("Event.Message: {0}", eventData.Message);
                Console.WriteLine("Event.Active: {0}", eventData.Active);
                Console.WriteLine("Event.Acknowledged: {0}", eventData.Acknowledged);
            }
        }
    }
}
#endregion
