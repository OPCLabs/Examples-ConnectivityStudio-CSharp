// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedVariable
#region Example
// This example shows how to create a Sparkplug edge node with a single metric, start and stop it, specifying an MQTT
// client ID.
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
using OpcLabs.EasySparkplug.OperationModel;

namespace SparkplugDocExamples.EdgeNode._EasySparkplugEdgeNode
{
    partial class Start_Stop
    {
        static public void ClientId()
        {
            // The MQTT client ID can be specified in the connection descriptor. When empty (the default), the component
            // generates a unique, semi-random client ID.
            // Note that the default port for the "mqtt" scheme is 1883.
            var hostDescriptor = new SparkplugHostDescriptor("mqtt://localhost")
            {
                ConnectionDescriptor =
                {
                    ClientId = "myApplication"
                }
            };

            // Alternatively, the MQTT client ID can be specified in the broker URL using the "clientId" query parameter,
            // as below.
            var hostDescriptor2 = new SparkplugHostDescriptor("mqtt://localhost?clientId=myApplication");

            // Instantiate the edge node object and hook events.
            var edgeNode = new EasySparkplugEdgeNode(hostDescriptor,    // or hostDescriptor2, if you prefer the alternative method
                "easyGroup", "easySparkplugDemo");
            edgeNode.SystemConnectionStateChanged += edgeNode_ClientId_SystemConnectionStateChanged;

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


        static void edgeNode_ClientId_SystemConnectionStateChanged(
            object sender, 
            SparkplugConnectionStateChangedEventArgs eventArgs)
        {
            // Display the new connection state (such as when the connection to the broker succeeds or fails).
            Console.WriteLine($"{nameof(EasySparkplugEdgeNode.SystemConnectionStateChanged)}: {eventArgs}");
        }
    }
}
#endregion
