// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

using System;
using OpcLabs.EasySparkplug;

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

namespace SparkplugEdgeNodeDemoLibrary
{
    static public class DataMetrics
    {
        static public SparkplugMetric[] Create()
        {
            // Create read-only metrics of various data types, without adding them to the result first. We store
            // references to them individually, because we later implement write-only metrics that write to these
            // read-only metrics.
            var booleanReadOnlyMetric = new SparkplugMetric("ReadOnly/BooleanValue").Writable(false).ValueType<bool>();
            var bytesReadOnlyMetric = new SparkplugMetric("ReadOnly/BytesValue").Writable(false).ValueType<byte[]>();
            var dateTimeReadOnlyMetric = new SparkplugMetric("ReadOnly/DateTimeValue").Writable(false).ValueType<DateTime>();
            var doubleReadOnlyMetric = new SparkplugMetric("ReadOnly/DoubleValue").Writable(false).ValueType<double>();
            var floatReadOnlyMetric = new SparkplugMetric("ReadOnly/FloatValue").Writable(false).ValueType<float>();
            var int16ReadOnlyMetric = new SparkplugMetric("ReadOnly/Int16Value").Writable(false).ValueType<short>();
            var int32ReadOnlyMetric = new SparkplugMetric("ReadOnly/Int32Value").Writable(false).ValueType<int>();
            var int64ReadOnlyMetric = new SparkplugMetric("ReadOnly/Int64Value").Writable(false).ValueType<long>();
            var int8ReadOnlyMetric = new SparkplugMetric("ReadOnly/Int8Value").Writable(false).ValueType<sbyte>();
            var stringReadOnlyMetric = new SparkplugMetric("ReadOnly/StringValue").Writable(false).ValueType<string>();
            // Sparkplug Text and String data types are both represented as System.String in .NET. The data type must be
            // set explicitly for Text metrics, otherwise the default String data type is used.
            var textReadOnlyMetric = new SparkplugMetric("ReadOnly/TextValue").Writable(false).ValueType(SparkplugDataType.Text);
            var uInt16ReadOnlyMetric = new SparkplugMetric("ReadOnly/UInt16Value").Writable(false).ValueType<ushort>();
            var uInt32ReadOnlyMetric = new SparkplugMetric("ReadOnly/UInt32Value").Writable(false).ValueType<uint>();
            var uInt64ReadOnlyMetric = new SparkplugMetric("ReadOnly/UInt64Value").Writable(false).ValueType<ulong>();
            var uInt8ReadOnlyMetric = new SparkplugMetric("ReadOnly/UInt8Value").Writable(false).ValueType<byte>();
            var uuidReadOnlyMetric = new SparkplugMetric("ReadOnly/UuidValue").Writable(false).ValueType<Guid>();

            return new[]
            {
                // Create Constant sub-folder. It contains read-only metrics with constant values.
                new SparkplugMetric("Constant/BooleanValue").ConstantValue(true),
                new SparkplugMetric("Constant/BytesValue").ConstantValue(new byte[] { 0x57, 0x21, 0x40, 0xfc }),
                new SparkplugMetric("Constant/DateTimeValue").ConstantValue(
                    // We are passing in UTC times, because we want always the same result, and so we must specify
                    // the DateTimeKind. You can pass in local times, but then they will be converted to UTC by the
                    // producer, and the result will depend on the time zone.
                    DateTime.SpecifyKind(new DateTime(2024, 7, 12, 14, 4, 55).AddSeconds(0.444),
                        DateTimeKind.Utc)),
                new SparkplugMetric("Constant/DoubleValue").ConstantValue(7.75630105797e-011),
                new SparkplugMetric("Constant/FloatValue").ConstantValue(2.77002e+29f),
                new SparkplugMetric("Constant/Int16Value").ConstantValue((short)-30956),
                new SparkplugMetric("Constant/Int32Value").ConstantValue(276673160),
                new SparkplugMetric("Constant/Int64Value").ConstantValue(1412096336825367659),
                new SparkplugMetric("Constant/Int8Value").ConstantValue((sbyte)-113),
                new SparkplugMetric("Constant/StringValue").ConstantValue("lorem ipsum"),
                // Sparkplug Text and String data types are both represented as System.String in .NET. The data type must be
                // set explicitly for Text metrics, otherwise the default String data type is used.
                new SparkplugMetric("Constant/TextValue").ConstantValue(SparkplugDataType.Text, "lorem ipsum dolor sit"),
                new SparkplugMetric("Constant/UInt16Value").ConstantValue((ushort)64421),
                new SparkplugMetric("Constant/UInt32Value").ConstantValue(3853116537U),
                new SparkplugMetric("Constant/UInt64Value").ConstantValue(9431348106520835314UL),
                new SparkplugMetric("Constant/UInt8Value").ConstantValue((byte)144),
                new SparkplugMetric("Constant/UuidValue").ConstantValue(
                    new Guid("{1AEF59AE-5029-42A7-9AE2-B2DC00072999}")),

                // Create Dynamic sub-folder. It contains metrics with dynamically changing values.
                new SparkplugMetric("Dynamic/BooleanValue").ReadValueFunction(NextRandomBoolean),
                new SparkplugMetric("Dynamic/BytesValue").ReadValueFunction(NextRandomBytes),
                new SparkplugMetric("Dynamic/DateTimeValue").ReadValueFunction(NextRandomDateTime),
                new SparkplugMetric("Dynamic/DoubleValue").ReadValueFunction(NextRandomDouble),
                new SparkplugMetric("Dynamic/FloatValue").ReadValueFunction(NextRandomFloat),
                new SparkplugMetric("Dynamic/Int16Value").ReadValueFunction(NextRandomInt16),
                new SparkplugMetric("Dynamic/Int32Value").ReadValueFunction(NextRandomInt32),
                new SparkplugMetric("Dynamic/Int64Value").ReadValueFunction(NextRandomInt64),
                new SparkplugMetric("Dynamic/Int8Value").ReadValueFunction(NextRandomInt8),
                new SparkplugMetric("Dynamic/StringValue").ReadValueFunction(NextRandomString),
                // Sparkplug Text and String data types are both represented as System.String in .NET. The data type must be
                // set explicitly for Text metrics, otherwise the default String data type is used.
                new SparkplugMetric("Dynamic/TextValue").ReadValueFunction(SparkplugDataType.Text, NextRandomText),
                new SparkplugMetric("Dynamic/UInt16Value").ReadValueFunction(NextRandomUInt16),
                new SparkplugMetric("Dynamic/UInt32Value").ReadValueFunction(NextRandomUInt32),
                new SparkplugMetric("Dynamic/UInt64Value").ReadValueFunction(NextRandomUInt64),
                new SparkplugMetric("Dynamic/UInt8Value").ReadValueFunction(NextRandomUInt8),
                new SparkplugMetric("Dynamic/UuidValue").ReadValueFunction(NextRandomUuid),

                new SparkplugMetric("Dynamic/BooleanArrayValue").ReadValueFunction(() =>
                    NextRandomArray(NextRandomBoolean)),
                new SparkplugMetric("Dynamic/DateTimeArrayValue").ReadValueFunction(() =>
                    NextRandomArray(NextRandomDateTime)),
                new SparkplugMetric("Dynamic/DoubleArrayValue").ReadValueFunction(() =>
                    NextRandomArray(NextRandomDouble)),
                new SparkplugMetric("Dynamic/FloatArrayValue").ReadValueFunction(() =>
                    NextRandomArray(NextRandomFloat)),
                new SparkplugMetric("Dynamic/Int16ArrayValue").ReadValueFunction(() =>
                    NextRandomArray(NextRandomInt16)),
                new SparkplugMetric("Dynamic/Int32ArrayValue").ReadValueFunction(() =>
                    NextRandomArray(NextRandomInt32)),
                new SparkplugMetric("Dynamic/Int64ArrayValue").ReadValueFunction(() =>
                    NextRandomArray(NextRandomInt64)),
                new SparkplugMetric("Dynamic/Int8ArrayValue").ReadValueFunction(() => 
                    NextRandomArray(NextRandomInt8)),
                new SparkplugMetric("Dynamic/StringArrayValue").ReadValueFunction(() =>
                    NextRandomArray(NextRandomString)),
                new SparkplugMetric("Dynamic/UInt16ArrayValue").ReadValueFunction(() =>
                    NextRandomArray(NextRandomUInt16)),
                new SparkplugMetric("Dynamic/UInt32ArrayValue").ReadValueFunction(() =>
                    NextRandomArray(NextRandomUInt32)),
                new SparkplugMetric("Dynamic/UInt64ArrayValue").ReadValueFunction(() =>
                    NextRandomArray(NextRandomUInt64)),
                // This is a tricky case. We want array of UInt8-s, but that is automatically recognized as scalar
                // Sparkplug Bytes. For a true array of Byte-s, the data type must be specified explicitly.
                new SparkplugMetric("Dynamic/UInt8ArrayValue").ReadValueFunction(SparkplugDataType.UInt8Array, () =>
                    NextRandomArray(NextRandomUInt8)),

                // The FullyWritable sub-folder contains metrics that have not only writable value, but also writable
                // timestamp.
                new SparkplugMetric("FullyWritable/BooleanValue").ReadWriteValue(true),
                new SparkplugMetric("FullyWritable/BytesValue").ReadWriteValue(new byte[] { 0x57, 0x21, 0x40, 0xfc }),
                new SparkplugMetric("FullyWritable/DateTimeValue").ReadWriteValue(
                    // We are passing in UTC times, because we want always the same result, and so we must specify
                    // the DateTimeKind. You can pass in local times, but then they will be converted to UTC by the
                    // producer, and the result will depend on the time zone.
                    DateTime.SpecifyKind(new DateTime(2024, 7, 12, 14, 4, 55).AddSeconds(0.444),
                        DateTimeKind.Utc)),
                new SparkplugMetric("FullyWritable/DoubleValue").ReadWriteValue(7.75630105797e-011),
                new SparkplugMetric("FullyWritable/FloatValue").ReadWriteValue(2.77002e+29f),
                new SparkplugMetric("FullyWritable/Int16Value").ReadWriteValue((short)-30956),
                new SparkplugMetric("FullyWritable/Int32Value").ReadWriteValue(276673160),
                new SparkplugMetric("FullyWritable/Int64Value").ReadWriteValue(1412096336825367659),
                new SparkplugMetric("FullyWritable/Int8Value").ReadWriteValue((sbyte)-113),
                new SparkplugMetric("FullyWritable/StringValue").ReadWriteValue("lorem ipsum"),
                // Sparkplug Text and String data types are both represented as System.String in .NET. The data type must be
                // set explicitly for Text metrics, otherwise the default String data type is used.
                new SparkplugMetric("FullyWritable/TextValue").ReadWriteValue(SparkplugDataType.Text, "lorem ipsum dolor sit"),
                new SparkplugMetric("FullyWritable/UInt16Value").ReadWriteValue((ushort)64421),
                new SparkplugMetric("FullyWritable/UInt32Value").ReadWriteValue(3853116537U),
                new SparkplugMetric("FullyWritable/UInt64Value").ReadWriteValue(9431348106520835314UL),
                new SparkplugMetric("FullyWritable/UInt8Value").ReadWriteValue((byte)144),
                new SparkplugMetric("FullyWritable/UuidValue").ReadWriteValue(
                    new Guid("{1AEF59AE-5029-42A7-9AE2-B2DC00072999}")),

                // The ReadOnly sub-folder contains metrics that are read-only, and their values can be changed through
                // corresponding data metrics in the WriteOnly sub-folder.
                booleanReadOnlyMetric,
                bytesReadOnlyMetric,
                dateTimeReadOnlyMetric,
                doubleReadOnlyMetric,
                floatReadOnlyMetric,
                int16ReadOnlyMetric,
                int32ReadOnlyMetric,
                int64ReadOnlyMetric,
                int8ReadOnlyMetric,
                stringReadOnlyMetric,
                textReadOnlyMetric,
                uInt16ReadOnlyMetric,
                uInt32ReadOnlyMetric,
                uInt64ReadOnlyMetric,
                uInt8ReadOnlyMetric,
                uuidReadOnlyMetric,

                // The Static sub-folder contains metrics with static values which can be changed through writing to
                // them (so-called "registers").
                new SparkplugMetric("Static/BooleanValue").ReadWriteValue(true),
                new SparkplugMetric("Static/BytesValue").ReadWriteValue(new byte[] { 0x57, 0x21, 0x40, 0xfc }),
                new SparkplugMetric("Static/DateTimeValue").ReadWriteValue(
                    // We are passing in UTC times, because we want always the same result, and so we must specify
                    // the DateTimeKind. You can pass in local times, but then they will be converted to UTC by the
                    // server, and the result will depend on the time zone.
                    DateTime.SpecifyKind(new DateTime(2024, 7, 12, 14, 4, 55).AddSeconds(0.444),
                        DateTimeKind.Utc)),
                new SparkplugMetric("Static/DoubleValue").ReadWriteValue(7.75630105797e-011),
                new SparkplugMetric("Static/FloatValue").ReadWriteValue(2.77002e+29f),
                new SparkplugMetric("Static/Int16Value").ReadWriteValue((short)-30956),
                new SparkplugMetric("Static/Int32Value").ReadWriteValue(276673160),
                new SparkplugMetric("Static/Int64Value").ReadWriteValue(1412096336825367659),
                new SparkplugMetric("Static/Int8Value").ReadWriteValue((sbyte)-113),
                new SparkplugMetric("Static/StringValue").ReadWriteValue("lorem ipsum"),
                // Sparkplug Text and String data types are both represented as System.String in .NET. The data type must be
                // set explicitly for Text metrics, otherwise the default String data type is used.
                new SparkplugMetric("Static/TextValue").ReadWriteValue(SparkplugDataType.Text, "lorem ipsum dolor sit"),
                new SparkplugMetric("Static/UInt16Value").ReadWriteValue((ushort)64421),
                new SparkplugMetric("Static/UInt32Value").ReadWriteValue(3853116537U),
                new SparkplugMetric("Static/UInt64Value").ReadWriteValue(9431348106520835314UL),
                new SparkplugMetric("Static/UInt8Value").ReadWriteValue((byte)144),
                new SparkplugMetric("Static/UuidValue").ReadWriteValue(
                    new Guid("{1AEF59AE-5029-42A7-9AE2-B2DC00072999}")),

                new SparkplugMetric("Static/BooleanArrayValue").ReadWriteValue(new[]
                {
                    true,
                    false,
                    true
                }),
                new SparkplugMetric("Static/DateTimeArrayValue").ReadWriteValue(new[]
                {
                    // We are passing in UTC times, because we want always the same result, and so we must specify
                    // the DateTimeKind. You can pass in local times, but then they will be converted to UTC by the
                    // server, and the result will depend on the time zone.
                    DateTime.SpecifyKind(new DateTime(2024, 7, 12, 14, 4, 55).AddSeconds(0.444),
                        DateTimeKind.Utc),
                    DateTime.SpecifyKind(new DateTime(2024, 4, 8), DateTimeKind.Utc),
                    DateTime.SpecifyKind(new DateTime(2023, 8, 14, 18, 13, 0), DateTimeKind.Utc)
                }),
                new SparkplugMetric("Static/DoubleArrayValue").ReadWriteValue(new[]
                {
                    7.75630105797e-011,
                    -0.467227097818268,
                    -3.51653052582609E+300
                }),
                new SparkplugMetric("Static/FloatArrayValue").ReadWriteValue(new[]
                {
                    2.77002e+29f,
                    -1.103936E+36f,
                    -9.002293E-28f
                }),
                new SparkplugMetric("Static/Int16ArrayValue").ReadWriteValue(new short[]
                {
                    -30956,
                    31277,
                    21977
                }),
                new SparkplugMetric("Static/Int32ArrayValue").ReadWriteValue(new[]
                {
                    276673160,
                    630080334,
                    -391755284
                }),
                new SparkplugMetric("Static/Int64ArrayValue").ReadWriteValue(new[]
                {
                    1412096336825367659,
                    -808781653700434592,
                    4707848393174903135
                }),
                new SparkplugMetric("Static/Int8ArrayValue").ReadWriteValue(new sbyte[]
                {
                    -113,
                    -92,
                    2
                }),
                new SparkplugMetric("Static/StringArrayValue").ReadWriteValue(new[]
                {
                    "lorem ipsum",
                    "dolor sit amet",
                    "consectetur adipiscing elit"
                }),
                new SparkplugMetric("Static/UInt16ArrayValue").ReadWriteValue(new ushort[]
                {
                    64421,
                    22663,
                    36755
                }),
                new SparkplugMetric("Static/UInt32ArrayValue").ReadWriteValue(new uint[]
                {
                    3853116537,
                    968679231,
                    995611904
                }),
                new SparkplugMetric("Static/UInt64ArrayValue").ReadWriteValue(new ulong[]
                {
                    9431348106520835314,
                    15635738044048254300,
                    946287779964705249
                }),
                // This is a tricky case. We want array of UInt8-s, but that is automatically recognized as scalar
                // Sparkplug Bytes. For a true array of Byte-s, the data type must be specified explicitly.
                new SparkplugMetric("Static/UInt8ArrayValue").ReadWriteValue(
                    dataType: SparkplugDataType.UInt8Array,
                    value: new byte[]
                    {
                        144,
                        19,
                        233
                    }),

                // Create and add write-only metrics of various data types. Implement write actions that write the value
                // to the corresponding read-only metric of the same data type.
                new SparkplugMetric("WriteOnly/BooleanValue").Readable(false).WriteValueAction((bool value) =>
                    booleanReadOnlyMetric.UpdateReadData(value)),
                new SparkplugMetric("WriteOnly/BytesValue").Readable(false).WriteValueAction((byte[] value) =>
                    bytesReadOnlyMetric.UpdateReadData(value)),
                new SparkplugMetric("WriteOnly/DateTimeValue").Readable(false).WriteValueAction((DateTime value) =>
                    dateTimeReadOnlyMetric.UpdateReadData(value)),
                new SparkplugMetric("WriteOnly/DoubleValue").Readable(false).WriteValueAction((double value) =>
                    doubleReadOnlyMetric.UpdateReadData(value)),
                new SparkplugMetric("WriteOnly/FloatValue").Readable(false).WriteValueAction((float value) =>
                    floatReadOnlyMetric.UpdateReadData(value)),
                new SparkplugMetric("WriteOnly/Int16Value").Readable(false).WriteValueAction((short value) =>
                    int16ReadOnlyMetric.UpdateReadData(value)),
                new SparkplugMetric("WriteOnly/Int32Value").Readable(false).WriteValueAction((int value) =>
                    int32ReadOnlyMetric.UpdateReadData(value)),
                new SparkplugMetric("WriteOnly/Int64Value").Readable(false).WriteValueAction((long value) =>
                    int64ReadOnlyMetric.UpdateReadData(value)),
                new SparkplugMetric("WriteOnly/Int8Value").Readable(false).WriteValueAction((sbyte value) =>
                    int8ReadOnlyMetric.UpdateReadData(value)),
                new SparkplugMetric("WriteOnly/StringValue").Readable(false).WriteValueAction((string value) =>
                    stringReadOnlyMetric.UpdateReadData(value)),
                new SparkplugMetric("WriteOnly/UInt16Value").Readable(false).WriteValueAction((ushort value) =>
                    uInt16ReadOnlyMetric.UpdateReadData(value)),
                new SparkplugMetric("WriteOnly/UInt32Value").Readable(false).WriteValueAction((uint value) =>
                    uInt32ReadOnlyMetric.UpdateReadData(value)),
                new SparkplugMetric("WriteOnly/UInt64Value").Readable(false).WriteValueAction((ulong value) =>
                    uInt64ReadOnlyMetric.UpdateReadData(value)),
                new SparkplugMetric("WriteOnly/UInt8Value").Readable(false).WriteValueAction((byte value) =>
                    uInt8ReadOnlyMetric.UpdateReadData(value)),
                new SparkplugMetric("WriteOnly/UuidValue").Readable(false).WriteValueAction((Guid value) =>
                    uuidReadOnlyMetric.UpdateReadData(value))
            };
        }


