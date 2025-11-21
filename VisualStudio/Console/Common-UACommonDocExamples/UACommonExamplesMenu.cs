// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable LocalizableElement

using System;
using System.Collections.Generic;
using OpcLabs.BaseLib.Console;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.Engine;

namespace UACommonDocExamples
{
    static class UACommonExamplesMenu
    {
        public static void Main1()
        {
            var actionArray = new Action[] {
                // ReSharper disable RedundantCommaInArrayInitializer
                _CertificateGenerationParameters.ValidityPeriodInMonths.Main1,

                _UAApplicationManifest.InstanceOwnStorePath.PlatformSpecific,
                _UAApplicationManifest.ApplicationName.Main1,

                _UABrowsePathParser.Parse.Main1,
                _UABrowsePathParser.ParseRelative.Main1,
                _UABrowsePathParser.TryParse.Main1,
                _UABrowsePathParser.TryParseRelative.Main1,

                _UAIndexRangeList.Usage.ReadValue,
                _UAIndexRangeList.Usage.Subscribe,

                _UANodeId._Construction.Main1,

                _UAStatusCode.ToString.Main1,
                // ReSharper restore RedundantCommaInArrayInitializer
            };

            var actionList = new List<Action>(actionArray);

            var originalSharedParameters = (EasyUAClientSharedParameters)EasyUAClient.SharedParameters.Clone();
            do
            {
                Console.WriteLine();
                if (!ConsoleDialog.SelectAndPerformAction("Select action to perform", "Return", actionList))
                    break;

                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();

                if (EasyUAClient.SharedParameters != originalSharedParameters)
                {
                    using (ConsoleUtilities.WithForegroundColor(ConsoleColor.Yellow))
                        Console.WriteLine(
                            "This example has changed some global parameters that can influence how other examples work. " +
                            "For this reason, the application will now exit. Start it again to continue.");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
            }
            while (true);
        }
    }
}
