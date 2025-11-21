// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable LocalizableElement
// ReSharper disable InconsistentNaming
// ReSharper disable PossibleNullReferenceException
// ReSharper disable StringLiteralTypo
#region Example
// This example shows how to select fields for event notifications.
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
    class SelectClauses
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
                new UAAttributeFieldCollection
                {
                    // Select specific fields using standard operand symbols.
                    UABaseEventObject.Operands.NodeId,
                    UABaseEventObject.Operands.SourceNode,
                    UABaseEventObject.Operands.SourceName,
                    UABaseEventObject.Operands.Time,

                    // Select specific fields using an event type ID and a simple relative path.
                    UAFilterElements.SimpleAttribute(UAObjectTypeIds.BaseEventType, "/Message"),
                    UAFilterElements.SimpleAttribute(UAObjectTypeIds.BaseEventType, "/Severity")
                });

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
                Console.WriteLine($"  {attributeField} -> {valueResult}");
            }

            // Extracting a specific field using a standard operand symbol.
            Console.WriteLine($"Source name: {e.EventData.FieldResults[UABaseEventObject.Operands.SourceName]}");

            // Extracting a specific field using an event type ID and a simple relative path.
            Console.WriteLine(
                $"Message: {e.EventData.FieldResults[UAFilterElements.SimpleAttribute(UAObjectTypeIds.BaseEventType, "/Message")]}");
        }
    }
}
#endregion
