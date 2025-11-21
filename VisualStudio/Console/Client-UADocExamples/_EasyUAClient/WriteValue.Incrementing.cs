// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CommentTypo
#region Example
// This example shows how to write an ever-incrementing value to an OPC UA variable.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Threading;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.AddressSpace;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples._EasyUAClient
{
    partial class WriteValue
    {
        public static void Incrementing()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"
            UANodeId nodeId = "nsu=http://test.org/UA/Data/ ;i=10221";
            // Example settings with Softing dataFEED OPC Suite: 
            //UAEndpointDescriptor endpointDescriptor =
            //    "opc.tcp://localhost:4980/Softing_dataFEED_OPC_Suite_Configuration1";
            //UANodeId nodeId = "nsu=Local%20Items ;s=Local Items.EAK_Test1.EAK_Testwert1_I4";

            // Instantiate the client object
            var client = new EasyUAClient();

            //
            int i = 0;

            do
            {
                Console.WriteLine($"@{DateTime.Now}: Writing {i}");
                try
                {
                    client.WriteValue(endpointDescriptor, nodeId, i);
                }
                catch (UAException uaException)
                {
                    Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
                    return;
                }
                i = unchecked((i + 1) & 0x7FFFFFFF);
                Thread.Sleep(2 * 1000);
            } while (!Console.KeyAvailable);
        }
    }
}
#endregion
