// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
// ReSharper disable CheckNamespace
#region Example 
// This example shows how to read a single item from the device, and display its value, timestamp and quality.
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
        public static void DeviceSource()
        {
            // Instantiate the client object.
            var client = new EasyDAClient();

            Console.WriteLine("Reading item...");
            DAVtq vtq;
            try
            {
                // DADataSource enumeration:
                // Selects the data source for OPC reads (from device, from OPC cache, or dynamically determined).
                // The data source (memory, OPC cache or OPC device) selection is based on the desired value age and
                // current status of data received from the server.

                vtq = client.ReadItem("OPCLabs.KitServer.2", "Simulation.Random", DADataSource.Device);
            }
            catch (OpcException opcException)
            {
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message);
                return;
            }

            Console.WriteLine("Vtq: {0}", vtq);
        }
    }
}
#endregion
