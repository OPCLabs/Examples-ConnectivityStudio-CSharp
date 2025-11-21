// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable InconsistentNaming
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to consume Sparkplug data while specifying own Sparkplug host application ID.
//
// In order to publish or observe messages for this example, start the SparkplugEdgeNodeConsoleDemo program first.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using OpcLabs.EasySparkplug;
using OpcLabs.EasySparkplug.OperationModel;
using System;
using System.Collections.Generic;

namespace SparkplugDocExamples.Consumer._SparkplugHostDescriptor
{
    class HostId
    {
        public static void Main1()
        {
            // Note that the default port for the "mqtt" scheme is 1883.
            // The second parameter is the host ID of this Sparkplug host application. Other Sparkplug components can use
            // this host ID to detect whether the application is online or offline.
            var hostDescriptor = new SparkplugHostDescriptor("mqtt://localhost", "easyApplication");

            // Instantiate the consumer object.
            var consumer = new EasySparkplugConsumer();

            Console.WriteLine("Subscribing...");
            consumer.SubscribeEdgeNodePayload(hostDescriptor, "easyGroup", "easySparkplugDemo",
                (sender, eventArgs) =>
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
                });

            Console.WriteLine("Processing notifications for 20 seconds...");
            System.Threading.Thread.Sleep(20 * 1000);

            Console.WriteLine("Unsubscribing...");
            consumer.UnsubscribeAllPayloads();

            Console.WriteLine("Waiting for 5 seconds...");
            System.Threading.Thread.Sleep(5 * 1000);

            Console.WriteLine("Finished.");
		}
    }
}
#endregion
