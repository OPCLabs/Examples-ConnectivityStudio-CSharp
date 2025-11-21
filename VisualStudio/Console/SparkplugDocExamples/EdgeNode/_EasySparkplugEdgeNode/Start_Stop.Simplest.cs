// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable InconsistentNaming
#region Example
// This example shows how to create a Sparkplug edge node with extremely simple code.
//
// You can use any Sparkplug application, including our SparkplugCmd utility and the SparkplugApplicationConsoleDemo
// program, to subscribe to the edge node data. 
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasySparkplug;

namespace SparkplugDocExamples.EdgeNode._EasySparkplugEdgeNode
{
    partial class Start_Stop
    {
        static public void Simplest()
        {
            var edgeNode = new EasySparkplugEdgeNode("mqtt://localhost", "easyGroup", "easySparkplugDemo");
            var random = new Random();
            edgeNode.Metrics.Add(new SparkplugMetric("MyMetric").ReadValueFunction(() => random.Next()));

            edgeNode.Start();
            Console.ReadLine();
            edgeNode.Stop();
        }
    }
}
#endregion
