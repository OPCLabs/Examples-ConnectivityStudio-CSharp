// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to read the DataType attributes of 3 different nodes at once. Using the same method, it is also possible 
// to read multiple attributes of the same node.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.BaseLib.OperationModel;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.AddressSpace;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples._EasyUAClient
{
    partial class ReadMultipleValues
    {
        public static void DataType()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client object.
            var client = new EasyUAClient();

            // Obtain values.
            ValueResult[] valueResultArray = client.ReadMultipleValues(new[]
                {
                    new UAReadArguments(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10845", UAAttributeId.DataType),
                    new UAReadArguments(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10853", UAAttributeId.DataType),
                    new UAReadArguments(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10855", UAAttributeId.DataType)
                });

            // Display results.
            foreach (ValueResult valueResult in valueResultArray)
            {
                Console.WriteLine();

                if (valueResult.Succeeded)
                {
                    Console.WriteLine($"Value: {valueResult.Value}");
                    var dataTypeId = valueResult.Value as UANodeId;
                    if (!(dataTypeId is null))
                    {
                        Console.WriteLine($"Value.ExpandedText: {dataTypeId.ExpandedText}");
                        Console.WriteLine($"Value.NamespaceUriString: {dataTypeId.NamespaceUriString}");
                        Console.WriteLine($"Value.NamespaceIndex: {dataTypeId.NamespaceIndex}");
                        Console.WriteLine($"Value.NumericIdentifier: {dataTypeId.NumericIdentifier}");
                    }
                }
                else
                    Console.WriteLine($"*** Failure: {valueResult.ErrorMessageBrief}");
            }

            // Example output:
            //
            //Value: SByte
            //Value.ExpandedText: nsu=http://opcfoundation.org/UA/ ;i=2
            //Value.NamespaceUriString: http://opcfoundation.org/UA/
            //Value.NamespaceIndex: 0
            //Value.NumericIdentifier: 2
            //
            //Value: Float
            //Value.ExpandedText: nsu=http://opcfoundation.org/UA/ ;i=10
            //Value.NamespaceUriString: http://opcfoundation.org/UA/
            //Value.NamespaceIndex: 0
            //Value.NumericIdentifier: 10
            //
            //Value: String
            //Value.ExpandedText: nsu=http://opcfoundation.org/UA/ ;i=12
            //Value.NamespaceUriString: http://opcfoundation.org/UA/
            //Value.NamespaceIndex: 0
            //Value.NumericIdentifier: 12
        }
    }
}
#endregion
