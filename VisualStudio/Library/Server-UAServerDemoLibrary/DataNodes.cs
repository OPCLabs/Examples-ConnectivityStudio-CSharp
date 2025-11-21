// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable PossibleNullReferenceException
// ReSharper disable StringLiteralTypo
#region Example
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client, server and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA.AddressSpace.Standard;
using OpcLabs.EasyOpc.UA.NodeSpace;

namespace UAServerDemoLibrary
{
    static public class DataNodes
    {
        /// <summary>
        /// Adds static and dynamic nodes that demonstrate various data types and access levels.
        /// </summary>
        /// <param name="parentFolder">The folder to which to add the nodes.</param>
        static public void AddToParent(UAFolder parentFolder)
        {
            // Create Data folder.
            UAFolder dataFolder = UAFolder.CreateIn(parentFolder, "Data");

            // Create read-only data variables of various data types, without adding them to the server first. We store
            // references to them individually, because we later implement write-only variables that write to these
            // read-only variables.
            var booleanReadOnlyDataVariable =
                new UADataVariable("BooleanValue").Writable(false).ValueType<bool>();
            var byteStringReadOnlyDataVariable =
                new UADataVariable("ByteStringValue").Writable(false).ValueType<byte[]>();
            var byteReadOnlyDataVariable = new UADataVariable("ByteValue").Writable(false).ValueType<byte>();
            var dateTimeReadOnlyDataVariable =
                new UADataVariable("DateTimeValue").Writable(false).ValueType<DateTime>();
            var doubleReadOnlyDataVariable =
                new UADataVariable("DoubleValue").Writable(false).ValueType<double>();
            var floatReadOnlyDataVariable =
                new UADataVariable("FloatValue").Writable(false).ValueType<float>();
            var guidReadOnlyDataVariable = new UADataVariable("GuidValue").Writable(false).ValueType<Guid>();
            var int16ReadOnlyDataVariable =
                new UADataVariable("Int16Value").Writable(false).ValueType<short>();
            var int32ReadOnlyDataVariable =
                new UADataVariable("Int32Value").Writable(false).ValueType<int>();
            var int64ReadOnlyDataVariable =
                new UADataVariable("Int64Value").Writable(false).ValueType<long>();
            var sByteReadOnlyDataVariable =
                new UADataVariable("SByteValue").Writable(false).ValueType<sbyte>();
            var stringReadOnlyDataVariable =
                new UADataVariable("StringValue").Writable(false).ValueType<string>();
            var uInt16ReadOnlyDataVariable =
                new UADataVariable("UInt16Value").Writable(false).ValueType<ushort>();
            var uInt32ReadOnlyDataVariable =
                new UADataVariable("UInt32Value").Writable(false).ValueType<uint>();
            var uInt64ReadOnlyDataVariable =
                new UADataVariable("UInt64Value").Writable(false).ValueType<ulong>();
            var variantReadOnlyDataVariable =
                new UADataVariable("VariantValue").Writable(false);

            // Create Constant sub-folder under the Data folder. It contains read-only data variables with constant values.
            dataFolder.Add(
                new UAFolder("Constant")
                {
                    new UAFolder("Scalar")
                    {
                        new UADataVariable("BooleanValue").ConstantValue(true),
                        new UADataVariable("ByteStringValue").ConstantValue(new byte[] { 0x57, 0x21, 0x40, 0xfc }),
                        new UADataVariable("ByteValue").ConstantValue((byte)144),
                        new UADataVariable("DateTimeValue").ConstantValue(
                            // We are passing in UTC times, because we want always the same result, and so we must specify
                            // the DateTimeKind. You can pass in local times, but then they will be converted to UTC by the
                            // server, and the result will depend on the time zone.
                            DateTime.SpecifyKind(new DateTime(2024, 7, 12, 14, 4, 55).AddSeconds(0.444),
                                DateTimeKind.Utc)),
                        new UADataVariable("DoubleValue").ConstantValue(7.75630105797e-011),
                        new UADataVariable("FloatValue").ConstantValue(2.77002e+29f),
                        new UADataVariable("GuidValue").ConstantValue(
                            new Guid("{1AEF59AE-5029-42A7-9AE2-B2DC00072999}")),
                        new UADataVariable("Int16Value").ConstantValue((short)-30956),
                        new UADataVariable("Int32Value").ConstantValue(276673160),
                        new UADataVariable("Int64Value").ConstantValue(1412096336825367659),
                        new UADataVariable("SByteValue").ConstantValue((sbyte)-113),
                        new UADataVariable("StringValue").ConstantValue("lorem ipsum"),
                        new UADataVariable("UInt16Value").ConstantValue((ushort)64421),
                        new UADataVariable("UInt32Value").ConstantValue(3853116537U),
                        new UADataVariable("UInt64Value").ConstantValue(9431348106520835314UL),
                        new UADataVariable("VariantValue").ConstantValue(529739609)
                    }
                });

            // Create Dynamic sub-folder under the Data folder. It contains data variables with dynamically changing values.
            dataFolder.Add(
                new UAFolder("Dynamic")
                {
                    new UAFolder("Array")
                    {
                        new UADataVariable("BooleanValue").ReadValueFunction(() => NextRandomArray(NextRandomBoolean)),
                        new UADataVariable("ByteStringValue").ReadValueFunction(() =>
                            NextRandomArray(NextRandomByteString)),
                        // This is a tricky case. We want array of Byte-s, but that is automatically recognized as scalar
                        // OPC UA ByteString. For a true array of Byte-s, the data type Id and array dimension list must be
                        // specified explicitly.
                        new UADataVariable("ByteValue").ReadValueFunction(
                            dataTypeId: UADataTypeIds.Byte,
                            arrayRank: 1,
                            () => NextRandomArray(NextRandomByte)),
                        new UADataVariable("DateTimeValue").ReadValueFunction(() =>
                            NextRandomArray(NextRandomDateTime)),
                        new UADataVariable("DoubleValue").ReadValueFunction(() => NextRandomArray(NextRandomDouble)),
                        new UADataVariable("FloatValue").ReadValueFunction(() => NextRandomArray(NextRandomFloat)),
                        new UADataVariable("GuidValue").ReadValueFunction(() => NextRandomArray(NextRandomGuid)),
                        new UADataVariable("Int16Value").ReadValueFunction(() => NextRandomArray(NextRandomInt16)),
                        new UADataVariable("Int32Value").ReadValueFunction(() => NextRandomArray(NextRandomInt32)),
                        new UADataVariable("Int64Value").ReadValueFunction(() => NextRandomArray(NextRandomInt64)),
                        new UADataVariable("SByteValue").ReadValueFunction(() => NextRandomArray(NextRandomSByte)),
                        new UADataVariable("StringValue").ReadValueFunction(() => NextRandomArray(NextRandomString)),
                        new UADataVariable("UInt16Value").ReadValueFunction(() => NextRandomArray(NextRandomUInt16)),
                        new UADataVariable("UInt32Value").ReadValueFunction(() => NextRandomArray(NextRandomUInt32)),
                        new UADataVariable("UInt64Value").ReadValueFunction(() => NextRandomArray(NextRandomUInt64)),
                        new UADataVariable("VariantValue").ReadValueFunction(() => NextRandomArray(NextRandomVariant))
                    },

                    new UAFolder("Scalar")
                    {
                        new UADataVariable("BooleanValue").ReadValueFunction(NextRandomBoolean),
                        new UADataVariable("ByteStringValue").ReadValueFunction(NextRandomByteString),
                        new UADataVariable("ByteValue").ReadValueFunction(NextRandomByte),
                        new UADataVariable("DateTimeValue").ReadValueFunction(NextRandomDateTime),
                        new UADataVariable("DoubleValue").ReadValueFunction(NextRandomDouble),
                        new UADataVariable("FloatValue").ReadValueFunction(NextRandomFloat),
                        new UADataVariable("GuidValue").ReadValueFunction(NextRandomGuid),
                        new UADataVariable("Int16Value").ReadValueFunction(NextRandomInt16),
                        new UADataVariable("Int32Value").ReadValueFunction(NextRandomInt32),
                        new UADataVariable("Int64Value").ReadValueFunction(NextRandomInt64),
                        new UADataVariable("SByteValue").ReadValueFunction(NextRandomSByte),
                        new UADataVariable("StringValue").ReadValueFunction(NextRandomString),
                        new UADataVariable("UInt16Value").ReadValueFunction(NextRandomUInt16),
                        new UADataVariable("UInt32Value").ReadValueFunction(NextRandomUInt32),
                        new UADataVariable("UInt64Value").ReadValueFunction(NextRandomUInt64),
                        new UADataVariable("VariantValue").ReadValueFunction(NextRandomVariant)
                    }
                });

            // The FullyWritable sub-folder contains data variables that have not only writable value, but also writable
            // source timestamp and status code.
            dataFolder.Add(
                new UAFolder("FullyWritable")
                {
                    new UAFolder("Scalar")
                    {
                        new UADataVariable("BooleanValue").ReadWriteValue(true)
                            .Writable(true, true, true),
                        new UADataVariable("ByteStringValue").ReadWriteValue(new byte[] { 0x57, 0x21, 0x40, 0xfc })
                            .Writable(true, true, true),
                        new UADataVariable("ByteValue").ReadWriteValue((byte)144)
                            .Writable(true, true, true),
                        new UADataVariable("DateTimeValue").ReadWriteValue(
                                // We are passing in UTC times, because we want always the same result, and so we must specify
                                // the DateTimeKind. You can pass in local times, but then they will be converted to UTC by the
                                // server, and the result will depend on the time zone.
                                DateTime.SpecifyKind(new DateTime(2024, 7, 12, 14, 4, 55).AddSeconds(0.444),
                                    DateTimeKind.Utc))
                            .Writable(true, true, true),
                        new UADataVariable("DoubleValue").ReadWriteValue(7.75630105797e-011)
                            .Writable(true, true, true),
                        new UADataVariable("FloatValue").ReadWriteValue(2.77002e+29f)
                            .Writable(true, true, true),
                        new UADataVariable("GuidValue")
                            .ReadWriteValue(new Guid("{1AEF59AE-5029-42A7-9AE2-B2DC00072999}"))
                            .Writable(true, true, true),
                        new UADataVariable("Int16Value").ReadWriteValue((short)-30956)
                            .Writable(true, true, true),
                        new UADataVariable("Int32Value").ReadWriteValue(276673160)
                            .Writable(true, true, true),
                        new UADataVariable("Int64Value").ReadWriteValue(1412096336825367659)
                            .Writable(true, true, true),
                        new UADataVariable("SByteValue").ReadWriteValue((sbyte)-113)
                            .Writable(true, true, true),
                        new UADataVariable("StringValue").ReadWriteValue("lorem ipsum")
                            .Writable(true, true, true),
                        new UADataVariable("UInt16Value").ReadWriteValue((ushort)64421)
                            .Writable(true, true, true),
                        new UADataVariable("UInt32Value").ReadWriteValue(3853116537U)
                            .Writable(true, true, true),
                        new UADataVariable("UInt64Value").ReadWriteValue(9431348106520835314UL)
                            .Writable(true, true, true),
                        new UADataVariable("VariantValue").ReadWriteValue(529739609)
                            .Writable(true, true, true)
                    }
                });

            // The ReadOnly sub-folder contains data variables that are read-only, and their values can be changed through
            // corresponding data variables in the WriteOnly sub-folder.
            dataFolder.Add(
                new UAFolder("ReadOnly")
                {
                    new UAFolder("Scalar")
                    {
                        booleanReadOnlyDataVariable,
                        byteStringReadOnlyDataVariable,
                        byteReadOnlyDataVariable,
                        dateTimeReadOnlyDataVariable,
                        doubleReadOnlyDataVariable,
                        floatReadOnlyDataVariable,
                        guidReadOnlyDataVariable,
                        int16ReadOnlyDataVariable,
                        int32ReadOnlyDataVariable,
                        int64ReadOnlyDataVariable,
                        sByteReadOnlyDataVariable,
                        stringReadOnlyDataVariable,
                        uInt16ReadOnlyDataVariable,
                        uInt32ReadOnlyDataVariable,
                        uInt64ReadOnlyDataVariable,
                        variantReadOnlyDataVariable
                    }
                });

            // The Static sub-folder contains data variables with static values which can be changed through writing to
            // them (so-called "registers").
            dataFolder.Add(
                new UAFolder("Static")
                {
                    // For demonstration, we consistently create one-dimensional arrays with initially 3 elements, where the
                    // first element has the same value as the scalar variable with the same name.
                    new UAFolder("Array")
                    {
                        new UADataVariable("BooleanValue").ReadWriteValue(new[]
                        {
                            true,
                            false,
                            true
                        }),
                        new UADataVariable("ByteStringValue").ReadWriteValue(new[]
                        {
                            new byte[] { 0x57, 0x21, 0x40, 0xfc },
                            new byte[] { 248, 131, 217, 210 },
                            new byte[] { 252, 152, 119, 65 }
                        }),
                        // This is a tricky case. We want array of Byte-s, but that is automatically recognized as scalar
                        // OPC UA ByteString. For a true array of Byte-s, the data type Id and array dimension list must be
                        // specified explicitly.
                        new UADataVariable("ByteValue").ReadWriteValue(
                            dataTypeId: UADataTypeIds.Byte,
                            arrayRank: 1,
                            value: new byte[]
                            {
                                144,
                                19,
                                233
                            }),
                        new UADataVariable("DateTimeValue").ReadWriteValue(new[]
                        {
                            // We are passing in UTC times, because we want always the same result, and so we must specify
                            // the DateTimeKind. You can pass in local times, but then they will be converted to UTC by the
                            // server, and the result will depend on the time zone.
                            DateTime.SpecifyKind(new DateTime(2024, 7, 12, 14, 4, 55).AddSeconds(0.444),
                                DateTimeKind.Utc),
                            DateTime.SpecifyKind(new DateTime(2024, 4, 8), DateTimeKind.Utc),
                            DateTime.SpecifyKind(new DateTime(2023, 8, 14, 18, 13, 0), DateTimeKind.Utc)
                        }),
                        new UADataVariable("DoubleValue").ReadWriteValue(new[]
                        {
                            7.75630105797e-011,
                            -0.467227097818268,
                            -3.51653052582609E+300
                        }),
                        new UADataVariable("FloatValue").ReadWriteValue(new[]
                        {
                            2.77002e+29f,
                            -1.103936E+36f,
                            -9.002293E-28f
                        }),
                        new UADataVariable("GuidValue").ReadWriteValue(new[]
                        {
                            new Guid("{1AEF59AE-5029-42A7-9AE2-B2DC00072999}"),
                            new Guid("{E8690EA3-25D0-4F19-9DFC-AA25D2772B2F}"),
                            new Guid("{9E081C84-7953-4A88-B709-447FC187EDD9}"),
                        }),
                        new UADataVariable("Int16Value").ReadWriteValue(new short[]
                        {
                            -30956,
                            31277,
                            21977
                        }),
                        new UADataVariable("Int32Value").ReadWriteValue(new[]
                        {
                            276673160,
                            630080334,
                            -391755284
                        }),
                        new UADataVariable("Int64Value").ReadWriteValue(new[]
                        {
                            1412096336825367659,
                            -808781653700434592,
                            4707848393174903135
                        }),
                        new UADataVariable("SByteValue").ReadWriteValue(new sbyte[]
                        {
                            -113,
                            -92,
                            2
                        }),
                        new UADataVariable("StringValue").ReadWriteValue(new[]
                        {
                            "lorem ipsum",
                            "dolor sit amet",
                            "consectetur adipiscing elit"
                        }),
                        new UADataVariable("UInt16Value").ReadWriteValue(new ushort[]
                        {
                            64421,
                            22663,
                            36755
                        }),
                        new UADataVariable("UInt32Value").ReadWriteValue(new uint[]
                        {
                            3853116537,
                            968679231,
                            995611904
                        }),
                        new UADataVariable("UInt64Value").ReadWriteValue(new ulong[]
                        {
                            9431348106520835314,
                            15635738044048254300,
                            946287779964705249
                        }),
                        new UADataVariable("VariantValue").ReadWriteValue(new object[]
                        {
                            529739609,
                            "lorem ipsum",
                            new Guid("{1AEF59AE-5029-42A7-9AE2-B2DC00072999}")
                        })
                    },

                    // We create 2-dimensional arrays with 4x3 size, and default element values.
                    new UAFolder("Array2D")
                    {
                        new UADataVariable("BooleanValue").ReadWriteValue(new bool[4, 3]),
                        new UADataVariable("ByteStringValue").ReadWriteValue(new byte[4, 3][]),
                        new UADataVariable("ByteValue").ReadWriteValue(new byte[4, 3]),
                        new UADataVariable("DateTimeValue").ReadWriteValue(new DateTime[4, 3]),
                        new UADataVariable("DoubleValue").ReadWriteValue(new double[4, 3]),
                        new UADataVariable("FloatValue").ReadWriteValue(new float[4, 3]),
                        new UADataVariable("GuidValue").ReadWriteValue(new Guid[4, 3]),
                        new UADataVariable("Int16Value").ReadWriteValue(new short[4, 3]),
                        new UADataVariable("Int32Value").ReadWriteValue(new int[4, 3]),
                        new UADataVariable("Int64Value").ReadWriteValue(new long[4, 3]),
                        new UADataVariable("SByteValue").ReadWriteValue(new sbyte[4, 3]),
                        new UADataVariable("StringValue").ReadWriteValue(new string[4, 3]),
                        new UADataVariable("UInt16Value").ReadWriteValue(new ushort[4, 3]),
                        new UADataVariable("UInt32Value").ReadWriteValue(new uint[4, 3]),
                        new UADataVariable("UInt64Value").ReadWriteValue(new ulong[4, 3]),
                        new UADataVariable("VariantValue").ReadWriteValue(new object[4, 3])
                    },

                    // Array nodes with specified and enforced maximum array dimensions.
                    new UAFolder("BoundedArray")
                    {
                        new UADataVariable("BooleanValue").ReadWriteValue(new bool[4], arrayDimensions: 5),
                        new UADataVariable("ByteStringValue").ReadWriteValue(new byte[4][], arrayDimensions: 5),
                        // This is a tricky case. We want array of Byte-s, but that is automatically recognized as scalar
                        // OPC UA ByteString. For a true array of Byte-s, the data type Id and array dimension list must be
                        // specified explicitly.
                        new UADataVariable("ByteValue").ReadWriteValue(
                            dataTypeId: UADataTypeIds.Byte,
                            arrayDimensionList: new[] { 5 },
                            new byte[4]),
                        new UADataVariable("DateTimeValue").ReadWriteValue(new DateTime[4], arrayDimensions: 5),
                        new UADataVariable("DoubleValue").ReadWriteValue(new double[4], arrayDimensions: 5),
                        new UADataVariable("FloatValue").ReadWriteValue(new float[4], arrayDimensions: 5),
                        new UADataVariable("GuidValue").ReadWriteValue(new Guid[4], arrayDimensions: 5),
                        new UADataVariable("Int16Value").ReadWriteValue(new short[4], arrayDimensions: 5),
                        new UADataVariable("Int32Value").ReadWriteValue(new int[4], arrayDimensions: 5),
                        new UADataVariable("Int64Value").ReadWriteValue(new long[4], arrayDimensions: 5),
                        new UADataVariable("SByteValue").ReadWriteValue(new sbyte[4], arrayDimensions: 5),
                        new UADataVariable("StringValue").ReadWriteValue(new string[4], arrayDimensions: 5),
                        new UADataVariable("UInt16Value").ReadWriteValue(new ushort[4], arrayDimensions: 5),
                        new UADataVariable("UInt32Value").ReadWriteValue(new uint[4], arrayDimensions: 5),
                        new UADataVariable("UInt64Value").ReadWriteValue(new ulong[4], arrayDimensions: 5),
                        new UADataVariable("VariantValue").ReadWriteValue(new object[4], arrayDimensions: 5)
                    },

                    new UAFolder("Scalar")
                    {
                        new UADataVariable("BooleanValue").ReadWriteValue(true),
                        new UADataVariable("ByteStringValue").ReadWriteValue(new byte[] { 0x57, 0x21, 0x40, 0xfc }),
                        new UADataVariable("ByteValue").ReadWriteValue((byte)144),
                        new UADataVariable("DateTimeValue").ReadWriteValue(
                            // We are passing in UTC times, because we want always the same result, and so we must specify
                            // the DateTimeKind. You can pass in local times, but then they will be converted to UTC by the
                            // server, and the result will depend on the time zone.
                            DateTime.SpecifyKind(new DateTime(2024, 7, 12, 14, 4, 55).AddSeconds(0.444),
                                DateTimeKind.Utc)),
                        new UADataVariable("DoubleValue").ReadWriteValue(7.75630105797e-011),
                        new UADataVariable("FloatValue").ReadWriteValue(2.77002e+29f),
                        new UADataVariable("GuidValue").ReadWriteValue(
                            new Guid("{1AEF59AE-5029-42A7-9AE2-B2DC00072999}")),
                        new UADataVariable("Int16Value").ReadWriteValue((short)-30956),
                        new UADataVariable("Int32Value").ReadWriteValue(276673160),
                        new UADataVariable("Int64Value").ReadWriteValue(1412096336825367659),
                        new UADataVariable("SByteValue").ReadWriteValue((sbyte)-113),
                        new UADataVariable("StringValue").ReadWriteValue("lorem ipsum"),
                        new UADataVariable("UInt16Value").ReadWriteValue((ushort)64421),
                        new UADataVariable("UInt32Value").ReadWriteValue(3853116537U),
                        new UADataVariable("UInt64Value").ReadWriteValue(9431348106520835314UL),
                        new UADataVariable("VariantValue").ReadWriteValue(529739609)
                    }
                });

            // Create and add write-only data variables of various data types. Implement write actions that write the value
            // to the corresponding read-only data variable of the same data type.
            dataFolder.Add(
                new UAFolder("WriteOnly")
                {
                    new UAFolder("Scalar")
                    {
                        new UADataVariable("BooleanValue").Readable(false).WriteValueAction((bool value) =>
                            booleanReadOnlyDataVariable.UpdateReadAttributeData(value)),
                        new UADataVariable("ByteStringValue").Readable(false).WriteValueAction((byte[] value) =>
                            byteStringReadOnlyDataVariable.UpdateReadAttributeData(value)),
                        new UADataVariable("ByteValue").Readable(false).WriteValueAction((byte value) =>
                            byteReadOnlyDataVariable.UpdateReadAttributeData(value)),
                        new UADataVariable("DateTimeValue").Readable(false).WriteValueAction((DateTime value) =>
                            dateTimeReadOnlyDataVariable.UpdateReadAttributeData(value)),
                        new UADataVariable("DoubleValue").Readable(false).WriteValueAction((double value) =>
                            doubleReadOnlyDataVariable.UpdateReadAttributeData(value)),
                        new UADataVariable("FloatValue").Readable(false).WriteValueAction((float value) =>
                            floatReadOnlyDataVariable.UpdateReadAttributeData(value)),
                        new UADataVariable("GuidValue").Readable(false).WriteValueAction((Guid value) =>
                            guidReadOnlyDataVariable.UpdateReadAttributeData(value)),
                        new UADataVariable("Int16Value").Readable(false).WriteValueAction((short value) =>
                            int16ReadOnlyDataVariable.UpdateReadAttributeData(value)),
                        new UADataVariable("Int32Value").Readable(false).WriteValueAction((int value) =>
                            int32ReadOnlyDataVariable.UpdateReadAttributeData(value)),
                        new UADataVariable("Int64Value").Readable(false).WriteValueAction((long value) =>
                            int64ReadOnlyDataVariable.UpdateReadAttributeData(value)),
                        new UADataVariable("SByteValue").Readable(false).WriteValueAction((sbyte value) =>
                            sByteReadOnlyDataVariable.UpdateReadAttributeData(value)),
                        new UADataVariable("StringValue").Readable(false).WriteValueAction((string value) =>
                            stringReadOnlyDataVariable.UpdateReadAttributeData(value)),
                        new UADataVariable("UInt16Value").Readable(false).WriteValueAction((ushort value) =>
                            uInt16ReadOnlyDataVariable.UpdateReadAttributeData(value)),
                        new UADataVariable("UInt32Value").Readable(false).WriteValueAction((uint value) =>
                            uInt32ReadOnlyDataVariable.UpdateReadAttributeData(value)),
                        new UADataVariable("UInt64Value").Readable(false).WriteValueAction((ulong value) =>
                            uInt64ReadOnlyDataVariable.UpdateReadAttributeData(value)),
                        new UADataVariable("VariantValue").Readable(false).WriteValueAction((object value) =>
                            variantReadOnlyDataVariable.UpdateReadAttributeData(value))
                    }
                });
        }


