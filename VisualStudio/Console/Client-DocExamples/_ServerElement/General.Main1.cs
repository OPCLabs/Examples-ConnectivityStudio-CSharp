// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable CommentTypo
// ReSharper disable InconsistentNaming
// ReSharper disable StringLiteralTypo
#region Example
// This example shows all information available about OPC servers.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.OperationModel;

namespace DocExamples._ServerElement
{
    class General
    {
        public static void Main1()
        {
            // Instantiate the client object.
            var client = new EasyDAClient();

            ServerElementCollection serverElements;
            try
            {
                serverElements = client.BrowseServers();
            }
            catch (OpcException opcException)
            {
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message);
                return;
            }

            foreach (ServerElement serverElement in serverElements)
            {
                Console.WriteLine($"Information about server \"{serverElement}\":");
                Console.WriteLine($"    .ServerClass: {serverElement.ServerClass}");
                Console.WriteLine($"    .ClsidString: {serverElement.ClsidString}");
                Console.WriteLine($"    .ProgId: {serverElement.ProgId}");
                Console.WriteLine($"    .Description: {serverElement.Description}");
                Console.WriteLine($"    .Vendor: {serverElement.Vendor}");
                Console.WriteLine($"    .ServerCategories: {serverElement.ServerCategories}");
                Console.WriteLine($"    .VersionIndependentProgId: {serverElement.VersionIndependentProgId}");
            }


            // Example output:
            //
            //Information about server "opcda:OPCLabs.KitServer.2/%7Bc8a12f17-1e03-401e-b53d-6c654dd576da%7D":
            //    .ServerClass: OPCLabs.KitServer.2
            //    .ClsidString: c8a12f17-1e03-401e-b53d-6c654dd576da
            //    .ProgId: OPCLabs.KitServer.2
            //    .Description: OPC Labs Kit Server
            //    .Vendor: OPC Labs, http://www.opclabs.com
            //    .ServerCategories: (OpcDataAccess10, OpcDataAccess20, OpcDataAccess30)
            //    .VersionIndependentProgId: OPCLabs.KitServer
        }
    }
}
#endregion
