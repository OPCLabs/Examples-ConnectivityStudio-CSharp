// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
// ReSharper disable SwitchStatementMissingSomeCases
#region Example
// Shows how to process a data type, displaying some of its properties, recursively.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Linq;
using OpcLabs.BaseLib.DataTypeModel;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.ComplexData;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples.ComplexData._DataType
{
    class Kind
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

            // Read a node. We know that this node returns complex data, so we can type cast to UAGenericObject.
            UAGenericObject genericObject;
            try
            {
                genericObject = (UAGenericObject)client.ReadValue(endpointDescriptor, nodeDescriptor);
            }
            catch (UAException uaException)
            {
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
                return;
            }
            // The data type is in the GenericData.DataType property of the UAGenericObject.
            DataType dataType = genericObject.GenericData.DataType;

            // Process the data type. We will inspect some of its properties, and dump them.
            ProcessDataType(dataType, maximumDepth: 3);
        }
        

        // Process the data type. It can be recursive in itself, so if you do not know the data type you are dealing with, 
        // it is recommended to make safeguards against infinite looping or recursion - here, the maximumDepth.
        public static void ProcessDataType(DataType dataType, int maximumDepth)
        {
            if (maximumDepth == 0)
                return;

            Console.WriteLine();
            Console.WriteLine("dataType.Name: {0}", dataType.Name);

            switch (dataType.Kind)
            {
                case DataTypeKind.Enumeration:
                    Console.WriteLine("The data type is an enumeration.");
                    var enumerationDataType = (EnumerationDataType) dataType;
                    Console.WriteLine("It has {0} enumeration members.", enumerationDataType.EnumerationMembers.Count);
                    Console.WriteLine("The names of the enumeration members are: {0}.",
                        String.Join(", ", enumerationDataType.EnumerationMembers.Select(member => member.Name)));
                    // Here you can process the members, or inspect SizeInBits etc.
                    break;

                case DataTypeKind.Opaque:
                    Console.WriteLine("The data type is opaque.");
                    var opaqueDataType = (OpaqueDataType) dataType;
                    Console.WriteLine("Its size is {0} bits.", opaqueDataType.SizeInBits);
                    // There isn't much more you can learn about an opaque data type (well, it may have Description and 
                    // other common members). It is, after all, opaque...
                    break;

                case DataTypeKind.Primitive:
                    Console.WriteLine("The data type is primitive.");
                    var primitiveDataType = (PrimitiveDataType) dataType;
                    Console.WriteLine("Its .NET value type is \"{0}\".", primitiveDataType.ValueType);
                    // There isn't much more you can learn about the primitive data type.
                    break;

                case DataTypeKind.Sequence:
                    Console.WriteLine("The data type is a sequence.");
                    var sequenceDataType = (SequenceDataType) dataType;
                    Console.WriteLine("Its length is {0} (-1 means that the length can vary).", sequenceDataType.Length);

                    Console.WriteLine("A dump of the element data type follows.");
                    ProcessDataType(sequenceDataType.ElementDataType, maximumDepth - 1);
                    break;

                case DataTypeKind.Structured:
                    Console.WriteLine("The data type is structured.");
                    var structuredDataType = (StructuredDataType) dataType;
                    Console.WriteLine("It has {0} data fields.", structuredDataType.DataFields.Count);
                    Console.WriteLine("The names of the data fields are: {0}.",
                        String.Join(", ", structuredDataType.DataFields.Select(field => field.Name)));

                    Console.WriteLine("A dump of each of the data fields follows.");
                    foreach (DataField dataField in structuredDataType.DataFields)
                    {
                        Console.WriteLine();
                        Console.WriteLine("dataField.Name: {0}", dataField.Name);
                        // Note that every data field also has properties like IsLength, IsOptional, IsSwitch which might 
                        // be of interest, but we are not dumping them here.
                        ProcessDataType(dataField.DataType, maximumDepth - 1);
                    }
                    break;

                case DataTypeKind.Union:
                    Console.WriteLine("The data type is union.");
                    var unionDataType = (UnionDataType)dataType;
                    Console.WriteLine("It has {0} data fields.", unionDataType.DataFields.Count);
                    Console.WriteLine("The names of the data fields are: {0}.",
                        String.Join(", ", unionDataType.DataFields.Select(field => field.Name)));
                    break;
            }
        }
    }
}
#endregion
