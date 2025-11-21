// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
#region Example
// This example shows how to obtain all sources under the "Simulation" area.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Diagnostics;
using OpcLabs.EasyOpc.AlarmsAndEvents;
using OpcLabs.EasyOpc.AlarmsAndEvents.AddressSpace;
using OpcLabs.EasyOpc.OperationModel;

namespace DocExamples.AlarmsAndEvents._EasyAEClient
{
    class BrowseSources
    {
        public static void Main1()
        {
            // Instantiate the client object.
            var client = new EasyAEClient();

            AENodeElementCollection nodeElements;
            try
            {
                nodeElements = client.BrowseSources("", "OPCLabs.KitEventServer.2", "Simulation");
            }
            catch (OpcException opcException)
            {
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message);
                return;
            }

            foreach (AENodeElement nodeElement in nodeElements)
            {
                Debug.Assert(nodeElement != null);

                Console.WriteLine("nodeElements[\"{0}\"]:", nodeElement.Name);
                Console.WriteLine("    .QualifiedName: {0}", nodeElement.QualifiedName);
            }
        }
    }
}
#endregion
