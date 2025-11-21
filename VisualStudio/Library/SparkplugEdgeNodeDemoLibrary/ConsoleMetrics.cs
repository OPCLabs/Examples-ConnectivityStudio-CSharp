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
    static public class ConsoleMetrics
    {
        /// <summary>
        /// Adds metrics that allow interaction with the console.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The Console metrics allow Sparkplug applications to display data on the Sparkplug edge node's console (if it has
        /// such device). They are included mainly for the fun of it, to demonstrate the fact that any actions can be tied
        /// to the Sparkplug commands. Real Sparkplug components will not do this.</para>
        /// </remarks>
        static public SparkplugMetric[] Create() => new[]
        {
            // The Write metric writes the value to the console.
            new SparkplugMetric("Write")
                .Readable(false)
                .WriteValueAction((string s) => Console.Write(s)),
            // The WriteLine metric writes the value to the console and appends a new line.
            new SparkplugMetric("WriteLine")
                .Readable(false)
                .WriteValueAction((string s) => Console.WriteLine(s))
        };
    }
}
#endregion
