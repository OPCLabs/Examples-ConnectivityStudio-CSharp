// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to obtain "data nodes" under the "Objects" node, recursively.
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
    partial class BrowseDataNodes
    {
        public static void Recursive()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client object
            var client = new EasyUAClient();

            try
            {
                BrowseFromNode(client, endpointDescriptor, UAObjectIds.ObjectsFolder, level:0);
            }
            catch (UAException uaException)
            {
                Console.WriteLine($"*** Failure: {uaException.GetBaseException().Message}");
            }
        }

        private static void BrowseFromNode(
            EasyUAClient client,
            UAEndpointDescriptor endpointDescriptor,
            UANodeDescriptor parentNodeDescriptor,
            int level)
        {
            Debug.Assert(!(client is null));
            Debug.Assert(!(endpointDescriptor is null));
            Debug.Assert(!(parentNodeDescriptor is null));

            // Obtain all node elements under parentNodeDescriptor
            UANodeElementCollection nodeElementCollection = 
                client.BrowseDataNodes(endpointDescriptor, parentNodeDescriptor);
            // Remark: BrowseDataNodes(...) may throw UAException; we handle it in the calling method.

            foreach (UANodeElement nodeElement in nodeElementCollection)
            {
                Debug.Assert(!(nodeElement is null));

                Console.Write(new string(' ', level*2));    // indent
                Console.WriteLine(nodeElement);

                // Browse recursively into the node.
                // The UANodeElement has an implicit conversion to UANodeDescriptor.
                BrowseFromNode(client, endpointDescriptor, nodeElement, level + 1);

                // Note that the number of nodes you obtain through recursive browsing may be very large, or even infinite.
                // Production code should contain appropriate safeguards for these cases.
            }
        }


        // Example output:
        //
        //ServerStatus -> nsu=http://opcfoundation.org/UA/ ;i=2256 (Variable) 
        //  StartTime -> nsu=http://opcfoundation.org/UA/ ;i=2257 (Variable) 
        //  CurrentTime -> nsu=http://opcfoundation.org/UA/ ;i=2258 (Variable) 
        //  State -> nsu=http://opcfoundation.org/UA/ ;i=2259 (Variable) 
        //  BuildInfo -> nsu=http://opcfoundation.org/UA/ ;i=2260 (Variable) 
        //    ProductUri -> nsu=http://opcfoundation.org/UA/ ;i=2262 (Variable) 
        //    ManufacturerName -> nsu=http://opcfoundation.org/UA/ ;i=2263 (Variable) 
        //    ProductName -> nsu=http://opcfoundation.org/UA/ ;i=2261 (Variable) 
        //    SoftwareVersion -> nsu=http://opcfoundation.org/UA/ ;i=2264 (Variable) 
        //    BuildNumber -> nsu=http://opcfoundation.org/UA/ ;i=2265 (Variable) 
        //    BuildDate -> nsu=http://opcfoundation.org/UA/ ;i=2266 (Variable) 
        //  SecondsTillShutdown -> nsu=http://opcfoundation.org/UA/ ;i=2992 (Variable) 
        //  ShutdownReason -> nsu=http://opcfoundation.org/UA/ ;i=2993 (Variable) 
        //ServerCapabilities -> nsu=http://opcfoundation.org/UA/ ;i=2268 (Object) 
        //  OperationLimits -> nsu=http://opcfoundation.org/UA/ ;i=11704 (Object) 
        //    MaxNodesPerRead -> nsu=http://opcfoundation.org/UA/ ;i=11705 (Variable) 
        //    MaxNodesPerHistoryReadData -> nsu=http://opcfoundation.org/UA/ ;i=12165 (Variable) 
        //    MaxNodesPerHistoryReadEvents -> nsu=http://opcfoundation.org/UA/ ;i=12166 (Variable) 
        //    MaxNodesPerWrite -> nsu=http://opcfoundation.org/UA/ ;i=11707 (Variable) 
        //    MaxNodesPerHistoryUpdateData -> nsu=http://opcfoundation.org/UA/ ;i=12167 (Variable) 
        //...
    }
}
#endregion
