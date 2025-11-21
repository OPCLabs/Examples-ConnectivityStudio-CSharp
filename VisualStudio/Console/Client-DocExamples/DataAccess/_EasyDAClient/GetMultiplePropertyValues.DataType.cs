// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to obtain a data type of all OPC items under a branch.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Linq;
using OpcLabs.BaseLib.ComInterop;
using OpcLabs.BaseLib.OperationModel;
using OpcLabs.EasyOpc;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.AddressSpace;

namespace DocExamples.DataAccess._EasyDAClient
{
    partial class GetMultiplePropertyValues
    {
        public static void DataType()
        {
            // Instantiate the client object.
            var client = new EasyDAClient();
            ServerDescriptor serverDescriptor = "OPCLabs.KitServer.2";

            // Browse for all leaves under the "Simulation" branch
            DANodeElementCollection nodeElementCollection = client.BrowseLeaves(serverDescriptor, "Simulation");

            // Create list of node descriptors, one for each leaf obtained
            DANodeDescriptor[] nodeDescriptorArray = nodeElementCollection
                .Where(element => !element.IsHint)  // filter out hint leafs that do not represent real OPC items (rare)
                .Select(element => new DANodeDescriptor(element))
                .ToArray();

            // Get the value of DataType property; it is a 16-bit signed integer
            ValueResult[] valueResultArray = client.GetMultiplePropertyValues(serverDescriptor,
                nodeDescriptorArray, DAPropertyIds.DataType);

            for (int i = 0; i < valueResultArray.Length; i++)
            {
                DANodeDescriptor nodeDescriptor = nodeDescriptorArray[i];

                // Check if there has been an error getting the property value
                ValueResult valueResult = valueResultArray[i];
                if (!(valueResult.Exception is null))
                {
                    Console.WriteLine("{0} *** Failure: {1}", nodeDescriptor.NodeId, valueResult.Exception.Message);
                    continue;
                }

                // Convert the data type to VarType
                var varType = (VarType)(short)valueResult.Value;

                // Display the obtained data type
                Console.WriteLine("{0}: {1}", nodeDescriptor.ItemId, varType);
            }
        }
    }
}
#endregion
