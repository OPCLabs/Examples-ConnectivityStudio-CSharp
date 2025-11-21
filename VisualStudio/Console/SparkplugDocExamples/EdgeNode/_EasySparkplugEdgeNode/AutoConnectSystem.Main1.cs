// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable InconsistentNaming
#region Example
// This example shows how to connect to and disconnect from a Sparkplug system manually, without automatic connection on
// start.
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
     class AutoConnectSystem
    {
        static public void Main1()
        {
            // Note that the default port for the "mqtt" scheme is 1883.
            var hostDescriptor = new SparkplugHostDescriptor("mqtt://localhost");

            // Instantiate the edge node object.
            var edgeNode = new EasySparkplugEdgeNode(hostDescriptor, "easyGroup", "easySparkplugDemo");

            // Configure the edge node so that we will connect to and disconnect from the Sparkplug system manually.
            edgeNode.AutoConnectSystem = false;
            
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
            edgeNode.Metrics.Add(new SparkplugMetric("MyMetric").ReadValueFunction(() => random.Next()));

            // Start the edge node.
            Console.WriteLine("The edge node is starting...");
            edgeNode.Start();

            // Obviously, since the edge node is already started and the data collection (polling) is used, there will be
            // publishing errors at this point, until we instruct the edge node to connect to the Sparkplug system.

            Console.WriteLine("The edge node is started.");
            Console.WriteLine();

            // Let the user decide when to connect.
            Console.WriteLine("Press Enter to connect...");
            Console.ReadLine();
            edgeNode.PerformConnectSystem();

            // The publishing errors will stop here (if the connection to the broker can be made), and the edge node will
            // start publishing data.

            // Let the user decide when to disconnect.
            Console.WriteLine("Press Enter to disconnect...");
            Console.ReadLine();
            edgeNode.PerformDisconnectSystem();

            // The publishing errors will resume here.

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
