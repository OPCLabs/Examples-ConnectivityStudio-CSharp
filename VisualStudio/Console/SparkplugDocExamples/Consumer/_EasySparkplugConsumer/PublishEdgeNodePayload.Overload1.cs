// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to publish a payload with multiple command metrics for a given edge node.
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
    class PublishEdgeNodePayload
    {
        public static void Overload1()
        {
            // Note that the default port for the "mqtt" scheme is 1883.
            var hostDescriptor = new SparkplugHostDescriptor("mqtt://localhost");

			// Instantiate the consumer object.
			var consumer = new EasySparkplugConsumer();

            // Create the payload with multiple command metrics.
            var payload = new SparkplugPayload
            {
                { "Simple", new SparkplugMetricData(42) },
                { "Simple2", new SparkplugMetricData(43) }
            };
            
            Console.WriteLine("Publishing...");
            try
            {
                consumer.PublishEdgeNodePayload(hostDescriptor, "easyGroup", "easySparkplugDemo", payload);
            }
            catch (SparkplugException sparkplugException)
            {
                Console.WriteLine($"*** Failure: {sparkplugException.GetBaseException().Message}");
                return;
            }

            Console.WriteLine("Finished.");
		}
	}
}
#endregion
