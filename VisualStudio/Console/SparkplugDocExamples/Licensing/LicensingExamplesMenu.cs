// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder

using System;
using System.Collections.Generic;
using OpcLabs.BaseLib.Console;

namespace SparkplugDocExamples.Licensing
{
    static class LicensingExamplesMenu
    {
        public static void Main1()
        {
            var actionArray = new Action[] {
                _LicensingManagement.RegisterManagedResourceWithExistenceCheck,
                LicenseInfo.AllFields,
                LicenseInfo.SerialNumber,
            };

            var actionList = new List<Action>(actionArray);

            do
            {
                Console.WriteLine();
                if (!ConsoleDialog.SelectAndPerformAction("Select action to perform", "Return", actionList))
                    break;

                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
            while (true);
        }
    }
}
