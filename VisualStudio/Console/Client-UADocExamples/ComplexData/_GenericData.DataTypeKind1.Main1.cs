// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
// ReSharper disable SwitchStatementMissingSomeCases
#region Example
// Shows how to process generic data type, displaying some of its properties, recursively.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Collections.Generic;
using OpcLabs.BaseLib.DataTypeModel;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.ComplexData;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples.ComplexData._GenericData
{
    class DataTypeKind1
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

            // Process the generic data type. We will inspect some of its properties, and dump them.
            ProcessGenericData(genericObject.GenericData, maximumDepth: 3);
        }
        

        // Process the generic data type. Its structure can sometimes be quite deep, therefore we are limiting the depth
        // of the recursion using maximumDepth.
        public static void ProcessGenericData(GenericData genericData, int maximumDepth)
        {
            if (maximumDepth == 0)
                return;

            Console.WriteLine();
            Console.WriteLine("genericData.DataType: {0}", genericData.DataType);

            switch (genericData.DataTypeKind)
            {
                case DataTypeKind.Enumeration:
                    Console.WriteLine("The generic data is an enumeration.");
                    var enumerationData = (EnumerationData) genericData;
                    Console.WriteLine("Its value is {0}.", enumerationData.Value);
                    // There is also a ValueName that you can inspect (if known).
                    break;

                case DataTypeKind.Opaque:
                    Console.WriteLine("The generic data is opaque.");
                    var opaqueData = (OpaqueData) genericData;
                    Console.WriteLine("Its size is {0} bits.", opaqueData.SizeInBits);
                    Console.WriteLine("The data bytes are {0}.", BitConverter.ToString(opaqueData.ByteArray));
                    // Use the Value property (a BitArray) if you need to access the value bit by bit.
                    break;

                case DataTypeKind.Primitive:
                    Console.WriteLine("The generic data is primitive.");
                    var primitiveData = (PrimitiveData) genericData;
                    Console.WriteLine("Its value is \"{0}\".", primitiveData.Value);
                    break;

                case DataTypeKind.Sequence:
                    Console.WriteLine("The generic data is a sequence.");
                    var sequenceData = (SequenceData) genericData;
                    Console.WriteLine("It has {0} elements.", sequenceData.Elements.Count);
                    Console.WriteLine("A dump of the elements follows.");
                    foreach (GenericData element in sequenceData.Elements)
                        ProcessGenericData(element, maximumDepth - 1);
                    break;

                case DataTypeKind.Structured:
                    Console.WriteLine("The generic data is structured.");
                    var structuredData = (StructuredData) genericData;
                    Console.WriteLine("It has {0} field data members.", structuredData.FieldData.Count);
                    Console.WriteLine("The names of the fields are: {0}.",
                        String.Join(", ", structuredData.FieldData.Keys));

                    Console.WriteLine("A dump of each of the fields follows.");
                    foreach (KeyValuePair<string, GenericData> pair in structuredData.FieldData)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Field name: {0}", pair.Key);
                        ProcessGenericData(pair.Value, maximumDepth - 1);
                    }
                    break;

                case DataTypeKind.Union:
                    Console.WriteLine("The generic data is a union.");
                    var unionData = (UnionData)genericData;
                    Console.WriteLine("The name of current field is: {0}", unionData.FieldName);
                    Console.WriteLine("Current field value is: {0}", unionData.FieldValue);
                    break;
            }
        }
    }
}
#endregion
