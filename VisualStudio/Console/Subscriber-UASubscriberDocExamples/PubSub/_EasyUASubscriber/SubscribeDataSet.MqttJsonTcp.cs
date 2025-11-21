// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable PossibleNullReferenceException
// ReSharper disable StringLiteralTypo
#region Example
// This example shows how to subscribe to all dataset messages on an OPC-UA PubSub connection with MQTT JSON mapping using
// TCP.
//
// The following package needs to be referenced in your project (or otherwise made available) for the MQTT transport to 
// work.
// - OpcLabs.MqttNet
// Refer to the documentation for more information.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Collections.Generic;
using System.Threading;
using OpcLabs.EasyOpc.UA.PubSub;
using OpcLabs.EasyOpc.UA.PubSub.Engine;
using OpcLabs.EasyOpc.UA.PubSub.OperationModel;

namespace UASubscriberDocExamples.PubSub._EasyUASubscriber
{
    partial class SubscribeDataSet
    {
        public static void MqttJsonTcp()
        {
            // Define the PubSub connection we will work with. Uses implicit conversion from a string.
            // Default port with MQTT is 1883.
            UAPubSubConnectionDescriptor pubSubConnectionDescriptor = "mqtt://opcua-pubsub.demo-this.com";
            // Specify the transport protocol mapping.
            // The statement below isn't actually necessary, due to automatic message mapping recognition feature; see
            // https://kb.opclabs.com/OPC_UA_PubSub_Automatic_Message_Mapping_Recognition for more details.
            pubSubConnectionDescriptor.TransportProfileUriString = UAPubSubTransportProfileUriStrings.MqttJson;

            // Define the arguments for subscribing to the dataset, specifying the MQTT topic name.
            var subscribeDataSetArguments = new UASubscribeDataSetArguments(pubSubConnectionDescriptor)
            {
                DataSetSubscriptionDescriptor = {CommunicationParameters = {BrokerDataSetReaderTransportParameters =
                {
                    QueueName = "opcuademo/json"
                }}}
            };

            // Instantiate the subscriber object and hook events.
            var subscriber = new EasyUASubscriber();
            subscriber.DataSetMessage += subscriber_DataSetMessage_MqttJsonTcp;

            Console.WriteLine("Subscribing...");
            subscriber.SubscribeDataSet(subscribeDataSetArguments);

            Console.WriteLine("Processing dataset message events for 20 seconds...");
            Thread.Sleep(20 * 1000);

            Console.WriteLine("Unsubscribing...");
            subscriber.UnsubscribeAllDataSets();

            Console.WriteLine("Waiting for 1 second...");
            // Unsubscribe operation is asynchronous, messages may still come for a short while.
            Thread.Sleep(1 * 1000);

            Console.WriteLine("Finished.");
        }

        static void subscriber_DataSetMessage_MqttJsonTcp(object sender, EasyUADataSetMessageEventArgs e)
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
                Console.WriteLine($"*** Failure: {e.ErrorMessage}");
            }
        }

        // Example output:
        //
        //Subscribing...
        //Processing dataset message events for 20 seconds...
        //
        //Dataset data: 2020-01-21T17:07:19.778,836,700,00; Good; Data; publisher=[String]31, class=eae79794-1af7-4f96-8401-4096cd1d8908, fields: 4
        //[BoolToggle, True {System.Boolean} @2020-01-21T16:07:19.778,836,700,00; Good]
        //[Int32, 482 {System.Int64} @2020-01-21T16:07:19.778,836,700,00; Good]
        //[Int32Fast, 2287 {System.Int64} @2020-01-21T16:07:19.778,836,700,00; Good]
        //[DateTime, 1/21/2020 5:07:19 PM {System.DateTime} @2020-01-21T16:07:19.778,836,700,00; Good]
        //
        //Dataset data: Good; Data; publisher=[String]32, fields: 4
        //[BoolToggle, True {System.Boolean}; Good]
        //[Int32, 482 {System.Int32}; Good]
        //[Int32Fast, 2287 {System.Int32}; Good]
        //[DateTime, 1/21/2020 5:07:19 PM {System.DateTime}; Good]
        //
        //Dataset data: Good; Data; publisher=[String]32, fields: 100
        //[Mass_0, 82 {System.Int64}; Good]
        //[Mass_1, 182 {System.Int64}; Good]
        //[Mass_2, 282 {System.Int64}; Good]
        //[Mass_3, 382 {System.Int64}; Good]
        //[Mass_4, 482 {System.Int64}; Good]
        //[Mass_5, 582 {System.Int64}; Good]
        //[Mass_6, 682 {System.Int64}; Good]
        //[Mass_7, 782 {System.Int64}; Good]
        //[Mass_8, 882 {System.Int64}; Good]
        //[Mass_9, 982 {System.Int64}; Good]
        //[Mass_10, 1082 {System.Int64}; Good]
        ////...
    }
}
#endregion
