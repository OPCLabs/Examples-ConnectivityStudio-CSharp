// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CoVariantArrayConversion
// ReSharper disable PossibleNullReferenceException
// ReSharper disable ConvertIfStatementToConditionalTernaryExpression
#region Example
// This example shows how to write values into 3 nodes at once, test for success of each write and display the exception 
// message in case of failure.
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
    partial class WriteMultipleValues
    {
        public static void TestSuccess()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"
            
            // Instantiate the client object.
            var client = new EasyUAClient();

            Console.WriteLine("Modifying values of nodes...");
            OperationResult[] operationResultArray = client.WriteMultipleValues(new[]
                {
                    new UAWriteValueArguments(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10221", 23456),
                    new UAWriteValueArguments(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10226", 
                        "This string cannot be converted to Double"),
                    new UAWriteValueArguments(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;s=UnknownNode", "ABC")
                });

            for (int i = 0; i < operationResultArray.Length; i++)
                if (operationResultArray[i].Succeeded)
                    Console.WriteLine($"Result {i}: success");
                else
                    Console.WriteLine($"Result {i}: {operationResultArray[i].Exception.GetBaseException().Message}");


            // Example output:
            //
            //Modifying values of nodes...
            //Result 0: success
            //Result 1: Input string was not in a correct format.
            //+ Attempting to change an object of type "System.String" to type "System.Double".
            //+ The specified original value (string) was "This string cannot be converted to Double".
            //+ The node descriptor used was: NodeId="nsu=http://test.org/UA/Data/ ;i=10226".
            //+ The client method called (or event/callback invoked) was 'WriteMultiple[3]'.
            //Result 2: The status of the OPC-UA attribute data is not Good. The actual status is 'BadNodeIdUnknown'.
            //+ During writing or method calls, readings may occur when value type is not specified.
            //+ The node descriptor used was: NodeId="nsu=http://test.org/UA/Data/ ;s=UnknownNode".
            //+ The client method called (or event/callback invoked) was 'WriteMultiple[3]'.
        }
    }
}
#endregion
