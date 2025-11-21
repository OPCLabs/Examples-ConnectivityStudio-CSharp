// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CoVariantArrayConversion
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to write values into 3 nodes at once, specifying a type explicitly. It tests for success of each 
// write and displays the exception message in case of failure.
//
// Reasons for specifying the type explicitly might be:
// - The data type in the server has subtypes, and the client therefore needs to pick the subtype to be written.
// - The data type that the reports is incorrect.
// - Writing with an explicitly specified type is more efficient.
//
// Alternative ways of specifying the type are using the ValueTypeCode or ValueTypeFullName properties.
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
        public static void ValueType()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client object
            var client = new EasyUAClient();

            // Modify value of a node
            OperationResult[] operationResultArray = client.WriteMultipleValues(new[]
                {
                    new UAWriteValueArguments(endpointDescriptor, 
                        "nsu=http://test.org/UA/Data/ ;i=10221", 23456) 
                        {ValueType = typeof(Int32)},    // here is the type explicitly specified
                    new UAWriteValueArguments(endpointDescriptor,
                        "nsu=http://test.org/UA/Data/ ;i=10226", "This string cannot be converted to Double")
                        {ValueType = typeof(Double)},    // here is the type explicitly specified
                    new UAWriteValueArguments(endpointDescriptor,
                        "nsu=http://test.org/UA/Data/ ;s=UnknownNode", "ABC")
                        {ValueType = typeof(string)}    // here is the type explicitly specified
                });

            for (int i = 0; i < operationResultArray.Length; i++)
                if (operationResultArray[i].Succeeded)
                    Console.WriteLine("Result {0}: success", i);
                else
                    Console.WriteLine("Result {0}: {1}", i, operationResultArray[i].Exception.GetBaseException().Message);
        }
    }
}
#endregion
