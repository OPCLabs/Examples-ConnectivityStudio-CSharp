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

using OpcLabs.EasyOpc.UA.NodeSpace;
using System;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.Generic;

namespace UAServerDemoLibrary
{
    static public class DemoNodes
    {
        /// <summary>
        /// Adds nodes that demonstrate various features of the OPC Wizard.
        /// </summary>
        /// <param name="parentFolder">The folder to which to add the nodes.</param>
        static public void AddToParent(UAFolder parentFolder)
        {
            // Demonstrate that in the simplest case, the data variable can be added directly to the Objects folder.
            parentFolder.Add(new UADataVariable("Simple").ReadWriteValue(0));

            // Demonstrate the fact that nodes can be organized in folders, and that data variables can be nested.
            UAFolder demoFolder = UAFolder.CreateIn(parentFolder, "Demo");

            // Create an empty data variable.
            demoFolder.Add(new UADataVariable("Empty"));

            // Demonstrate that the data variables can be nested.
            demoFolder.Add(
                new UADataVariable("Random")
                {
                    new UADataVariable("Nested").ReadValueFunction(() => Random.NextDouble())
                }.ReadValueFunction(() => Random.NextDouble()));

            // Demonstrate that the data values returned can also contain status codes that are not fully "Good".
            demoFolder.Add(new UADataVariable("GoodLocalOverride").ReadWrite(new UAAttributeData<int>(
                value: 0,
                UACodeBits.GoodLocalOverride,
                DateTime.UtcNow)));
            demoFolder.Add(new UADataVariable("BadNoCommunication").ReadWrite(new UAAttributeData<int>(
                value: 0,
                UACodeBits.BadNoCommunication,
                DateTime.UtcNow)));
            demoFolder.Add(new UADataVariable("Unreliable").ReadFunction(() => new UAAttributeData<int>(
                value: 42,
                (Random.Next(2) != 0) ? UACodeBits.Good : UACodeBits.BadNoCommunication,
                DateTime.UtcNow)));

            // Demonstrate that the data variable may decide to fail the write operation.
            demoFolder.Add(new UADataVariable("WriteFailure").WriteFunction<int>(_ =>
                UACodeBits.BadNoCommunication));

            // Depending on the needs, you can specify custom minimum sampling interval for the data variable.
            demoFolder.Add(new UADataVariable("SlowSampling")
                .ReadValueFunction(() => Random.Next())
                .SetMinimumSamplingInterval(5 * 1000));

            // Create a 3-dimensional array data variable.
            demoFolder.Add(new UADataVariable("Array3D").ReadWriteValue(new int[2, 4, 3]));

            // A data variable of data type BaseDataType that, in fact, only accepts float values to be written into it.
            // This is a demonstration of how the data variable should *not* behave, because the client has no way of
            // determining the data type that the server expects.
            var variantRestrictedDataVariable = new UADataVariable("VariantRestricted");
            demoFolder.Add(variantRestrictedDataVariable
                
                .WriteValueFunction<object>(value =>
                {
                    if (value is float)
                    {
                        variantRestrictedDataVariable.UpdateWriteAttributeData(value);
                        return null;
                    }
                    return UACodeBits.BadTypeMismatch;
                })
                .ReadWriteValue((object)0.0f));
        }


        static private readonly Random Random = new Random();
    }
}
#endregion
