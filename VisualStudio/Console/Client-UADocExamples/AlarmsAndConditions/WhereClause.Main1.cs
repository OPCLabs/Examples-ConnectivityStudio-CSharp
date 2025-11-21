// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable LocalizableElement
// ReSharper disable InconsistentNaming
// ReSharper disable PossibleNullReferenceException
// ReSharper disable StringLiteralTypo
#region Example
// This example shows how to specify criteria for event notifications.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.AddressSpace;
using OpcLabs.EasyOpc.UA.AddressSpace.Standard;
using OpcLabs.EasyOpc.UA.AlarmsAndConditions;
using OpcLabs.EasyOpc.UA.Filtering;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples.AlarmsAndConditions
{
    class WhereClause
    {
        public static void Main1()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:62544/Quickstarts/AlarmConditionServer";

            // Instantiate the client object and hook events.
            var client = new EasyUAClient();
            client.EventNotification += client_EventNotification;

            Console.WriteLine("Subscribing...");
            client.SubscribeEvent(
                endpointDescriptor,
                UAObjectIds.Server,
                1000,
                new UAEventFilterBuilder(
                    // Either the severity is >= 500, or the event comes from a specified source node
                    UAFilterElements.Or(
                        UAFilterElements.GreaterThanOrEqual(UABaseEventObject.Operands.Severity, 500),
                        UAFilterElements.Equals(
                            UABaseEventObject.Operands.SourceNode, 
                            new UANodeId("nsu=http://opcfoundation.org/Quickstarts/AlarmCondition ;ns=2;s=1:Metals/SouthMotor"))),
                    UABaseEventObject.AllFields));

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


        // Example output:
        //
        //Subscribing...
        //13000008
        //Processing event notifications for 30 seconds...
        //[] Success
        //[] Success; Refresh; RefreshInitiated
        //[] Success; Refresh; [SouthMotor] 100! {DialogConditionType} "The dialog was activated" @9/9/2021 2:22:18 PM (10 fields)
        //[] Success; Refresh; [SouthMotor] 700! {ExclusiveDeviationAlarmType} "The alarm severity has increased." @9/22/2021 7:17:59 AM (10 fields)
        //[] Success; Refresh; [SouthMotor] 900! {NonExclusiveLevelAlarmType} "The alarm severity has increased." @9/22/2021 7:17:57 AM (10 fields)
        //[] Success; Refresh; [SouthMotor] 100! {TripAlarmType} "The alarm is active." @9/22/2021 7:17:59 AM (10 fields)
        //[] Success; Refresh; [SouthMotor] 100! {TripAlarmType} "The alarm severity has increased." @9/9/2021 3:39:51 PM (10 fields)
        //[] Success; Refresh; [SouthMotor] 100! {TripAlarmType} "The alarm severity has increased." @9/9/2021 3:38:57 PM (10 fields)
        //[] Success; Refresh; RefreshComplete
        //[] Success; [SouthMotor] 100! {NonExclusiveLevelAlarmType} "The alarm was deactivated by the system." @9/22/2021 7:18:05 AM (10 fields)
        //[] Success; [SouthMotor] 300! {TripAlarmType} "The alarm severity has increased." @9/22/2021 7:18:08 AM (10 fields)
        //[] Success; [SouthMotor] 900! {ExclusiveDeviationAlarmType} "The alarm severity has increased." @9/22/2021 7:18:10 AM (10 fields)
        //[] Success; [SouthMotor] 100! {NonExclusiveLevelAlarmType} "The alarm is active." @9/22/2021 7:18:13 AM (10 fields)
        //[] Success; [SouthMotor] 500! {TripAlarmType} "The alarm severity has increased." @9/22/2021 7:18:17 AM (10 fields)
        //[] Success; [SouthMotor] 100! {ExclusiveDeviationAlarmType} "The alarm was deactivated by the system." @9/22/2021 7:18:21 AM (10 fields)
        //[] Success; [SouthMotor] 300! {NonExclusiveLevelAlarmType} "The alarm severity has increased." @9/22/2021 7:18:21 AM (10 fields)
        //[] Success; [SouthMotor] 700! {TripAlarmType} "The alarm severity has increased." @9/22/2021 7:18:26 AM (10 fields)
        //[] Success; [SouthMotor] 500! {NonExclusiveLevelAlarmType} "The alarm severity has increased." @9/22/2021 7:18:29 AM (10 fields)
        //[] Success; [SouthMotor] 100! {ExclusiveDeviationAlarmType} "The alarm is active." @9/22/2021 7:18:32 AM (10 fields)
        //Unsubscribing...
        //Waiting for 5 seconds...
        //Finished.
    }
}
#endregion
