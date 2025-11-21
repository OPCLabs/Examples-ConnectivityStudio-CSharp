// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable InconsistentNaming
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to implement custom data source connection and disconnection handling for a Sparkplug device.
//
// You can use any Sparkplug application, including our SparkplugCmd utility and the SparkplugApplicationConsoleDemo
// program, to subscribe to the edge node data. 
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using OpcLabs.EasySparkplug;
using OpcLabs.EasySparkplug.OperationModel;
using System;
using Timer = System.Timers.Timer;

namespace SparkplugDocExamples.EdgeNode._SparkplugDevice
{
     class ConnectDataSource
    {
        static public void Main1()
        {
            // Note that the default port for the "mqtt" scheme is 1883.
            var hostDescriptor = new SparkplugHostDescriptor("mqtt://localhost");

            // Instantiate the edge node object.
            var edgeNode = new EasySparkplugEdgeNode(hostDescriptor, "easyGroup", "easySparkplugDemo");

            // Hook the SystemConnectionStateChanged event to handle system connection state changes.
            edgeNode.SystemConnectionStateChanged += (sender, eventArgs) =>
            {
                // Display the new connection state (such as when the connection to the broker succeeds or fails).
                Console.WriteLine($"{nameof(EasySparkplugEdgeNode.SystemConnectionStateChanged)}: {eventArgs}");
            };

            // Define a metric on the edge node providing random integers.
            var random = new Random();
            edgeNode.Metrics.Add(new SparkplugMetric("MyMetric1").ReadValueFunction(() => random.Next()));

            // Create a device.
            SparkplugDevice device = SparkplugDevice.CreateIn(edgeNode, "Device");

            // Hook the DataSourceConditionChanged event to handle data source connection state changes.
            device.DataSourceConditionChanged += (sender, eventArgs) =>
            {
                // Display the new data source condition.
                Console.WriteLine($"{nameof(SparkplugDevice.DataSourceConditionChanged)}: {eventArgs}");
            };

            // Hook the events to connect and disconnect the device data source.
            device.ConnectDataSource += DeviceOnConnectDataSource;
            device.DisconnectDataSource += DeviceOnDisconnectDataSource;

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

        /// <summary>
        /// Handles the <see cref="SparkplugDevice.ConnectDataSource"/> event.
        /// </summary>
        static private void DeviceOnConnectDataSource(object sender, SparkplugProducerProcessedEventArgs eventArgs)
        {
            // The event sender is the device.
            var device = (SparkplugDevice)sender;
            Console.WriteLine(nameof(device.ConnectDataSource));

            // Simulate a connection to the data source that takes 5 seconds to either succeed or fail.
            // In a real application, you would connect to the actual data source here.
            var timer = new Timer(5 * 1000) { AutoReset = false };
            timer.Elapsed += (s, e) =>
            {
                if (Random.Next(0, 3) == 0)
                {
                    // Simulate a successful connection to the data source. Reconnect after 10 seconds.
                    device.DataSourceConnectionSuccess();
                }
                else
                {
                    // Simulate a failure to connect to the data source.
                    device.DataSourceConnectionFailure(
                        "Simulated connection failure.", 
                        10*1000);
                }
            };
            timer.Start();

            // Indicate that the event is handled. This is necessary to do, otherwise the default behavior would kick in,
            // and the data source would be considered immediately successfully connected.
            eventArgs.Handled = true; 
        }

        /// <summary>
        /// Handles the <see cref="SparkplugDevice.DisconnectDataSource"/> event.
        /// </summary>
        static private void DeviceOnDisconnectDataSource(object sender, SparkplugProducerProcessedEventArgs eventArgs)
        {
            // The event sender is the device.
            var device = (SparkplugDevice)sender;
            Console.WriteLine(nameof(device.DisconnectDataSource));
            
            // Simulate a disconnection from the data source that takes 5 seconds.
            // In a real application, you would disconnect from the actual data source here.
            var timer = new Timer(5 * 1000) { AutoReset = false };
            timer.Elapsed += (s, e) => device.DataSourceDisconnectionSuccess();
            timer.Start();

            // Indicate that the event is handled. This is necessary to do, otherwise the default behavior would kick in,
            // and the data source would be considered immediately successfully disconnected.
            eventArgs.Handled = true;
        }


        static private readonly Random Random = new Random();
    }
}
#endregion
