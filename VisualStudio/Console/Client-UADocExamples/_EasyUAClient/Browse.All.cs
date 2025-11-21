// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable LocalizableElement
#region Example
// Shows how to obtain references of all kinds to nodes of all classes, from the "Server" node in the address space.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Diagnostics;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.AddressSpace;
using OpcLabs.EasyOpc.UA.AddressSpace.Standard;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples._EasyUAClient
{
    partial class Browse
    {
        public static void All()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client object
            var client = new EasyUAClient();

            // Obtain nodes under "Server" node
            UANodeElementCollection nodeElementCollection;
            try
            {
                nodeElementCollection = client.Browse(
                    endpointDescriptor,
                    UAObjectIds.Server,
                    new UABrowseParameters(UANodeClass.All, new[] { UAReferenceTypeIds.References }));
            }
            catch (UAException uaException)
            {
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
                return;
            }

            // Display results
            foreach (UANodeElement nodeElement in nodeElementCollection)
            {
                Debug.Assert(nodeElement != null);
                Console.WriteLine();
                Console.WriteLine("nodeElement.DisplayName: {0}", nodeElement.DisplayName);
                Console.WriteLine("nodeElement.NodeId: {0}", nodeElement.NodeId);
                Console.WriteLine("nodeElement.NodeId.ExpandedText: {0}", nodeElement.NodeId.ExpandedText);
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
        //nodeElement.DisplayName: ServerStatus
        //nodeElement.NodeId: Server_ServerStatus
        //nodeElement.NodeId.ExpandedText: nsu=http://opcfoundation.org/UA/ ;i=2256
        //
        //nodeElement.DisplayName: ServiceLevel
        //nodeElement.NodeId: Server_ServiceLevel
        //nodeElement.NodeId.ExpandedText: nsu=http://opcfoundation.org/UA/ ;i=2267
        //
        //nodeElement.DisplayName: Auditing
        //nodeElement.NodeId: Server_Auditing
        //nodeElement.NodeId.ExpandedText: nsu=http://opcfoundation.org/UA/ ;i=2994
        //
        //nodeElement.DisplayName: ServerCapabilities
        //nodeElement.NodeId: Server_ServerCapabilities
        //nodeElement.NodeId.ExpandedText: nsu=http://opcfoundation.org/UA/ ;i=2268
        //
        //nodeElement.DisplayName: ServerDiagnostics
        //nodeElement.NodeId: Server_ServerDiagnostics
        //nodeElement.NodeId.ExpandedText: nsu=http://opcfoundation.org/UA/ ;i=2274
        //
        //nodeElement.DisplayName: VendorServerInfo
        //nodeElement.NodeId: Server_VendorServerInfo
        //nodeElement.NodeId.ExpandedText: nsu=http://opcfoundation.org/UA/ ;i=2295
        //
        //nodeElement.DisplayName: ServerRedundancy
        //nodeElement.NodeId: Server_ServerRedundancy
        //nodeElement.NodeId.ExpandedText: nsu=http://opcfoundation.org/UA/ ;i=2296
        //
        //nodeElement.DisplayName: Namespaces
        //nodeElement.NodeId: Server_Namespaces
        //nodeElement.NodeId.ExpandedText: nsu=http://opcfoundation.org/UA/ ;i=11715
        //
        //nodeElement.DisplayName: GetMonitoredItems
        //nodeElement.NodeId: Server_GetMonitoredItems
        //nodeElement.NodeId.ExpandedText: nsu=http://opcfoundation.org/UA/ ;i=11492
        //
        //nodeElement.DisplayName: Data
        //nodeElement.NodeId: nsu=http://test.org/UA/Data/ ;ns=2;i=10157
        //nodeElement.NodeId.ExpandedText: nsu=http://test.org/UA/Data/ ;ns=2;i=10157
        //
        //nodeElement.DisplayName: Boilers
        //nodeElement.NodeId: nsu=http://opcfoundation.org/UA/Boiler/ ;ns=4;i=1240
        //nodeElement.NodeId.ExpandedText: nsu=http://opcfoundation.org/UA/Boiler/ ;ns=4;i=1240
        //
        //nodeElement.DisplayName: ServerType
        //nodeElement.NodeId: ServerType
        //nodeElement.NodeId.ExpandedText: nsu=http://opcfoundation.org/UA/ ;i=2004
    }
}
#endregion
