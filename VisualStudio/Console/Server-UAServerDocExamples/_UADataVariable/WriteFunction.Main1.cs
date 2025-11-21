// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable ConvertClosureToMethodGroup
// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to use a function to define what happens with the attribute data when an OPC client writes to a
// data variable. This is an example of the push data consumption model.
// You can use any OPC UA client, including our Connectivity Explorer and OpcCmd utility, to connect to the server. 
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client, server and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.NodeSpace;

namespace UAServerDocExamples._UADataVariable
{
    partial class WriteFunction
    {
        public static void Main1()
        {
            // Instantiate the server object.
            // By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
            var server = new EasyUAServer();

            // Create a writable data variable and add a function that will be called when the data variable is written to.
            // The function returns a status code that indicates the outcome of the Write operation. We have chosen to only
            // allow "Good" and "Uncertain", non-negative values to be written to the variable.
            server.Add(new UADataVariable("WriteToThisVariable").WriteFunction<int>(
                data =>
                {
                    if (data.StatusCode.IsBad || (data.TypedValue < 0))
                    {
                        Console.WriteLine($"Attribute data rejected: {data}");
                        return UACodeBits.BadOutOfRange;
                    }
                    Console.WriteLine($"Attribute data written: {data}");
                    return null;    // "Good"
                }));

            // Start the server.
            Console.WriteLine("The server is starting...");
            server.Start();

            Console.WriteLine("The server is started.");
            Console.WriteLine("Any value written to the example data variable will be displayed on the console.");
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
