// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable CheckNamespace
// ReSharper disable AssignNullToNotNullAttribute
#region Example
// Shows how to read different sections from an OPC UA file, using the file provider model.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.IO;
using OpcLabs.BaseLib.Extensions.FileProviders;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.FileTransfer;

namespace UADocExamples.FileProviders._FileInfo2
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

            Console.WriteLine("Getting file info...");
            IFileInfo2 fileInfo2 = fileTransferClient.GetFileInfo2(endpointDescriptor, fileNodeDescriptor);
            // From this point onwards, the code is independent of the concrete realization of the file provider, and would
            // be identical e.g. for files in the physical file system, if the corresponding file provider was used.

            // Open the file, read two separate sections of it, and close it.
            try
            {
                Console.WriteLine("Opening file...");
                using (Stream stream = fileInfo2.CreateReadStream())
                {
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
            // Methods in the file provider model throw IOException and other exceptions, but not UAException.
            catch (Exception exception)
            {
                Console.WriteLine($"*** Failure: {exception.GetBaseException().Message}");
                return;
            }

            Console.WriteLine("Finished...");
        }
    }
}
#endregion
