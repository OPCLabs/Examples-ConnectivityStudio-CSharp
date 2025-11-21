// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
// ReSharper disable UnusedParameter.Local
#region Example
// This example shows how to handle conversion errors on the server level.
// You can use any OPC UA client, including our Connectivity Explorer and OpcCmd utility, to connect to the server. 
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client, server and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.NodeSpace;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UAServerDocExamples._EasyUAServer
{
    class ConversionError
    {
        public static void Main1()
        {
            // Instantiate the server object.
            // By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
            var server = new EasyUAServer();

            // Define a data variable of type Byte.
            UADataVariable dataVariable = UADataVariable.CreateIn(server.Objects, "MyDataVariable").ValueType<byte>();

            // Add a Read handler that returns random values between 0 and 511. Those greater than 255 will cause conversion
            // errors.
            var random = new Random();
            dataVariable.Read += (sender, args) => args.HandleAndReturn(random.Next(0, 512));

            // Hook events to the server.
            // Note that the conversion error event can also be handled on the data variable or folder level, if that's what
            // you requirements call for.
            server.ConversionError += ServerOnConversionError;

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

        // Event handler for the ConversionError event. It simply prints out the event.
        static private void ServerOnConversionError(object sender, UADataVariableConversionErrorEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine($"*** {e}");

            // Following are some useful properties in the event notification:
            //   e.DataVariable
            //   e.Action
            //   e.ServiceResult

            switch (e.Action)
            {
                case UADataVariableConversionAction.Read:
                    Console.WriteLine("The conversion error occurred during a Read operation.");
                    break;
                case UADataVariableConversionAction.Write:
                    Console.WriteLine("The conversion error occurred during a Write operation.");
                    break;
                case UADataVariableConversionAction.Update:
                    Console.WriteLine("The conversion error occurred during an Update operation.");
                    break;
            }

            Console.WriteLine($"It occured on the data variable: {e.DataVariable}.");
            Console.WriteLine($"The service result was: {e.ServiceResult.Message}");
        }
    }
}
#endregion
