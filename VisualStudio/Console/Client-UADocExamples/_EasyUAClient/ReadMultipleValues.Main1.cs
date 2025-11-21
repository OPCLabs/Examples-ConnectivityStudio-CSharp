// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
// ReSharper disable ConvertIfStatementToConditionalTernaryExpression
#region Example
// This example shows how to read the Value attributes of 3 different nodes at once. Using the same method, it is also possible 
// to read multiple attributes of the same node.
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
    partial class ReadMultipleValues
    {
        public static void Main1()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client object.
            var client = new EasyUAClient();

            // Obtain values. By default, the Value attributes of the nodes will be read.
            ValueResult[] valueResultArray = client.ReadMultipleValues(new[]
                {
                    new UAReadArguments(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10845"),
                    new UAReadArguments(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10853"),
                    new UAReadArguments(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10855")
                });

            // Display results.
            foreach (ValueResult valueResult in valueResultArray)
            {
                if (valueResult.Succeeded)
                    Console.WriteLine($"Value: {valueResult.Value}");
                else
                    Console.WriteLine($"*** Failure: {valueResult.ErrorMessageBrief}");
            }


            // Example output:
            //
            //Value: 8
            //Value: -8.06803E+21
            //Value: Strawberry Pig Banana Snake Mango Purple Grape Monkey Purple? Blueberry Lemon^            
        }
    }
}
#endregion
