// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable ConvertClosureToMethodGroup
// ReSharper disable LocalizableElement
#region Example
// This example shows how to retrieve the attribute data in the pull data consumption model. In this model, the data that
// OPC clients write to the server is pulled and processed by your code when needed.
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

namespace UAServerDocExamples._UADataVariable
{
    class WriteAttributeData
    {
        public static void Main1()
        {
            // Instantiate the server object.
            // By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
            var server = new EasyUAServer();

            // Create a read-write data variable with an initial value.
            var dataVariable = UADataVariable.CreateIn(server.Objects, "WriteToThisVariable").ReadWriteValue(0);

            // Create a timer for pulling the data from OPC writes. In a real server the activity may also come from other
            // sources.
            var timer = new Timer
            {
                Interval = 1000,
                AutoReset = true,
            };

            // Periodically display the value of the data variable on the console.
            timer.Elapsed += (sender, args) => Console.Write($"  {dataVariable.WriteAttributeData.Value}");
            timer.Start();
            Console.WriteLine("Values of the example data variable are displayed on the console periodically.");

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

            // Stop the timer.
            timer.Stop();

            Console.WriteLine("The server is stopped.");
        }
    }
}
#endregion
