// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable UnusedVariable
#region Example
// This example show to add metrics to the Sparkplug edge node and/or device using the CreateIn method. This method combines
// creation of the metric with adding it to the parent component.
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

namespace SparkplugDocExamples.EdgeNode._SparkplugMetric
{
    class CreateIn
    {
        static public void Main1()
        {
            // Note that the default port for the "mqtt" scheme is 1883.
            var hostDescriptor = new SparkplugHostDescriptor("mqtt://localhost");

            // Instantiate the edge node object and hook events.
            var edgeNode = new EasySparkplugEdgeNode(hostDescriptor, "easyGroup", "easySparkplugDemo");
            edgeNode.SystemConnectionStateChanged += edgeNode_Main1_SystemConnectionStateChanged;

            // Create a constant metric in the edge node.
            SparkplugMetric constantMetric = SparkplugMetric.CreateIn(edgeNode, "Constant").ConstantValue("abc");

            // Create a device.
            SparkplugDevice device = SparkplugDevice.CreateIn(edgeNode, "Device");

            // Create a read/write metric ("register") in the device.
            SparkplugMetric readWriteMetric = SparkplugMetric.CreateIn(device, "ReadWrite").ReadWriteValue(0);

            // Note: You can, of course, also create the SparkplugMetric using its constructor, and add it to the Metrics
            // collection then on the edge node or device then. The CreateIn method is just a convenience shorthand for
            // this; in addition, it returns the created metric, so you can easily configure it further, and/or store it.

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
