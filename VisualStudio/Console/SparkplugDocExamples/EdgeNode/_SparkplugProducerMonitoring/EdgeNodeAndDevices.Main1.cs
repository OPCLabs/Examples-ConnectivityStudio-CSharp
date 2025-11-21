// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

#region Example
// This example shows how to monitor birth, death, and rebirth of a Sparkplug edge node and its devices.
//
// You can use any Sparkplug application, including our SparkplugCmd utility and the SparkplugApplicationConsoleDemo
// program, to subscribe to the edge node data. 
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using Microsoft.Extensions.DependencyInjection;
using OpcLabs.EasySparkplug;
using System;
using OpcLabs.EasySparkplug.Services;

namespace SparkplugDocExamples.EdgeNode._SparkplugProducerMonitoring
{
    class EdgeNodeAndDevices
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

            // Define a metric providing random integers.
            var random = new Random();
            SparkplugMetric.CreateIn(edgeNode, "MyMetric").ReadValueFunction(() => random.Next());

            // Define two devices, each with a single metric providing random integers.
            SparkplugDevice myDevice1 = SparkplugDevice.CreateIn(edgeNode, "MyDevice1");
            SparkplugMetric.CreateIn(myDevice1, "MyMetric1").ReadValueFunction(() => random.Next());
            SparkplugDevice myDevice2 = SparkplugDevice.CreateIn(edgeNode, "MyDevice2");
            SparkplugMetric.CreateIn(myDevice2, "MyMetric2").ReadValueFunction(() => random.Next());

            // Obtain monitoring services and hook events to them.
            ISparkplugProducerMonitoring edgeNodeMonitoring = edgeNode.GetService<ISparkplugProducerMonitoring>();
            if (!(edgeNodeMonitoring is null))
            {
                // Monitor the edge node itself.
                edgeNodeMonitoring.Birth += (sender, eventArgs) =>
                    Console.WriteLine($"{sender}.{nameof(edgeNodeMonitoring.Birth)}");
                edgeNodeMonitoring.Death += (sender, eventArgs) =>
                    Console.WriteLine($"{sender}.{nameof(edgeNodeMonitoring.Death)}");
                edgeNodeMonitoring.Rebirth += (sender, eventArgs) =>
                    Console.WriteLine($"{sender}.{nameof(edgeNodeMonitoring.Rebirth)}");

                // Monitor all devices in the edge node.
                foreach (SparkplugDevice device in edgeNode.Devices)
                {
                    ISparkplugProducerMonitoring deviceMonitoring = device.GetService<ISparkplugProducerMonitoring>();
                    if (!(deviceMonitoring is null))
                    {
                        deviceMonitoring.Birth += (sender, eventArgs) =>
                            Console.WriteLine($"{sender}.{nameof(deviceMonitoring.Birth)}");
                        deviceMonitoring.Death += (sender, eventArgs) =>
                            Console.WriteLine($"{sender}.{nameof(deviceMonitoring.Death)}");
                        deviceMonitoring.Rebirth += (sender, eventArgs) =>
                            Console.WriteLine($"{sender}.{nameof(deviceMonitoring.Rebirth)}");
                    }
                }
            }

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
