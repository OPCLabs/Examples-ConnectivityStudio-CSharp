// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to set the validity period of the application instance certificate.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.BaseLib.Security.Cryptography.PkiCertificates;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.Application;
using OpcLabs.EasyOpc.UA.Application.Extensions;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UACommonDocExamples._CertificateGenerationParameters
{
    class ValidityPeriodInMonths
    {
        public static void Main1()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            Console.WriteLine("Setting the validity period of the auto-generated instance certificate to 50 years (600 months)...");
            EasyUAApplication.Instance.ApplicationParameters.InstanceCertificateGenerationParameters.ValidityPeriodInMonths = 600;

            Console.WriteLine("Obtaining the application interface...");
            var client = new EasyUAClient();
            EasyUAApplication application = EasyUAApplication.Instance;

            try
            {
                Console.WriteLine("Removing the current application instance certificate pack...");
                application.RemoveOwnCertificatePack(mustExist:false);

                Console.WriteLine("Do something - invoke an OPC read, to trigger auto-generation of a new instance certificate...");
                // If you are doing server development: Instantiate and start the server here, instead of invoking the client.
                client.ReadValue(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10853");

                Console.WriteLine("Finding the current default application instance certificate...");
                IPkiCertificate instanceCertificate = application.FindOwnCertificate();

                if (!(instanceCertificate is null))
                    Console.WriteLine($"Expiration date: {instanceCertificate.NotAfter}");
            }
            catch (UAException uaException)
            {
                Console.WriteLine($"*** Failure: {uaException.GetBaseException().Message}");
            }
        }
    }
}
#endregion
