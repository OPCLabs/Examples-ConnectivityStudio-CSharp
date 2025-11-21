// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable PossibleNullReferenceException
// ReSharper disable UnusedVariable
#region Example
// This example shows how to subscribe to all metrics of a given edge node using MQTT version 5.0.
//
// In order to publish or observe messages for this example, start the SparkplugEdgeNodeConsoleDemo program first.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasySparkplug;
using OpcLabs.EasySparkplug.OperationModel;

namespace SparkplugDocExamples.Consumer._EasySparkplugConsumer
{
    partial class SubscribeEdgeNodeMetric
    {
        public static void Mqtt5()
        {
            // The MQTT protocol version can be specified in the broker URL using the "version" query parameter, as below.
            // Possible values are 310, 311, and 500, which correspond to MQTT 3.1, MQTT 3.1.1, and MQTT 5.0, respectively.
            // The default is MQTT 3.1.1.
            // Note that the default port for the "mqtt" scheme is 1883.
            var hostDescriptor = new SparkplugHostDescriptor("mqtt://localhost?version=500");

            // Alternatively, if you cannot or do not want to manipulate the broker URL, set a custom property in the
            // broker descriptor, as below. This method is subject to change with the implementation of the Sparkplug
            // component provider. Use value 3 for MQTT 3.1, 4 for MQTT 3.1.1, and 5 for MQTT 5.0.
            var brokerDescriptor = new SparkplugBrokerDescriptor("mqtt://localhost")
                {
                    CustomPropertyValueDictionary =
                    {
                        ["NetSparkplugComponentProvider.MqttClientOptions.ProtocolVersion"] = 5
                    }
                };
            // Create the host descriptor for the alternative method.
            var hostDescriptor2 = new SparkplugHostDescriptor(brokerDescriptor);
            
            // Instantiate the consumer object and hook events.
            var consumer = new EasySparkplugConsumer();
            consumer.MetricNotification += consumer_Mqtt5_MetricNotification;

            Console.WriteLine("Subscribing...");
            // In this example, we specify the precise Sparkplug group ID and edge node ID, but allow any metric name.
			consumer.SubscribeEdgeNodeMetric(hostDescriptor,    // or hostDescriptor2, if you prefer the alternative method
                "easyGroup", "easySparkplugDemo", "#");

            Console.WriteLine("Processing notifications for 20 seconds...");
            System.Threading.Thread.Sleep(20 * 1000);

            Console.WriteLine("Unsubscribing...");
            consumer.UnsubscribeAllMetrics();

            Console.WriteLine("Waiting for 5 seconds...");
            System.Threading.Thread.Sleep(5 * 1000);

            Console.WriteLine("Finished.");
		}


		static void consumer_Mqtt5_MetricNotification(object sender, EasySparkplugMetricNotificationEventArgs eventArgs)
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
                    Console.WriteLine("Received data from Sparkplug host.");
                    Console.WriteLine($"Metric name: {eventArgs.MetricName}");
                    Console.WriteLine($"Value: {eventArgs.MetricData?.Value}");
                    break;
                case SparkplugNotificationType.Birth:
                    Console.WriteLine("Received birth message from Sparkplug host.");
                    Console.WriteLine($"Metric name: {eventArgs.MetricName}");
                    Console.WriteLine($"Value: {eventArgs.MetricData?.Value}");
                    break;
                case SparkplugNotificationType.Death:
                    Console.WriteLine("Received death message from Sparkplug host.");
                    Console.WriteLine($"Metric name: {eventArgs.MetricName}");
                    break;
            }
            if (!eventArgs.Succeeded)
                Console.WriteLine($"*** Failure: {eventArgs.ErrorMessageBrief}");
        }
	}
}
#endregion
