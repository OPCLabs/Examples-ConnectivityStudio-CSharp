// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// Shows how to unregister all clients from a GDS.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Collections.Generic;
using System.Linq;
using OpcLabs.BaseLib.Collections.Generic.Extensions;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.AddressSpace;
using OpcLabs.EasyOpc.UA.Discovery;
using OpcLabs.EasyOpc.UA.Extensions;
using OpcLabs.EasyOpc.UA.Gds;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples.Gds._EasyUAGlobalDiscoveryClient
{
    class UnregisterApplication
    {
        public static void Main1()
        {
            // Define which GDS we will work with.
            UAEndpointDescriptor gdsEndpointDescriptor =
                ((UAEndpointDescriptor)"opc.tcp://opcua.demo-this.com:58810/GlobalDiscoveryServer")
                .WithUserNameIdentity("appadmin", "demo");

            // Instantiate the global discovery client object
            var globalDiscoveryClient = new EasyUAGlobalDiscoveryClient();

            // Find application IDs of all client applications registered in the GDS.
            var clientApplicationIds = new HashSet<UANodeId>();
            try
            {
                globalDiscoveryClient.QueryApplications(
                    gdsEndpointDescriptor: gdsEndpointDescriptor,
                    startingRecordId: 0,
                    maximumRecordsToReturn: 0,
                    applicationName: "",
                    applicationUriString: "",
                    applicationTypes: UAApplicationTypes.Client,
                    productUriString: "",
                    serverCapabilities: new string[0],
                    lastCounterResetTime: out _,
                    nextRecordId: out _,
                    applications: out UAApplicationDescription[] applicationDescriptionArray);

                foreach (UAApplicationDescription applicationDescription in applicationDescriptionArray)
                {
                    var applicationRecordArray = globalDiscoveryClient.FindApplications(
                        gdsEndpointDescriptor,
                        applicationDescription.ApplicationUriString);
                    clientApplicationIds.AddRange(applicationRecordArray.Select(description => description.ApplicationId));
                }
            }
            catch (UAException uaException)
            {
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
                return;
            }

            // Unregister all client applications found.
            foreach (UANodeId applicationId in clientApplicationIds)
            {
                Console.WriteLine();
                Console.WriteLine("Application ID: {0}", applicationId);

                try
                {
                    globalDiscoveryClient.UnregisterApplication(gdsEndpointDescriptor, applicationId);
                }
                catch (UAException uaException)
                {
                    Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
                    continue;
                }
                Console.WriteLine("Unregistered.");
            }
        }
    }
}
#endregion
