// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable CheckNamespace
// ReSharper disable AssignNullToNotNullAttribute
#region Example
// Shows how to create and delete OPC UA files, using the file provider model.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.BaseLib.Extensions.FileProviders;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.Extensions;
using OpcLabs.EasyOpc.UA.FileTransfer;

namespace UADocExamples.FileProviders._WritableFileInfo
{
    class CreateAndDelete
    {
        public static void Main1()
        {
            // Unified Automation .NET based demo server (UaNETServer/UaServerNET.exe)
            var endpointDescriptor = new UAEndpointDescriptor("opc.tcp://localhost:48030")
                .WithUserNameIdentity("john", "master");

            // A node that represents an OPC UA file system (a root directory).
            UANodeDescriptor fileSystemNodeDescriptor = "nsu=http://www.unifiedautomation.com/DemoServer/ ;s=Demo.Files.FileSystem";

            // Create a random number generator - will be used for file/directory names.
            var random = new Random();
            
            // Instantiate the file transfer client object
            var fileTransferClient = new EasyUAFileTransferClient();

            Console.WriteLine("Getting writable file provider...");
            IWritableFileProvider writableFileProvider =
                fileTransferClient.GetWritableFileProvider(endpointDescriptor, fileSystemNodeDescriptor);
            // From this point onwards, the code is independent of the concrete realization of the file provider, and would
            // be identical e.g. for files in the physical file system, if the corresponding file provider was used.

            // Create two files, and delete the first one.
            try
            {
                string fileName1 = "MyFile1-" + random.Next();
                Console.WriteLine($"Creating first file, '{fileName1}'...");
                IWritableFileInfo writableFileInfo1 = writableFileProvider.GetWritableFileInfo(fileName1);
                writableFileInfo1.WriteAllBytes(Array.Empty<byte>());

                string fileName2 = "MyFile2-" + random.Next();
                Console.WriteLine($"Creating second file, '{fileName2}'...");
                IWritableFileInfo writableFileInfo2 = writableFileProvider.GetWritableFileInfo(fileName2);
                writableFileInfo2.WriteAllBytes(Array.Empty<byte>());

                Console.WriteLine("Deleting the first file...");
                writableFileInfo1.Delete();
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
