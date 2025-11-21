// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// Shows how to check if an application needs to update its certificate.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.AddressSpace;
using OpcLabs.EasyOpc.UA.Application;
using OpcLabs.EasyOpc.UA.Extensions;
using OpcLabs.EasyOpc.UA.Gds;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples.Gds._EasyUACertificateManagementClient
{
    class GetCertificateStatus
    {
        public static void Main1()
        {
            // Define which GDS we will work with.
            UAEndpointDescriptor gdsEndpointDescriptor = 
                ((UAEndpointDescriptor)"opc.tcp://opcua.demo-this.com:58810/GlobalDiscoveryServer")
                .WithUserNameIdentity("appadmin", "demo");

            // Register our client application with the GDS, so that we obtain an application ID that we need later.
            // Obtain the application interface.
            EasyUAApplication application = EasyUAApplication.Instance;
            UANodeId applicationId;
            try
            {
                applicationId = application.RegisterToGds(gdsEndpointDescriptor);
            }
            catch (UAException uaException)
            {
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
                return;
            }
            Console.WriteLine("Application ID: {0}", applicationId);

            // Instantiate the certificate management client object
            var certificateManagementClient = new EasyUACertificateManagementClient();

            // Check if the application needs to update its certificate.
            bool updateRequired;
            try
            {
                updateRequired = certificateManagementClient.GetCertificateStatus(gdsEndpointDescriptor, applicationId,
                    UANodeId.Null, UANodeId.Null);
            }
            catch (UAException uaException)
            {
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
                return;
            }

            // Display results
            Console.WriteLine("Update required: {0}", updateRequired);
        }


        // Example output:
        //Application ID: nsu=http://opcfoundation.org/UA/GDS/applications/ ;ns=2;g=aec94459-f513-4979-8619-8383555fca61
        //Update required: False
    }
}
#endregion
