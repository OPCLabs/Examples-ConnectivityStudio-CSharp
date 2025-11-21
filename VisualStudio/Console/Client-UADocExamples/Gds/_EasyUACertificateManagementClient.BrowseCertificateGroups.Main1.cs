// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// Shows how to browse and display the certificate groups available in the Certificate Manager.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.Gds;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples.Gds._EasyUACertificateManagementClient
{
    class BrowseCertificateGroups
    {
        public static void Main1()
        {
            // Define which GDS we will work with.
            UAEndpointDescriptor gdsEndpointDescriptor = "opc.tcp://opcua.demo-this.com:58810/GlobalDiscoveryServer";

            // Instantiate the certificate management client object.
            var certificateManagementClient = new EasyUACertificateManagementClient();

            // Browse the certificate groups available in the GDS.
            UACertificateGroupElementCollection certificateGroupElementCollection;
            try
            {
                certificateGroupElementCollection = certificateManagementClient.BrowseCertificateGroups(gdsEndpointDescriptor);
            }
            catch (UAException uaException)
            {
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
                return;
            }

            // Display results
            foreach (UACertificateGroupElement certificateGroupElement in certificateGroupElementCollection)
            {
                Console.WriteLine(certificateGroupElement);
            }
        }
    }
}
#endregion
