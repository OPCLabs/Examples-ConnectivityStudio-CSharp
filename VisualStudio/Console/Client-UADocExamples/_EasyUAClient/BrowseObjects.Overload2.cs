// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to obtain objects under the "Server" node in the address space.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.AddressSpace;
using OpcLabs.EasyOpc.UA.AddressSpace.Standard;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples._EasyUAClient
{
    class BrowseObjects
    {
        public static void Overload2()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client object
            var client = new EasyUAClient();

            // Obtain objects under "Server" node.
            UANodeElementCollection nodeElementCollection;
            try
            {
                nodeElementCollection = client.BrowseObjects(endpointDescriptor, UAObjectIds.Server);
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
        //nodeElement.DisplayName: ServerCapabilities
        //nodeElement.NodeId: Server_ServerCapabilities
        //nodeElement.NodeId.ExpandedText: nsu = http://opcfoundation.org/UA/ ;i=2268
        //
        //nodeElement.DisplayName: ServerDiagnostics
        //nodeElement.NodeId: Server_ServerDiagnostics
        //nodeElement.NodeId.ExpandedText: nsu = http://opcfoundation.org/UA/ ;i=2274
        //
        //nodeElement.DisplayName: VendorServerInfo
        //nodeElement.NodeId: Server_VendorServerInfo
        //nodeElement.NodeId.ExpandedText: nsu = http://opcfoundation.org/UA/ ;i=2295
        //
        //nodeElement.DisplayName: ServerRedundancy
        //nodeElement.NodeId: Server_ServerRedundancy
        //nodeElement.NodeId.ExpandedText: nsu = http://opcfoundation.org/UA/ ;i=2296
        //
        //nodeElement.DisplayName: Namespaces
        //nodeElement.NodeId: Server_Namespaces
        //nodeElement.NodeId.ExpandedText: nsu = http://opcfoundation.org/UA/ ;i=11715
    }
}
#endregion
