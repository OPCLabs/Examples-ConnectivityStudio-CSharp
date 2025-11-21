// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
// ReSharper disable CheckNamespace
#region Example
// This example shows how to read 4 items from the device, and display their values, timestamps and qualities.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Diagnostics;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.OperationModel;

namespace DocExamples.DataAccess._EasyDAClient
{
    partial class ReadMultipleItems
    {
        public static void DeviceSource()
        {
            // Instantiate the client object.
            var client = new EasyDAClient();

            // DADataSource enumeration:
            // Selects the data source for OPC reads (from device, from OPC cache, or dynamically determined).
            // The data source (memory, OPC cache or OPC device) selection will be based on the desired value age and
            // current status of data received from the server.

            DAVtqResult[] vtqResults = client.ReadMultipleItems(
                new []
                    {
                        new DAReadItemArguments("OPCLabs.KitServer.2", "Simulation.Random", DADataSource.Device),
                        new DAReadItemArguments("OPCLabs.KitServer.2", "Trends.Ramp (1 min)", DADataSource.Device),
                        new DAReadItemArguments("OPCLabs.KitServer.2", "Trends.Sine (1 min)", DADataSource.Device),
                        new DAReadItemArguments("OPCLabs.KitServer.2", "Simulation.Register_I4", DADataSource.Device)
                    });

            for (int i = 0; i < vtqResults.Length; i++)
            {
                Debug.Assert(vtqResults[i] != null);

                if (vtqResults[i].Succeeded)
                    Console.WriteLine("vtqResults[{0}].Vtq: {1}", i, vtqResults[i].Vtq);
                else
                    Console.WriteLine("vtqResults[{0}] *** Failure: {1}", i, vtqResults[i].ErrorMessageBrief);
            }
        }


        // Example output:
        //
        //vtqResults[0].Vtq: 0.00125125888851588 { System.Double} @2020-04-10T12:44:16.250; GoodNonspecific(192)
        //vtqResults[1].Vtq: 0.270812898874283 {System.Double} @2020-04-10T12:44:16.248; GoodNonspecific(192)
        //vtqResults[2].Vtq: 0.991434340167834 {System.Double} @2020-04-10T12:44:16.250; GoodNonspecific(192)
        //vtqResults[3].Vtq: 0 {System.Int32} @1601-01-01T00:00:00.000; GoodNonspecific(192)
    }
}
#endregion
