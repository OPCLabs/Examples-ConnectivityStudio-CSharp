// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable PossibleNullReferenceException
#region Example
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client, server and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA.NodeSpace;

namespace UAServerDemoLibrary
{
    static public class ConsoleNodes
    {
        /// <summary>
        /// Adds nodes that allow interaction with the console.
        /// </summary>
        /// <param name="parentFolder">The folder to which to add the nodes.</param>
        /// <remarks>
        /// <para>
        /// The Console nodes allow OPC UA clients to display data on the OPC UA server's console (if it has such device).
        /// They are included mainly for the fun of it, to demonstrate the fact that any actions can be tied to the OPC write
        /// operations. Real OPC servers will not do this.</para>
        /// </remarks>
        static public void AddToParent(UAFolder parentFolder)
        {
            // Create the Console folder.
            UAFolder consoleFolder = new UAFolder("Console")
            {
                // The Write data variable writes the value to the console.
                new UADataVariable("Write")
                    .Readable(false)
                    .WriteValueAction((string s) => Console.Write(s)),
                // The WriteLine data variable writes the value to the console and appends a new line.
                new UADataVariable("WriteLine")
                    .Readable(false)
                    .WriteValueAction((string s) => Console.WriteLine(s))
            };
            parentFolder.Add(consoleFolder);
        }
    }
}
#endregion
