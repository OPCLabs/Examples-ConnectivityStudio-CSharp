// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to subscribe to changes of a single metric, and display the value of the metric with each change
// using a callback method that is provided as lambda expression.
//
// In order to publish or observe messages for this example, start the SparkplugEdgeNodeConsoleDemo program first.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using OpcLabs.EasySparkplug;
using System;

namespace SparkplugDocExamples.Consumer._EasySparkplugConsumer
{
    partial class SubscribeEdgeNodeMetric
    {
        public static void CallbackLambda()
        {
            // Note that the default port for the "mqtt" scheme is 1883.
            var hostDescriptor = new SparkplugHostDescriptor("mqtt://localhost");

			// Instantiate the consumer object.
			var consumer = new EasySparkplugConsumer();

            Console.WriteLine("Subscribing...");
            // The callback is a lambda expression the displays the value
            // In this example, we specify the precise Sparkplug group ID, edge node ID, and metric name.
            consumer.SubscribeEdgeNodeMetric(hostDescriptor, 
                "easyGroup", "easySparkplugDemo", "Random",
                (sender, eventArgs) =>
                {
                    if (eventArgs.Succeeded)
                    {
                        if (eventArgs.HasData)
                            Console.WriteLine("Value: {0}", eventArgs.MetricData?.Value);
                    }
                    else
                        Console.WriteLine("*** Failure: {0}", eventArgs.ErrorMessageBrief);
                });

            Console.WriteLine("Processing notifications for 20 seconds...");
            System.Threading.Thread.Sleep(20 * 1000);

            Console.WriteLine("Unsubscribing...");
            consumer.UnsubscribeAllMetrics();

            Console.WriteLine("Waiting for 5 seconds...");
            System.Threading.Thread.Sleep(5 * 1000);

            Console.WriteLine("Finished.");
		}
	}
}
#endregion
