// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to subscribe to all metrics of a given edge node with extremely simple code.
//
// In order to publish or observe messages for this example, start the SparkplugEdgeNodeConsoleDemo program first.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasySparkplug;

namespace SparkplugDocExamples.Consumer._EasySparkplugConsumer
{
    partial class SubscribeEdgeNodeMetric
    {
        public static void Simplest()
        {
			var consumer = new EasySparkplugConsumer();
			consumer.SubscribeEdgeNodeMetric(new SparkplugHostDescriptor("mqtt://localhost"), 
                "easyGroup", "easySparkplugDemo", "#", 
                (sender, args) => Console.WriteLine(args));

            System.Threading.Thread.Sleep(20 * 1000);
            consumer.UnsubscribeAllMetrics();
		}
	}
}
#endregion
