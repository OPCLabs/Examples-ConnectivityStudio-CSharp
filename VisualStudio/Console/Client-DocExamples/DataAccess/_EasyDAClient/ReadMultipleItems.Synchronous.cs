// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
// ReSharper disable CheckNamespace
#region Example
// This example shows how to read 4 items at once synchronously, and display their values, timestamps and qualities.
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
        public static void Synchronous()
        {
            // Instantiate the client object.
            var client = new EasyDAClient();

            // Specify that only synchronous method is allowed. By default, both synchronous and asynchronous methods are
            // allowed, and the component picks a suitable method automatically. Disallowing asynchronous method leaves
            // only the synchronous method available for selection.
            client.InstanceParameters.Mode.AllowAsynchronousMethod = false;

            DAVtqResult[] vtqResults = client.ReadMultipleItems("OPCLabs.KitServer.2",
                new DAItemDescriptor[]
                    {
                        "Simulation.Random", "Trends.Ramp (1 min)", "Trends.Sine (1 min)", "Simulation.Register_I4"
                    });

            for (int i = 0; i < vtqResults.Length; i++)
            {
                Debug.Assert(vtqResults[i] != null);

                if (vtqResults[i].Succeeded)
                    Console.WriteLine("vtqResult[{0}].Vtq: {1}", i, vtqResults[i].Vtq);
                else
                    Console.WriteLine("vtqResult[{0}] *** Failure: {1}", i, vtqResults[i].ErrorMessageBrief);
            }
        }

        
        // Example output:
        //
        //vtqResult[0].Vtq: 0.00125125888851588 { System.Double} @2020-04-10T15:29:20.642; GoodNonspecific(192)
        //vtqResult[1].Vtq: 0.344052940607071 {System.Double} @2020-04-10T15:29:20.643; GoodNonspecific(192)
        //vtqResult[2].Vtq: 0.830410616568378 {System.Double} @2020-04-10T15:29:20.643; GoodNonspecific(192)
        //vtqResult[3].Vtq: 0 {System.Int32} @1601-01-01T00:00:00.000; GoodNonspecific(192)
    }
}
#endregion
