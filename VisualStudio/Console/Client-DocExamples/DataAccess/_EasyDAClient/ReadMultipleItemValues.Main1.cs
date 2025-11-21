// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
// ReSharper disable CheckNamespace
// ReSharper disable ConvertIfStatementToConditionalTernaryExpression
#region Example
// This example shows how to read values of 4 items at once, and display them.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Diagnostics;
using OpcLabs.BaseLib.OperationModel;
using OpcLabs.EasyOpc.DataAccess;

namespace DocExamples.DataAccess._EasyDAClient
{
    class ReadMultipleItemValues
    {
        public static void Main1()
        {
            // Instantiate the client object.
            var client = new EasyDAClient();

            ValueResult[] valueResults = client.ReadMultipleItemValues("OPCLabs.KitServer.2",
                new DAItemDescriptor[]
                {
                    "Simulation.Random", "Trends.Ramp (1 min)", "Trends.Sine (1 min)", "Simulation.Register_I4"
                });

            for (int i = 0; i < valueResults.Length; i++)
            {
                ValueResult valueResult = valueResults[i];
                Debug.Assert(!(valueResult is null));

                if (valueResult.Succeeded)
                    Console.WriteLine($"valueResults[{i}].Value: {valueResult.Value}");
                else
                    Console.WriteLine($"valueResults[{i}] *** Failure: {valueResult.ErrorMessageBrief}");
            }
        }


        // Example output:
        //
        //valueResults[0].Value: 0.00125125888851588
        //valueResults[1].Value: 0.732510924339294
        //valueResults[2].Value: -0.993968485238202
        //valueResults[3].Value: 0
    }
}
#endregion
