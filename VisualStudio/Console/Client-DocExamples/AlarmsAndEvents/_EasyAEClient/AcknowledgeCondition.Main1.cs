// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
#region Example
// This example shows how to acknowledge an event condition in the OPC server.
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
using OpcLabs.EasyOpc.OperationModel;

namespace DocExamples.AlarmsAndEvents._EasyAEClient
{
    class AcknowledgeCondition
    {
        // Instantiate the OPC-A&E client object.
        static readonly EasyAEClient AEClient = new EasyAEClient();

        // Instantiate the OPC-DA client object.
        static readonly EasyDAClient DAClient = new EasyDAClient();

        static volatile bool _done;

        public static void Main1()
        {
            var eventHandler = new EasyAENotificationEventHandler(AEClient_Notification);
            AEClient.Notification += eventHandler;

            Console.WriteLine("Processing event notifications for 1 minute...");
            var subscriptionFilter = new AESubscriptionFilter
            {
                Sources = new AENodeDescriptor[] { "Simulation.ConditionState1" }
            };
            int handle = AEClient.SubscribeEvents("", "OPCLabs.KitEventServer.2", 1000, null, subscriptionFilter);

            // Give the refresh operation time to complete
            Thread.Sleep(5 * 1000);

            // Trigger an acknowledgeable event
            try
            {
                DAClient.WriteItemValue("", "OPCLabs.KitServer.2", "SimulateEvents.ConditionState1.Activate", true);
            }
            catch (OpcException opcException)
            {
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message);
                return;
            }

            _done = false;
            DateTime endTime = DateTime.Now + new TimeSpan(0, 0, 5);
            while ((!_done) && (DateTime.Now < endTime))
                Thread.Sleep(1000);

            // Give some time to also receive the acknowledgement notification
            Thread.Sleep(5 * 1000);

            AEClient.UnsubscribeEvents(handle);
            AEClient.Notification -= eventHandler;
        }

        // Notification event handler
        static void AEClient_Notification(object sender, EasyAENotificationEventArgs e)
        {
            Console.WriteLine();
            if (!e.Succeeded)
            {
                Console.WriteLine("*** Failure: {0}", e.ErrorMessageBrief);
                return;
            }

            Console.WriteLine("Refresh: {0}", e.Refresh);
            Console.WriteLine("RefreshComplete: {0}", e.RefreshComplete);
            AEEventData eventData = e.EventData;
            if (!(eventData is null))
            {
                Console.WriteLine("Event.QualifiedSourceName: {0}", eventData.QualifiedSourceName);
                Console.WriteLine("Event.Message: {0}", eventData.Message);
                Console.WriteLine("Event.Active: {0}", eventData.Active);
                Console.WriteLine("Event.Acknowledged: {0}", eventData.Acknowledged);
                Console.WriteLine("Event.AcknowledgeRequired: {0}", eventData.AcknowledgeRequired);

                if (eventData.AcknowledgeRequired)
                {
                    Console.WriteLine(">>>>> ACKNOWLEDGING THIS EVENT");
                    try
                    {
                        AEClient.AcknowledgeCondition("", "OPCLabs.KitEventServer.2", "Simulation.ConditionState1", "Simulated",
                            eventData.ActiveTime, eventData.Cookie);
                    }
                    catch (OpcException opcException)
                    {
                        Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message);
                        return;
                    }
                    Console.WriteLine(">>>>> EVENT ACKNOWLEDGED");
                    _done = true;
                }
            }
        }
    }
}
#endregion
