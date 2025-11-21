// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable InconsistentNaming
// ReSharper disable LocalizableElement
// ReSharper disable StringLiteralTypo
#region Example
// This example shows how to specify host name(s) for the server. This is useful when the server is running on a 
// computer that has multiple host names, and you want to make the server accessible under them.
// You can use any OPC UA client, including our Connectivity Explorer and OpcCmd utility, to connect to the server. 
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client, server and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.Application;
using OpcLabs.EasyOpc.UA.Application.Extensions;
using OpcLabs.EasyOpc.UA.NodeSpace;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UAServerDocExamples._EasyUAServer
{
    class HostNames
    {
        public static void Main1()
        {
            // Obtain the application interface.
            EasyUAApplication application = EasyUAApplication.Instance;

            // Remove the own application certificate pack. This assures that, when needed, the server will create a new one
            // with the parameters we want and specify.
            try
            {
                Console.WriteLine("Removing the own application certificate pack...");
                application.RemoveOwnCertificatePack();
                Console.WriteLine("The application certificate pack has been removed.");
            }
            catch (UAException uaException)
            {
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
            }

            // Instantiate the server object.
            // By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
            var server = new EasyUAServer();

            // Specify a host name for the server (and its application certificate).
            server.HostNames.Add("mycomputer.mycompany.example");

            //
            
            // Define a data variable providing random integers.
            var random = new Random();
            server.Add(new UADataVariable("MyDataVariable").ReadValueFunction(() => random.Next()));

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
