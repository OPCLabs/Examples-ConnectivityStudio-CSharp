// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable CheckNamespace
#region Example
// Shows how to obtain object describing the data type of complex data node with OPC UA Complex Data plug-in.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using Microsoft.Extensions.DependencyInjection;
using OpcLabs.BaseLib.DataTypeModel;
using OpcLabs.BaseLib.OperationModel.Generic;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.AddressSpace;
using OpcLabs.EasyOpc.UA.AddressSpace.Standard;
using OpcLabs.EasyOpc.UA.InformationModel;
using OpcLabs.EasyOpc.UA.Plugins.ComplexData;

namespace UADocExamples.ComplexData._IEasyUAClientComplexData
{
    class ResolveDataType
    {
        public static void Main1()
        {
            // Define which server we will work with.
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client object.
            var client = new EasyUAClient();

            // Obtain the data type ID.
            //
            // In many cases, you would be able to obtain the data type ID of a particular node by reading its DataType
            // attribute, or easier, by calling the extension method ReadDataType on the IEasyUAClient interface.
            // The sample server, however, shows a more advanced approach in which the data type ID refers to an abstract
            // data type, and the actual values are then sub-types of this base data type. This abstract data type does not
            // have any encodings associated with it and it is therefore not possible to extract its description from the
            // server. We therefore use a hard-coded data type ID for one of the sub-types in this example.
            //
            // The code to obtain the data type ID for given node would normally look like this:
            //    UANodeId dataTypeId = client.ReadDataType(
            //        endpointDescriptor,
            //        "nsu=http://test.org/UA/Data/ ;i=10239");	// [ObjectsFolder]/Data.Static.Scalar.StructureValue
            //
            UANodeId dataTypeId = "nsu=http://test.org/UA/Data/ ;i=9440";    // ScalarValueDataType

            // Get the IEasyUAClientComplexData service from the client. This is needed for advanced complex data 
            // operations.
            IEasyUAClientComplexData complexData = client.GetService<IEasyUAClientComplexData>();
            
            // Resolve the data type ID to the data type object, containing description of the data type.
            ValueResult<DataType> dataTypeResult = complexData.ResolveDataType(
                new UAModelNodeDescriptor(endpointDescriptor, dataTypeId), 
                UABrowseNames.DefaultBinary);
            // Check if the operation succeeded. Use the ThrowIfFailed method instead if you want exception be thrown.
            if (!dataTypeResult.Succeeded)
            {
                Console.WriteLine("*** Failure: {0}", dataTypeResult.ErrorMessageBrief);
                return;
            }

            // The actual data type is in the Value property.
            // Display basic information about what we have obtained.
            Console.WriteLine(dataTypeResult.Value);

            // If we want to see the whole hierarchy of the received data type, we can format it with the "V" (verbose)
            // specifier. In the debugger, you can view the same by displaying the private DebugView property.
            Console.WriteLine();
            Console.WriteLine("{0:V}", dataTypeResult.Value);

            // For processing the internals of the data type, refer to examples for GenericData class.


            // Example output (truncated):
            //
            //ScalarValueDataType = structured
            //
            //ScalarValueDataType = structured
            //  [BooleanValue] Boolean = primitive(System.Boolean)
            //  [ByteStringValue] ByteString = primitive(System.Byte[])
            //  [ByteValue] Byte = primitive(System.Byte)
            //  [DateTimeValue] DateTime = primitive(System.DateTime)
            //  [DoubleValue] Double = primitive(System.Double)
            //  [EnumerationValue] Int32 = primitive(System.Int32)
            //  [ExpandedNodeIdValue] ExpandedNodeId = structured
            //    [ByteString] optional ByteStringNodeId = structured
            //      [Identifier] ByteString = primitive(System.Byte[])
            //      [NamespaceIndex] UInt16 = primitive(System.UInt16)
            //    [FourByte] optional FourByteNodeId = structured
            //      [Identifier] UInt16 = primitive(System.UInt16)
            //      [NamespaceIndex] Byte = primitive(System.Byte)
            //    [Guid] optional GuidNodeId = structured
            //      [Identifier] Guid = primitive(System.Guid)
            //      [NamespaceIndex] UInt16 = primitive(System.UInt16)
            //    [NamespaceURI] optional CharArray = primitive(System.String)
            //    [NamespaceURISpecified] switch Bit = primitive(System.Boolean)
            //    [NodeIdType] switch NodeIdType = enumeration(6)
            //      TwoByte = 0
            //      FourByte = 1
            //      Numeric = 2
            //      String = 3
            //      Guid = 4
            //      ByteString = 5
            //    [Numeric] optional NumericNodeId = structured
            //      [Identifier] UInt32 = primitive(System.UInt32)
            //      [NamespaceIndex] UInt16 = primitive(System.UInt16)
            //    [ServerIndex] optional UInt32 = primitive(System.UInt32)
            //    [ServerIndexSpecified] switch Bit = primitive(System.Boolean)
            //    [String] optional StringNodeId = structured
            //      [Identifier] CharArray = primitive(System.String)
            //      [NamespaceIndex] UInt16 = primitive(System.UInt16)
            //    [TwoByte] optional TwoByteNodeId = structured
            //      [Identifier] Byte = primitive(System.Byte)
            //  [FloatValue] Float = primitive(System.Single)
            //  [GuidValue] Guid = primitive(System.Guid)
            //  [Int16Value] Int16 = primitive(System.Int16)
            //  [Int32Value] Int32 = primitive(System.Int32)
            //  [Int64Value] Int64 = primitive(System.Int64)
            //  [Integer] Variant = structured
            //    [ArrayDimensions] optional sequence[*] of Int32 = primitive(System.Int32)
            //    [ArrayDimensionsSpecified] switch sequence[1] of Bit = primitive(System.Boolean)
            //    [ArrayLength] length optional Int32 = primitive(System.Int32)
            //    [ArrayLengthSpecified] switch sequence[1] of Bit = primitive(System.Boolean)
            //    [Boolean] optional sequence[*] of Boolean = primitive(System.Boolean)
            //    [Byte] optional sequence[*] of Byte = primitive(System.Byte)
        }
    }
}
#endregion
