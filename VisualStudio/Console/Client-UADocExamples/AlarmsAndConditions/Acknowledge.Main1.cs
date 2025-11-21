// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable StringLiteralTypo
// ReSharper disable CommentTypo
#region Example
// This example shows how to acknowledge an OPC UA event.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Threading;
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
        public static void Main1()
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

            Console.WriteLine("Subscribing...");
            client.SubscribeEvent(
                endpointDescriptor,
                UAObjectIds.Server,
                1000,
                new UAEventFilterBuilder(
                    UAFilterElements.Equals(
                        UABaseEventObject.Operands.NodeId,
                        new UANodeId(nodeIdExpandedText: "nsu=http://opcfoundation.org/Quickstarts/AlarmCondition ;ns=2;s=1:Colours/EastTank?Yellow")),
                    UABaseEventObject.AllFields),
                (sender, eventArgs) =>
                {
                    if (!eventArgs.Succeeded)
                    {
                        Console.WriteLine($"*** Failure: {eventArgs.ErrorMessageBrief}");
                        return;
                    }
                    if (!(eventArgs.EventData is null))
                    {
                        UABaseEventObject baseEventObject = eventArgs.EventData.BaseEvent;
                        Console.WriteLine(baseEventObject);

                        // Make sure we do not catch the event more than once.
                        if (anEvent.WaitOne(0))
                            return;

                        nodeId = baseEventObject.NodeId;
                        eventId = baseEventObject.EventId;

                        anEvent.Set();
                    }
                },
                state:null);

            Console.WriteLine("Waiting for an event for 30 seconds...");
            if (!anEvent.WaitOne(30*1000))
            {
                Console.WriteLine("Event not received.");
                return;
            }

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



        // Example output:
        //Subscribing...
        //Waiting for an event for 30 seconds...
        //[EastTank] 100! "The alarm was acknoweledged." @11/9/2019 9:56:23 AM
        //Acknowledging an event...
        //Waiting for 5 seconds...
        //[EastTank] 100! "The alarm was acknoweledged." @11/9/2019 9:56:23 AM
        //Unsubscribing...
        //Waiting for 5 seconds...
        //Finished.
    }
}
#endregion
