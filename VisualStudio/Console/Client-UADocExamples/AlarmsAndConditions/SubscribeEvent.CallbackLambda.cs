// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to subscribe to event notifications and display each incoming event
// using a callback method that is provided as lambda expression.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.AddressSpace.Standard;

namespace UADocExamples.AlarmsAndConditions
{
    partial class SubscribeEvent
    {
        public static void CallbackLambda()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:62544/Quickstarts/AlarmConditionServer";

            // Instantiate the client object
            var client = new EasyUAClient();

            Console.WriteLine("Subscribing...");
            // The callback is a lambda expression the displays the event
            client.SubscribeEvent(
                endpointDescriptor,
                UAObjectIds.Server,
                1000,
                (sender, eventArgs) => Console.WriteLine(eventArgs));
            // Remark: Production code needs to check eventArgs.Exception before accessing eventArgs.EventData.

            Console.WriteLine("Processing event notifications for 30 seconds...");
            System.Threading.Thread.Sleep(30 * 1000);

            Console.WriteLine("Unsubscribing...");
            client.UnsubscribeAllMonitoredItems();

            Console.WriteLine("Waiting for 2 seconds...");
            System.Threading.Thread.Sleep(2 * 1000);
        }
    }
}
#endregion
