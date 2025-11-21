// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable InconsistentNaming
// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// Shows how to register a license located in an embedded managed resource.
// You can use any OPC UA client, including our Connectivity Explorer and OpcCmd utility, to connect to the server. 
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client, server and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Reflection;
using OpcLabs.BaseLib.ComponentModel;
using OpcLabs.EasyOpc.UA;

namespace UAServerDocExamples.Licensing
{
    partial class _LicensingManagement
    {
        public static void RegisterManagedResource()
        {
            // Register a license that is we embed as a managed resource in this program.

            // The first two arguments should always be "OPCWizard" and "Multipurpose".
            // The third argument determines the assembly where the license resides.
            // The fourth argument is the namespace-qualified name of the managed resource, or a pattern identifying it.
            // We could use precise "Key-DemoOrTrial-WebForm-1995003302-20240717.txt" instead.
            OpcLabs.BaseLib.ComponentModel.LicensingManagement.Instance.RegisterManagedResource("OPCWizard", "Multipurpose",
                Assembly.GetExecutingAssembly(), "*.Key-*.*");

            // Instantiate the server object, obtain the serial number from the license info, and display the serial number.
            var server = new EasyUAServer();
            long serialNumber = (uint)server.LicenseInfo["Multipurpose.SerialNumber"];
            Console.WriteLine("SerialNumber: {0}", serialNumber);

            // The license we ship for this purpose is a trial license with low runtime limit, so it won't be of much use.
            // But you get the point...
        }
    }
}
#endregion
