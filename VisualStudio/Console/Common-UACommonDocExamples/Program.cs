// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

using System;
using System.Collections.Generic;
using OpcLabs.BaseLib.Console;

namespace UACommonDocExamples
{
    class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Action action;
            do
            {
                Console.WriteLine();
                action = ConsoleDialog.SelectItem("Select example group", "Return", new Dictionary<Action, string>
                {
                    {UACommonExamplesMenu.Main1, "Main"},
                    {Application.ApplicationExamplesMenu.Main1, "Application"},
                });
                action?.Invoke();
            }
            while (!(action is null));
        }
    }
}
