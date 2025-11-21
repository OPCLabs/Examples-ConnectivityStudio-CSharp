// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable InconsistentNaming
// ReSharper disable LocalizableElement
#region Example
// This example shows how to obtain the effective endpoint descriptor of the server, and use it together with effective node
// descriptor of data variable to operate on the server using an OPC UA client object.
// You can use any OPC UA client, including our Connectivity Explorer and OpcCmd utility, to connect to the server. 
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client, server and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Threading;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.NodeSpace;

namespace UAServerDocExamples._EasyUAServer
{
    class EffectiveEndpointDescriptor
    {
        public static void Main1()
        {
            // Instantiate the server object.
            // By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
            var server = new EasyUAServer();

            // Define data variables providing random integers.
            var random = new Random();
            UADataVariable myDataVariable1 = UADataVariable.CreateIn(server.Objects, "MyDataVariable1")
                .ReadValueFunction(() => random.Next(0, 100));
            UADataVariable myDataVariable2 = UADataVariable.CreateIn(server.Objects, "MyDataVariable2")
                .ReadValueFunction(() => random.Next(100, 200));

            // Start the server.
            Console.WriteLine("The server is starting...");
            server.Start();

            // Give the server some time to make its endpoints ready to accept client connections. For precise determination,
            // you can use IEasyUAServerEndpointMonitoring.EndpointStateChanged event on the server object.
            Thread.Sleep(1000);
            
            // Instantiate the client object.
            var client = new EasyUAClient();

            Console.WriteLine("Subscribing to data changes...");

            // Subscribe to data changes of our first data variable. Display the data changes on the console.
            client.SubscribeDataChange(server.EffectiveEndpointDescriptor, myDataVariable1.EffectiveNodeDescriptor, 1000,
                (sender, args) => Console.WriteLine(args), state: 1);

            // Subscribe to data changes of our second data variable. Display the data changes on the console.
            // Passing the server object as the first argument makes an implicit conversion to the endpoint descriptor.
            // Passing the data variable object as the second argument makes an implicit conversion to the node descriptor.
            // We are doing an equivalent of the previous call, but in a more compact way.
            client.SubscribeDataChange(server, myDataVariable2, 1000, 
                (sender, args) => Console.WriteLine(args), state: 2);

            // Let the user decide when to stop.
            Console.WriteLine("Press Enter to stop the server...");
            Console.ReadLine();

            Console.WriteLine("Unsubscribing all items...");
            client.UnsubscribeAllMonitoredItems();

            // Stop the server.
            Console.WriteLine("The server is stopping...");
            server.Stop();

            Console.WriteLine("The server is stopped.");
        }
    }
}
#endregion
