// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable CheckNamespace
// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// Shows how to obtain a new application certificate pack from the certificate manager (GDS), and store it for subsequent
// usage, with progress reporting.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Collections.Generic;
using OpcLabs.BaseLib.Security.Cryptography.PkiCertificates;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.AddressSpace;
using OpcLabs.EasyOpc.UA.Application;
using OpcLabs.EasyOpc.UA.Application.Extensions;
using OpcLabs.EasyOpc.UA.Extensions;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UACommonDocExamples.Application._IEasyUAClientServerApplication
{
    partial class ObtainNewCertificatePack
    {
        public static void Progress()
        {
            // Define which GDS we will work with.
            UAEndpointDescriptor gdsEndpointDescriptor = 
                ((UAEndpointDescriptor)"opc.tcp://opcua.demo-this.com:58810/GlobalDiscoveryServer")
                .WithUserNameIdentity("appadmin", "demo");

            // Obtain the application interface.
            EasyUAApplication application = EasyUAApplication.Instance;

            // Display which application we are about to work with.
            Console.WriteLine("Application URI string: {0}",
                application.GetApplicationElement().ApplicationUriString);

            // Obtain a new application certificate pack from the certificate manager (GDS), and store it for subsequent
            // usage.
            UANodeIdPkiCertificateDictionary certificateDictionary;
            try
            {
                certificateDictionary = application.ObtainNewCertificatePack(gdsEndpointDescriptor,
                    new Progress<string>(s => Console.WriteLine("Progress: {0}", s)));
            }
            catch (UAException uaException)
            {
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
                return;
            }

            // Display results
            foreach (KeyValuePair<UANodeId, IPkiCertificate> pair in certificateDictionary)
            {
                Console.WriteLine();
                Console.WriteLine($"Certificate type Id: {pair.Key}");
                Console.WriteLine($"Certificate: {pair.Value}");
            }
        }
    }
}
#endregion
