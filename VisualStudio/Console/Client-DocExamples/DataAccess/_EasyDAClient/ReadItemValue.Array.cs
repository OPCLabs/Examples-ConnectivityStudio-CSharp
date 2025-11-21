// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
#region Example
// Shows how to read an OPC item that is of array type.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.OperationModel;

namespace DocExamples.DataAccess._EasyDAClient
{
    partial class ReadItemValue
    {
        public static void Array()
        {
            // Instantiate the client object.
            var client = new EasyDAClient();

            Console.WriteLine("Reading array value...");
            int[] value;
            try
            {
                // UInt16 is returned as Int32, because UInt16 is not a CLS-compliant type (and is not supported in VB.NET).
                value = (int[])client.ReadItemValue("", "OPCLabs.KitServer.2", "Simulation.ReadValue_ArrayOfUI2");
            }
            catch (OpcException opcException)
            {
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message);
                return;
            }

            if (!(value is null))
            {
                Console.WriteLine(value[0]);
                Console.WriteLine(value[1]);
                Console.WriteLine(value[2]);
            }
        }
    }
}
#endregion
