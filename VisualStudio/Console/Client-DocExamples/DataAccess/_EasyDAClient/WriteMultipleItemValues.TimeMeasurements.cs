// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
#region Example
// This example measures the time needed to write 2000 item values all at once, and in 20 groups by 100 items.
// Note that the writes will currently all fail, as we do not have the appropriate writeable items available.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Diagnostics;
using System.Threading;
using OpcLabs.BaseLib.OperationModel;
using OpcLabs.EasyOpc.DataAccess.OperationModel;
using OpcLabs.EasyOpc.DataAccess;

namespace DocExamples.DataAccess._EasyDAClient
{
    partial class WriteMultipleItemValues
    {
        const int NumberOfGroups = 100;
        const int ItemsInGroup = 20;
        private const int TotalItems = NumberOfGroups * ItemsInGroup;

        // Main method
        public static void TimeMeasurements()
        {
            // Make the measurements 10 times; note that first time the times might be longer.
            for (int i = 1; i <= 10; i++)
            {
                // Pause - we do not want the component to use the values it has in memory
                Thread.Sleep(2 * 1000);

                // Write all item values at once, and measure the time
                var stopwatch1 = new Stopwatch();
                stopwatch1.Start();
                WriteAllAtOnce();
                stopwatch1.Stop();
                Console.WriteLine("WriteAllAtOnce has taken (milliseconds): {0}", stopwatch1.ElapsedMilliseconds);

                // Pause - we do not want the component to use the values it has in memory
                Thread.Sleep(2 * 1000);

                // Write item values in groups, and measure the time
                var stopwatch2 = new Stopwatch();
                stopwatch2.Start();
                WriteInGroups();
                stopwatch2.Stop();
                Console.WriteLine("WriteInGroups has taken (milliseconds): {0}", stopwatch2.ElapsedMilliseconds);
            }
        }

        // Write all item values at once
        private static void WriteAllAtOnce()
        {
            // Instantiate the client object.
            var client = new EasyDAClient();

            // Create an array of arguments for all items
            var arguments = new DAItemValueArguments[TotalItems];
            int index = 0;
            for (int iLoop = 0; iLoop < NumberOfGroups; iLoop++)
                for (int iItem = 0; iItem < ItemsInGroup; iItem++)
                    arguments[index++] = new DAItemValueArguments(
                        "OPCLabs.KitServer.2",
                        String.Format("Simulation.Incrementing.Copy_{0}.Phase_{1}", iLoop + 1, iItem + 1),
                        0);

            // Perform the OPC write
            OperationResult[] operationResults = client.WriteMultipleItemValues(arguments);

            // Count successful results
            int successCount = 0;
            for (int iItem = 0; iItem < TotalItems; iItem++)
            {
                Debug.Assert(operationResults[iItem] != null);
                if (operationResults[iItem].Succeeded)
                    successCount++;
            }

            if (successCount != TotalItems)
                Console.WriteLine("Warning: There were some failures, success count is {0}", successCount);
        }

        // Write item values in groups
        private static void WriteInGroups()
        {
            var client = new EasyDAClient();

            int successCount = 0;
            for (int iLoop = 0; iLoop < NumberOfGroups; iLoop++)
            {
                // Create an array of item arguments for items in one group
                var arguments = new DAItemValueArguments[ItemsInGroup];
                for (int iItem = 0; iItem < ItemsInGroup; iItem++)
                    arguments[iItem] = new DAItemValueArguments(
                        "OPCLabs.KitServer.2",
                        String.Format("Simulation.Incrementing.Copy_{0}.Phase_{1}", iLoop + 1, iItem + 1),
                        0);

                // Perform the OPC write
                OperationResult[] operationResults = client.WriteMultipleItemValues(arguments);

                // Count successful results (totalling to previous value)
                for (int iItem = 0; iItem < ItemsInGroup; iItem++)
                {
                    Debug.Assert(operationResults[iItem] != null);
                    if (operationResults[iItem].Succeeded) successCount++;
                }
            }

            if (successCount != TotalItems)
                Console.WriteLine("Warning: There were some failures, success count is {0}", successCount);
        }
    }
}
#endregion
