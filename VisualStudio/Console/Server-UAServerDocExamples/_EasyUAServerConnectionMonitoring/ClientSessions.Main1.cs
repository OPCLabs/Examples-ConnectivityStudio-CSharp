// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to monitor OPC UA client connections to the server.
// You can use any OPC UA client, including our Connectivity Explorer and OpcCmd utility, to connect to the server. 
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client, server and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using Microsoft.Extensions.DependencyInjection;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.NodeSpace;
using OpcLabs.EasyOpc.UA.OperationModel;
using OpcLabs.EasyOpc.UA.Services;

namespace UAServerDocExamples._EasyUAServerConnectionMonitoring
{
    class ClientSessions
    {
        public static void Main1()
        {
            // Instantiate the server object.
            // By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
            var server = new EasyUAServer();

            // Define a data variable providing random integers.
            var random = new Random();
            server.Add(new UADataVariable("MyDataVariable").ReadValueFunction(() => random.Next()));

            // Obtain the server connection monitoring service.
            IEasyUAServerConnectionMonitoring serverConnectionMonitoring = server.GetService<IEasyUAServerConnectionMonitoring>();
            if (serverConnectionMonitoring is null)
            {
                Console.WriteLine("The server connection monitoring service is not available.");
                return;
            }

            // Hook events.
            serverConnectionMonitoring.ClientSessionConnected += ServerConnectionMonitoringOnClientSessionConnected;
            serverConnectionMonitoring.ClientSessionDisconnected += ServerConnectionMonitoringOnClientSessionDisconnected;

            // Start the server.
            Console.WriteLine("The server is starting...");
            server.Start();

            Console.WriteLine("The server is started.");
            Console.WriteLine();

            // Let the user decide when to stop.
            Console.WriteLine("Press Enter to stop the server...");
            Console.ReadLine();

            // Stop the server.
            Console.WriteLine("The server is stopping...");
            server.Stop();

            Console.WriteLine("The server is stopped.");
        }

        // Event handler for the ClientSessionConnected event.
        static private void ServerConnectionMonitoringOnClientSessionConnected(
            object sender, 
            EasyUAClientSessionConnectionEventArgs e)

        {
            var serverConnectionMonitoring = (IEasyUAServerConnectionMonitoring)sender;
            Console.WriteLine(
                $"A client session has connected to endpoint \"{e.EndpointUrlString}\". " + 
                $"There is now {serverConnectionMonitoring.ClientSessionCount} client session(s).");
        }

        // Event handler for the ClientSessionDisconnected event.
        static private void ServerConnectionMonitoringOnClientSessionDisconnected(
            object sender,
            EasyUAClientSessionConnectionEventArgs e)
        {
            var serverConnectionMonitoring = (IEasyUAServerConnectionMonitoring)sender;
            Console.WriteLine(
                $"A client session has disconnected from endpoint \"{e.EndpointUrlString}\". " + 
                $"There is now {serverConnectionMonitoring.ClientSessionCount} client session(s).");
        }
    }
}
#endregion
