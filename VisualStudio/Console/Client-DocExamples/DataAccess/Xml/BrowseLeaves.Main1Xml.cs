// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable CommentTypo
#region Example
// This example shows how to obtain all leaves under the "Simulation" branch of the address space. For each leaf, it displays 
// the ItemID of the node.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.AddressSpace;
using OpcLabs.EasyOpc.OperationModel;

namespace DocExamples.DataAccess.Xml
{
    class BrowseLeaves
    {
        public static void Main1Xml()
        {
            // Instantiate the client object.
            var client = new EasyDAClient();
            DANodeElementCollection leafElements;
            try
            {
                ServerDescriptor serverDescriptor = "http://opcxml.demo-this.com/XmlDaSampleServer/Service.asmx";
                leafElements = client.BrowseLeaves(serverDescriptor, "Static/Analog Types");
            }
            catch (OpcException opcException)
            {
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message);
                return;
            }

            foreach (DANodeElement leafElement in leafElements)
                Console.WriteLine($"LeafElements(\"{leafElement.Name}\").ItemId: {leafElement.ItemId}");
        }


        // Example output:
        //
        //LeafElements("Int").ItemId: Static/Analog Types/Int
        //LeafElements("Double").ItemId: Static/Analog Types/Double
        //LeafElements("Int[]").ItemId: Static/Analog Types/Int[]
        //LeafElements("Double[]").ItemId: Static/Analog Types/Double[]
    }
}
#endregion
