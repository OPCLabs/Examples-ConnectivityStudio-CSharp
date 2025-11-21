// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable InconsistentNaming
#region Example
// This example shows how to get notified when the Sparkplug edge encounters a failure during message publishing.
//
// You can use any Sparkplug application, including our SparkplugCmd utility and the SparkplugApplicationConsoleDemo
// program, to subscribe to the edge node data. 
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Threading;
using OpcLabs.EasySparkplug;
using Timer = System.Timers.Timer;

namespace SparkplugDocExamples.EdgeNode._EasySparkplugEdgeNode
{
     class PublishingError
    {
        static public void Main1()
        {
            // Note that the default port for the "mqtt" scheme is 1883.
            var hostDescriptor = new SparkplugHostDescriptor("mqtt://localhost");

            // Instantiate the edge node object.
            var edgeNode = new EasySparkplugEdgeNode(hostDescriptor, "easyGroup", "easySparkplugDemo");

            // Configure the edge node so that we will publish data fully manually.
            edgeNode.PublishingInterval = Timeout.Infinite;
            edgeNode.ReportByException = true;
            
            // Hook the SystemConnectionStateChanged event to handle system connection state changes.
            edgeNode.SystemConnectionStateChanged += (sender, eventArgs) =>
            {
                // Display the new connection state (such as when the connection to the broker succeeds or fails).
                Console.WriteLine($"{nameof(EasySparkplugEdgeNode.SystemConnectionStateChanged)}: {eventArgs}");
            };

            // Hook the PublishingError event to handle errors that occur during publishing.
            edgeNode.PublishingError += (sender, eventArgs) =>
            {
                // Display the error that occurred.
                Console.WriteLine($"{nameof(EasySparkplugEdgeNode.PublishingError)}: {eventArgs}");
            };

            // Define a metric providing random integers.
            var random = new Random();
            SparkplugMetric myMetric = SparkplugMetric.CreateIn(edgeNode, "MyMetric").ValueType<int>();

            // Start the edge node.
            Console.WriteLine("The edge node is starting...");
            edgeNode.Start();

            Console.WriteLine("The edge node is started.");
            Console.WriteLine();

            // Create a timer for publishing the data, and start it.
            var timer = new Timer
            {
                Interval = 2*1000,  // 2 seconds
                AutoReset = true,
            };
            timer.Elapsed += (sender, eventArgs) =>
                edgeNode.PublishDataPayload(new SparkplugPayload(myMetric.Name, new SparkplugMetricData(random.Next())));
            timer.Start();

            // You can simulate a publishing error e.g. by stopping the MQTT broker or disconnecting the network cable.
            // Note that without the manual publishing, triggering the error would not be easy, as the edge node
            // automatically pauses its own publishing attempts when it detects a connection failure.
            
            // Let the user decide when to stop.
            Console.WriteLine("Press Enter to stop the edge node...");
            Console.ReadLine();
            
            // Stop the timer.
            timer.Stop();

            // Stop the edge node.
            Console.WriteLine("The edge node is stopping...");
            edgeNode.Stop();

            Console.WriteLine("The edge node is stopped.");
        }
    }
}
#endregion
