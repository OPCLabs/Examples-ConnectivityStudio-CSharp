// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// Shows how to write complex data with OPC UA Complex Data plug-in.
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
    class WriteValue
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

            // Read a node which returns complex data. 
            // We know that this node returns complex data, so we can type cast to UAGenericObject.
            Console.WriteLine("Reading...");
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


            // Modify the data read.
            // This node returns one of the two data types, randomly (this is not common, usually the type is fixed). The
            // data types are sub-types of one common type which the data type of the node. We therefore use the data type 
            // ID in the returned UAGenericObject to detect which data type has been returned.

            // For processing the internals of the data, refer to examples for GenericData and DataType classes.
            // We know how the data is structured, and have hard-coded a logic that modifies certain values inside. It is
            // also possible to discover the structure of the data type in the program, and write generic clients that can 
            // cope with any kind of complex data.
            //
            // Note that the code below is not fully robust - it will throw an exception if the data is not as expected.
            Console.WriteLine("Modifying...");
            Console.WriteLine(genericObject.DataTypeId);
            if (genericObject.DataTypeId.NodeDescriptor.Match("nsu=http://test.org/UA/Data/ ;i=9440"))  // ScalarValueDataType
            {
                // Negate the byte in the "ByteValue" field.
                var structuredData = (StructuredData)genericObject.GenericData;
                var byteValue = (PrimitiveData)structuredData.FieldData["ByteValue"];
                byteValue.Value = (Byte)~((Byte)byteValue.Value);
                Console.WriteLine(byteValue.Value);
            }
            else if (genericObject.DataTypeId.NodeDescriptor.Match("nsu=http://test.org/UA/Data/ ;i=9669")) // ArrayValueDataType
            {
                // Negate bytes at indexes 0 and 1 of the array in the "ByteValue" field.
                var structuredData = (StructuredData)genericObject.GenericData;
                var byteValue = (SequenceData)structuredData.FieldData["ByteValue"];
                var element0 = (PrimitiveData)byteValue.Elements[0];
                var element1 = (PrimitiveData)byteValue.Elements[1];
                element0.Value = (Byte)~((Byte)element0.Value);
                element1.Value = (Byte)~((Byte)element1.Value);
                Console.WriteLine(element0.Value);
                Console.WriteLine(element1.Value);
            }


            // Write the modified complex data back to the node.
            // The data type ID in the UAGenericObject is borrowed without change from what we have read, so that the server
            // knows which data type we are writing. The data type ID not necessary if writing precisely the same data type
            // as the node has (not a subtype).
            Console.WriteLine("Writing...");
            try
            {
                client.WriteValue(endpointDescriptor, nodeDescriptor, genericObject);
            }
            catch (UAException uaException)
            {
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
            }
        }
    }
}
#endregion
