// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to subscribe to all dataset messages on an OPC-UA PubSub connection with Ethernet UADP mapping.
//
// In order to produce network messages for this example, run the UADemoPublisher tool. For documentation, see
// https://kb.opclabs.com/UADemoPublisher_Basics . In some cases, you may have to specify the interface name to be used.
// The UADemoPublisher must be told to use the Ethernet transport: run it with the -eth switch on the command line.
//
// The OpcLabs.Pcap assembly needs to be referenced in your project (or otherwise made available, together with its
// dependencies) for the Ethernet transport to work, and additional software installation may be needed as well. Refer to
// the documentation for more information.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Collections.Generic;
using System.Threading;
using OpcLabs.EasyOpc.UA.PubSub;
using OpcLabs.EasyOpc.UA.PubSub.OperationModel;

namespace UASubscriberDocExamples.PubSub._EasyUASubscriber
{
    partial class SubscribeDataSet
    {
        public static void Ethernet()
        {
            // Define the PubSub connection we will work with. Uses implicit conversion from a string.
            // "opc.eth" is the scheme for OPC UA Ethernet. "FF-FF-FF-FF-FF-FF" is the Ethernet broadcast address.
            UAPubSubConnectionDescriptor pubSubConnectionDescriptor = "opc.eth://FF-FF-FF-FF-FF-FF";
            // In some cases you may have to set the interface (network adapter) name that needs to be used, similarly to
            // the statement below. Your actual interface name may differ, of course.
            //pubSubConnectionDescriptor.ResourceAddress.InterfaceName = "Ethernet";

            // Instantiate the subscriber object and hook events.
            var subscriber = new EasyUASubscriber();
            subscriber.DataSetMessage += subscriber_DataSetMessage_Ethernet;

            Console.WriteLine("Subscribing...");
            subscriber.SubscribeDataSet(pubSubConnectionDescriptor);

            Console.WriteLine("Processing dataset message events for 20 seconds...");
            Thread.Sleep(20 * 1000);

            Console.WriteLine("Unsubscribing...");
            subscriber.UnsubscribeAllDataSets();

            Console.WriteLine("Waiting for 1 second...");
            // Unsubscribe operation is asynchronous, messages may still come for a short while.
            Thread.Sleep(1 * 1000);

            Console.WriteLine("Finished.");
        }

        static void subscriber_DataSetMessage_Ethernet(object sender, EasyUADataSetMessageEventArgs e)
        {
            // Display the dataset.
            if (e.Succeeded)
            {
                // An event with null DataSetData just indicates a successful connection.
                if (!(e.DataSetData is null))
                {
                    Console.WriteLine();
                    Console.WriteLine($"Dataset data: {e.DataSetData}");
                    foreach (KeyValuePair<string, UADataSetFieldData> pair in e.DataSetData.FieldDataDictionary)
                        Console.WriteLine(pair);
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"*** Failure: {e.ErrorMessageBrief}");
            }
        }

        // Example output:
        //
        //Subscribing...
        //Processing dataset message events for 20 seconds...
        //
        //Dataset data: Good; Data; publisher="32", writer=1, class=eae79794-1af7-4f96-8401-4096cd1d8908, fields: 4
        //[#0, True {System.Boolean} @0001-01-01T00:00:00.000 @@0001-01-01T00:00:00.000; Good]
        //[#1, 7945 {System.Int32} @0001-01-01T00:00:00.000 @@0001-01-01T00:00:00.000; Good]
        //[#2, 5246 {System.Int32} @0001-01-01T00:00:00.000 @@0001-01-01T00:00:00.000; Good]
        //[#3, 9/30/2019 11:19:14 AM {System.DateTime} @0001-01-01T00:00:00.000 @@0001-01-01T00:00:00.000; Good]
        //
        //Dataset data: Good; Data; publisher="32", writer=3, class=96976b7b-0db7-46c3-a715-0979884b55ae, fields: 100
        //[#0, 45 {System.Int64} @0001-01-01T00:00:00.000 @@0001-01-01T00:00:00.000; Good]
        //[#1, 145 {System.Int64} @0001-01-01T00:00:00.000 @@0001-01-01T00:00:00.000; Good]
        //[#2, 245 {System.Int64} @0001-01-01T00:00:00.000 @@0001-01-01T00:00:00.000; Good]
        //[#3, 345 {System.Int64} @0001-01-01T00:00:00.000 @@0001-01-01T00:00:00.000; Good]
        //[#4, 445 {System.Int64} @0001-01-01T00:00:00.000 @@0001-01-01T00:00:00.000; Good]
        //[#5, 545 {System.Int64} @0001-01-01T00:00:00.000 @@0001-01-01T00:00:00.000; Good]
        //[#6, 645 {System.Int64} @0001-01-01T00:00:00.000 @@0001-01-01T00:00:00.000; Good]
        //[#7, 745 {System.Int64} @0001-01-01T00:00:00.000 @@0001-01-01T00:00:00.000; Good]
        //[#8, 845 {System.Int64} @0001-01-01T00:00:00.000 @@0001-01-01T00:00:00.000; Good]
        //[#9, 945 {System.Int64} @0001-01-01T00:00:00.000 @@0001-01-01T00:00:00.000; Good]
        //[#10, 1045 {System.Int64} @0001-01-01T00:00:00.000 @@0001-01-01T00:00:00.000; Good]
        //...
    }
}
#endregion
