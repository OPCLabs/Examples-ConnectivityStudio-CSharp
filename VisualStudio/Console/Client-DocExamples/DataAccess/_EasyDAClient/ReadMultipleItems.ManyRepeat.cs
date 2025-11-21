// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
// ReSharper disable CheckNamespace
#region Example
// This example repeatedly reads a large number of items.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.OperationModel;

namespace DocExamples.DataAccess._EasyDAClient
{
    partial class ReadMultipleItems
    {
        private const int RepeatCount = 10;
        private const int NumberOfItems = 1000;

        public static void ManyRepeat()
        {
            Console.WriteLine("Creating array of arguments...");
            var arguments = new DAReadItemArguments[NumberOfItems];
            for (int i = 0; i < NumberOfItems; i++)
            {
                int copy = (i / 100) + 1;
                int phase = i % 100;
                string itemId = FormattableString.Invariant($"Simulation.Incrementing.Copy_{copy}.Phase_{phase}");
                Console.WriteLine(itemId);

                var readItemArguments = new DAReadItemArguments("OPCLabs.KitServer.2", itemId);
                arguments[i] = readItemArguments;
            }

            // Instantiate the client object.
            var client = new EasyDAClient();

            for (int iRepeat = 1; iRepeat <= RepeatCount; iRepeat++)
            {
                Console.WriteLine("Reading items...");
                DAVtqResult[] vtqResults = client.ReadMultipleItems(arguments);

                int successCount = 0;
                foreach (DAVtqResult vtqResult in vtqResults)
                    if (vtqResult.Succeeded)
                        successCount++;
                Console.WriteLine($"Success count: {successCount}");
            }
        }
    }
}
#endregion
