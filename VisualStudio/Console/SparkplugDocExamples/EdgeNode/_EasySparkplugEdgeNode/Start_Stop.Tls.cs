// $Header:             $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable InconsistentNaming
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to create a Sparkplug edge node with a single metric, start and stop it, using TLS.
//
// You can use any Sparkplug application, including our SparkplugCmd utility and the SparkplugApplicationConsoleDemo
// program, to subscribe to the edge node data. 
// The MQTT broker may need additional configuration to support TLS connections, such as enabling the TLS listener.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.BaseLib;
using OpcLabs.EasySparkplug;
using OpcLabs.EasySparkplug.OperationModel;
using OpcLabs.EasySparkplug.System;

namespace SparkplugDocExamples.EdgeNode._EasySparkplugEdgeNode
{
    partial class Start_Stop
    {
        static public void Tls()
        {
            // The TLS protocol can be specified in the broker URL using the "mqtts", "ssl" or "tls" scheme, as below.
            // (the schemes are equivalent). Default port is 8883.
            var hostDescriptor = new SparkplugHostDescriptor("mqtts://localhost");

            // Enable the console interaction by the component. The interactive user will then be able to validate remote
            // certificates and/or specify local certificate(s) to use.
            ComponentParameters componentParameters = EasySparkplugInfrastructure.Instance.Parameters;
            componentParameters.PluginSetups.FindName("ConsoleInteraction").Enabled = true;

            // By default, the local certificates, if needed, are provided by parameterized querying of certificate stores.
            // The statement below disables this, and the local certificate(s) will be specified interactively by the user.
            // For more information, see https://kb.opclabs.com/Certificate_security_plugin .
            //componentParameters.PluginConfigurations.Find<CertificateSecurityParameters>().AllowStatic = false;

            // Instantiate the edge node object and hook events.
            var edgeNode = new EasySparkplugEdgeNode(hostDescriptor,
                "easyGroup", "easySparkplugDemo");
            edgeNode.SystemConnectionStateChanged += edgeNode_Tls_SystemConnectionStateChanged;

            // Define a metric providing random integers.
            var random = new Random();
            edgeNode.Metrics.Add(new SparkplugMetric("MyMetric").ReadValueFunction(() => random.Next()));

            // Start the edge node.
            Console.WriteLine("The edge node is starting...");
            edgeNode.Start();

            Console.WriteLine("The edge node is started.");
            Console.WriteLine();

            // We do not use Console.ReadLine() to let the user decide when to stop, because it can collide with the
            // certificate security console interaction.
            Console.WriteLine("Waiting for 30 seconds...");
            System.Threading.Thread.Sleep(30 * 1000);

            // Stop the edge node.
            Console.WriteLine("The edge node is stopping...");
            edgeNode.Stop();

            Console.WriteLine("The edge node is stopped.");
        }


        static void edgeNode_Tls_SystemConnectionStateChanged(
            object sender, 
            SparkplugConnectionStateChangedEventArgs eventArgs)
        {
            // Display the new connection state (such as when the connection to the broker succeeds or fails).
            Console.WriteLine($"{nameof(EasySparkplugEdgeNode.SystemConnectionStateChanged)}: {eventArgs}");
        }
    }
}
#endregion
