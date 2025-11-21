// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to subscribe to all dataset messages on an OPC-UA PubSub connection with MQTT UADP mapping using
// TCP.
//
// The OpcLabs.MqttNet assembly needs to be referenced in your project (or otherwise made available, together with its
// dependencies) for the MQTT transport to work. Refer to the documentation for more information.
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
        public static void MqttUadpTcp()
        {
            // Define the PubSub connection we will work with. Uses implicit conversion from a string.
            // Default port with MQTT is 1883.
            UAPubSubConnectionDescriptor pubSubConnectionDescriptor = "mqtt://opcua-pubsub.demo-this.com";
            // Specify the transport protocol mapping.
            // The statement below isn't actually necessary, due to automatic message mapping recognition feature; see
            // https://kb.opclabs.com/OPC_UA_PubSub_Automatic_Message_Mapping_Recognition for more details.
            pubSubConnectionDescriptor.TransportProfileUriString = UAPubSubTransportProfileUriStrings.MqttUadp;

            // Define the arguments for subscribing to the dataset, specifying the MQTT topic name.
            var subscribeDataSetArguments = new UASubscribeDataSetArguments(pubSubConnectionDescriptor)
            {
                DataSetSubscriptionDescriptor = {CommunicationParameters = {BrokerDataSetReaderTransportParameters =
                {
                    QueueName = "opcuademo/uadp/none"
                }}}
            };

            // Instantiate the subscriber object and hook events.
            var subscriber = new EasyUASubscriber();
            subscriber.DataSetMessage += subscriber_DataSetMessage_MqttUadpTcp;

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

        static void subscriber_DataSetMessage_MqttUadpTcp(object sender, EasyUADataSetMessageEventArgs e)
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
        //Dataset data: Good; Data; publisher="32", writer=1, class=eae79794-1af7-4f96-8401-4096cd1d8908, fields: 4
        //[#0, False {System.Boolean}; Good]
        //[#1, 6685 {System.Int32}; Good]
        //[#2, 1444 {System.Int32}; Good]
        //[#3, 1/4/2020 6:06:20 PM {System.DateTime}; Good]
        //
        //Dataset data: Good; Data; publisher="32", writer=3, class=96976b7b-0db7-46c3-a715-0979884b55ae, fields: 100
        //[#0, 85 {System.Int64}; Good]
        //[#1, 185 {System.Int64}; Good]
        //[#2, 285 {System.Int64}; Good]
        //[#3, 385 {System.Int64}; Good]
        //[#4, 485 {System.Int64}; Good]
        //[#5, 585 {System.Int64}; Good]
        //[#6, 685 {System.Int64}; Good]
        //[#7, 785 {System.Int64}; Good]
        //[#8, 885 {System.Int64}; Good]
        //[#9, 985 {System.Int64}; Good]
        //[#10, 1085 {System.Int64}; Good]
        //...
    }
}
#endregion
