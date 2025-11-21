// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable AccessToStaticMemberViaDerivedType
// ReSharper disable ArrangeModifiersOrder
// ReSharper disable InconsistentNaming
// ReSharper disable LocalizableElement
#region Example
// This example shows how to set the OPC Wizard parameters for best OPC compliance.
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
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UAServerDocExamples._EasyUAServer
{
    class _Parameterization
    {
        public static void OpcCompliance()
        {
            // You need to set both the shared parameters and instance parameters of the EasyUAServer to the values preset
            // for OPC compliance, as shown in the code below. The main difference from the default ("Interoperability")
            // settings is that the OPC compliance settings do not allow insecure connections, but there are other
            // differences as well.
            //
            // You will need to establish mutual trust between the OPC UA server and the client in order to successfully
            // establish a secure connection.

            // Set the shared parameters for OPC compliance.
            EasyUAServer.SharedParameters = EasyUAServerSharedParameters.OpcCompliance;

            // Instantiate the server object.
            // By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
            var server = new EasyUAServer();

            // Hook event handler for the EndpointStateChanged event. It simply prints out the event.
            server.EndpointStateChanged += (sender, args) => Console.WriteLine(args);

            // Set the instance parameters for OPC compliance.
            server.InstanceParameters = EasyUAServerInstanceParameters.OpcCompliance;

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
