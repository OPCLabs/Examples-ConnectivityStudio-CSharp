// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable CommentTypo
#region Example
// This example demonstrates how to place the application instance certificate in the platform-specific (Windows, Linux,
// ...) certificate store.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.Application;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UACommonDocExamples._UAApplicationManifest
{
    class InstanceOwnStorePath
    {
        public static void PlatformSpecific()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Set the application certificate store path, which determines the location of the client certificate.
            // Note that this only works once in each host process.
            EasyUAApplication.Instance.ApplicationParameters.ApplicationManifest.InstanceOwnStorePath = "CurrentUser\\My";

            // Do something - invoke an OPC read, to trigger creation of the certificate.
            // If you are doing server development: Instantiate and start the server here, instead of invoking the client.
            var client = new EasyUAClient();
            try
            {
                client.ReadValue(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10853");
            }
            catch (UAException uaException)
            {
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
            }

            // The certificate will be located or created in the specified platform-specific certificate store.
            // On Windows, when viewed by the certmgr.msc tool, it will be under
            // Certificates - Current User -> Personal -> Certificates.

            Console.WriteLine("Finished.");
        }
    }
}
#endregion
