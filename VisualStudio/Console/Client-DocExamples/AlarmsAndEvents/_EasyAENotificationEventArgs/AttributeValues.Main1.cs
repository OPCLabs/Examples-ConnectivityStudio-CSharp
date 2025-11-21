// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

#pragma warning disable IDE1006 // Naming Styles
// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
#region Example
// This example shows how to subscribe to events with specified event attributes, and obtain the attribute values in event 
// notifications.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Collections.Generic;
using System.Threading;
using OpcLabs.EasyOpc.AlarmsAndEvents;
using OpcLabs.EasyOpc.AlarmsAndEvents.OperationModel;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.OperationModel;

namespace DocExamples.AlarmsAndEvents._EasyAENotificationEventArgs
{
    class AttributeValues
    {
        public static void Main1()
        {
            // Instantiate the OPC-A&E client object.
            var aeClient = new EasyAEClient();

            // Instantiate the OPC-DA client object.
            var daClient = new EasyDAClient();

            var eventHandler = new EasyAENotificationEventHandler(aeClient_Notification);
            aeClient.Notification += eventHandler;

            // Inactivate the event condition (we will later activate it and receive the notification)
            daClient.WriteItemValue("", "OPCLabs.KitServer.2", "SimulateEvents.ConditionState1.Inactivate", true);

            var subscriptionFilter = new AESubscriptionFilter
            {
                Sources = new AENodeDescriptor[] { "Simulation.ConditionState1" }
            };

            // Prepare a dictionary holding requested event attributes for each event category
            // The event category IDs and event attribute IDs are hard-coded here, but can be obtained from the OPC 
            // server by querying as well.
            var returnedAttributesByCategory = new AEAttributeSetDictionary
            {
                [0x00ECFF02] = new long[] {0x00EB0003, 0x00EB0008}
            };

            Console.WriteLine("Subscribing to events...");
            int handle = aeClient.SubscribeEvents("", "OPCLabs.KitEventServer.2", 1000, null, subscriptionFilter,
                returnedAttributesByCategory);

            // Give the refresh operation time to complete
            Thread.Sleep(5 * 1000);

            // Trigger an event carrying specified attributes (activate the condition)
            try
            {
                daClient.WriteItemValue("", "OPCLabs.KitServer.2",
                    "SimulateEvents.ConditionState1.AttributeValues.15400963", 123456);
                daClient.WriteItemValue("", "OPCLabs.KitServer.2",
                    "SimulateEvents.ConditionState1.AttributeValues.15400968", "Some string value");
                daClient.WriteItemValue("", "OPCLabs.KitServer.2", "SimulateEvents.ConditionState1.Activate", true);
            }
            catch (OpcException opcException)
            {
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message);
                return;
            }

            Console.WriteLine("Processing event notifications for 10 seconds...");
            Thread.Sleep(10 * 1000);

            aeClient.UnsubscribeEvents(handle);
        }

        // Notification event handler
        static void aeClient_Notification(object sender, EasyAENotificationEventArgs e)
        {
            if (!e.Succeeded)
            {
                Console.WriteLine("*** Failure: {0}", e.ErrorMessageBrief);
                return;
            }
            if (!e.Refresh && (!(e.EventData is null)))
            {
                // Display all received event attribute IDs and their corresponding values
                Console.WriteLine("Event attribute count: {0}", e.EventData.AttributeValues.Count);
                foreach (KeyValuePair<long, object> pair in e.EventData.AttributeValues)
                    Console.WriteLine("    {0}: {1}", pair.Key, pair.Value);
            }
        }
    }
}
#endregion
