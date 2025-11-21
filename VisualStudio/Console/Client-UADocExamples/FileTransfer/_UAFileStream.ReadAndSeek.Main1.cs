// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable CheckNamespace
// ReSharper disable AssignNullToNotNullAttribute
#region Example
// Shows how to read different sections from an OPC UA file stream.
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
    class ReadAndSeek
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
                // Get a stream object that corresponds to an OPC UA file.
                Console.WriteLine("Opening the file for reading...");
                using (Stream stream = fileTransferClient.OpenStream(endpointDescriptor, fileNodeDescriptor))
                {
                    // The OPC UA file stream object behaves like any other stream in .NET.

                    Console.WriteLine("Reading first section of a stream...");
                    var buffer1 = new byte[16];
                    int bytesRead1 = stream.Read(buffer1, 0, buffer1.Length);
                    Console.WriteLine($"{bytesRead1} bytes read, buffer: {BitConverter.ToString(buffer1)}");

                    Console.WriteLine("Reading second section of a stream...");
                    var buffer2 = new byte[10];
                    int bytesRead2 = stream.Read(buffer2, 0, buffer2.Length);
                    Console.WriteLine($"{bytesRead2} bytes read, buffer: {BitConverter.ToString(buffer2)}");

                    Console.WriteLine("Seeking...");
                    stream.Seek(100, SeekOrigin.Begin);

                    Console.WriteLine("Reading third section of a stream...");
                    var buffer3 = new byte[20];
                    int bytesRead3 = stream.Read(buffer3, 0, buffer3.Length);
                    Console.WriteLine($"{bytesRead3} bytes read, buffer: {BitConverter.ToString(buffer3)}");
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
