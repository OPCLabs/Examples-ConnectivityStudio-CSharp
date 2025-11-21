// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable LocalizableElement
#region Example
// This example shows how to obtain the effective node descriptors of server nodes (i.e. their node IDs and browse paths).
// You can use any OPC UA client, including our Connectivity Explorer and OpcCmd utility, to connect to the server. 
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client, server and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.NodeSpace;

namespace UAServerDocExamples._UAServerNode
{
    class EffectiveNodeDescriptor
    {
        public static void Main1()
        {
            // Instantiate the server object.
            // By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
            var server = new EasyUAServer();

            // Define some nodes in the server.
            var dataVariable1 = UADataVariable.CreateIn(server.Objects, "DataVariable1");
            var folder1 = UAFolder.CreateIn(server.Objects, "Folder1");
            var dataVariable2 = UADataVariable.CreateIn(folder1, "DataVariable2");

            // Display the node Ids (including the namespace URI).
            Console.WriteLine();
            Console.WriteLine(server.Objects.EffectiveNodeDescriptor.NodeId);
            Console.WriteLine(dataVariable1.EffectiveNodeDescriptor.NodeId);
            Console.WriteLine(folder1.EffectiveNodeDescriptor.NodeId);
            Console.WriteLine(dataVariable2.EffectiveNodeDescriptor.NodeId);

            // Display the browse paths.
            Console.WriteLine();
            Console.WriteLine(server.Objects.EffectiveNodeDescriptor.BrowsePath);
            Console.WriteLine(dataVariable1.EffectiveNodeDescriptor.BrowsePath);
            Console.WriteLine(folder1.EffectiveNodeDescriptor.BrowsePath);
            Console.WriteLine(dataVariable2.EffectiveNodeDescriptor.BrowsePath);
        }
    }
}
#endregion
