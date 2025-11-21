// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable InconsistentNaming
#region Example
// This example shows the edge node publishing can be controlled by its primary host application
//
// You can use any Sparkplug application, including our SparkplugCmd utility and the SparkplugApplicationConsoleDemo
// program, to subscribe to the edge node data. 
// CAUTION: For this example, the application must be configured as a primary host application with the host ID
// "easyApplication".
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasySparkplug;

namespace SparkplugDocExamples.EdgeNode._EasySparkplugEdgeNode
{
     class PrimaryHostId
    {
        static public void Main1()
        {
            // Note that the default port for the "mqtt" scheme is 1883.
            var hostDescriptor = new SparkplugHostDescriptor("mqtt://localhost");

            // Instantiate the edge node object.
            var edgeNode = new EasySparkplugEdgeNode(hostDescriptor, "easyGroup", "easySparkplugDemo");

            // Configure the edge node with the host ID of its primary host application.
            // The primary host application must be configured with this host ID.
            // Note that it is also possible to use a constructor overload with a primary host ID parameter.
            edgeNode.PrimaryHostId = "easyApplication";

            // Hook the SystemConnectionStateChanged event to handle system connection state changes.
            edgeNode.SystemConnectionStateChanged += (sender, eventArgs) =>
            {
                // Display the new connection state (such as when the connection to the broker succeeds or fails).
                Console.WriteLine($"{nameof(EasySparkplugEdgeNode.SystemConnectionStateChanged)}: {eventArgs}");
            };

            // Hook the ApplicationOnlineChanged event to handle changes in the online state of the primary host
            // application.
            edgeNode.ApplicationOnlineChanged += (sender, eventArgs) =>
            {
                // The edge node will publish data only when the primary host application is online.
                Console.WriteLine($"{nameof(EasySparkplugEdgeNode.ApplicationOnlineChanged)}: {edgeNode.ApplicationOnline}");
            };
            
            // Define a metric on the edge node providing random integers.
            var random = new Random();
            edgeNode.Metrics.Add(new SparkplugMetric("MyMetric1").ReadValueFunction(() => random.Next()));

            // Create a device.
            SparkplugDevice device = SparkplugDevice.CreateIn(edgeNode, "Device");

            // Define a metric on the device providing random integers.
            device.Metrics.Add(new SparkplugMetric("MyMetric2").ReadValueFunction(() => random.Next()));

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
