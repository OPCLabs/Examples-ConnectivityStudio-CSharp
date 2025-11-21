// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable InconsistentNaming
// ReSharper disable LocalizableElement
// ReSharper disable UnusedVariable
#region Example
// This example shows how sub-nodes of server nodes can be accessed by their node name using an indexer.
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
    class Indexer
    {
        public static void Main1()
        {
            // Instantiate the server object.
            // By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
            var server = new EasyUAServer();

            // Define some nodes in the server.
            UADataVariable constantDataVariable = UADataVariable.CreateIn(server.Objects, "Constant").ConstantValue("abc");
            UADataVariable nestedConstantDataVariable = UADataVariable.CreateIn(constantDataVariable, "NestedConstant").ConstantValue(42);
            
            // Get the nested constant data variable.
            UAServerNode serverNode1 = server.Objects["Constant"]["NestedConstant"];
            Console.WriteLine(serverNode1);
        }
    }
}
#endregion
