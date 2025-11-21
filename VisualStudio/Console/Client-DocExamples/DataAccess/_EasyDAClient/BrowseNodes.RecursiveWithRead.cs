// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable AssignNullToNotNullAttribute
#region Example
// Recursively browses and displays the nodes in the OPC address space, and attempts to read and display values of all OPC 
// items it finds.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Diagnostics;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.AddressSpace;
using OpcLabs.EasyOpc.OperationModel;

namespace DocExamples.DataAccess._EasyDAClient
{
    partial class BrowseNodes
    {
        const string ServerClass = "OPCLabs.KitServer.2";

        // Instantiate the client object.
        static readonly EasyDAClient Client = new EasyDAClient();

        static void BrowseAndReadFromNode(string parentItemId)
        {
            // Obtain all node elements under parentItemId
            var browseParameters = new DABrowseParameters(); // no filtering whatsoever
            DANodeElementCollection nodeElementCollection = Client.BrowseNodes("", ServerClass, parentItemId,
                browseParameters);
            // Remark: that BrowseNodes(...) may also throw OpcException; a production code should contain handling for it, here 
            // omitted for brevity.

            foreach (DANodeElement nodeElement in nodeElementCollection)
            {
                Debug.Assert(nodeElement != null);

                // If the node is a leaf, it might be possible to read from it
                if (nodeElement.IsLeaf)
                {
                    // Determine what the display - either the value read, or exception message in case of failure.
                    string display;
                    try
                    {
                        object value = Client.ReadItemValue("", ServerClass, nodeElement);
                        display = $"{value}";
                    }
                    catch (OpcException exception)
                    {
                        display = $"** {exception.GetBaseException().Message} **";
                    }

                    Console.WriteLine("{0} -> {1}", nodeElement.ItemId, display);
                }
                // If the node is not a leaf, just display its itemId
                else
                    Console.WriteLine("{0}", nodeElement.ItemId);

                // If the node is a branch, browse recursively into it.
                if (nodeElement.IsBranch &&
                    (nodeElement.ItemId != "SimulateEvents")
                    /* this branch is too big for the purpose of this example */)
                    BrowseAndReadFromNode(nodeElement);
            }
        }

        public static void RecursiveWithRead()
        {
            Console.WriteLine("Browsing and reading values...");
            // Set timeout to only wait 1 second - default would be 1 minute to wait for good quality that may never come.
            Client.InstanceParameters.Timeouts.ReadItem = 1000;

            // Do the actual browsing and reading, starting from root of OPC address space (denoted by empty string for itemId)
            try
            {
                BrowseAndReadFromNode("");
            }
            catch (OpcException opcException)
            {
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message);
            }
        }
    }
}
#endregion
