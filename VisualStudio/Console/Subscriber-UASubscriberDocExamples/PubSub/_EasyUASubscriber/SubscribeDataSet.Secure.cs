// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

#pragma warning disable IDE1006 // Naming Styles
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to securely subscribe to signed and encrypted dataset messages.
// An external Security Key Service (SKS) is needed (not a part of QuickOPC).
//
// The network messages for this example can be published e.g. using the UADemoPublisher tool - see
// https://kb.opclabs.com/How_to_publish_or_subscribe_to_secure_OPC_UA_PubSub_messages .
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Collections.Generic;
using System.Threading;
using OpcLabs.EasyOpc.UA.Engine;
using OpcLabs.EasyOpc.UA.PubSub;
using OpcLabs.EasyOpc.UA.PubSub.OperationModel;

namespace UASubscriberDocExamples.PubSub._EasyUASubscriber
{
    partial class SubscribeDataSet
    {
        public static void Secure()
        {
            // Define the PubSub connection we will work with. Uses implicit conversion from a string.
            UAPubSubConnectionDescriptor pubSubConnectionDescriptor = "opc.udp://239.0.0.1";
            // In some cases you may have to set the interface (network adapter) name that needs to be used, similarly to
            // the statement below. Your actual interface name may differ, of course.
            //pubSubConnectionDescriptor.ResourceAddress.InterfaceName = "Ethernet";

            // Define the arguments for subscribing to the dataset.
            var subscribeDataSetArguments = new UASubscribeDataSetArguments(pubSubConnectionDescriptor)
            {
                DataSetSubscriptionDescriptor =
                {
                    CommunicationParameters =
                    {
                        // Specifies the security mode for the PubSub network messages received. This is a minimum security
                        // mode that you want to accept.
                        SecurityMode = UAMessageSecurityModes.SecuritySignAndEncrypt,
                        SecurityKeyServiceTemplate =
                        {
                            // Specifies the URL of the SKS (Security Key Service) endpoint.
                            UrlString = "opc.tcp://localhost:48010", 
                            // Specifies the security mode that will be used to connect to the SKS.
                            EndpointSelectionPolicy = UAMessageSecurityModes.SecuritySignAndEncrypt,
                            // Specifies the user name and password used for "logging in" to the SKS.
                            UserIdentity = { UserNameTokenInfo = { UserName = "root", Password = "secret" }}
                        },
                        // Specifies the Id of the security group in the SKS that will be used (the security group in the
                        // SKS is configured to use certain security policy, and has other parameters detailing how the
                        // security keys are generated).
                        SecurityGroupId = "TestGroup"
                    }
                }
            };

            // Instantiate the subscriber object and hook events.
            var subscriber = new EasyUASubscriber();
            subscriber.DataSetMessage += subscriber_DataSetMessage_Secure;

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

        static void subscriber_DataSetMessage_Secure(object sender, EasyUADataSetMessageEventArgs e)
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
    }
}
#endregion
