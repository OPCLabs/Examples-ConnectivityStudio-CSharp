// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to subscribe to dataset messages and specify field names, without having the full metadata.
//
// In order to produce network messages for this example, run the UADemoPublisher tool. For documentation, see
// https://kb.opclabs.com/UADemoPublisher_Basics . In some cases, you may have to specify the interface name to be used.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Collections.Generic;
using System.Threading;
using OpcLabs.EasyOpc.UA.PubSub;
using OpcLabs.EasyOpc.UA.PubSub.Configuration;
using OpcLabs.EasyOpc.UA.PubSub.OperationModel;

namespace UASubscriberDocExamples.PubSub._EasyUASubscriber
{
    partial class SubscribeDataSet
    {
        public static void FieldNames()
        {
            // Define the PubSub connection we will work with. Uses implicit conversion from a string.
            UAPubSubConnectionDescriptor pubSubConnectionDescriptor = "opc.udp://239.0.0.1";
            // In some cases you may have to set the interface (network adapter) name that needs to be used, similarly to
            // the statement below. Your actual interface name may differ, of course.
            //pubSubConnectionDescriptor.ResourceAddress.InterfaceName = "Ethernet";

            // Define the filter. Publisher Id (unsigned 64-bits) is 31, and the dataset writer Id is 1.
            var filter = new UASubscribeDataSetFilter(UAPublisherId.CreateUInt64(31), UAWriterGroupDescriptor.Null, 1);

            // Define the metadata, with the use of collection initializer for its fields. For UADP, the order of field
            // metadata must correspond to the order of fields in the dataset message.
            // Since the encoding is not RawData, we do not have to specify the type information for the fields.
            var metaData = new UADataSetMetaData
            {
                new UAFieldMetaData("BoolToggle"),
                new UAFieldMetaData("Int32"),
                new UAFieldMetaData("Int32Fast"),
                new UAFieldMetaData("DateTime")
            };

            // Instantiate the subscriber object and hook events.
            var subscriber = new EasyUASubscriber();
            subscriber.DataSetMessage += subscriber_DataSetMessage_FieldNames;

            Console.WriteLine("Subscribing...");
            subscriber.SubscribeDataSet(pubSubConnectionDescriptor, filter, metaData);

            Console.WriteLine("Processing dataset message events for 20 seconds...");
            Thread.Sleep(20 * 1000);

            Console.WriteLine("Unsubscribing...");
            subscriber.UnsubscribeAllDataSets();

            Console.WriteLine("Waiting for 1 second...");
            // Unsubscribe operation is asynchronous, messages may still come for a short while.
            Thread.Sleep(1 * 1000);

            Console.WriteLine("Finished.");
        }

        static void subscriber_DataSetMessage_FieldNames(object sender, EasyUADataSetMessageEventArgs e)
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
        //Dataset data: Good; Data; publisher=(UInt64)31, writer=1, fields: 4
        //[BoolToggle, True {System.Boolean}; Good]
        //[Int32, 25 {System.Int32}; Good]
        //[Int32Fast, 928 {System.Int32}; Good]
        //[DateTime, 10/3/2019 10:43:01 AM {System.DateTime}; Good]
        //
        //Dataset data: Good; Data; publisher=(UInt64)31, writer=1, fields: 4
        //[Int32, 26 {System.Int32}; Good]
        //[Int32Fast, 1007 {System.Int32}; Good]
        //[DateTime, 10/3/2019 10:43:02 AM {System.DateTime}; Good]
        //[BoolToggle, True {System.Boolean}; Good]
        //
        //Dataset data: Good; Data; publisher=(UInt64)31, writer=1, fields: 4
        //[Int32Fast, 1113 {System.Int32}; Good]
        //[DateTime, 10/3/2019 10:43:02 AM {System.DateTime}; Good]
        //[BoolToggle, True {System.Boolean}; Good]
        //[Int32, 26 {System.Int32}; Good]
        //
        //Dataset data: Good; Data; publisher=(UInt64)31, writer=1, fields: 4
        //[BoolToggle, False {System.Boolean}; Good]
        //[Int32, 27 {System.Int32}; Good]
        //[Int32Fast, 1201 {System.Int32}; Good]
        //[DateTime, 10/3/2019 10:43:03 AM {System.DateTime}; Good]
        //
        //Dataset data: Good; Data; publisher=(UInt64)31, writer=1, fields: 4
        //[Int32Fast, 1260 {System.Int32}; Good]
        //[DateTime, 10/3/2019 10:43:03 AM {System.DateTime}; Good]
        //[BoolToggle, False {System.Boolean}; Good]
        //[Int32, 27 {System.Int32}; Good]
        //
        //...
    }
}
#endregion
