// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
#region Example
// Shows how to write into multiple OPC items using a single method call, and read multiple item values back.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Diagnostics;
using OpcLabs.BaseLib.OperationModel;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.OperationModel;

namespace DocExamples.DataAccess._EasyDAClient
{
    partial class WriteMultipleItemValues
    {
        public static void Main1()
        {
            // Instantiate the client object.
            var client = new EasyDAClient();

            Console.WriteLine("Writing multiple item values...");
            OperationResult[] resultArray = client.WriteMultipleItemValues(
                new[] { 
                    new DAItemValueArguments("", "OPCLabs.KitServer.2", "Simulation.Register_I4", 12345),
                    new DAItemValueArguments("", "OPCLabs.KitServer.2", "Simulation.Register_BOOL", true),
                    new DAItemValueArguments("", "OPCLabs.KitServer.2", "Simulation.Register_R4", 234.56)
                });
            
            for (int i = 0; i < resultArray.Length; i++)
            {
                Debug.Assert(resultArray[i] != null);
                if (resultArray[i].Succeeded)
                    Console.WriteLine($"Results[{i}]: success");
                else
                {
                    Debug.Assert(!(resultArray[i].Exception is null));
                    Console.WriteLine($"Results[{i}] *** Failure: {resultArray[i].ErrorMessageBrief}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Reading multiple item values...");
            ValueResult[] valueResultArray = client.ReadMultipleItemValues("OPCLabs.KitServer.2",
                new DAItemDescriptor[] { 
                        "Simulation.Register_I4", 
                        "Simulation.Register_BOOL", 
                        "Simulation.Register_R4" });

            for (int i = 0; i < valueResultArray.Length; i++)
            {
                Debug.Assert(valueResultArray[i] != null);
                Console.WriteLine($"valueResultArray[{i}]: {valueResultArray[i]}");
            }


            // Example output:
            //
            //Writing multiple item values...
            //Results[0]: success
            //Results[1]: success
            //Results[2]: success
            //
            //Reading multiple item values...
            //valueResultArray[0]: Success; 12345 {System.Int32}
            //valueResultArray[1]: Success; True {System.Boolean}
            //valueResultArray[2]: Success; 234.56 {System.Single}
        }
    }
}
#endregion
