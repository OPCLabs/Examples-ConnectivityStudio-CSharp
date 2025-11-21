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
    static public class DemoMetrics
    {
        /// <summary>
        /// Adds metrics that demonstrate various features of EasySparkplug.
        /// </summary>
        static public SparkplugMetric[] Create() => new[]
        {
            // Demonstrate that in the simplest case, metrics can be added directly to the root level.
            new SparkplugMetric("Ramp").ReadValueFunction(() => (Environment.TickCount % 1000)/1000.0),
            new SparkplugMetric("Simple").ReadWriteValue(0),
            new SparkplugMetric("Simple2").ReadWriteValue(0),

            // Demonstrate the fact that metrics can be organized in folders, and that metrics can be nested.
            new SparkplugMetric("Random").ReadValueFunction(() => Random.NextDouble()),
            new SparkplugMetric("Nested/Random").ReadValueFunction(() => Random.NextDouble()),
            new SparkplugMetric("Nested/FurtherNested/Random").ReadValueFunction(() => Random.NextDouble()),

            // Demonstrate that the metric may decide to fail the write operation.
            // Note that the consumer can only determine that the write operation failed by subscribing to the metric and
            // observing whether the value has changed after the write operation
            new SparkplugMetric("WriteFailure").WriteFunction<int>(_ => false)
        };

        
        static private readonly Random Random = new Random();
    }
}
#endregion
