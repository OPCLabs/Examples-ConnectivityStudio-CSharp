// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable PossibleNullReferenceException
// ReSharper disable ConvertIfStatementToConditionalTernaryExpression
#region Example
// This example shows how subscribe to changes of multiple items and display each change, identifying the different
// subscriptions by an integer.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Threading;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.OperationModel;

namespace DocExamples.DataAccess._EasyDAClient
{
    partial class SubscribeMultipleItems
    {
        public static void StateAsInteger()
        {
            // Instantiate the client object.
            using (var client = new EasyDAClient())
            {
                // Hook events
                client.ItemChanged += client_StateAsInteger_ItemChanged;

                Console.WriteLine("Subscribing...");
                int[] handleArray = client.SubscribeMultipleItems(new[]
                {
                    new DAItemGroupArguments("", "OPCLabs.KitServer.2", "Simulation.Random", 1000,
                        state: 1), // An integer we have chosen to identify the subscription
                    new DAItemGroupArguments("", "OPCLabs.KitServer.2", "Trends.Ramp (1 min)", 1000,
                        state: 2), // An integer we have chosen to identify the subscription
                    new DAItemGroupArguments("", "OPCLabs.KitServer.2", "Trends.Sine (1 min)", 1000,
                        state: 3), // An integer we have chosen to identify the subscription
                    new DAItemGroupArguments("", "OPCLabs.KitServer.2", "Simulation.Register_I4", 1000,
                        state: 4) // An integer we have chosen to identify the subscription
                });

                for (int i = 0; i < handleArray.Length; i++)
                    Console.WriteLine($"handleArray[{i}]: {handleArray[i]}");

                Console.WriteLine("Processing item changed events for 10 seconds...");
                Thread.Sleep(10 * 1000);

                Console.WriteLine("Unsubscribing...");
            }

            Console.WriteLine("Waiting for 5 seconds...");
            Thread.Sleep(5 * 1000);

            Console.WriteLine("Finished.");
        }

        // Item changed event handler
        static void client_StateAsInteger_ItemChanged(object sender, EasyDAItemChangedEventArgs eventArgs)
        {
            // Obtain the integer state we have passed in.
            var stateAsInteger = (int)eventArgs.Arguments.State;

            // Display the data
            if (eventArgs.Succeeded)
                Console.WriteLine($"{stateAsInteger}: {eventArgs.Vtq}");
            else
                Console.WriteLine($"{stateAsInteger} *** Failure: {eventArgs.ErrorMessageBrief}");
        }
    }
}
#endregion
