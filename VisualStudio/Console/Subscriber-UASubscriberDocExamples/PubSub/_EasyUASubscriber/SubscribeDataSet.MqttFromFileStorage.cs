// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable PossibleNullReferenceException
// ReSharper disable StringLiteralTypo
#region Example
// This example shows how to subscribe to MQTT dataset messages stored in a file system. This can be used e.g. for
// troubleshooting.
//
// A related example (SubscribeDataSet.MqttTcpSaveCopy) shows how to capture the MQTT messages into the file system.
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
using OpcLabs.EasyOpc.UA.PubSub.OperationModel;

namespace UASubscriberDocExamples.PubSub._EasyUASubscriber
{
    partial class SubscribeDataSet
    {
        public static void MqttFromFileStorage()
        {
            // Define the PubSub connection we will work with. By specifying a file (file URI) with the directory path, MQTT
            // messages will be provided from the file storage.
            UAPubSubConnectionDescriptor pubSubConnectionDescriptor = @"C:\MqttReceived";

            // Define the arguments for subscribing to the dataset, specifying the MQTT topic name.
            var subscribeDataSetArguments = new UASubscribeDataSetArguments(pubSubConnectionDescriptor)
            {
                DataSetSubscriptionDescriptor = {CommunicationParameters = {BrokerDataSetReaderTransportParameters =
                {
                    QueueName = "opcuademo/json/#"
                }}}
            };

            // Instantiate the subscriber object and hook events.
            var subscriber = new EasyUASubscriber();
            subscriber.DataSetMessage += subscriber_DataSetMessage_MqttFromFileStorage;

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

        static void subscriber_DataSetMessage_MqttFromFileStorage(object sender, EasyUADataSetMessageEventArgs e)
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
