// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
#region Example
// This example measures the time needed to read 2000 items all at once, and in 20 groups by 100 items.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Diagnostics;
using System.Threading;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.OperationModel;

namespace DocExamples.DataAccess._EasyDAClient
{
    partial class ReadMultipleItems
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

                // Read all items at once, and measure the time
                var stopwatch1 = new Stopwatch();
                stopwatch1.Start();
                ReadAllAtOnce();
                stopwatch1.Stop();
                Console.WriteLine("ReadAllAtOnce has taken (milliseconds): {0}", stopwatch1.ElapsedMilliseconds);

                // Pause - we do not want the component to use the values it has in memory
                Thread.Sleep(2 * 1000);

                // Read items in groups, and measure the time
                var stopwatch2 = new Stopwatch();
                stopwatch2.Start();
                ReadInGroups();
                stopwatch2.Stop();
                Console.WriteLine("ReadInGroups has taken (milliseconds): {0}", stopwatch2.ElapsedMilliseconds);
            }

            // Example output (measured with Version 5.20, Release configuration):
            /*
                ReadAllAtOnce has taken (milliseconds): 3432
                ReadInGroups has taken (milliseconds): 1563
                ReadAllAtOnce has taken (milliseconds): 539
                ReadInGroups has taken (milliseconds): 1625
                ReadAllAtOnce has taken (milliseconds): 579
                ReadInGroups has taken (milliseconds): 1594
                ReadAllAtOnce has taken (milliseconds): 638
                ReadInGroups has taken (milliseconds): 1610   
                ...
            */

            // Note that Version 5.12 and earlier were yielding much larger penalty to repeated reads.
            // Example output (measured with Version 5.12, Release configuration):
            /*
                ReadAllAtOnce has taken (milliseconds): 4241
                ReadInGroups has taken (milliseconds): 8094
                ReadAllAtOnce has taken (milliseconds): 269
                ReadInGroups has taken (milliseconds): 7813
                ReadAllAtOnce has taken (milliseconds): 285
                ReadInGroups has taken (milliseconds): 7813
                ReadAllAtOnce has taken (milliseconds): 283
                ReadInGroups has taken (milliseconds): 7844                    
                ...
            */
        }

        // Read all items at once
        private static void ReadAllAtOnce()
        {
            // Instantiate the client object.
            var client = new EasyDAClient();

            // Create an array of item descriptors for all items
            var itemDescriptors = new DAItemDescriptor[TotalItems];
            int index = 0;
            for (int iLoop = 0; iLoop < NumberOfGroups; iLoop++)
                for (int iItem = 0; iItem < ItemsInGroup; iItem++)
                    itemDescriptors[index++] = new DAItemDescriptor(
                        String.Format("Simulation.Incrementing.Copy_{0}.Phase_{1}", iLoop + 1, iItem + 1));

            // Perform the OPC read
            DAVtqResult[] vtqResults = client.ReadMultipleItems("OPCLabs.KitServer.2", itemDescriptors);

            // Count successful results
            int successCount = 0;
            for (int iItem = 0; iItem < TotalItems; iItem++)
            {
                Debug.Assert(vtqResults[iItem] != null);
                if (vtqResults[iItem].Succeeded)
                    successCount++;
            }

            if (successCount != TotalItems)
                Console.WriteLine("Warning: There were some failures, success count is {0}", successCount);
        }

        // Read items in groups
        private static void ReadInGroups()
        {
            var client = new EasyDAClient();

            int successCount = 0;
            for (int iLoop = 0; iLoop < NumberOfGroups; iLoop++)
            {
                // Create an array of item descriptors for items in one group
                var itemDescriptors = new DAItemDescriptor[ItemsInGroup];
                for (int iItem = 0; iItem < ItemsInGroup; iItem++)
                    itemDescriptors[iItem] = new DAItemDescriptor(
                        String.Format("Simulation.Incrementing.Copy_{0}.Phase_{1}", iLoop + 1, iItem + 1));

                // Perform the OPC read
                DAVtqResult[] vtqResults = client.ReadMultipleItems("OPCLabs.KitServer.2", itemDescriptors);

                // Count successful results (totalling to previous value)
                for (int iItem = 0; iItem < ItemsInGroup; iItem++)
                {
                    Debug.Assert(vtqResults[iItem] != null);
                    if (vtqResults[iItem].Succeeded) successCount++;
                }
            }

            if (successCount != TotalItems)
                Console.WriteLine("Warning: There were some failures, success count is {0}", successCount);
        }
    }
}
#endregion
