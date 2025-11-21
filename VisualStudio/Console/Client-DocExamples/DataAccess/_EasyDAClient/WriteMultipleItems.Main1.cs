// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable StringLiteralTypo
#region Example
// This example shows how to write values, timestamps and qualities into 3 items at once.
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
    class WriteMultipleItems
    {
        public static void Main1()
        {
            // Instantiate the client object.
            var client = new EasyDAClient();

            Console.WriteLine("Writing multiple items...");
            OperationResult[] resultArray = client.WriteMultipleItems(
                new[] { 
                    new DAItemVtqArguments("OPCLabs.KitServer.2", "Simulation.Register_I4", 
                        new DAVtq(23456, DateTime.UtcNow, DAQualities.GoodNonspecific)),
                    new DAItemVtqArguments("OPCLabs.KitServer.2", "Simulation.Register_R8", 
                        new DAVtq(2.34567890, DateTime.UtcNow, DAQualities.GoodNonspecific)),
                    new DAItemVtqArguments("OPCLabs.KitServer.2", "Simulation.Register_BSTR", 
                        new DAVtq("ABC", DateTime.UtcNow, DAQualities.GoodNonspecific))
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
    }
}
#endregion
