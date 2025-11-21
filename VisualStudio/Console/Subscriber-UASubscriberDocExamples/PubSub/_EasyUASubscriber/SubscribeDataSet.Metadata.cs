// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to subscribe to dataset messages with RawData field encoding, specifying the metadata necessary
// for their decoding directly in the code.
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
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.PubSub;
using OpcLabs.EasyOpc.UA.PubSub.Configuration;
using OpcLabs.EasyOpc.UA.PubSub.OperationModel;

namespace UASubscriberDocExamples.PubSub._EasyUASubscriber
{
    partial class SubscribeDataSet
    {
        public static void Metadata()
        {
            // Define the PubSub connection we will work with. Uses implicit conversion from a string.
            UAPubSubConnectionDescriptor pubSubConnectionDescriptor = "opc.udp://239.0.0.1";
            // In some cases you may have to set the interface (network adapter) name that needs to be used, similarly to
            // the statement below. Your actual interface name may differ, of course.
            //pubSubConnectionDescriptor.ResourceAddress.InterfaceName = "Ethernet";

            // Define the filter. Publisher Id (unsigned 16-bits) is 30, and the writer group Id is 101.
            // The dataset writer Id (1) must not be specified in the filter, because it does not appear in the message.
            var filter = new UASubscribeDataSetFilter(UAPublisherId.CreateUInt16(30), writerGroupDescriptor: 101);

            // Define the metadata, with the use of collection initializer for its fields.
            var metaData = new UADataSetMetaData
            {
                new UAFieldMetaData("BoolToggle", UABuiltInType.Boolean),
                new UAFieldMetaData("Int32", UABuiltInType.Int32),
                new UAFieldMetaData("Int32Fast", UABuiltInType.Int32),
                new UAFieldMetaData("DateTime", UABuiltInType.DateTime)
            };

            // Define the dataset subscription, with specific communication parameters.
            // The dataset offset is needed with messages that do not contain dataset writer Ids and use RawData field
            // encoding. An exception to this rule is when the dataset is the only or first in the dataset message payload,
            // which is also the case here, but we are specifying the dataset offset anyway, for illustration.
            var dataSetSubscriptionDescriptor = new UADataSetSubscriptionDescriptor(
                pubSubConnectionDescriptor, filter, metaData)
            {
                CommunicationParameters = { UadpDataSetReaderMessageParameters = { DataSetOffset = 15 }}
            };

            // Instantiate the subscriber object and hook events.
            var subscriber = new EasyUASubscriber();
            subscriber.DataSetMessage += subscriber_DataSetMessage_Metadata;

            Console.WriteLine("Subscribing...");
            subscriber.SubscribeDataSet(dataSetSubscriptionDescriptor);

            Console.WriteLine("Processing dataset message events for 20 seconds...");
            Thread.Sleep(20 * 1000);

            Console.WriteLine("Unsubscribing...");
            subscriber.UnsubscribeAllDataSets();

            Console.WriteLine("Waiting for 1 second...");
            // Unsubscribe operation is asynchronous, messages may still come for a short while.
            Thread.Sleep(1 * 1000);

            Console.WriteLine("Finished.");
        }

        static void subscriber_DataSetMessage_Metadata(object sender, EasyUADataSetMessageEventArgs e)
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
        //Dataset data: Good; Data; publisher=(UInt16)30, group=101, fields: 4
        //[BoolToggle, False {System.Boolean}; Good]
        //[Int32, 3072 {System.Int32}; Good]
        //[Int32Fast, 894 {System.Int32}; Good]
        //[DateTime, 10/1/2019 12:21:14 PM {System.DateTime}; Good]
        //
        //Dataset data: Good; Data; publisher=(UInt16)30, group=101, fields: 4
        //[BoolToggle, False {System.Boolean}; Good]
        //[Int32, 3072 {System.Int32}; Good]
        //[Int32Fast, 920 {System.Int32}; Good]
        //[DateTime, 10/1/2019 12:21:14 PM {System.DateTime}; Good]
        //
        //Dataset data: Good; Data; publisher=(UInt16)30, group=101, fields: 4
        //[BoolToggle, False {System.Boolean}; Good]
        //[Int32, 3073 {System.Int32}; Good]
        //[Int32Fast, 1003 {System.Int32}; Good]
        //[DateTime, 10/1/2019 12:21:15 PM {System.DateTime}; Good]
        //
        //Dataset data: Good; Data; publisher=(UInt16)30, group=101, fields: 4
        //[BoolToggle, False {System.Boolean}; Good]
        //[Int32, 3073 {System.Int32}; Good]
        //[Int32Fast, 1074 {System.Int32}; Good]
        //[DateTime, 10/1/2019 12:21:15 PM {System.DateTime}; Good]
        //
        //Dataset data: Good; Data; publisher=(UInt16)30, group=101, fields: 4
        //[BoolToggle, True {System.Boolean}; Good]
        //[Int32, 3074 {System.Int32}; Good]
        //[Int32Fast, 1140 {System.Int32}; Good]
        //[DateTime, 10/1/2019 12:21:16 PM {System.DateTime}; Good]
        //
        //...
    }
}
#endregion
