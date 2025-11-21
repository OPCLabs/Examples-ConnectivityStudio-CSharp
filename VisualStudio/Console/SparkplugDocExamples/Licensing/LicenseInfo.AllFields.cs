// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable LocalizableElement
#region Example
// Shows how to display all fields of the available license(s).
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Collections.Generic;
using OpcLabs.BaseLib.Collections.Specialized;
using OpcLabs.EasySparkplug;

namespace SparkplugDocExamples.Licensing
{
    partial class LicenseInfo
    {
        public static void AllFields()
        {
            // Instantiate the edge node object.
            // NOTE: If you are using the consumer object (EasySparkplugConsumer), instantiate it instead.
            var edgeNode = new EasySparkplugEdgeNode();

            // Obtain the license info.
            StringObjectDictionary licenseInfo = edgeNode.LicenseInfo;

            // Display all elements.
            foreach (KeyValuePair<string, object> pair in licenseInfo)
                Console.WriteLine($"{pair.Key}: {pair.Value}");
        }
    }
}
#endregion
