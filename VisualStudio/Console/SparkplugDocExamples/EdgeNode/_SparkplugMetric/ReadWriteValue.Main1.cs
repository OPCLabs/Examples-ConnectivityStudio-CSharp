// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

#region Example
// This example shows how to create a read-write metric that behaves like a register, specifying initial value.
//
// You can use any Sparkplug application, including our SparkplugCmd utility and the SparkplugApplicationConsoleDemo
// program, to subscribe to the edge node data. SparkplugCmd, or other capable Sparkplug application, can be used to write
// data into the metric.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using OpcLabs.EasySparkplug;
using System;

namespace SparkplugDocExamples.EdgeNode._SparkplugMetric
{
    partial class ReadWriteValue
    {
        static public void Main1()
        {
            // Note that the default port for the "mqtt" scheme is 1883.
            var hostDescriptor = new SparkplugHostDescriptor("mqtt://localhost");

            // Instantiate the edge node object and hook events.
            var edgeNode = new EasySparkplugEdgeNode(hostDescriptor, "easyGroup", "easySparkplugDemo");
            edgeNode.SystemConnectionStateChanged += (sender, eventArgs) =>
            {
                // Display the new connection state (such as when the connection to the broker succeeds or fails).
                Console.WriteLine($"{nameof(EasySparkplugEdgeNode.SystemConnectionStateChanged)}: {eventArgs}");
            };

            // Create a metric that is a read-write register, defining its initial value.
            // The type of the metric (Int32, in this case) is inferred from the initial value.
            edgeNode.Add(new SparkplugMetric("MyMetric").ReadWriteValue(42));

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
