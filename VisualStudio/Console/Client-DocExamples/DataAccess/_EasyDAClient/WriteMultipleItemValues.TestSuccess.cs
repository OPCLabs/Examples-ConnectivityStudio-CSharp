// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
#region Example
// This example shows how to write values into 3 items at once, test for success of each write and display the exception 
// message in case of failure.
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
        public static void TestSuccess()
        {
            // Instantiate the client object.
            var client = new EasyDAClient();

            Console.WriteLine("Writing multiple item values...");
            OperationResult[] resultArray = client.WriteMultipleItemValues(
                new[] { 
                    new DAItemValueArguments("", "OPCLabs.KitServer.2", "Simulation.Register_I4", 23456),
                    new DAItemValueArguments("", "OPCLabs.KitServer.2", "Simulation.Register_R8", 
                        "This string cannot be converted to VT_R8"),
                    new DAItemValueArguments("", "OPCLabs.KitServer.2", "UnknownItem", "ABC")
                });


            for (int i = 0; i < resultArray.Length; i++)
            {
                Debug.Assert(resultArray[i] != null);
                if (resultArray[i].Succeeded)
                    Console.WriteLine("Result {0}: success", i);
                else
                {
                    Debug.Assert(!(resultArray[i].Exception is null));
                    Console.WriteLine("Result {0} *** Failure: {1}", i, resultArray[i].ErrorMessageBrief);
                }
            }
        }


        // Example output:
        //
        //Writing multiple item values...
        //Result 0: success
        //Result 1 *** Failure: Type mismatch.  [...]
        //Result 2 *** Failure: The item is no longer available in the server address space. [...]
    }
}
#endregion
