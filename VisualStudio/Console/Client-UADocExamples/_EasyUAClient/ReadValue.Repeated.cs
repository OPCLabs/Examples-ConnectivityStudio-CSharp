// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CommentTypo
// ReSharper disable LocalizableElement
#region Example
// This example shows how to repeatedly read value of a single node, and display it.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Threading;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples._EasyUAClient
{
    partial class ReadValue
    {
        public static void Repeated()
        {
            const string endpointDescriptorUrlString =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"
            const string nodeIdExpandedText = "nsu=http://test.org/UA/Data/ ;i=10221";
            // Example settings with Softing dataFEED OPC Suite:
            //  endpointDescriptorUrlString = "opc.tcp://localhost:4980/Softing_dataFEED_OPC_Suite_Configuration1";
            //  nodeIdExpandedText = "nsu=Local%20Items ;s=Local Items.EAK_Test1.EAK_Testwert1_I4";

            // Instantiate the client object.
            var client = new EasyUAClient();

            for (int i = 1; i <= 60; i++)
            {
                Console.Write($"@{DateTime.Now}: ");

                // Obtain value of a node
                object value;
                try
                {
                    value = client.ReadValue(endpointDescriptorUrlString, nodeIdExpandedText);
                }
                catch (UAException uaException)
                {
                    Console.WriteLine($"*** Failure: {uaException.GetBaseException().Message}");
                    return;
                }

                // Display results
                Console.WriteLine($"Read {value}");

                //
                Thread.Sleep(1000);
            }
        }
    }
}
#endregion
