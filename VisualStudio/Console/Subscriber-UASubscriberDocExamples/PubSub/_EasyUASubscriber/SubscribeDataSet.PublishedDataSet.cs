// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

#pragma warning disable IDE1006 // Naming Styles
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to subscribe to dataset messages, specifying just the published dataset name, and resolving all
// the dataset subscription arguments from an OPC-UA PubSub configuration file.
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

namespace UASubscriberDocExamples.PubSub._EasyUASubscriber
{
    partial class SubscribeDataSet
    {
        public static void PublishedDataSet()
        {
            // Define the PubSub resolver. We want the information be resolved from a PubSub binary configuration file that
            // we have. The file itself is at the root of the project, and we have specified that it has to be copied to the
            // project's output directory.
            var pubSubResolverDescriptor = UAPubSubResolverDescriptor.File("UADemoPublisher-Default.uabinary");

            // Instantiate the subscriber object.
            var subscriber = new EasyUASubscriber();

            Console.WriteLine("Subscribing...");
            // Specify the published dataset name, and let all other subscription arguments be resolved automatically.
            subscriber.SubscribeDataSet(pubSubResolverDescriptor, "AllTypes-Dynamic", (sender, args) =>
            {
                // Display the dataset.
                if (args.Succeeded)
                {
                    // An event with null DataSetData just indicates a successful connection.
                    if (!(args.DataSetData is null))
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Dataset data: {args.DataSetData}");
                        foreach (KeyValuePair<string, UADataSetFieldData> pair in args.DataSetData.FieldDataDictionary)
                            Console.WriteLine(pair);
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"*** Failure: {args.ErrorMessageBrief}");
                }
            });

            Console.WriteLine("Processing dataset message events for 20 seconds...");
            Thread.Sleep(20 * 1000);

            Console.WriteLine("Unsubscribing...");
            subscriber.UnsubscribeAllDataSets();

            Console.WriteLine("Waiting for 1 second...");
            // Unsubscribe operation is asynchronous, messages may still come for a short while.
            Thread.Sleep(1 * 1000);

            Console.WriteLine("Finished.");
        }

        // Example output:
        //
        //Subscribing...
        //Processing dataset message events for 20 seconds...
        //
        //Dataset data: Good; Data; publisher=(UInt64)31, writer=4, fields: 16
        //[BoolToggle, False {System.Boolean}; Good]
        //[Byte, 137 {System.Byte}; Good]
        //[Int16, 10377 {System.Int16}; Good]
        //[Int32, 43145 {System.Int32}; Good]
        //[Int64, 43145 {System.Int64}; Good]
        //[SByte, 9 {System.Int16}; Good]
        //[UInt16, 43145 {System.Int32}; Good]
        //[UInt32, 43145 {System.Int64}; Good]
        //[UInt64, 43145 {System.Decimal}; Good]
        //[Float, 43145 {System.Single}; Good]
        //[Double, 43145 {System.Double}; Good]
        //[String, Lima {System.String}; Good]
        //[ByteString, [20] {176, 63, 39, 37, 31, ...} {System.Byte[]}; Good]
        //[Guid, 45a99b50-e265-41f2-adea-d0bcedc3ff4b {System.Guid}; Good]
        //[DateTime, 10/3/2019 7:15:34 AM {System.DateTime}; Good]
        //[UInt32Array, [10] {43145, 43146, 43147, 43148, 43149, ...} {System.Int64[]}; Good]
        //
        //Dataset data: Good; Data; publisher=(UInt64)31, writer=4, fields: 16
        //[BoolToggle, True {System.Boolean}; Good]
        //[Byte, 138 {System.Byte}; Good]
        //[Int16, 10378 {System.Int16}; Good]
        //[Int32, 43146 {System.Int32}; Good]
        //[Int64, 43146 {System.Int64}; Good]
        //[SByte, 10 {System.Int16}; Good]
        //[UInt16, 43146 {System.Int32}; Good]
        //[UInt32, 43146 {System.Int64}; Good]
        //[UInt64, 43146 {System.Decimal}; Good]
        //[Float, 43146 {System.Single}; Good]
        //[Double, 43146 {System.Double}; Good]
        //[String, Mike {System.String}; Good]
        //[Guid, a0f06d75-9896-4fa3-9724-b564359da21b {System.Guid}; Good]
        //[DateTime, 10/3/2019 7:15:34 AM {System.DateTime}; Good]
        //[UInt32Array, [10] {43146, 43147, 43148, 43149, 43150, ...} {System.Int64[]}; Good]
        //[ByteString, [20] {176, 63, 39, 37, 31, ...} {System.Byte[]}; Good]
        //
        //Dataset data: Good; Data; publisher=(UInt64)31, writer=4, fields: 16
        //[DateTime, 10/3/2019 7:15:35 AM {System.DateTime}; Good]
        //[BoolToggle, True {System.Boolean}; Good]
        //[Byte, 138 {System.Byte}; Good]
        //[Int16, 10378 {System.Int16}; Good]
        //[Int32, 43146 {System.Int32}; Good]
        //[Int64, 43146 {System.Int64}; Good]
        //[SByte, 10 {System.Int16}; Good]
        //[UInt16, 43146 {System.Int32}; Good]
        //[UInt32, 43146 {System.Int64}; Good]
        //[UInt64, 43146 {System.Decimal}; Good]
        //[Float, 43146 {System.Single}; Good]
        //[Double, 43146 {System.Double}; Good]
        //[String, Mike {System.String}; Good]
        //[ByteString, [20] {176, 63, 39, 37, 31, ...} {System.Byte[]}; Good]
        //[Guid, a0f06d75-9896-4fa3-9724-b564359da21b {System.Guid}; Good]
        //[UInt32Array, [10] {43146, 43147, 43148, 43149, 43150, ...} {System.Int64[]}; Good]
        //
        //...
    }
}
#endregion
