// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
#region Example 
// This example shows how to obtain a data type of an OPC item.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.BaseLib.ComInterop;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.Extensions;
using OpcLabs.EasyOpc.OperationModel;

namespace DocExamples.DataAccess._EasyDAClientExtension
{
    class GetDataTypePropertyValue
    {
        public static void Main1()
        {
            // Instantiate the client object.
            var client = new EasyDAClient();

            // Get the DataType property value, already converted to VarType
            VarType varType;
            try
            {
                varType = client.GetDataTypePropertyValue("", "OPCLabs.KitServer.2", "Simulation.Random");
            }
            catch (OpcException opcException)
            {
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message);
                return;
            }

            // Display the obtained data type
            Console.WriteLine("VarType: {0}", varType); // Display data type symbolically
        }
    }
}
#endregion
