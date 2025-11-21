// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
using System;
using System.Collections.Generic;
using OpcLabs.BaseLib.Console;

namespace ReactiveDocExamples
{
    class Program
    {
        static void Main()
        {
            var actionArray = new Action[] {
                // ReSharper disable RedundantCommaInArrayInitializer

                _AENotificationObservable.Subscribe.Main1,
                
                _DAItemChangedObservable.Subscribe.Main1,
                _DAItemChangedObservable.Subscribe.PercentDeadband,
                _DAWriteItemValueObserver.OnNext.Main1,
                _DAClientReactive.Composition.Pipeline,

                _UADataChangeNotificationObservable.Subscribe.AbsoluteDeadband,
                _UADataChangeNotificationObservable.Subscribe.DataChangeTrigger,
                _UADataChangeNotificationObservable.Subscribe.IndexRangeList,
                _UADataChangeNotificationObservable.Subscribe.Main1,
                _UADataChangeNotificationObservable.Subscribe.PercentDeadband,
                _UADataChangeNotificationObservable.Subscribe.Timeouts,

                _UAClientReactive.Composition.Pipeline,

                _UAWriteValueObserver.OnNext.Main1,

                // ReSharper restore RedundantCommaInArrayInitializer
            };

            var actionList = new List<Action>(actionArray);
            bool cont;
            do
            {
                Console.WriteLine();
                cont = ConsoleDialog.SelectAndPerformAction("Select action to perform", "Return", actionList);
                if (cont)
                {
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                }
            }
            while (cont);
        }
    }
}
