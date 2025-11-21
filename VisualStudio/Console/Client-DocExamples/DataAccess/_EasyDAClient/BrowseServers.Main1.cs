// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo
#region Example
// This example shows how to obtain all ProgIDs of all OPC Data Access servers on the local machine.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.OperationModel;

namespace DocExamples.DataAccess._EasyDAClient
{
    class BrowseServers
    {
        public static void Main1()
        {
            // Instantiate the client object.
            var client = new EasyDAClient();
            ServerElementCollection serverElements;
            try
            {
                serverElements = client.BrowseServers("");
            }
            catch (OpcException opcException)
            {
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message);
                return;
            }

            foreach (ServerElement serverElement in serverElements)
                Console.WriteLine($"ServerElements(\"{serverElement.ClsidString}\").ProgId: {serverElement.ProgId}");
        }


        // Example output:
        //
        //ServerElements("c8a12f17-1e03-401e-b53d-6c654dd576da").ProgId: OPCLabs.KitServer.2
    }
}
#endregion
