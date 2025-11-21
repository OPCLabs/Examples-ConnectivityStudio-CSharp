// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

#pragma warning disable IDE0028 // Simplify collection initialization
// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable UseObjectOrCollectionInitializer
#region Example
// This example shows different ways of constructing generic data.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Collections;
using OpcLabs.BaseLib.DataTypeModel;

namespace UADocExamples.ComplexData._GenericData
{
    class _Construction
    {
        public static void Main1()
        {
            // Create enumeration data with value of 1.
            var enumerationData = new EnumerationData(1);
            Console.WriteLine(enumerationData);

            // Create opaque data from an array of 2 bytes, specifying its size as 15 bits.
            var opaqueData1 = new OpaqueData(new byte[] {0xAA, 0x55}, sizeInBits:15);
            Console.WriteLine(opaqueData1);

            // Create opaque data from a bit array.
            var opaqueData2 = new OpaqueData(new BitArray(new[] { false, true, false, true, false }));
            Console.WriteLine(opaqueData2);

            // Create primitive data with System.Double value of 180.0.
            var primitiveData1 = new PrimitiveData(180.0d);
            Console.WriteLine(primitiveData1);

            // Create primitive data with System.String value.
            var primitiveData2 = new PrimitiveData("Temperature is too high!");
            Console.WriteLine(primitiveData2);

            // Create sequence data with two elements, using collection initializer syntax.
            var sequenceData1 = new SequenceData
            {
                opaqueData1,
                opaqueData2
            };
            Console.WriteLine(sequenceData1);

            // Create the same sequence data, using the Add method.
            var sequenceData2 = new SequenceData();
            sequenceData2.Elements.Add(opaqueData1);
            sequenceData2.Elements.Add(opaqueData2);
            Console.WriteLine(sequenceData2);

            // Create the same sequence data, using an array (an enumerable) of its elements.
            var sequenceData3 = new SequenceData(
                new GenericDataCollection(new[] {opaqueData1, opaqueData2}));
            Console.WriteLine(sequenceData3);

            // Create structured data with two members, using collection initializer syntax.
            var structuredData1 = new StructuredData
            {
                {"Message", primitiveData2},
                {"Status", enumerationData}
            };
            Console.WriteLine(structuredData1);

            // Create the same structured data using the Add method.
            var structuredData2 = new StructuredData();
            structuredData2.Add("Message", primitiveData2);
            structuredData2.Add("Status", enumerationData);
            Console.WriteLine(structuredData2);

            // Create union data.
            var unionData1 = new UnionData("DoubleField", primitiveData1);
            Console.WriteLine(unionData1);
        }
    }
}
#endregion
