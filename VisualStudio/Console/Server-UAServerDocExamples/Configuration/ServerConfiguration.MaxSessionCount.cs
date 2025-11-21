// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable AccessToStaticMemberViaDerivedType
// ReSharper disable ArrangeModifiersOrder
// ReSharper disable InconsistentNaming
// ReSharper disable LocalizableElement
#region Example
// This example shows how to limit the maximum number of sessions the OPC UA clients can open with the server.
// You can use any OPC UA client, including our Connectivity Explorer and OpcCmd utility, to connect to the server. 
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client, server and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.Engine;
using OpcLabs.EasyOpc.UA.NodeSpace;

namespace UAServerDocExamples.Configuration
{
    class ServerConfiguration
    {
        public static void MaxSessionCount()
        {
            // Get the shared engine parameters object
            EasyUAServerEngineParameters engineParameters = EasyUAServer.SharedParameters.EngineParameters;

            // Set the maximum number of open session to 2.
            // This particular property is documented here:
            // https://www.opclabs.com/files/onlinedocs/UA-.NETStandard/Latest/Browser%20Help/webframe.html#Opc.Ua.Core~Opc.Ua.ServerConfiguration~MaxSessionCount.html
            engineParameters.ConfigurationPropertyOverrides["ServerConfiguration.MaxSessionCount"] = 2;
            
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

            Console.WriteLine("The server is stopped.");
        }
    }
}
#endregion
