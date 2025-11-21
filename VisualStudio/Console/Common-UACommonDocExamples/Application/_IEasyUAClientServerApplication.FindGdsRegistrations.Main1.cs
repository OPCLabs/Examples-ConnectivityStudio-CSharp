// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// Shows how to find all application's registrations in the GDS.
// In order to be able to find the registration, run the example for the RegisterToGds method first.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Collections.Generic;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.AddressSpace;
using OpcLabs.EasyOpc.UA.Application;
using OpcLabs.EasyOpc.UA.Application.Extensions;
using OpcLabs.EasyOpc.UA.Discovery;
using OpcLabs.EasyOpc.UA.Extensions;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UACommonDocExamples.Application._IEasyUAClientServerApplication
{
    class FindGdsRegistrations
    {
        public static void Main1()
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

            // Find all application's registrations in the GDS.
            IReadOnlyDictionary<UANodeId, UAApplicationElement> applicationElementDictionary;
            try
            {
                applicationElementDictionary = application.FindGdsRegistrations(gdsEndpointDescriptor);
            }
            catch (UAException uaException)
            {
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
                return;
            }

            // Display results
            foreach (KeyValuePair<UANodeId, UAApplicationElement> pair in applicationElementDictionary)
            {
                Console.WriteLine();
                Console.WriteLine("Application ID: {0}", pair.Key);

                UAApplicationElement applicationElement = pair.Value;
                Console.WriteLine("Application element: {0}", applicationElement);
                Console.WriteLine("  Application URI string: {0}", applicationElement.ApplicationUriString);
                Console.WriteLine("  Discovery URI strings: {0}", applicationElement.DiscoveryUriStrings);
                Console.WriteLine("  Product URI string: {0}", applicationElement.ProductUriString);
            }
        }
    }
}
#endregion