        // Random value generators.

        static private readonly Random Random = new Random();

        static private readonly string[] RandomStrings = new[] { "lorem", "ipsum", "dolor", "sit", "amet" };

        static private T[] NextRandomArray<T>(Func<T> nextRandomElement) =>
            new[] { nextRandomElement(), nextRandomElement(), nextRandomElement() };

        static private bool NextRandomBoolean() => Random.Next(2) != 0;

        static private byte[] NextRandomBytes() =>
            new[] { NextRandomUInt8(), NextRandomUInt8(), NextRandomUInt8(), NextRandomUInt8() };

        static private DateTime NextRandomDateTime() =>
            DateTime.MinValue.AddMilliseconds((DateTime.MaxValue - new DateTime(1700, 1, 1)).TotalMilliseconds *
                                              Random.NextDouble());

        static private float NextRandomFloat() =>
            (float)Math.Pow(10, Math.Log10(float.MaxValue) * Random.NextDouble()) * (2 * Random.Next(2) - 1);

        static private double NextRandomDouble() =>
            Math.Pow(10, Math.Log10(double.MaxValue) * Random.NextDouble()) * (2 * Random.Next(2) - 1);

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

        static private sbyte NextRandomInt8() => (sbyte)Random.Next(sbyte.MinValue, sbyte.MaxValue + 1);

        static private string NextRandomString() => RandomStrings[Random.Next(RandomStrings.Length)];

        static private string NextRandomText() => NextRandomString() + " " + NextRandomString();

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

        static private byte NextRandomUInt8() => (byte)Random.Next(byte.MinValue, byte.MaxValue + 1);

        static private Guid NextRandomUuid() => Guid.NewGuid();
    }
}
#endregion
