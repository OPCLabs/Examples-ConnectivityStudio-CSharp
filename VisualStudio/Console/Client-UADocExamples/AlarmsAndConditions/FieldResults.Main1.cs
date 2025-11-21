// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CommentTypo
// ReSharper disable InconsistentNaming
// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
// ReSharper disable StringLiteralTypo
#region Example
// This example shows how to display all fields of incoming events, or extract specific fields.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Collections.Generic;
using OpcLabs.BaseLib.OperationModel;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.AddressSpace.Standard;
using OpcLabs.EasyOpc.UA.AlarmsAndConditions;
using OpcLabs.EasyOpc.UA.Filtering;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples.AlarmsAndConditions
{
    class FieldResults
    {
        public static void Main1()
        {
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
            Console.WriteLine();

            // Display the event.
            if (e.EventData is null)
            {
                Console.WriteLine(e);
                return;
            }
            Console.WriteLine("All fields:");
            foreach (KeyValuePair<UAAttributeField, ValueResult> pair in e.EventData.FieldResults)
            {
                UAAttributeField attributeField = pair.Key;
                ValueResult valueResult = pair.Value;
                Console.WriteLine("  {0} -> {1}", attributeField, valueResult);
            }

            // Extracting a specific field using a standard operand symbol.
            Console.WriteLine($"Source name: {e.EventData.FieldResults[UABaseEventObject.Operands.SourceName]}");

            // Extracting a specific field using an event type ID and a simple relative path.
            Console.WriteLine(
                $"Message: {e.EventData.FieldResults[UAFilterElements.SimpleAttribute(UAObjectTypeIds.BaseEventType, "/Message")]}");
        }


        // Example output (truncated):
        //Subscribing...
        //Processing event notifications for 30 seconds...
        //
        //[] Success
        //
        //[] Success; Refresh; RefreshInitiated
        //
        //All fields:
        //  NodeId="BaseEventType", NodeId -> Success; nsu=http://opcfoundation.org/Quickstarts/AlarmCondition ;ns=2;s=1:Colours/EastTank?OnlineState {OpcLabs.EasyOpc.UA.AddressSpace.UANodeId}
        //  NodeId="BaseEventType"/EventId -> Success; [16] {95, 68, 22, 205, 114, ...} {System.Byte[]}
        //  NodeId="BaseEventType"/EventType -> Success; DialogConditionType {OpcLabs.EasyOpc.UA.AddressSpace.UANodeId}
        //  NodeId="BaseEventType"/SourceNode -> Success; nsu=http://opcfoundation.org/Quickstarts/AlarmCondition ;ns=2;s=1:Colours/EastTank {OpcLabs.EasyOpc.UA.AddressSpace.UANodeId}
        //  NodeId="BaseEventType"/SourceName -> Success; EastTank {System.String}
        //  NodeId="BaseEventType"/Time -> Success; 9/10/2019 8:08:23 PM {System.DateTime}
        //  NodeId="BaseEventType"/ReceiveTime -> Success; 9/10/2019 8:08:23 PM {System.DateTime}
        //  NodeId="BaseEventType"/LocalTime -> Success; 00:00, DST {OpcLabs.EasyOpc.UA.UATimeZoneData}
        //  NodeId="BaseEventType"/Message -> Success; The dialog was activated {System.String}
        //  NodeId="BaseEventType"/Severity -> Success; 100 {System.Int32}
        //Source name: Success; EastTank {System.String}
        //Message: Success; The dialog was activated {System.String}
        //
        //All fields:
        //  NodeId="BaseEventType", NodeId -> Success; nsu=http://opcfoundation.org/Quickstarts/AlarmCondition ;ns=2;s=1:Colours/EastTank?Red {OpcLabs.EasyOpc.UA.AddressSpace.UANodeId}
        //  NodeId="BaseEventType"/EventId -> Success; [16] {124, 156, 219, 54, 120, ...} {System.Byte[]}
        //  NodeId="BaseEventType"/EventType -> Success; ExclusiveDeviationAlarmType {OpcLabs.EasyOpc.UA.AddressSpace.UANodeId}
        //  NodeId="BaseEventType"/SourceNode -> Success; nsu=http://opcfoundation.org/Quickstarts/AlarmCondition ;ns=2;s=1:Colours/EastTank {OpcLabs.EasyOpc.UA.AddressSpace.UANodeId}
        //  NodeId="BaseEventType"/SourceName -> Success; EastTank {System.String}
        //  NodeId="BaseEventType"/Time -> Success; 10/14/2019 4:00:13 PM {System.DateTime}
        //  NodeId="BaseEventType"/ReceiveTime -> Success; 10/14/2019 4:00:13 PM {System.DateTime}
        //  NodeId="BaseEventType"/LocalTime -> Success; 00:00, DST {OpcLabs.EasyOpc.UA.UATimeZoneData}
        //  NodeId="BaseEventType"/Message -> Success; The alarm was acknoweledged. {System.String}
        //  NodeId="BaseEventType"/Severity -> Success; 500 {System.Int32}
        //Source name: Success; EastTank {System.String}
        //Message: Success; The alarm was acknoweledged. {System.String}
        //
        //...
    }
}
#endregion
