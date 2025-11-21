// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
#region Example
// This example shows all information available about categories that particular OPC servers do support.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc;
using OpcLabs.EasyOpc.AlarmsAndEvents;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.OperationModel;

namespace DocExamples._ServerCategories
{
    class General
    {
        public static void Main1()
        {
            // Instantiate the OPC-DA client object.
            var daClient = new EasyDAClient();

            Console.WriteLine();
            Console.WriteLine("OPC DATA ACCESS");
            ServerElementCollection daServerElements;
            try
            {
                daServerElements = daClient.BrowseServers();
            }
            catch (OpcException opcException)
            {
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message);
                return;
            }
            DumpServerElements(daServerElements);

            // Instantiate the OPC-A&E client object.
            var aeClient = new EasyAEClient();

            Console.WriteLine();
            Console.WriteLine("OPC ALARMS AND EVENTS");
            ServerElementCollection aeServerElements;
            try
            {
                aeServerElements = aeClient.BrowseServers();
            }
            catch (OpcException opcException)
            {
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message);
                return;
            }
            DumpServerElements(aeServerElements);
        }

        private static void DumpServerElements(ServerElementCollection serverElements)
        {
            foreach (ServerElement serverElement in serverElements)
            {
                Console.WriteLine($"Categories of \"{serverElement.ProgId}\":");
                ServerCategories serverCategories = serverElement.ServerCategories;
                Console.WriteLine($"    .OpcAlarmsAndEvents10: {serverCategories.OpcAlarmsAndEvents10}");
                Console.WriteLine($"    .OpcDataAccess10: {serverCategories.OpcDataAccess10}");
                Console.WriteLine($"    .OpcDataAccess20: {serverCategories.OpcDataAccess20}");
                Console.WriteLine($"    .OpcDataAccess30: {serverCategories.OpcDataAccess30}");
                Console.WriteLine($"    .ToString(): {serverCategories}");
            }
        }

        
        // Example output:
        //
        //OPC DATA ACCESS
        //Categories of "OPCLabs.KitServer.2":
        //    .OpcAlarmsAndEvents10: False
        //    .OpcDataAccess10: True
        //    .OpcDataAccess20: True
        //    .OpcDataAccess30: True
        //    .ToString(): (OpcDataAccess10, OpcDataAccess20, OpcDataAccess30)
        //
        //OPC ALARMS AND EVENTS
        //Categories of "OPCLabs.KitEventServer.2":
        //    .OpcAlarmsAndEvents10: True
        //    .OpcDataAccess10: False
        //    .OpcDataAccess20: False
        //    .OpcDataAccess30: False
        //    .ToString(): (OpcAlarmsAndEvents10)
    }
}
#endregion
