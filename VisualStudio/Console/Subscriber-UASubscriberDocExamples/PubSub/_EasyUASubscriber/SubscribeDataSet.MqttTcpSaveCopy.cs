// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable PossibleNullReferenceException
// ReSharper disable StringLiteralTypo
#region Example
// This example shows how to subscribe to all dataset messages on an OPC-UA PubSub connection with MQTT (JSON or UADP
// mapping) using TCP, and store a copy of all received messages into a file system.
//
// A related example (SubscribeDataSet.MqttFromFileStorage) shows hot the captured messages can be replayed from the file
// system, e.g. for troubleshooting.
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
using OpcLabs.EasyOpc.UA.AddressSpace;
using OpcLabs.EasyOpc.UA.PubSub;
using OpcLabs.EasyOpc.UA.PubSub.OperationModel;

namespace UASubscriberDocExamples.PubSub._EasyUASubscriber
{
    partial class SubscribeDataSet
    {
        public static void MqttTcpSaveCopy()
        {
            // Define the PubSub connection we will work with. Uses implicit conversion from a string.
            // Default port with MQTT is 1883.
            UAPubSubConnectionDescriptor pubSubConnectionDescriptor = "mqtt://opcua-pubsub.demo-this.com";
            // Configure the PubSub connection so that a copy of the received messages is stored into the file system, under
            // the specified "root" directory.
            pubSubConnectionDescriptor.CustomPropertyValueDictionary[
                new UAQualifiedName("{OpcLabs}", "MqttFileCopyReceivedRoot")] = @"C:\MqttReceived";

            // Define the arguments for subscribing to the dataset, specifying the MQTT topic name.
            var subscribeDataSetArguments = new UASubscribeDataSetArguments(pubSubConnectionDescriptor)
            {
                DataSetSubscriptionDescriptor = {CommunicationParameters = {BrokerDataSetReaderTransportParameters =
                {
                    QueueName = "opcuademo/#"
                }}}
            };

            // Instantiate the subscriber object and hook events.
            var subscriber = new EasyUASubscriber();
            subscriber.DataSetMessage += subscriber_DataSetMessage_MqttTcpSaveCopy;

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

        static void subscriber_DataSetMessage_MqttTcpSaveCopy(object sender, EasyUADataSetMessageEventArgs e)
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
    }
}
#endregion
