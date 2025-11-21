// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to subscribe to changes of multiple metrics and display each change, identifying the different
// subscriptions by an integer.
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
    class SubscribeMetric
    {
        public static void StateAsInteger()
        {
            // Note that the default port for the "mqtt" scheme is 1883.
            var hostDescriptor = new SparkplugHostDescriptor("mqtt://localhost");

			// Instantiate the consumer object and hook events.
			var consumer = new EasySparkplugConsumer();
            consumer.MetricNotification += consumer_StateAsInteger_MetricNotification;

            Console.WriteLine("Subscribing...");
            consumer.SubscribeMetric(
                new EasySparkplugMetricSubscriptionArguments(SparkplugComponentTypes.EdgeNode, hostDescriptor, 
                    "easyGroup", "easySparkplugDemo", "", "Random", null)
                    { State = 1 });    // An integer we have chosen to identify the subscription
            consumer.SubscribeMetric(
                new EasySparkplugMetricSubscriptionArguments(SparkplugComponentTypes.EdgeNode, hostDescriptor,
                        "easyGroup", "easySparkplugDemo", "", "Simple", null)
                    { State = 2 });    // An integer we have chosen to identify the subscription
            consumer.SubscribeMetric(
                new EasySparkplugMetricSubscriptionArguments(SparkplugComponentTypes.EdgeNode, hostDescriptor,
                        "easyGroup", "easySparkplugDemo", "", "Ramp", null)
                    { State = 3 });    // An integer we have chosen to identify the subscription

            Console.WriteLine("Processing notifications for 20 seconds...");
            System.Threading.Thread.Sleep(20 * 1000);

            Console.WriteLine("Unsubscribing...");
            consumer.UnsubscribeAllMetrics();

            Console.WriteLine("Waiting for 5 seconds...");
            System.Threading.Thread.Sleep(5 * 1000);

            Console.WriteLine("Finished.");
		}


		static void consumer_StateAsInteger_MetricNotification(object sender, EasySparkplugMetricNotificationEventArgs eventArgs)
        {
            // Obtain the integer state we have passed in.
            // Note that the metric name also comes with the notification and can be used to determine which metric the
            // notification relates to. The reason we are using the state is that it allows to pass in the information that
            // your application understands immediately, and is thus more efficient.
            var stateAsInteger = (int)eventArgs.Arguments.State;

            // Display the data.
            if (eventArgs.Succeeded)
            {
                if (eventArgs.HasData)
                    Console.WriteLine($"{stateAsInteger}: {eventArgs.MetricData}");
            }
            else
                Console.WriteLine($"{stateAsInteger} *** Failure: {eventArgs.ErrorMessageBrief}");
        }
    }
}
#endregion
