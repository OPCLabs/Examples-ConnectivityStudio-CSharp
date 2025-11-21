// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to obtain properties under the "Server" node in the address space.
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
    class BrowseProperties
    {
        public static void Overload2()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client object
            var client = new EasyUAClient();

            // Obtain properties under "Server" node.
            UANodeElementCollection nodeElementCollection;
            try
            {
                nodeElementCollection = client.BrowseProperties(endpointDescriptor, UAObjectIds.Server);
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
        //nodeElement.DisplayName: ServerArray
        //nodeElement.NodeId: Server_ServerArray
        //nodeElement.NodeId.ExpandedText: nsu=http://opcfoundation.org/UA/ ;i=2254
        //
        //nodeElement.DisplayName: NamespaceArray
        //nodeElement.NodeId: Server_NamespaceArray
        //nodeElement.NodeId.ExpandedText: nsu=http://opcfoundation.org/UA/ ;i=2255
        //
        //nodeElement.DisplayName: ServiceLevel
        //nodeElement.NodeId: Server_ServiceLevel
        //nodeElement.NodeId.ExpandedText: nsu=http://opcfoundation.org/UA/ ;i=2267
        //
        //nodeElement.DisplayName: Auditing
        //nodeElement.NodeId: Server_Auditing
        //nodeElement.NodeId.ExpandedText: nsu=http://opcfoundation.org/UA/ ;i=2994
    }
}
#endregion
