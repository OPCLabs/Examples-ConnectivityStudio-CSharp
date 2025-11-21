// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable MergeConditionalExpression
// ReSharper disable StringLiteralTypo
#region Example
// This example shows how to obtain acknowledged state of events, and acknowledge an event that is not acknowledged yet.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Diagnostics;
using System.Threading;
using OpcLabs.BaseLib.OperationModel;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.AddressSpace;
using OpcLabs.EasyOpc.UA.AddressSpace.Standard;
using OpcLabs.EasyOpc.UA.AlarmsAndConditions;
using OpcLabs.EasyOpc.UA.Filtering;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples.AlarmsAndConditions
{
    partial class Acknowledge
    {
        public static void AckedState()
        {
            // Define which server we will work with.
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:62544/Quickstarts/AlarmConditionServer";

            // Instantiate the client objects.
            var client = new EasyUAClient();
            IEasyUAAlarmsAndConditionsClient alarmsAndConditionsClient = client.AsAlarmsAndConditionsClient();

            UANodeId nodeId = null;
            byte[] eventId = null;
            var anEvent = new ManualResetEvent(initialState: false);

            // Prepare the Select clauses.
            UAAttributeFieldCollection selectClauses = UABaseEventObject.AllFields;
            UASimpleAttributeOperand ackedStateIdOperand = UAFilterElements.SimpleAttribute(UAObjectTypeIds.BaseEventType, "/AckedState/Id");
            selectClauses.Add(ackedStateIdOperand);

            Console.WriteLine("Subscribing...");
            client.SubscribeEvent(
                endpointDescriptor,
                UAObjectIds.Server,
                1000,
                new UAEventFilterBuilder(
                    // We will auto-acknowledge an event with severity less than 200.
                    UAFilterElements.LessThan(UABaseEventObject.Operands.Severity, 200),
                    selectClauses),
                (sender, eventArgs) =>
                {
                    if (!eventArgs.Succeeded)
                    {
                        Console.WriteLine($"*** Failure: {eventArgs.ErrorMessageBrief}");
                        return;
                    }

                    UAEventData eventData = eventArgs.EventData;
                    if (!(eventData is null))
                    {
                        UABaseEventObject baseEventObject = eventData.BaseEvent;
                        Console.WriteLine(baseEventObject);

                        // Obtain the acknowledge state of the event.
                        ValueResult ackedStateIdResult = eventData.FieldResults[ackedStateIdOperand];
                        Debug.Assert(!(ackedStateIdResult is null));
                        if (!ackedStateIdResult.Succeeded)
                            return;
                        bool? ackedStateId = (ackedStateIdResult.Value is bool) ? (bool?)ackedStateIdResult.Value : null;
                        Console.WriteLine($"AckedState/Id: {ackedStateId}");

                        // Only attempt to acknowledge when not acknowledged yet.
                        if (ackedStateId != false)
                            return;

                        // Make sure we do not catch the event more than once.
                        if (anEvent.WaitOne(0))
                            return;

                        nodeId = baseEventObject.NodeId;
                        eventId = baseEventObject.EventId;

                        anEvent.Set();
                    }
                },
                state:null);

            Console.WriteLine("Waiting for an acknowledgeable event for 10 minutes...");
            if (!anEvent.WaitOne(10*60*1000))
            {
                Console.WriteLine("Event not received.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Acknowledging an event...");
            try
            {
                alarmsAndConditionsClient.Acknowledge(
                    endpointDescriptor,
                    nodeId,
                    eventId,
                    "Acknowledged by an automated example code.");
            }
            catch (UAException uaException)
            {
                Console.WriteLine($"*** Failure: {uaException.GetBaseException().Message}");
            }

            Console.WriteLine("Waiting for 5 seconds...");
            Thread.Sleep(5 * 1000);

            Console.WriteLine("Unsubscribing...");
            client.UnsubscribeAllMonitoredItems();

            Console.WriteLine("Waiting for 5 seconds...");
            Thread.Sleep(5 * 1000);

            Console.WriteLine("Finished.");
        }
    }
}
#endregion
