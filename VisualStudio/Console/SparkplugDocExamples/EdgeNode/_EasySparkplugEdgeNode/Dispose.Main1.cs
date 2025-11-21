// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

#region Example
// This example shows how to use the IDisposable interface to automatically stop the Sparkplug edge node.
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
    class Dispose
    {
        static public void Main1()
        {
            // Note that the default port for the "mqtt" scheme is 1883.
            var hostDescriptor = new SparkplugHostDescriptor("mqtt://localhost");

            // Instantiate the edge node object.
            // The "using" statement ensures disposal of the resource it acquires.
            using (var edgeNode = new EasySparkplugEdgeNode(hostDescriptor, "easyGroup", "easySparkplugDemo"))
            {
                // Hook events.
                edgeNode.SystemConnectionStateChanged += edgeNode_Main1_SystemConnectionStateChanged;

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

                // The IDisposable.Dispose call (automatically made at the end of the "using" statement) stops the
                // EasySparkplugEdgeNode if it is started.
                Console.WriteLine("The edge node is stopping...");
            }
            Console.WriteLine("The edge node is stopped.");
        }


        static void edgeNode_Main1_SystemConnectionStateChanged(
            object sender, 
            SparkplugConnectionStateChangedEventArgs eventArgs)
        {
            // Display the new connection state (such as when the connection to the broker succeeds or fails).
            Console.WriteLine($"{nameof(EasySparkplugEdgeNode.SystemConnectionStateChanged)}: {eventArgs}");
        }
    }
}
#endregion