        // Random value generators.

        static private readonly Random Random = new Random();

        static private readonly string[] RandomStrings = new[] { "lorem", "ipsum", "dolor", "sit", "amet" };

        static private T[] NextRandomArray<T>(Func<T> nextRandomElement) =>
            new[] { nextRandomElement(), nextRandomElement(), nextRandomElement() };

        static private bool NextRandomBoolean() => Random.Next(2) != 0;

        static private byte NextRandomByte() => (byte)Random.Next(byte.MinValue, byte.MaxValue + 1);

        static private byte[] NextRandomByteString() =>
            new[] { NextRandomByte(), NextRandomByte(), NextRandomByte(), NextRandomByte() };

        static private DateTime NextRandomDateTime() =>
            DateTime.MinValue.AddMilliseconds((DateTime.MaxValue - DateTime.MinValue).TotalMilliseconds *
                                              Random.NextDouble());

        static private float NextRandomFloat() =>
            (float)Math.Pow(10, Math.Log10(float.MaxValue) * Random.NextDouble()) * (2 * Random.Next(2) - 1);

        static private double NextRandomDouble() =>
            Math.Pow(10, Math.Log10(double.MaxValue) * Random.NextDouble()) * (2 * Random.Next(2) - 1);

        static private Guid NextRandomGuid() => Guid.NewGuid();

