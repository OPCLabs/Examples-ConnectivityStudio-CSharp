// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
#region Example
// Shows how to write into multiple OPC items using a single method call, specifying their requested data types.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Diagnostics;
using OpcLabs.BaseLib.ComInterop;
using OpcLabs.BaseLib.OperationModel;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.OperationModel;

namespace DocExamples.DataAccess._EasyDAClient
{
    partial class WriteMultipleItemValues
    {
        public static void RequestedDataType()
        {
            // Instantiate the client object.
            var client = new EasyDAClient();

            Console.WriteLine("Writing multiple item values...");
            OperationResult[] resultArray = client.WriteMultipleItemValues(new[] { 
                    new DAItemValueArguments("", "OPCLabs.KitServer.2", "Simulation.Register_I2", 12345) 
                        { ItemDescriptor = { RequestedDataType = VarTypes.I2}}, // <-- the requested data type
                    new DAItemValueArguments("", "OPCLabs.KitServer.2", "Simulation.Register_R4", 234.56)
                        { ItemDescriptor = { RequestedDataType = VarTypes.R4}}  // <-- the requested data type 
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

            Console.WriteLine("Reading multiple item values...");
            ValueResult[] valueResultArray = client.ReadMultipleItemValues("OPCLabs.KitServer.2",
                new DAItemDescriptor[] { "Simulation.Register_I2", "Simulation.Register_R4" });

            for (int i = 0; i < valueResultArray.Length; i++)
            {
                Debug.Assert(valueResultArray[i] != null);
                Console.WriteLine("valueResultArray[{0}]: {1}", i, valueResultArray[i]);
            }
        }
    }
}
#endregion
