// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// Shows how to find all registrations in the GDS.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.Discovery;
using OpcLabs.EasyOpc.UA.Extensions;
using OpcLabs.EasyOpc.UA.Gds;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples.Gds._EasyUAGlobalDiscoveryClient
{
    class FindApplications
    {
        public static void Main1()
        {
            // Define which GDS we will work with.
            UAEndpointDescriptor gdsEndpointDescriptor =
                ((UAEndpointDescriptor)"opc.tcp://opcua.demo-this.com:58810/GlobalDiscoveryServer")
                .WithUserNameIdentity("appuser", "demo");

            // Instantiate the global discovery client object
            var globalDiscoveryClient = new EasyUAGlobalDiscoveryClient();

            // Find all (client or server) applications registered in the GDS.
            UAApplicationDescription[] applicationDescriptionArray;
            try
            {
                globalDiscoveryClient.QueryApplications(
                    gdsEndpointDescriptor: gdsEndpointDescriptor,
                    startingRecordId: 0,
                    maximumRecordsToReturn: 0,
                    applicationName: "",
                    applicationUriString: "",
                    applicationTypes: UAApplicationTypes.All,
                    productUriString: "",
                    serverCapabilities: new string[0],
                    lastCounterResetTime: out _,
                    nextRecordId: out _,
                    applications: out applicationDescriptionArray);
            }
            catch (UAException uaException)
            {
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
                return;
            }

            // For each application returned by the query, find its registrations in the GDS.
            foreach (UAApplicationDescription applicationDescription in applicationDescriptionArray)
            {
                Console.WriteLine();
                Console.WriteLine("Application URI string: {0}", applicationDescription.ApplicationUriString);

                UAApplicationRecordDataType[] applicationRecordArray;
                try
                {
                    applicationRecordArray = globalDiscoveryClient.FindApplications(
                        gdsEndpointDescriptor,
                        applicationDescription.ApplicationUriString);
                }
                catch (UAException uaException)
                {
                    Console.WriteLine("  *** Failure: {0}", uaException.GetBaseException().Message);
                    continue;
                }

                // Display results
                foreach (UAApplicationRecordDataType applicationRecord in applicationRecordArray)
                    Console.WriteLine("  Application ID: {0}", applicationRecord.ApplicationId);
            }


            // Example output:
            //
            //Application URI string: urn:sampleserver
            //  Application ID: nsu=http://opcfoundation.org/UA/GDS/applications/ ;ns=2;g=09ecaa08-6ec6-462c-a214-1e66a3099107
            //
            //Application URI string: urn:alarmconditionserver
            //  Application ID: nsu=http://opcfoundation.org/UA/GDS/applications/ ;ns=2;g=783e1e9a-8036-43b6-928f-97488c460266
            //
            //Application URI string: urn:PC:MultiTargetUADocExamples:5.54.1026.1:neutral:null
            //  Application ID: nsu=http://opcfoundation.org/UA/GDS/applications/ ;ns=2;g=9e700ea5-55a6-4c3c-ba9f-b91c890dc519
            //
            //Application URI string: urn:PC:UADocExamples:5.56.0.16:neutral:null
            //  Application ID: nsu=http://opcfoundation.org/UA/GDS/applications/ ;ns=2;g=e182e28c-086b-4fc7-82c7-70ca7cda3033
            //
            //Application URI string: urn:PC:cscript:5.812.10240.16384
            //  Application ID: nsu=http://opcfoundation.org/UA/GDS/applications/ ;ns=2;g=aec94459-f513-4979-8619-8383555fca61
        }
    }
}
#endregion