        static private short NextRandomInt16() => (short)Random.Next(short.MinValue, short.MaxValue + 1);

        static private int NextRandomInt32()
        {
            byte[] buffer = new byte[4];
            Random.NextBytes(buffer);
            return BitConverter.ToInt32(buffer, 0);
        }

        static private long NextRandomInt64()
        {
            byte[] buffer = new byte[8];
            Random.NextBytes(buffer);
            return BitConverter.ToInt64(buffer, 0);
        }

        static private sbyte NextRandomSByte() => (sbyte)Random.Next(sbyte.MinValue, sbyte.MaxValue + 1);

        static private string NextRandomString() => RandomStrings[Random.Next(RandomStrings.Length)];

        static private ushort NextRandomUInt16() => (ushort)Random.Next(ushort.MinValue, ushort.MaxValue + 1);

        static private uint NextRandomUInt32()
        {
            byte[] buffer = new byte[4];
            Random.NextBytes(buffer);
            return BitConverter.ToUInt32(buffer, 0);
        }

        static private ulong NextRandomUInt64()
        {
            byte[] buffer = new byte[8];
            Random.NextBytes(buffer);
            return BitConverter.ToUInt64(buffer, 0);
        }

        static private object NextRandomVariant()
        {
            switch (Random.Next(15))
            {
                case 0:
                    return NextRandomBoolean();
                case 1:
                    return NextRandomByteString();
                case 2:
                    return NextRandomByte();
                case 3:
                    return NextRandomDateTime();
                case 4:
                    return NextRandomDouble();
                case 5:
                    return NextRandomFloat();
                case 6:
                    return NextRandomGuid();
                case 7:
                    return NextRandomInt16();
                case 8:
                    return NextRandomInt32();
                case 9:
                    return NextRandomInt64();
                case 10:
                    return NextRandomSByte();
                case 11:
                    return NextRandomString();
                case 12:
                    return NextRandomUInt16();
                case 13:
                    return NextRandomUInt32();
                case 14:
                    return NextRandomUInt64();
                default:
                    return null;
            }
        }
    }
}
#endregion
