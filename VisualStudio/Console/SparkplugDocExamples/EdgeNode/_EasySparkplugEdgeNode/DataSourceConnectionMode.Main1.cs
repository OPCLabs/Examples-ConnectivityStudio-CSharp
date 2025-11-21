// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable InconsistentNaming
#region Example
// This example shows how to connect to the data source only when the producer is online.
//
// You can use any Sparkplug application, including our SparkplugCmd utility and the SparkplugApplicationConsoleDemo
// program, to subscribe to the edge node data. 
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasySparkplug;

namespace SparkplugDocExamples.EdgeNode._EasySparkplugEdgeNode
{
     class DataSourceConnectionMode
    {
        static public void Main1()
        {
            // Note that the default port for the "mqtt" scheme is 1883.
            var hostDescriptor = new SparkplugHostDescriptor("mqtt://localhost");

            // Instantiate the edge node object.
            var edgeNode = new EasySparkplugEdgeNode(hostDescriptor, "easyGroup", "easySparkplugDemo");

            // Configure the edge node to connect to the data source only when the producer is online. You can test it out
            // e.g. by stopping or disconnecting from the MQTT broker.
            //
            // You can compare the output of this example with and without the statement below. With the statement, the
            // Poll event will not be raised unless the producer is online. Without the statement, the Poll event will
            // always be raised while the edge node is started, regardless of the MQTT broker connection state.
            edgeNode.DataSourceConnectionMode = SparkplugDataSourceConnectionMode.WhenProducerOnline;

            // Hook the SystemConnectionStateChanged event to handle system connection state changes.
            edgeNode.SystemConnectionStateChanged += (sender, eventArgs) =>
            {
                // Display the new connection state (such as when the connection to the broker succeeds or fails).
                Console.WriteLine($"{nameof(EasySparkplugEdgeNode.SystemConnectionStateChanged)}: {eventArgs}");
            };

            // Hook the ProducerOnlineChanged event to handle changes in the online state of the producer.
            edgeNode.ProducerOnlineChanged += (sender, eventArgs) =>
            {
                // Display the new producer online state.
                Console.WriteLine($"{nameof(EasySparkplugEdgeNode.ProducerOnlineChanged)}: {edgeNode.ProducerOnline}");
            };

            // Hook the Poll event (but do not mark the polling as processed by ourselves).
            edgeNode.Poll += (sender, eventArgs) =>
            {
                // Display when the component is polling for new data.
                Console.WriteLine(nameof(EasySparkplugEdgeNode.Poll));
            };

            // Define a metric providing random integers.
            var random = new Random();
            edgeNode.Metrics.Add(new SparkplugMetric("MyMetric").ReadValueFunction(() => random.Next()));

            // Start the edge node.
            Console.WriteLine("The edge node is starting...");
            edgeNode.Start();

            Console.WriteLine("The edge node is started.");
            Console.WriteLine();

            // Let the user decide when to stop.
            Console.WriteLine("Press Enter to stop the edge node...");
            Console.ReadLine();

            // Stop the edge node.
            Console.WriteLine("The edge node is stopping...");
            edgeNode.Stop();

            Console.WriteLine("The edge node is stopped.");
        }
    }
}
#endregion
