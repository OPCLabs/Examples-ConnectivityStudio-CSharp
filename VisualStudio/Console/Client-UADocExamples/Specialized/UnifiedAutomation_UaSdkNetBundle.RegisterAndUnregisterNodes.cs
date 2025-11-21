// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable InconsistentNaming
// ReSharper disable StringLiteralTypo
#region Example
// This example shows how to register and unregister multiple nodes with Unified Automation UA .NET SDK Bundle.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using Microsoft.Extensions.DependencyInjection;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.OperationModel;
using OpcLabs.EasyOpc.UA.Services;
using OpcLabs.EasyOpc.UA.Services.Extensions;

namespace UADocExamples.Specialized
{
    class UnifiedAutomation_UaSdkNetBundle
    {
        public static void RegisterAndUnregisterNodes()
        {
            UAEndpointDescriptor endpointDescriptor = "opc.tcp://localhost:48030/";

            // Instantiate the client object and hook events
            using (var client = new EasyUAClient())
            {
                // Obtain the client node registration service.
                IEasyUAClientNodeRegistration clientNodeRegistration =
                    client.GetRequiredService<IEasyUAClientNodeRegistration>();

                // Obtain the client connection control service.
                IEasyUAClientConnectionControl clientConnectionControl =
                    client.GetRequiredService<IEasyUAClientConnectionControl>();

                var nodeDescriptorArray = new UANodeDescriptor[]
                {
                    "nsu=http://www.unifiedautomation.com/DemoServer/ ;ns=2;s=Demo.Massfolder_Dynamic.Variable000",
                    "nsu=http://www.unifiedautomation.com/DemoServer/ ;ns=2;s=Demo.Massfolder_Dynamic.Variable001",
                    "nsu=http://www.unifiedautomation.com/DemoServer/ ;ns=2;s=Demo.Massfolder_Dynamic.Variable002",
                    "nsu=http://www.unifiedautomation.com/DemoServer/ ;ns=2;s=Demo.Massfolder_Dynamic.Variable003",
                    "nsu=http://www.unifiedautomation.com/DemoServer/ ;ns=2;s=Demo.Massfolder_Dynamic.Variable004",
                    "nsu=http://www.unifiedautomation.com/DemoServer/ ;ns=2;s=Demo.Massfolder_Dynamic.Variable005",
                    "nsu=http://www.unifiedautomation.com/DemoServer/ ;ns=2;s=Demo.Massfolder_Dynamic.Variable006",
                    "nsu=http://www.unifiedautomation.com/DemoServer/ ;ns=2;s=Demo.Massfolder_Dynamic.Variable007",
                    "nsu=http://www.unifiedautomation.com/DemoServer/ ;ns=2;s=Demo.Massfolder_Dynamic.Variable008",
                    "nsu=http://www.unifiedautomation.com/DemoServer/ ;ns=2;s=Demo.Massfolder_Dynamic.Variable009"
                };

                Console.WriteLine("Registering nodes");
                int[] registrationHandleArray =
                    clientNodeRegistration.RegisterMultipleNodes(endpointDescriptor, nodeDescriptorArray);

                Console.WriteLine("Locking the connection");
                // Locking the connection will attempt to open it, and when successful, the nodes will be registered with
                // the server at that time. The use of locking is not necessary, but it may bring benefits together with the
                // node registration. See the conceptual documentation for more information.
                int lockHandle = clientConnectionControl.LockConnection(endpointDescriptor);

                Console.WriteLine("Waiting for 10 seconds...");
                // The example uses this delay to demonstrate the fact that your code might have other tasks to do, before
                // it accesses the previously registered nodes.
                System.Threading.Thread.Sleep(10 * 1000);

                Console.WriteLine("Reading (1)");
                UAAttributeDataResult[] resultArray1 = client.ReadMultiple(endpointDescriptor, nodeDescriptorArray);
                foreach (UAAttributeDataResult result in resultArray1)
                    Console.WriteLine(result);

                Console.WriteLine("Reading (2)");
                UAAttributeDataResult[] resultArray2 = client.ReadMultiple(endpointDescriptor, nodeDescriptorArray);
                foreach (UAAttributeDataResult result in resultArray2)
                    Console.WriteLine(result);

                Console.WriteLine("Unlocking the connection");
                clientConnectionControl.UnlockConnection(lockHandle);

                Console.WriteLine("Unregistering nodes");
                clientNodeRegistration.UnregisterMultipleNodes(registrationHandleArray);

                Console.WriteLine("Finished.");
            }
        }
    }
}
#endregion
