// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable CheckNamespace
// ReSharper disable AssignNullToNotNullAttribute
#region Example
// Shows how to open an OPC UA file stream for reading, and read its content using a text reader object.
// Note: Consider using a higher-level abstraction, OPC UA file provider, instead.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.IO;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.FileTransfer;
using OpcLabs.EasyOpc.UA.IO.Extensions;

namespace UADocExamples.FileTransfer._UAFileStream
{
    class ReadText
    {
        public static void Main1()
        {
            // Unified Automation .NET based demo server (UaNETServer/UaServerNET.exe)
            UAEndpointDescriptor endpointDescriptor = "opc.tcp://localhost:48030";

            // A node that represents an instance of OPC UA FileType object.
            UANodeDescriptor fileNodeDescriptor = "nsu=http://www.unifiedautomation.com/DemoServer/ ;s=Demo.Files.TextFile";
            
            // Instantiate the file transfer client object
            var fileTransferClient = new EasyUAFileTransferClient();

            try
            {
                // Get a stream reader object that corresponds to an OPC UA file.
                Console.WriteLine("Opening the file for reading...");

                // We know that the file contains text, so we read it using a stream reader. If the file content was
                // binary, you would process the stream according to the data format.
                using (StreamReader streamReader = fileTransferClient.OpenStreamReader(endpointDescriptor, fileNodeDescriptor))
                {
                    // The OPC UA stream reader object behaves like any other stream reader in .NET.

                    // Read in the text from the file and display it line by line.
                    Console.WriteLine();
                    Console.WriteLine("Reading text lines:");
                    int i = 0;
                    while (!streamReader.EndOfStream)
                    {
                        string line = streamReader.ReadLine();
                        Console.WriteLine($"[{i}] {line}");
                        i++;
                    }
                }
            }
            // OPC UA errors encountered during opening of a UA file stream and operations on such stream are transformed
            // to IOException-s.
            catch (IOException ioException)
            {
                Console.WriteLine("*** Failure: {0}", ioException.GetBaseException().Message);
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Finished...");
        }
    }
}
#endregion
