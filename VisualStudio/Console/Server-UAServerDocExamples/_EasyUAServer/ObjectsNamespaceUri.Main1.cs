// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable LocalizableElement
#region Example
// This example shows how to configure the namespace URI of the custom nodes, using a Uri object.
// You can use any OPC UA client, including our Connectivity Explorer and OpcCmd utility, to connect to the server. 
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client, server and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.NodeSpace;

namespace UAServerDocExamples._EasyUAServer
{
    class ObjectsNamespaceUri
    {
        public static void Main1()
        {
            // Instantiate the server object.
            // By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
            var server = new EasyUAServer();

            // Set custom value for the namespace URI of our nodes under the Objects folder, using a Uri object.
            server.ObjectsNamespaceUri = new Uri("http://mynamespace.example");

            // Create some data variable and a folder in the Objects folder.
            var dataVariable1 = UADataVariable.CreateIn(server.Objects, "DataVariable1");
            var folder1 = UAFolder.CreateIn(server.Objects, "Folder1");

            // Display the node Ids (including the namespace URI).
            Console.WriteLine(server.Objects.EffectiveNodeDescriptor.NodeId);
            Console.WriteLine(dataVariable1.EffectiveNodeDescriptor.NodeId);
            Console.WriteLine(folder1.EffectiveNodeDescriptor.NodeId);
        }
    }
}
#endregion 
