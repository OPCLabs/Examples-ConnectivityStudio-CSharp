// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable AccessToStaticMemberViaDerivedType
// ReSharper disable ArrangeModifiersOrder
#region Example
// This example demonstrates the loggable entries originating in the OPC-UA server engine and the EasyUAServer component.
// You can use any OPC UA client, including our Connectivity Explorer and OpcCmd utility, to connect to the server. 
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client, server and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Threading;
using OpcLabs.BaseLib.Instrumentation;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.NodeSpace;

namespace UAServerDocExamples._EasyUAServer
{
    class LogEntry
    {
        public static void Main1()
        {
            // Hook static events.
            EasyUAServer.LogEntry += EasyUAServerOnLogEntry;

            // Instantiate the server object.
            // By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
            var server = new EasyUAServer();

            // Define a data variable providing random integers.
            var random = new Random();
            server.Add(new UADataVariable("MyDataVariable").ReadValueFunction(() => random.Next()));

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

            Console.WriteLine("The server is stopped");

            // Give the remaining events chance to be handled.
            Thread.Sleep(2 * 1000);

            // Unhook static events.
            EasyUAServer.LogEntry -= EasyUAServerOnLogEntry;
        }

        // Event handler for the LogEntry event. It simply prints out the event.
        private static void EasyUAServerOnLogEntry(object sender, LogEntryEventArgs logEntryEventArgs)
        {
            Console.WriteLine(logEntryEventArgs);
        }
    }
}
#endregion
