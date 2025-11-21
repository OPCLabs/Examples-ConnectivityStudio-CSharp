// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable ConvertClosureToMethodGroup
// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how data from parent node can be used in the read event handler.
// You can use any OPC UA client, including our Connectivity Explorer and OpcCmd utility, to connect to the server. 
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client, server and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.BaseLib.NodeSpace;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.NodeSpace;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UAServerDocExamples._UAServerNode
{
    class ParentNode
    {
        public static void Main1()
        {
            // Instantiate the server object.
            // By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
            var server = new EasyUAServer();

            // Create multiple folder nodes, each with a data variable in it. Distinguish the folders by their state, however
            // the data variables are constructed the same, and use the same read event handler.

            UAFolder myFolder1 = UAFolder.CreateIn(server.Objects, "MyFolder1").SetState(1);
            UADataVariable.CreateIn(myFolder1, "MyDataVariable").Read += MyDataVariableOnRead;

            UAFolder myFolder2 = UAFolder.CreateIn(server.Objects, "MyFolder2").SetState(2);
            UADataVariable.CreateIn(myFolder2, "MyDataVariable").Read += MyDataVariableOnRead;

            UAFolder myFolder3 = UAFolder.CreateIn(server.Objects, "MyFolder3").SetState(3);
            UADataVariable.CreateIn(myFolder3, "MyDataVariable").Read += MyDataVariableOnRead;

            UAFolder myFolder4 = UAFolder.CreateIn(server.Objects, "MyFolder4").SetState(4);
            UADataVariable.CreateIn(myFolder4, "MyDataVariable").Read += MyDataVariableOnRead;

            UAFolder myFolder5 = UAFolder.CreateIn(server.Objects, "MyFolder5").SetState(5);
            UADataVariable.CreateIn(myFolder5, "MyDataVariable").Read += MyDataVariableOnRead;

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

        /// <summary>
        /// Event handler for the read event on the data variable. 
        /// </summary>
        /// <param name="sender">The data variable object that sends the event.</param>
        /// <param name="e">Data for the variable read event.</param>
        static private void MyDataVariableOnRead(object sender, UADataVariableReadEventArgs e)
        {
            // Obtain the parent folder of the data variable that is being read.
            UAServerNode parentNode = e.DataVariable.ParentNode;

            // Obtain the state associated with the folder, where the data variable is located.
            // Use it as the offset for the random value, so that each data variable generates values in a unique range.
            int offset = (int)parentNode.State*100;

            // Generate a random value, indicate that the read has been handled, and return the generated value.
            e.HandleAndReturn(Random.Next(offset, offset + 100));
        }

        static private readonly Random Random = new Random();
    }
}
#endregion
