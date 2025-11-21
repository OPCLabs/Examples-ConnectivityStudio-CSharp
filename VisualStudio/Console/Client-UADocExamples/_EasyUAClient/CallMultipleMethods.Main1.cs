// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable LocalizableElement
#region Example
// This example shows how to call multiple methods, and pass arguments to and
// from them.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Diagnostics;
using OpcLabs.BaseLib.OperationModel;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples._EasyUAClient
{
    class CallMultipleMethods
    {
        public static void Main1()
        {

            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"
            UANodeDescriptor nodeDescriptor = "nsu=http://test.org/UA/Data/ ;i=10755";

            object[] inputs1 =
            {
                false, 
                1, 
                2, 
                3, 
                4, 
                5, 
                6, 
                7, 
                8, 
                9, 
                10
            };

            TypeCode[] typeCodes1 =
            {
                TypeCode.Boolean,
                TypeCode.SByte,
                TypeCode.Byte,
                TypeCode.Int16,
                TypeCode.UInt16,
                TypeCode.Int32,
                TypeCode.UInt32,
                TypeCode.Int64,
                TypeCode.UInt64,
                TypeCode.Single,
                TypeCode.Double
            };

            object[] inputs2 =
            {
                false, 
                1, 
                2, 
                3, 
                4, 
                5, 
                6, 
                7, 
                8, 
                9, 
                10,
                "eleven"
            };

            TypeCode[] typeCodes2 =
            {
                TypeCode.Boolean,
                TypeCode.SByte,
                TypeCode.Byte,
                TypeCode.Int16,
                TypeCode.UInt16,
                TypeCode.Int32,
                TypeCode.UInt32,
                TypeCode.Int64,
                TypeCode.UInt64,
                TypeCode.Single,
                TypeCode.Double,
                TypeCode.String
            };

            // Instantiate the client object
            var client = new EasyUAClient();

            // Perform the operation.
            ValueArrayResult[] results = client.CallMultipleMethods(new[]
            {
                new UACallArguments(endpointDescriptor, nodeDescriptor, 
                    "nsu=http://test.org/UA/Data/ ;i=10756", inputs1, typeCodes1),
                new UACallArguments(endpointDescriptor, nodeDescriptor, 
                    "nsu=http://test.org/UA/Data/ ;i=10774", inputs2, typeCodes2)
            });

            // Display results
            for (int i = 0; i < results.Length; i++)
            {
                Console.WriteLine();
                Console.WriteLine($"results[{i}]:");

                ValueArrayResult result = results[i];
                if (result.Succeeded)
                {
                    object[] outputs = result.ValueArray;
                    Debug.Assert(outputs != null);

                    for (int j = 0; j < outputs.Length; j++)
                        Console.WriteLine($"    outputs[{j}]: {outputs[j]}");
                }
                else
                    Console.WriteLine($"*** Failure: {result.ErrorMessageBrief}");
            }

            // Example output:
            //results[0]:
            //    outputs[0]: False
            //    outputs[1]: 1
            //    outputs[2]: 2
            //    outputs[3]: 3
            //    outputs[4]: 4
            //    outputs[5]: 5
            //    outputs[6]: 6
            //    outputs[7]: 7
            //    outputs[8]: 8
            //    outputs[9]: 9
            //    outputs[10]: 10

            //results[1]:
            //    outputs[0]: False
            //    outputs[1]: 1
            //    outputs[2]: 2
            //    outputs[3]: 3
            //    outputs[4]: 4
            //    outputs[5]: 5
            //    outputs[6]: 6
            //    outputs[7]: 7
            //    outputs[8]: 8
            //    outputs[9]: 9
            //    outputs[10]: 10
            //    outputs[11]: eleven
        }
    }
}
#endregion
