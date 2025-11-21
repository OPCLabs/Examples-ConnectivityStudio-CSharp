// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable PossibleNullReferenceException
// ReSharper disable ConvertIfStatementToConditionalTernaryExpression
#region Example
// This example shows how subscribe to changes of multiple items and display each change, identifying the different
// subscriptions by an object.
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
        class CustomObject
        {
            public CustomObject(string name)
            {
                Name = name;
            }

            public string Name { get; }
        }


        public static void StateAsObject()
        {
            // Instantiate the client object.
            using (var client = new EasyDAClient())
            {
                // Hook events
                client.ItemChanged += client_StateAsObject_ItemChanged;

                Console.WriteLine("Subscribing...");
                int[] handleArray = client.SubscribeMultipleItems(new[]
                {
                    new DAItemGroupArguments("", "OPCLabs.KitServer.2", "Simulation.Random", 1000,
                        new CustomObject("First")), // A custom object that corresponds to the subscription
                    new DAItemGroupArguments("", "OPCLabs.KitServer.2", "Trends.Ramp (1 min)", 1000,
                        new CustomObject("Second")), // A custom object that corresponds to the subscription
                    new DAItemGroupArguments("", "OPCLabs.KitServer.2", "Trends.Sine (1 min)", 1000,
                        new CustomObject("Third")), // A custom object that corresponds to the subscription
                    new DAItemGroupArguments("", "OPCLabs.KitServer.2", "Simulation.Register_I4", 1000,
                        new CustomObject("Fourth")) // A custom object that corresponds to the subscription
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
        static void client_StateAsObject_ItemChanged(object sender, EasyDAItemChangedEventArgs eventArgs)
        {
            // Obtain the custom object we have passed in.
            var stateAsObject = (CustomObject)eventArgs.Arguments.State;

            // Display the data
            if (eventArgs.Succeeded)
                Console.WriteLine($"{stateAsObject.Name}: {eventArgs.Vtq}");
            else
                Console.WriteLine($"{stateAsObject.Name} *** Failure: {eventArgs.ErrorMessageBrief}");
        }
    }
}
#endregion
