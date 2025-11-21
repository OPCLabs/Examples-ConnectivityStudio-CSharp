// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to subscribe to dataset messages and extract data of a specific field.
//
// In order to produce network messages for this example, run the UADemoPublisher tool. For documentation, see
// https://kb.opclabs.com/UADemoPublisher_Basics . In some cases, you may have to specify the interface name to be used.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Threading;
using OpcLabs.EasyOpc.UA.PubSub;
using OpcLabs.EasyOpc.UA.PubSub.Configuration;
using OpcLabs.EasyOpc.UA.PubSub.OperationModel;

namespace UASubscriberDocExamples.PubSub._EasyUASubscriber
{
    partial class SubscribeDataSet
    {
        public static void ExtractField()
        {
            // Define the PubSub connection we will work with. Uses implicit conversion from a string.
            UAPubSubConnectionDescriptor pubSubConnectionDescriptor = "opc.udp://239.0.0.1";
            // In some cases you may have to set the interface (network adapter) name that needs to be used, similarly to
            // the statement below. Your actual interface name may differ, of course.
            //pubSubConnectionDescriptor.ResourceAddress.InterfaceName = "Ethernet";

            // Define the filter. Publisher Id (unsigned 64-bits) is 31, and the dataset writer Id is 1.
            var filter = new UASubscribeDataSetFilter(UAPublisherId.CreateUInt64(31), UAWriterGroupDescriptor.Null, 1);

            // Define the metadata, with the use of collection initializer for its fields. For UADP, the order of field
            // metadata must correspond to the order of fields in the dataset message. If the field names were contained
            // in the dataset message (such as in JSON), or if we knew the metadata from some other source, this step would
            // not be needed.
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
            subscriber.DataSetMessage += subscriber_DataSetMessage_ExtractField;

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

        static void subscriber_DataSetMessage_ExtractField(object sender, EasyUADataSetMessageEventArgs e)
        {
            // Display the dataset.
            if (e.Succeeded)
            {
                // An event with null DataSetData just indicates a successful connection.
                if (!(e.DataSetData is null))
                {
                    // Extract field data, looking up the field by its name.
                    UADataSetFieldData int32FastValueData = e.DataSetData.FieldDataDictionary["Int32Fast"];
                    Console.WriteLine(int32FastValueData);
                }
            }
            else
            {
                Console.WriteLine($"*** Failure: {e.ErrorMessageBrief}");
            }
        }

        // Example output:
        //
        //Subscribing...
        //Processing dataset message events for 20 seconds...
        //6502 {System.Int32} @2019-10-06T10:02:01.254,647,600,00; Good
        //6538 {System.Int32} @2019-10-06T10:02:01.755,010,700,00; Good
        //6615 {System.Int32} @2019-10-06T10:02:02.255,780,200,00; Good
        //6687 {System.Int32} @2019-10-06T10:02:02.756,495,900,00; Good
        //6769 {System.Int32} @2019-10-06T10:02:03.257,320,200,00; Good
        //6804 {System.Int32} @2019-10-06T10:02:03.757,667,300,00; Good
        //6877 {System.Int32} @2019-10-06T10:02:04.258,405,000,00; Good
        //6990 {System.Int32} @2019-10-06T10:02:04.759,532,900,00; Good
        //7063 {System.Int32} @2019-10-06T10:02:05.260,257,200,00; Good
        //7163 {System.Int32} @2019-10-06T10:02:05.761,261,800,00; Good
        //7255 {System.Int32} @2019-10-06T10:02:06.262,176,800,00; Good
        //7321 {System.Int32} @2019-10-06T10:02:06.762,839,800,00; Good
        //7397 {System.Int32} @2019-10-06T10:02:07.263,598,900,00; Good
        //7454 {System.Int32} @2019-10-06T10:02:07.764,168,900,00; Good
        //7472 {System.Int32} @2019-10-06T10:02:08.264,350,400,00; Good
        //...
    }
}
#endregion
