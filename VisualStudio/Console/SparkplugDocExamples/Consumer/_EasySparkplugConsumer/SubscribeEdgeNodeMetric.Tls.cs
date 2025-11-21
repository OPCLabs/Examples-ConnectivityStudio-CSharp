// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable CommentTypo
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to subscribe to all metrics of a given edge node using TLS.
//
// In order to publish or observe messages for this example, start the SparkplugEdgeNodeConsoleDemo program first.
// The MQTT broker may need additional configuration to support TLS connections, such as enabling the TLS listener.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.BaseLib;
using OpcLabs.EasySparkplug;
using OpcLabs.EasySparkplug.OperationModel;
using OpcLabs.EasySparkplug.System;

namespace SparkplugDocExamples.Consumer._EasySparkplugConsumer
{
    partial class SubscribeEdgeNodeMetric
    {
        public static void Tls()
        {
            // The TLS protocol can be specified in the broker URL using the "mqtts", "ssl" or "tls" scheme, as below.
            // (the schemes are equivalent). Default port is 8883.
            var hostDescriptor = new SparkplugHostDescriptor("mqtts://localhost");

            // Enable the console interaction. The interactive user will then be able to validate remote certificates and/or
            // specify local certificate(s) to use.
            ComponentParameters componentParameters = EasySparkplugInfrastructure.Instance.Parameters;
            componentParameters.PluginSetups.FindName("ConsoleInteraction").Enabled = true;

            // By default, the local certificates, if needed, are provided by parameterized querying of certificate stores.
            // The statement below disables this, and the local certificate(s) will be specified interactively by the user.
            // For more information, see https://kb.opclabs.com/Certificate_security_plugin .
            //componentParameters.PluginConfigurations.Find<CertificateSecurityParameters>().AllowStatic = false;

            // Instantiate the consumer object and hook events.
            var consumer = new EasySparkplugConsumer();
            consumer.MetricNotification += consumer_Tls_MetricNotification;

            Console.WriteLine("Subscribing...");
            // In this example, we specify the precise Sparkplug group ID and edge node ID, but allow any metric name.
			consumer.SubscribeEdgeNodeMetric(hostDescriptor,
                "easyGroup", "easySparkplugDemo", "#");

            Console.WriteLine("Processing notifications for 20 seconds...");
            System.Threading.Thread.Sleep(20 * 1000);

            Console.WriteLine("Unsubscribing...");
            consumer.UnsubscribeAllMetrics();

            Console.WriteLine("Waiting for 5 seconds...");
            System.Threading.Thread.Sleep(5 * 1000);

            Console.WriteLine("Finished.");
		}


		static void consumer_Tls_MetricNotification(object sender, EasySparkplugMetricNotificationEventArgs eventArgs)
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
