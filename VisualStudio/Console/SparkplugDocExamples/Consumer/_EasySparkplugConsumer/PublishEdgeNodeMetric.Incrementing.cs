// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to send an ever-incrementing value to a Sparkplug metric.
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
using System.Threading;

namespace SparkplugDocExamples.Consumer._EasySparkplugConsumer
{
    partial class PublishEdgeNodeMetric
    {
        public static void Incrementing()
        {
            // Note that the default port for the "mqtt" scheme is 1883.
            var hostDescriptor = new SparkplugHostDescriptor("mqtt://localhost");

			// Instantiate the consumer object.
			var consumer = new EasySparkplugConsumer();

            //
            Console.WriteLine("Publishing... (press any key to stop)");
            int i = 0;

            do
            {
                Console.WriteLine($"@{DateTime.Now}: Publishing {i}");
                try
                {
                    consumer.PublishEdgeNodeMetric(hostDescriptor,
                        "easyGroup", "easySparkplugDemo", "Simple",
                        new SparkplugMetricData(i));   // the command metric value
                }
                catch (SparkplugException sparkplugException)
                {
                    Console.WriteLine($"*** Failure: {sparkplugException.GetBaseException().Message}");
                    return;
                }
                i = unchecked((i + 1) & 0x7FFFFFFF);
                Thread.Sleep(2 * 1000);
            } while (!Console.KeyAvailable);

            Console.WriteLine("Finished.");
		}
	}
}
#endregion
