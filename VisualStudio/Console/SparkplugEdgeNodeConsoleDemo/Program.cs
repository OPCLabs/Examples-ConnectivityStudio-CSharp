// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable AccessToDisposedClosure
// ReSharper disable ArrangeModifiersOrder
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable PossibleNullReferenceException
#region Example
// A fully functional Sparkplug edge node running in a console host.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client, server and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using OpcLabs.BaseLib;
using OpcLabs.BaseLib.Collections.Generic.Extensions;
using OpcLabs.EasySparkplug;
using OpcLabs.EasySparkplug.Services;
using OpcLabs.EasySparkplug.System;
using SparkplugEdgeNodeDemoLibrary;

namespace SparkplugEdgeNodeConsoleDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("EasySparkplug Edge Node Console Demo");
            Console.WriteLine();

            // Parse command line arguments for broker URL, group ID, edge node ID, and primary host ID.

            string brokerUrlString = "mqtt://localhost";
            if (args.Length >= 1)
                brokerUrlString = args[0];

            string groupId = "easyGroup";
            if (args.Length >= 2)
                groupId = args[1];

            string edgeNodeId = "easySparkplugDemo";
            if (args.Length >= 3)
                edgeNodeId = args[2];

            string primaryHostId = "";  // we use "easyApplication" in some examples
            if (args.Length >= 4)
                primaryHostId = args[3];

            Console.WriteLine($"Broker URL: {brokerUrlString}");
            Console.WriteLine($"Group ID: {groupId}");
            Console.WriteLine($"Edge node ID: {edgeNodeId}");
            Console.WriteLine($"Primary host ID: {primaryHostId}");
            Console.WriteLine();

            // Enable the console interaction by the component. The interactive user will then be able to validate remote
            // certificates and/or specify local certificate(s) to use.
            ComponentParameters componentParameters = EasySparkplugInfrastructure.Instance.Parameters;
            componentParameters.PluginSetups.FindName("ConsoleInteraction").Enabled = true;

            // Instantiate the edge node object.
            using (var edgeNode = new EasySparkplugEdgeNode(brokerUrlString, groupId, edgeNodeId))
            {
                // Configure the primary host ID the edge node will use.
                // Leave the primary host ID empty if the edge node is not serving a primary host.
                edgeNode.PrimaryHostId = primaryHostId;

                // Add metrics from the demo library to the edge node.
                edgeNode.Metrics.AddRange(DemoMetrics.Create());

                // Add devices to the edge node, with metrics from the demo library.
                edgeNode.Devices.Add(new SparkplugDevice("data", DataMetrics.Create()));
                edgeNode.Devices.Add(new SparkplugDevice("console", ConsoleMetrics.Create()));
                edgeNode.Devices.Add(new SparkplugDevice("demo", DemoMetrics.Create()));

                // Hook events to the edge node object.
                edgeNode.ApplicationOnlineChanged += (sender, eventArgs) =>
                    Console.WriteLine($"{nameof(edgeNode.ApplicationOnlineChanged)}: {edgeNode.ApplicationOnline}");
                edgeNode.MetricNotification += (sender, eventArgs) =>
                    Console.WriteLine($"{nameof(edgeNode.MetricNotification)}: {eventArgs}");
                edgeNode.PublishingError += (sender, eventArgs) => 
                    Console.WriteLine($"{nameof(edgeNode.PublishingError)}: {eventArgs}");
                edgeNode.Starting += (sender, eventArgs) => Console.WriteLine(nameof(edgeNode.Starting));
                edgeNode.Stopped += (sender, eventArgs) => Console.WriteLine(nameof(edgeNode.Stopped));
                edgeNode.SystemConnectionStateChanged += (sender, eventArgs) =>
                    Console.WriteLine($"{nameof(edgeNode.SystemConnectionStateChanged)}: {eventArgs}");

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
                edgeNode.Start();

                // Let the user decide when to stop.
                var cancelled = new ManualResetEvent(initialState: false);
                Console.CancelKeyPress += (sender, eventArgs) =>
                {
                    // Signal the main thread to exit.
                    cancelled.Set();

                    // Prevent the process from terminating immediately.
                    eventArgs.Cancel = true;
                };

                Console.WriteLine("Press Ctrl+C to stop the edge node...");
                Console.WriteLine();
                cancelled.WaitOne();

                // Stop the edge node.
                edgeNode.Stop();
            }
        }
    }
}
#endregion
