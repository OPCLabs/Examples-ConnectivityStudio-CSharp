// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to obtain information about OPC UA servers from the Global Discovery Server (GDS).
// The result is hierarchical, i.e. each server is returned in one element, and the element contains all its discovery URLs.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.Discovery;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples._EasyUAClient
{
    partial class DiscoverGlobalServers
    {
        public static void Hierarchical()
        {
            // Instantiate the client object
            var client = new EasyUAClient();

            // Obtain collection of application elements
            UADiscoveryElementCollection discoveryElementCollection;
            try
            {
                discoveryElementCollection = client.DiscoverGlobalServers(
                    "opc.tcp://opcua.demo-this.com:58810/GlobalDiscoveryServer", 
                    flat:false);
            }
            catch (UAException uaException)
            {
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
                return;
            }

            // Display results
            foreach (UADiscoveryElement discoveryElement in discoveryElementCollection)
            {
                Console.WriteLine();
                Console.WriteLine("Server name: {0}", discoveryElement.ServerName);
                Console.WriteLine("Discovery URI strings:");
                foreach (string discoveryUriString in discoveryElement.DiscoveryUriStrings)
                    Console.WriteLine("  {0}", discoveryUriString);
                Console.WriteLine("Server capabilities: {0}", discoveryElement.ServerCapabilities);
                Console.WriteLine("Application URI string: {0}", discoveryElement.ApplicationUriString);
                Console.WriteLine("Product URI string: {0}", discoveryElement.ProductUriString);
            }
        }
    }
}
#endregion
