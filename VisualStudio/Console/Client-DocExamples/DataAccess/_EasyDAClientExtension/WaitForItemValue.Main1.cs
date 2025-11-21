// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
// ReSharper disable CheckNamespace
#region Example 
// This example shows how to wait on an item until a value with "good" quality becomes available.
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
    class WaitForItemValue
    {
        public static void Main1()
        {
            // Instantiate the client object.
            var client = new EasyDAClient();

            Console.WriteLine("Waiting until an item value with \"good\" quality becomes available...");
            object value;
            try
            {
                value = client.WaitForItemValue("", "OPCLabs.KitServer.2", "Demo.Unreliable", 
                    groupParameters: 100,   // this is the requested update rate
                    millisecondsTimeout: 60*1000);
            }
            catch (OpcException opcException)
            {
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message);
                return;
            }

            // Display the obtained item value.
            Console.WriteLine($"value: {value}");
        }
    }
}
#endregion
