// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CommentTypo
// ReSharper disable InconsistentNaming
// ReSharper disable StringLiteralTypo
// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to subscribe to event notifications and display each incoming event.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.AddressSpace.Standard;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples.AlarmsAndConditions
{
    partial class SubscribeEvent
    {
        public static void Overload1()
        {
            // Define which server we will work with.
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:62544/Quickstarts/AlarmConditionServer";

            // Instantiate the client object and hook events.
            var client = new EasyUAClient();
            client.EventNotification += client_EventNotification;

            Console.WriteLine("Subscribing...");
            client.SubscribeEvent(endpointDescriptor, UAObjectIds.Server, 1000);

            Console.WriteLine("Processing event notifications for 30 seconds...");
            System.Threading.Thread.Sleep(30 * 1000);

            Console.WriteLine("Unsubscribing...");
            client.UnsubscribeAllMonitoredItems();

            Console.WriteLine("Waiting for 5 seconds...");
            System.Threading.Thread.Sleep(5 * 1000);

            Console.WriteLine("Finished.");
        }

        static void client_EventNotification(object sender, EasyUAEventNotificationEventArgs e)
        {
            // Display the event.
            Console.WriteLine(e);
        }



        // Example output (truncated):
        //Subscribing...
        //Processing event notifications for 30 seconds...
        //[] Success
        //[] Success; Refresh; RefreshInitiated
        //[] Success; Refresh; [EastTank] 100! {DialogConditionType} "The dialog was activated" @9/9/2021 2:22:18 PM (10 fields)
        //[] Success; Refresh; [EastTank] 100! {ExclusiveDeviationAlarmType} "The alarm is active." @9/9/2021 4:19:37 PM (10 fields)
        //[] Success; Refresh; [EastTank] 500! {NonExclusiveLevelAlarmType} "The alarm severity has increased." @9/9/2021 4:19:35 PM (10 fields)
        //[] Success; Refresh; [EastTank] 900! {TripAlarmType} "The alarm severity has increased." @9/9/2021 4:19:29 PM (10 fields)
        //[] Success; Refresh; [EastTank] 100! {TripAlarmType} "The alarm severity has increased." @9/9/2021 3:39:03 PM (10 fields)
        //[] Success; Refresh; [EastTank] 100! {TripAlarmType} "The alarm severity has increased." @9/9/2021 3:40:03 PM (10 fields)
        //[] Success; Refresh; [NorthMotor] 100! {DialogConditionType} "The dialog was activated" @9/9/2021 2:22:18 PM (10 fields)
        //[] Success; Refresh; [NorthMotor] 500! {ExclusiveDeviationAlarmType} "The alarm severity has increased." @9/9/2021 4:19:35 PM (10 fields)
        //[] Success; Refresh; [NorthMotor] 900! {NonExclusiveLevelAlarmType} "The alarm severity has increased." @9/9/2021 4:19:29 PM (10 fields)
        //[] Success; Refresh; [NorthMotor] 100! {TripAlarmType} "The alarm is active." @9/9/2021 4:19:32 PM (10 fields)
        //[] Success; Refresh; [NorthMotor] 100! {TripAlarmType} "The alarm severity has increased." @9/9/2021 3:39:08 PM (10 fields)
        //[] Success; Refresh; [NorthMotor] 100! {TripAlarmType} "The alarm severity has increased." @9/9/2021 3:40:14 PM (10 fields)
        //[] Success; Refresh; [WestTank] 100! {DialogConditionType} "The dialog was activated" @9/9/2021 2:22:18 PM (10 fields)
        //[] Success; Refresh; [WestTank] 900! {ExclusiveDeviationAlarmType} "The alarm severity has increased." @9/9/2021 4:19:29 PM (10 fields)
        //[] Success; Refresh; [WestTank] 100! {NonExclusiveLevelAlarmType} "The alarm is active." @9/9/2021 4:19:32 PM (10 fields)
        //[] Success; Refresh; [WestTank] 100! {TripAlarmType} "The alarm is active." @9/9/2021 4:19:37 PM (10 fields)
        //[] Success; Refresh; [WestTank] 100! {TripAlarmType} "The alarm severity has increased." @9/9/2021 3:38:55 PM (10 fields)
        //[] Success; Refresh; [WestTank] 100! {TripAlarmType} "The alarm severity has increased." @9/9/2021 3:39:43 PM (10 fields)
        //[] Success; Refresh; [SouthMotor] 100! {DialogConditionType} "The dialog was activated" @9/9/2021 2:22:18 PM (10 fields)
        //[] Success; Refresh; [SouthMotor] 100! {ExclusiveDeviationAlarmType} "The alarm is active." @9/9/2021 4:19:32 PM (10 fields)
        //[] Success; Refresh; [SouthMotor] 100! {NonExclusiveLevelAlarmType} "The alarm is active." @9/9/2021 4:19:37 PM (10 fields)
        //[] Success; Refresh; [SouthMotor] 500! {TripAlarmType} "The alarm severity has increased." @9/9/2021 4:19:35 PM (10 fields)
        //[] Success; Refresh; [SouthMotor] 100! {TripAlarmType} "The alarm severity has increased." @9/9/2021 3:39:51 PM (10 fields)
        //[] Success; Refresh; [SouthMotor] 100! {TripAlarmType} "The alarm severity has increased." @9/9/2021 3:38:57 PM (10 fields)
        //[] Success; Refresh; RefreshComplete
        //[] Success; [Internal] 500! {SystemEventType} "Raising Events" @9/9/2021 4:19:39 PM (10 fields)
        //[] Success; [Internal] 500! {AuditEventType} "Events Raised" @9/9/2021 4:19:39 PM (10 fields)
        //[] Success; [EastTank] 100! {TripAlarmType} "The alarm was deactivated by the system." @9/9/2021 4:19:39 PM (10 fields)
        //[] Success; [NorthMotor] 100! {NonExclusiveLevelAlarmType} "The alarm was deactivated by the system." @9/9/2021 4:19:39 PM (10 fields)
        //[] Success; [WestTank] 100! {ExclusiveDeviationAlarmType} "The alarm was deactivated by the system." @9/9/2021 4:19:39 PM (10 fields)
        //[] Success; [Internal] 500! {SystemEventType} "Raising Events" @9/9/2021 4:19:40 PM (10 fields)
        //[] Success; [Internal] 500! {AuditEventType} "Events Raised" @9/9/2021 4:19:40 PM (10 fields)
        //[] Success; [Internal] 500! {SystemEventType} "Raising Events" @9/9/2021 4:19:41 PM (10 fields)
        //[] Success; [Internal] 500! {AuditEventType} "Events Raised" @9/9/2021 4:19:41 PM (10 fields)
        //[] Success; [Internal] 500! {SystemEventType} "Raising Events" @9/9/2021 4:19:42 PM (10 fields)
        //[] Success; [Internal] 500! {AuditEventType} "Events Raised" @9/9/2021 4:19:42 PM (10 fields)
        //[] Success; [Internal] 500! {SystemEventType} "Raising Events" @9/9/2021 4:19:43 PM (10 fields)
        //[] Success; [NorthMotor] 300! {TripAlarmType} "The alarm severity has increased." @9/9/2021 4:19:43 PM (10 fields)
        //[] Success; [Internal] 500! {AuditEventType} "Events Raised" @9/9/2021 4:19:43 PM (10 fields)
        //[] Success; [WestTank] 300! {NonExclusiveLevelAlarmType} "The alarm severity has increased." @9/9/2021 4:19:43 PM (10 fields)
        //[] Success; [SouthMotor] 300! {ExclusiveDeviationAlarmType} "The alarm severity has increased." @9/9/2021 4:19:43 PM (10 fields)
        //[] Success; [Internal] 500! {SystemEventType} "Raising Events" @9/9/2021 4:19:44 PM (10 fields)
        //[] Success; [EastTank] 700! {NonExclusiveLevelAlarmType} "The alarm severity has increased." @9/9/2021 4:19:44 PM (10 fields)
        //[] Success; [Internal] 500! {AuditEventType} "Events Raised" @9/9/2021 4:19:44 PM (10 fields)
        //[] Success; [NorthMotor] 700! {ExclusiveDeviationAlarmType} "The alarm severity has increased." @9/9/2021 4:19:44 PM (10 fields)
        //[] Success; [SouthMotor] 700! {TripAlarmType} "The alarm severity has increased." @9/9/2021 4:19:44 PM (10 fields)
        //[] Success; [Internal] 500! {SystemEventType} "Raising Events" @9/9/2021 4:19:45 PM (10 fields)
        //[] Success; [EastTank] 300! {ExclusiveDeviationAlarmType} "The alarm severity has increased." @9/9/2021 4:19:45 PM (10 fields)
        //[] Success; [Internal] 500! {AuditEventType} "Events Raised" @9/9/2021 4:19:45 PM (10 fields)
        //...
    }
}
#endregion
