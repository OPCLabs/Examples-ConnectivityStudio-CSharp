// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable InconsistentNaming
#region Example
// This example shows how to turn off the polling by the component, and instead manually publish the data by reporting when
// they have changed.
//
// You can use any Sparkplug application, including our SparkplugCmd utility and the SparkplugApplicationConsoleDemo
// program, to subscribe to the edge node data. 
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Threading;
using OpcLabs.EasySparkplug;
using Timer = System.Timers.Timer;

namespace SparkplugDocExamples.EdgeNode._EasySparkplugEdgeNode
{
     class ReportByException
    {
        static public void Main1()
        {
            // Note that the default port for the "mqtt" scheme is 1883.
            var hostDescriptor = new SparkplugHostDescriptor("mqtt://localhost");

            // Instantiate the edge node object.
            var edgeNode = new EasySparkplugEdgeNode(hostDescriptor, "easyGroup", "easySparkplugDemo");

            // Configure the edge node so that we will publish data fully manually.
            edgeNode.PublishingInterval = Timeout.Infinite;
            edgeNode.ReportByException = true;
            
            // Hook the SystemConnectionStateChanged event to handle system connection state changes.
            edgeNode.SystemConnectionStateChanged += (sender, eventArgs) =>
            {
                // Display the new connection state (such as when the connection to the broker succeeds or fails).
                Console.WriteLine($"{nameof(EasySparkplugEdgeNode.SystemConnectionStateChanged)}: {eventArgs}");
            };

            // Define metrics.
            var random = new Random();
            SparkplugMetric myMetric1 = SparkplugMetric.CreateIn(edgeNode, "MyMetric1").ValueType<int>();
            SparkplugMetric myMetric2 = SparkplugMetric.CreateIn(edgeNode, "MyMetric2").ValueType<int>();
            SparkplugMetric myMetric3 = SparkplugMetric.CreateIn(edgeNode, "MyMetric3").ValueType<int>();

            // Start the edge node.
            Console.WriteLine("The edge node is starting...");
            edgeNode.Start();

            Console.WriteLine("The edge node is started.");
            Console.WriteLine();

            // Create a timer for publishing the data, and start it.
            var timer = new Timer { AutoReset = true };
            timer.Elapsed += (sender, eventArgs) =>
            {
                // Do not publish individual updates, but rather lock the publishing so that we can make multiple updates.
                edgeNode.LockPublishing();
                try
                {
                    // Update some of the metrics (in this example, with random data).
                    if (random.Next(2) != 0)
                        myMetric1.UpdateReadData(random.Next());
                    if (random.Next(2) != 0)
                        myMetric2.UpdateReadData(random.Next());
                    if (random.Next(2) != 0)
                        myMetric3.UpdateReadData(random.Next());
                }
                finally
                {
                    // At this point, the edge node will publish the data for all metrics that have been updated.
                    edgeNode.UnlockPublishing();
                }
                
                // Set the next interval to a random value between 0 and 3 seconds.
                timer.Interval = random.Next(3 * 1000); 
            };
            timer.Start();
            
            // Let the user decide when to stop.
            Console.WriteLine("Press Enter to stop the edge node...");
            Console.ReadLine();
            
            // Stop the timer.
            timer.Stop();

            // Stop the edge node.
            Console.WriteLine("The edge node is stopping...");
            edgeNode.Stop();

            Console.WriteLine("The edge node is stopped.");
        }
    }
}
#endregion
