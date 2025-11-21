// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to obtain "data nodes" (objects, variables and properties) under the "Objects" node in the address
// space.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.AddressSpace;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples._EasyUAClient
{
    partial class BrowseDataNodes
    {
        public static void Overload1()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client object
            var client = new EasyUAClient();

            // Obtain data nodes under "Objects" node.
            UANodeElementCollection nodeElementCollection;
            try
            {
                nodeElementCollection = client.BrowseDataNodes(endpointDescriptor);
            }
            catch (UAException uaException)
            {
                Console.WriteLine($"*** Failure: {uaException.GetBaseException().Message}");
                return;
            }

            // Display results
            foreach (UANodeElement nodeElement in nodeElementCollection)
            {
                Console.WriteLine();
                Console.WriteLine($"nodeElement.DisplayName: {nodeElement.DisplayName}");
                Console.WriteLine($"nodeElement.NodeId: {nodeElement.NodeId}");
                Console.WriteLine($"nodeElement.NodeId.ExpandedText: {nodeElement.NodeId.ExpandedText}");
            }
        }

        // Example output:
        //
        //nodeElement.DisplayName: Server
        //nodeElement.NodeId: Server
        //nodeElement.NodeId.ExpandedText: nsu = http://opcfoundation.org/UA/ ;i=2253
        //
        //nodeElement.DisplayName: Data
        //nodeElement.NodeId: nsu = http://test.org/UA/Data/ ;ns=2;i=10157
        //nodeElement.NodeId.ExpandedText: nsu = http://test.org/UA/Data/ ;ns=2;i=10157
        //
        //nodeElement.DisplayName: Boilers
        //nodeElement.NodeId: nsu = http://opcfoundation.org/UA/Boiler/ ;ns=4;i=1240
        //nodeElement.NodeId.ExpandedText: nsu = http://opcfoundation.org/UA/Boiler/ ;ns=4;i=1240
        //
        //nodeElement.DisplayName: MemoryBuffers
        //nodeElement.NodeId: nsu = http://samples.org/UA/memorybuffer ;ns=7;i=1025
        //nodeElement.NodeId.ExpandedText: nsu = http://samples.org/UA/memorybuffer ;ns=7;i=1025
    }
}
#endregion
