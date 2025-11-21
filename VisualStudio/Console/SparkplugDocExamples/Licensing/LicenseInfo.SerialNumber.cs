// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// Shows how to obtain the serial number of the active license, and determine whether it is a stock demo or trial license.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasySparkplug;

namespace SparkplugDocExamples.Licensing
{
    partial class LicenseInfo
    {
        public static void SerialNumber()
        {
            // Instantiate the edge node object.
            // NOTE: If you are using the consumer object (EasySparkplugConsumer), instantiate it instead.
            var edgeNode = new EasySparkplugEdgeNode();

            // Obtain the serial number from the license info.
            long serialNumber = (uint)edgeNode.LicenseInfo["Multipurpose.SerialNumber"];

            // Display the serial number.
            Console.WriteLine("SerialNumber: {0}", serialNumber);

            // Determine whether we are running as demo or trial.
            if ((1111110000 <= serialNumber) && (serialNumber <= 1111119999))
                Console.WriteLine("This is a stock demo or trial license.");
            else
                Console.WriteLine("This is not a stock demo or trial license.");
        }
    }
}
#endregion
