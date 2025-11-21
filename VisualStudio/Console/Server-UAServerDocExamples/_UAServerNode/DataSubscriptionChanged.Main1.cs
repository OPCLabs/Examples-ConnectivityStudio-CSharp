// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable ConvertClosureToMethodGroup
// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to implement own handling of data subscriptions.
// You can use any OPC UA client, including our Connectivity Explorer and OpcCmd utility, to connect to the server. 
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client, server and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.NodeSpace;
using Timer = System.Timers.Timer;

namespace UAServerDocExamples._UAServerNode
{
    class DataSubscriptionChanged
    {
        public static void Main1()
        {
            // Instantiate the server object.
            // By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
            var server = new EasyUAServer();

            // Create a read-only data variable.
            var random = new Random();
            var dataVariable = UADataVariable.CreateIn(server.Objects, "SubscribeToThisVariable")
                .ValueType<int>()
                .Writable(false);

            dataVariable.UseDataPolling = false;    // Recommended, but not strictly necessary.
            dataVariable.DataSubscriptionChanged += (sender, args) =>
            {
                switch (args.Action)
                {
                    case UADataSubscriptionChangedAction.Add:
                    {
                        // Obtain the sampling interval from the data subscription.
                        int samplingInterval = args.DataSubscription.SamplingInterval;
                        Console.WriteLine($"Data subscription added, sampling interval: {samplingInterval}");

                        // Create a timer that will provide the data variable with a new data. In a real server the activity
                        // may also come from other sources.
                        var timer = new Timer { AutoReset = true, Interval = samplingInterval };
                        args.DataSubscription.State = timer;

                        // Set the read attribute data of the data variable to a random value whenever the timer interval elapses.
                        timer.Elapsed += (s, e) => args.DataSubscription.OnNext(random.Next());

                        // Start the subscription timer.
                        timer.Start();
                        break;
                    }

                    case UADataSubscriptionChangedAction.Remove:
                    {
                        Console.WriteLine("Data subscription removed");

                        // Dispose of the subscription timer (stopping it too).
                        var timer = (Timer)args.DataSubscription.State;
                        timer.Dispose();
                        break;
                    }

                    case UADataSubscriptionChangedAction.Modify:
                    {
                        int samplingInterval = args.DataSubscription.SamplingInterval;
                        Console.WriteLine($"Data subscription modified, sampling interval: {samplingInterval}");

                        // Change the interval of the subscription timer.
                        var timer = (Timer)args.DataSubscription.State;
                        timer.Interval = samplingInterval;
                        break;
                    }
                }
                args.Handled = true;    // Do not forget to indicate that your code has handled the event.
            };

            // The read behavior of the data variable needs to be defined as well, separately from the data subscriptions.
            dataVariable.ReadValueFunction(() => random.Next());
            
            // Start the server.
            Console.WriteLine("The server is starting...");
            server.Start();

            Console.WriteLine("The server is started.");
            Console.WriteLine();

            // Let the user decide when to stop.
            Console.WriteLine("Press Enter to stop the server...");
            Console.ReadLine();

            // Stop the server.
            Console.WriteLine("The server is stopping...");
            server.Stop();

            Console.WriteLine("The server is stopped.");
        }
    }
}
#endregion
