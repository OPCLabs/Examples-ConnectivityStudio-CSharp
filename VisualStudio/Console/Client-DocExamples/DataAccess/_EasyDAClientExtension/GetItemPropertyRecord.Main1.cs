// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
// ReSharper disable CheckNamespace
#region Example 
// This example shows how to obtain a structure containing property values for an OPC item, and display some property values.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.Extensions;
using OpcLabs.EasyOpc.OperationModel;

namespace DocExamples.DataAccess._EasyDAClientExtension
{
    class GetItemPropertyRecord
    {
        public static void Main1()
        {
            // Instantiate the client object.
            var client = new EasyDAClient();

            // Get a structure containing values of all well-known properties
            DAItemPropertyRecord itemPropertyRecord;
            try
            {
                itemPropertyRecord = client.GetItemPropertyRecord("", "OPCLabs.KitServer.2", "Simulation.Random");
            }
            catch (OpcException opcException)
            {
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message);
                return;
            }

            // Display some of the obtained property values
            Console.WriteLine("itemPropertyRecord.AccessRights: {0}", itemPropertyRecord.AccessRights);
            Console.WriteLine("itemPropertyRecord.DataType: {0}", itemPropertyRecord.DataType);
            Console.WriteLine("itemPropertyRecord.Timestamp: {0}", itemPropertyRecord.Timestamp);
        }
    }
}
#endregion
