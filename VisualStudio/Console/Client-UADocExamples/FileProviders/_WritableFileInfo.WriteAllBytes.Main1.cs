// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable CheckNamespace
// ReSharper disable AssignNullToNotNullAttribute
#region Example
// Shows how to write the full contents of an OPC UA file at once, using the file provider model.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Text;
using OpcLabs.BaseLib.Extensions.FileProviders;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.FileTransfer;

namespace UADocExamples.FileProviders._WritableFileInfo
{
    class WriteAllBytes
    {
        public static void Main1()
        {
            // Unified Automation .NET based demo server (UaNETServer/UaServerNET.exe)
            UAEndpointDescriptor endpointDescriptor = "opc.tcp://localhost:48030";

            // A node that represents an instance of OPC UA FileType object.
            UANodeDescriptor fileNodeDescriptor = "nsu=http://www.unifiedautomation.com/DemoServer/ ;s=Demo.Files.TextFile";
            
            // Instantiate the file transfer client object
            var fileTransferClient = new EasyUAFileTransferClient();

            Console.WriteLine("Getting writable file info...");
            IWritableFileInfo writableFileInfo = fileTransferClient.GetWritableFileInfo(endpointDescriptor, fileNodeDescriptor);
            // From this point onwards, the code is independent of the concrete realization of the file provider, and would
            // be identical e.g. for files in the physical file system, if the corresponding file provider was used.

            // Write all contents into a specified file node.
            byte[] bytes = Encoding.UTF8.GetBytes("TEXT FROM FILE TRANSFER CLIENT EXAMPLE. Demonstrates writing the whole contents of a file at once.");
            try
            {
                Console.WriteLine("Writing the whole file...");
                writableFileInfo.WriteAllBytes(bytes);

                // Due to an issue in the server, the file might not be readable now, without server restart.
                Console.WriteLine("Reading the data back...");
                byte[] data = writableFileInfo.ReadAllBytes();
                Console.WriteLine(Encoding.UTF8.GetString(data));
            }
            // Methods in the file provider model throw IOException and other exceptions, but not UAException.
            catch (Exception exception)
            {
                Console.WriteLine($"*** Failure: {exception.GetBaseException().Message}");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Finished...");
        }
    }
}
#endregion
