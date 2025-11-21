// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
// ReSharper disable CheckNamespace
#region Example
// This example shows how to write a value, timestamp and quality into a single item.
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
    class WriteItem
    {
        public static void Main1()
        {
            // Instantiate the client object.
            var client = new EasyDAClient();

            try
            {
                client.WriteItem("", "OPCLabs.KitServer.2", "Simulation.Register_I4", 
                    12345, DateTime.SpecifyKind(new DateTime(1980, 1, 1), DateTimeKind.Utc), 192);
            }
            catch (OpcException opcException)
            {
                Console.WriteLine($"*** Failure: {opcException.GetBaseException().Message}");
                return;
            }
            Console.WriteLine("Success");
        }
    }
}
#endregion
