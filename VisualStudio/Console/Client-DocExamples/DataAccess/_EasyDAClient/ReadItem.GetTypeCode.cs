// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
// ReSharper disable CheckNamespace
#region Example 
// This example shows how to read a single item and obtains a type code of the received value.
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
    partial class ReadItem
    {
        public static void GetTypeCode()
        {
            // Instantiate the client object.
            var client = new EasyDAClient();

            DAVtq vtq;
            try
            {
                vtq = client.ReadItem("", "OPCLabs.KitServer.2", "Simulation.Random");
            }
            catch (OpcException opcException)
            {
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message);
                return;
            }

            if (!(vtq.Value is null))
            {
                TypeCode typeCode = Type.GetTypeCode(vtq.Value.GetType());

                Console.WriteLine("TypeCode: {0}", typeCode);
            }
        }
    }
}
#endregion
