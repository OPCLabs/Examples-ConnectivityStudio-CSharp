// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to receive payload with only the metrics that actually contained in the message.
//
// In order to publish or observe messages for this example, start the SparkplugEdgeNodeConsoleDemo program first.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Collections.Generic;
using OpcLabs.EasySparkplug;
using OpcLabs.EasySparkplug.OperationModel;

namespace SparkplugDocExamples.Consumer._EasySparkplugConsumer
{
    class DeliverCompleteDataSet
    {
        public static void Main1()
        {
            // Note that the default port for the "mqtt" scheme is 1883.
            var hostDescriptor = new SparkplugHostDescriptor("mqtt://localhost");

			// Instantiate the consumer object and hook events.
			var consumer = new EasySparkplugConsumer();
            consumer.PayloadNotification += consumer_Main1_PayloadNotification;

            Console.WriteLine("Subscribing...");
			consumer.SubscribePayload(
                new EasySparkplugPayloadSubscriptionArguments(
                    hostDescriptor, 
                    new SparkplugEdgeNodeDescriptor("easyGroup", "easySparkplugDemo"))
                {
                    // Specify that we do not want the component to fill in metrics that have not been sent in the message.
                    DeliverCompleteDataSet = false
                });

            Console.WriteLine("Processing notifications for 20 seconds...");
            System.Threading.Thread.Sleep(20 * 1000);

            Console.WriteLine("Unsubscribing...");
            consumer.UnsubscribeAllPayloads();

            Console.WriteLine("Waiting for 5 seconds...");
            System.Threading.Thread.Sleep(5 * 1000);

            Console.WriteLine("Finished.");
		}


		static void consumer_Main1_PayloadNotification(object sender, EasySparkplugPayloadNotificationEventArgs eventArgs)
        {
            // Handle different types of notifications.
            Console.WriteLine();
            switch (eventArgs.NotificationType)
            {
                case SparkplugNotificationType.Connect:
                    Console.WriteLine($"Connected to Sparkplug host, client ID: {eventArgs.ClientId}.");
                    break;
                case SparkplugNotificationType.Disconnect:
                    Console.WriteLine("Disconnected from Sparkplug host.");
                    break;
                case SparkplugNotificationType.Data:
                case SparkplugNotificationType.Birth:
                    Console.WriteLine("Received birth or data message from Sparkplug host.");
                    // Display the metrics name and data for each metric delivered in the payload.
                    foreach (KeyValuePair<string, SparkplugMetricElement> pair in eventArgs.Payload)
                        Console.WriteLine($"{pair.Key}: {pair.Value.MetricData}");
                    break;
                case SparkplugNotificationType.Death:
                    Console.WriteLine("Received death message from Sparkplug host.");
                    break;
            }
            if (!eventArgs.Succeeded)
                Console.WriteLine($"*** Failure: {eventArgs.ErrorMessageBrief}");
        }
	}
}
#endregion
