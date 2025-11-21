// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder

using System;
using System.Collections.Generic;
using OpcLabs.BaseLib.Console;
using SparkplugDocExamples.Consumer;
using SparkplugDocExamples.EdgeNode;
using SparkplugDocExamples.Licensing;

namespace SparkplugDocExamples
{
    static class SparkplugExamplesMenu
    {
        public static void Main1()
        {
            Action action;
            do
            {
                Console.WriteLine();
                action = ConsoleDialog.SelectItem("Select example group", "Return", new Dictionary<Action, string>
                {
                    {ConsumerExamplesMenu.Main1, "Consumer"},
                    {EdgeNodeExamplesMenu.Main1, "EdgeNode"},
                    {LicensingExamplesMenu.Main1, "Licensing"},
                });
                action?.Invoke();
            }
            while (!(action is null));
        }
    }
}
