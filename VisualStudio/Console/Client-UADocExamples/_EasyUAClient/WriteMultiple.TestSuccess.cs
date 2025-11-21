// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CoVariantArrayConversion
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to write data (a value, timestamps and status code) into 3 nodes at once, test for success of each 
// write and display the exception message in case of failure.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.BaseLib.OperationModel;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples._EasyUAClient
{
    class WriteMultiple
    {
        public static void TestSuccess()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client object
            var client = new EasyUAClient();

            // Modify data of nodes' attributes
            OperationResult[] operationResultArray = client.WriteMultiple(new[]
                {
                    new UAWriteArguments(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10221", 
                        new UAAttributeData(23456, UASeverity.GoodOrSuccess, DateTime.UtcNow)),
                    new UAWriteArguments(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10226", 
                        new UAAttributeData(2.34567890, UASeverity.GoodOrSuccess, DateTime.UtcNow)),
                    new UAWriteArguments(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10227", 
                        new UAAttributeData("ABC", UASeverity.GoodOrSuccess, DateTime.UtcNow))
                });

            // The target server may not support this, and in such case failures will occur.

            for (int i = 0; i < operationResultArray.Length; i++)
                if (operationResultArray[i].Succeeded)
                    Console.WriteLine("Result {0}: success", i);
                else
                    Console.WriteLine("Result {0}: {1}", i, operationResultArray[i].Exception.GetBaseException().Message);
        }
    }
}
#endregion
