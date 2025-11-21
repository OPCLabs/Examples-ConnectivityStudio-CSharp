// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable AssignNullToNotNullAttribute
#region Example
// This example shows how to obtain data types of leaves in the OPC-DA address 
// space by browsing and filtering, i.e. without the use of OPC properties. 
// This technique allows determining the data types with servers that only 
// support OPC-DA 1.0. It can also be more effective than the use of 
// GetMultiplePropertyValues, if there is large number of leaves, and 
// relatively small number of data types to be checked.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Collections.Generic;
using OpcLabs.BaseLib.ComInterop;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.AddressSpace;
using OpcLabs.EasyOpc.OperationModel;

namespace DocExamples.DataAccess._EasyDAClient
{
    partial class BrowseNodes
    {
        public static void DataTypes()
        {
            // Instantiate the client object.
            var client = new EasyDAClient();

            // Define the list of data types we will be checking for. 
            // Change as needed for your application.
            // This technique is only usable if there is a known list of 
            // data types you are interested in. If you are interested in 
            // all leaves, even those that are of data types not explicitly 
            // listed, always include VarTypes.Empty as the first data type. 
            // The leaves of "unlisted" data types will have VarTypes.Empty 
            // associated with them.
            var dataTypes = new VarType[] { VarTypes.Empty, VarTypes.I2, VarTypes.R4 };

            // For each leaf found, this dictionary wil hold its associated data type.
            var dataTypeDictionary = new Dictionary<DANodeElement, VarType>();

            // For each data type, browse for leaves of this data type.
            foreach (VarType dataType in dataTypes)
            {
                var browseParameters = new DABrowseParameters(DABrowseFilter.Leaves, "", "", dataType);
                DANodeElementCollection nodeElements;
                try
                {
                    nodeElements = client.BrowseNodes("", "OPCLabs.KitServer.2", "Greenhouse", browseParameters);
                }
                catch (OpcException opcException)
                {
                    Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message);
                    return;
                }

                // Store the leaf information into the dictionary, and 
                // associate the current data type with it.
                foreach (var nodeElement in nodeElements)
                    dataTypeDictionary[nodeElement] = dataType;
            }

            // Display each leaf found, and its associated data type.
            foreach (KeyValuePair<DANodeElement, VarType> pair in dataTypeDictionary)
            {
                DANodeElement nodeElement = pair.Key;
                VarType dataType = pair.Value;
                Console.WriteLine("{0}: {1}", nodeElement, dataType);
            }
        }
    }
}
#endregion
