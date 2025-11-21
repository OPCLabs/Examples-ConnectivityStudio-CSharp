// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable InconsistentNaming
// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// Shows how to register a license located in an embedded managed resource, verifying its existence upfront.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Reflection;
using OpcLabs.BaseLib.Portable;
using OpcLabs.EasySparkplug;

namespace SparkplugDocExamples.Licensing
{
    class _LicensingManagement
    {
        public static void RegisterManagedResourceWithExistenceCheck()
        {
            // Register a license that is we embed as a managed resource in this program.

            // The first two arguments should always be "EasySparkplug" and "Multipurpose".
            // The third argument determines the assembly where the license resides.
            // The fourth argument is the namespace-qualified name of the managed resource, or a pattern identifying it.
            // We could use precise "Key-DemoOrTrial-WebForm-1015004362-21250624.txt" instead.
            try
            {
                OpcLabs.BaseLib.ComponentModel.LicensingManagement.Instance.RegisterManagedResourceWithExistenceCheck(
                    "EasySparkplug", 
                    "Multipurpose",
                    Assembly.GetExecutingAssembly(), 
                    "*.Key-*.*");
            }
            // This exception will be thrown if the specified managed resource does not exist or is not accessible.
            // Note, however, that the validity of the license is not checked at this point.
            // You can experiment with this code path by modifying the resource name in the call above.
            catch (CustomException customException)
            {
                Console.WriteLine($"*** Failure: {customException.Message}");
                return;
            }

            // Instantiate the edge node object, obtain the serial number from the license info, and display the serial
            // number.
            // NOTE: If you are using the consumer object (EasySparkplugConsumer), instantiate it instead.
            var edgeNode = new EasySparkplugEdgeNode();
            long serialNumber = (uint)edgeNode.LicenseInfo["Multipurpose.SerialNumber"];
            Console.WriteLine("SerialNumber: {0}", serialNumber);

            // The license we ship for this purpose is a trial license with low runtime limit, so it won't be of much use.
            // But you get the point...
        }
    }
}
#endregion
