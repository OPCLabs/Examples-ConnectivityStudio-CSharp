// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// Shows how to find client or server applications that meet the specified filters, using the global discovery client.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA.Discovery;
using OpcLabs.EasyOpc.UA.Gds;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples.Gds._EasyUAGlobalDiscoveryClient
{
    class QueryApplications
    {
        public static void Main1()
        {
            // Instantiate the global discovery client object
            var globalDiscoveryClient = new EasyUAGlobalDiscoveryClient();

            // Find all (client or server) applications registered in the GDS.
            UAApplicationDescription[] applicationDescriptionArray;
            try
            {
                globalDiscoveryClient.QueryApplications(
                    gdsEndpointDescriptor:"opc.tcp://opcua.demo-this.com:58810/GlobalDiscoveryServer",
                    startingRecordId:0,
                    maximumRecordsToReturn:0,
                    applicationName:"",
                    applicationUriString:"",
                    applicationTypes:UAApplicationTypes.All,
                    productUriString:"",
                    serverCapabilities:new string[0],
                    lastCounterResetTime:out _,
                    nextRecordId: out _,
                    applications: out applicationDescriptionArray);
            }
            catch (UAException uaException)
            {
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
                return;
            }

            // Display results
            foreach (UAApplicationDescription applicationDescription in applicationDescriptionArray)
            {
                Console.WriteLine();
                Console.WriteLine("Application name: {0}", applicationDescription.ApplicationName);
                Console.WriteLine("Application type: {0}", applicationDescription.ApplicationType);
                Console.WriteLine("Application URI string: {0}", applicationDescription.ApplicationUriString);
                Console.WriteLine("Discovery URI strings: {0}", applicationDescription.DiscoveryUriStrings);
            }
        }
    }
}
#endregion
