// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable LocalizableElement
#region Example
// Shows how to display all fields of the available license(s).
// You can use any OPC UA client, including our Connectivity Explorer and OpcCmd utility, to connect to the server. 
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client, server and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Collections.Generic;
using OpcLabs.BaseLib.Collections.Specialized;
using OpcLabs.EasyOpc.UA;

namespace UAServerDocExamples.Licensing
{
    partial class LicenseInfo
    {
        public static void AllFields()
        {
            // Instantiate the server object.
            var server = new EasyUAServer();

            // Obtain the license info.
            StringObjectDictionary licenseInfo = server.LicenseInfo;

            // Display all elements.
            foreach (KeyValuePair<string, object> pair in licenseInfo)
                Console.WriteLine($"{pair.Key}: {pair.Value}");
        }
    }
}
#endregion
