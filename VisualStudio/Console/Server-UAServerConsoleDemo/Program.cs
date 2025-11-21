// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable AccessToStaticMemberViaDerivedType
// ReSharper disable AccessToDisposedClosure
// ReSharper disable ConvertToUsingDeclaration
// ReSharper disable PossibleNullReferenceException
// ReSharper disable UnusedParameter.Local
#region Example
// A fully functional OPC UA demo server running in a console host.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client, server and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.Services;
using UAServerDemoLibrary;

namespace UAServerConsoleDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("OPC Wizard Server Console Demo");
            Console.WriteLine();

            // Enable the console interaction by the server.
            EasyUAServer.SharedParameters.PluginSetups.FindName("UAConsoleInteraction").Enabled = true;

            // Instantiate the server object.
            // By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
            using (var server = new EasyUAServer())
            {
                // Define various nodes.
                ConsoleNodes.AddToParent(server.Objects);
                DataNodes.AddToParent(server.Objects);
                DemoNodes.AddToParent(server.Objects);

                // Hook events to the server object.
                server.EndpointStateChanged += (sender, eventArgs) =>
                    Console.WriteLine($"{nameof(server.EndpointStateChanged)}: {eventArgs}");
                server.Starting += (sender, eventArgs) => Console.WriteLine(nameof(server.Starting));
                server.Stopped += (sender, eventArgs) => Console.WriteLine(nameof(server.Stopped));

                // Obtain the server connection monitoring service.
                IEasyUAServerConnectionMonitoring serverConnectionMonitoring = server.GetService<IEasyUAServerConnectionMonitoring>();
                if (!(serverConnectionMonitoring is null))
                {
                    // Hook events to the connection monitoring service.
                    serverConnectionMonitoring.ClientSessionConnected += (sender, eventArgs) =>
                        Console.WriteLine($"{nameof(serverConnectionMonitoring.ClientSessionConnected)}: {eventArgs}");
                    serverConnectionMonitoring.ClientSessionDisconnected += (sender, eventArgs) =>
                        Console.WriteLine($"{nameof(serverConnectionMonitoring.ClientSessionDisconnected)}: {eventArgs}");
                }

                // Start the server.
                server.Start();

                // Let the user decide when to stop.
                var cancelled = new ManualResetEvent(initialState: false);
                Console.CancelKeyPress += (sender, eventArgs) =>
                {
                    // Signal the main thread to exit.
                    cancelled.Set();

                    // Prevent the process from terminating immediately.
                    eventArgs.Cancel = true;
                };

                Console.WriteLine("Press Ctrl+C to stop the server...");
                cancelled.WaitOne();

                // Stop the server.
                server.Stop();
            }
        }
    }
}
#endregion
