// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable PossibleNullReferenceException
// ReSharper disable StringLiteralTypo
#region Example
// This example shows how to set parameters specific to JSON message mapping.
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
using OpcLabs.EasyOpc.UA.PubSub.Configuration;
using OpcLabs.EasyOpc.UA.PubSub.Engine;
using OpcLabs.EasyOpc.UA.PubSub.OperationModel;

namespace UASubscriberDocExamples.PubSub._EasyUASubscriber
{
    partial class SubscribeDataSet
    {
        public static void MappingParameters()
        {
            // Define the PubSub connection we will work with. Uses implicit conversion from a string.
            // Default port with MQTT is 1883.
            UAPubSubConnectionDescriptor pubSubConnectionDescriptor = "mqtt://opcua-pubsub.demo-this.com";
            // Specify the transport protocol mapping.
            // The statement below isn't actually necessary, due to automatic message mapping recognition feature; see
            // https://kb.opclabs.com/OPC_UA_PubSub_Automatic_Message_Mapping_Recognition for more details.
            pubSubConnectionDescriptor.TransportProfileUriString = UAPubSubTransportProfileUriStrings.MqttJson;

            // Set a custom property on the PubSub connection that influences how the JSON parsing works. 
            // We are instructing the message parser to turn off the automatic recognition of message format.
            // For more details, see https://kb.opclabs.com/OPC_UA_PubSub_JSON_mapping_component .
            pubSubConnectionDescriptor.CustomPropertyValueDictionary[new UAQualifiedName("{OpcLabs}",
                "OpcLabs.EasyOpc.UA.Toolkit.PubSub.Sdk.JsonReceiveMessageMapping.MessageParsingParameters.AutoRecognizeMessageFormat")] =
                false;

            // Define the arguments for subscribing to the dataset.
            var subscribeDataSetArguments = new UASubscribeDataSetArguments(pubSubConnectionDescriptor)
            {
                DataSetSubscriptionDescriptor =
                {
                    CommunicationParameters =
                    {
                        BrokerDataSetReaderTransportParameters = {QueueName = "opcuademo/json"},
                        // We must set the DataSetFieldContentMask when the format auto-recognition is turned off.
                        DataSetFieldContentMask = UADataSetFieldContentMask.RawData,
                        JsonDataSetReaderMessageParameters =
                        {
                            // We must set the DataSetMessageContentMask when the format auto-recognition is turned off.
                            DataSetMessageContentMask =
                                UAJsonDataSetMessageContentMask.DataSetWriterId |
                                UAJsonDataSetMessageContentMask.SequenceNumber |
                                UAJsonDataSetMessageContentMask.Status,
                            // We must set the NetworkMessageContentMask when the format auto-recognition is turned off.
                            NetworkMessageContentMask =
                                UAJsonNetworkMessageContentMask.NetworkMessageHeader |
                                UAJsonNetworkMessageContentMask.DataSetMessageHeader |
                                UAJsonNetworkMessageContentMask.PublisherId
                        }
                    },
                    Filter =
                    {
                        DataSetWriterDescriptor = 1,
                    }
                }
            };

            // Instantiate the subscriber object and hook events.
            var subscriber = new EasyUASubscriber();
            subscriber.DataSetMessage += subscriber_DataSetMessage_MappingParameters;

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

        static void subscriber_DataSetMessage_MappingParameters(object sender, EasyUADataSetMessageEventArgs e)
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
        //Dataset data: Good; Data; publisher=[String]30, writer=1, fields: 4
        //[BoolToggle, True {System.Boolean}; Good]
        //[Int32, 9034 {System.Int64}; Good]
        //[Int32Fast, 1751 {System.Int64}; Good]
        //[DateTime, 1/30/2020 5:23:11 PM {System.DateTime}; Good]
        //
        //Dataset data: Good; Data; publisher=[String]30, writer=1, fields: 4
        //[BoolToggle, True {System.Boolean}; Good]
        //[Int32, 9036 {System.Int64}; Good]
        //[Int32Fast, 2526 {System.Int64}; Good]
        //[DateTime, 1/30/2020 5:23:13 PM {System.DateTime}; Good]
        //...
    }
}
#endregion
