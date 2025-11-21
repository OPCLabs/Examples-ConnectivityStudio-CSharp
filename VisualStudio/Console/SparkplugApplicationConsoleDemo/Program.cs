// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable PossibleNullReferenceException
#region Example
// A fully functional Sparkplug host application running in a console.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client, server and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using OpcLabs.EasySparkplug;
using System;
using System.Threading;
using OpcLabs.BaseLib;
using OpcLabs.EasySparkplug.System;

namespace SparkplugApplicationConsoleDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("EasySparkplug Application Console Demo");
            Console.WriteLine();

            // Parse command line arguments for broker URL, group ID, edge node ID, and host ID.
            
            string brokerUrlString = "mqtt://localhost";
            if (args.Length >= 1)
                brokerUrlString = args[0];

            string groupId = "easyGroup";
            if (args.Length >= 2)
                groupId = args[1];

            string edgeNodeId = "easySparkplugDemo";
            if (args.Length >= 3)
                edgeNodeId = args[2];
            
            string hostId = "";  // we use "easyApplication" in some examples
            if (args.Length >= 4)
                hostId = args[3];

            Console.WriteLine($"Broker URL: {brokerUrlString}");
            Console.WriteLine($"Group ID: {groupId}");
            Console.WriteLine($"Edge node ID: {edgeNodeId}");
            Console.WriteLine($"Host ID: {hostId}");
            Console.WriteLine();

            // Enable the console interaction by the component. The interactive user will then be able to validate remote
            // certificates and/or specify local certificate(s) to use.
            ComponentParameters componentParameters = EasySparkplugInfrastructure.Instance.Parameters;
            componentParameters.PluginSetups.FindName("ConsoleInteraction").Enabled = true;

            // Instantiate the consumer object.
            var hostDescriptor = new SparkplugHostDescriptor(brokerUrlString, hostId);
            using (var consumer = new EasySparkplugConsumer(hostDescriptor))
            {
                // Subscribe to all metrics of the specified edge node(s).
                consumer.SubscribeEdgeNodeMetric(groupId, edgeNodeId, "#",
                    (sender, eventArgs) => Console.WriteLine($"{nameof(consumer.MetricNotification)}: {eventArgs}"));

                // Subscribe to all device metrics of the specified edge node(s).
                consumer.SubscribeDeviceMetric(groupId, edgeNodeId, "#", "#",
                    (sender, eventArgs) => Console.WriteLine($"{nameof(consumer.MetricNotification)}: {eventArgs}"));

                // Let the user decide when to stop.
                var cancelled = new ManualResetEvent(initialState: false);
                Console.CancelKeyPress += (sender, eventArgs) =>
                {
                    // Signal the main thread to exit.
                    cancelled.Set();

                    // Prevent the process from terminating immediately.
                    eventArgs.Cancel = true;
                };

                Console.WriteLine("Press Ctrl+C to stop the application...");
                Console.WriteLine();
                cancelled.WaitOne();
            }
        }
    }
}
#endregion
