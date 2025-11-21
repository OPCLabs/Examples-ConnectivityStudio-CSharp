// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// Shows how to read complex data with OPC UA Complex Data plug-in.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.BaseLib.DataTypeModel;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.ComplexData;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples.ComplexData._EasyUAClient
{
    class ReadValue
    {
        public static void Main1()
        {
            // Define which server and node we will work with.
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"
            UANodeDescriptor nodeDescriptor =
                "nsu=http://test.org/UA/Data/ ;i=10239"; // [ObjectsFolder]/Data.Static.Scalar.StructureValue

            // Instantiate the client object.
            var client = new EasyUAClient();

            // Read a node which returns complex data. This is done in the same way as regular reads - just the data 
            // returned is different.
            object value;
            try
            {
                value = client.ReadValue(endpointDescriptor, nodeDescriptor);
            }
            catch (UAException uaException)
            {
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
                return;
            }

            // Display basic information about what we have read.
            Console.WriteLine(value);


            // We know that this node returns complex data, so we can type cast to UAGenericObject.
            var genericObject = (UAGenericObject) value;

            // The actual data is in the GenericData property of the UAGenericObject.
            //
            // If we want to see the whole hierarchy of the received complex data, we can format it with the "V" (verbose)
            // specifier. In the debugger, you can view the same by displaying the private DebugView property.
            Console.WriteLine();
            Console.WriteLine("{0:V}", genericObject.GenericData);

            // For processing the internals of the data, refer to examples for GenericData and DataType classes.

            // .Int32Value
            var int32Value = ((genericObject.GenericData as StructuredData)
                ?.FieldData["Int32Value"] as PrimitiveData)
                ?.Value as int?;
            Console.WriteLine();
            Console.WriteLine($"Int32Value: {int32Value}");


            // Example output (truncated):
            //
            //(ScalarValueDataType) structured
            //
            //(ScalarValueDataType) structured   
            //  [BooleanValue] (Boolean) primitive; True {System.Boolean}
            //  [ByteStringValue] (ByteString) primitive; System.Byte[] {System.Byte[]}
            //  [ByteValue] (Byte) primitive; 153 {System.Byte}
            //  [DateTimeValue] (DateTime) primitive; 5/11/2013 4:32:00 PM {System.DateTime}
            //  [DoubleValue] (Double) primitive; -8.93178007363702E+27 {System.Double}
            //  [EnumerationValue] (Int32) primitive; 0 {System.Int32}
            //  [ExpandedNodeIdValue] (ExpandedNodeId) structured
            //    [NamespaceURI] (CharArray) primitive; "http://samples.org/UA/memorybuffer/Instance" {System.String}
            //    [NamespaceURISpecified] (Bit) primitive; True {System.Boolean}
            //    [NodeIdType] (NodeIdType) enumeration; 3 (String)
            //    [ServerIndexSpecified] (Bit) primitive; False {System.Boolean}
            //    [String] (StringNodeId) structured
            //      [Identifier] (CharArray) primitive; "????" {System.String}
            //      [NamespaceIndex] (UInt16) primitive; 0 {System.UInt16}
            //  [FloatValue] (Float) primitive; 78.37176 {System.Single}
            //  [GuidValue] (Guid) primitive; 8129cdaf-24d9-8140-64f2-3a6d7a957fd7 {System.Guid}
            //  [Int16Value] (Int16) primitive; 2793 {System.Int16}
            //  [Int32Value] (Int32) primitive; 1133391074 {System.Int32}
            //  [Int64Value] (Int64) primitive; -1039109760798965779 {System.Int64}
            //  [Integer] (Variant) structured
            //    [ArrayDimensionsSpecified] sequence[1]
            //      [0] (Bit) primitive; False {System.Boolean}
            //    [ArrayLengthSpecified] sequence[1]
            //      [0] (Bit) primitive; False {System.Boolean}
            //    [Int64] sequence[1]
            //      [0] (Int64) primitive; 0 {System.Int64}
            //    [VariantType] sequence[6]
            //      [0] (Bit) primitive; False {System.Boolean}
            //      [1] (Bit) primitive; False {System.Boolean}
            //      [2] (Bit) primitive; False {System.Boolean}
            //      [3] (Bit) primitive; True {System.Boolean}
            //      [4] (Bit) primitive; False {System.Boolean}
            //      [5] (Bit) primitive; False {System.Boolean}
            //  [LocalizedTextValue] (LocalizedText) structured
            //    [Locale] (CharArray) primitive; "ko" {System.String}
            //    [LocaleSpecified] (Bit) primitive; True {System.Boolean}
            //    [Reserved1] sequence[6]
            //      [0] (Bit) primitive; False {System.Boolean}
            //      [1] (Bit) primitive; False {System.Boolean}
            //      [2] (Bit) primitive; False {System.Boolean}
            //      [3] (Bit) primitive; False {System.Boolean}
            //      [4] (Bit) primitive; False {System.Boolean}
            //      [5] (Bit) primitive; False {System.Boolean}
            //    [Text] (CharArray) primitive; "? ?? ??+ ??? ??) ?: ???? ?! ?!" {System.String}
            //    [TextSpecified] (Bit) primitive; True {System.Boolean}
            //  [NodeIdValue] (NodeId) structured                                                                               
        }
    }
}
#endregion
