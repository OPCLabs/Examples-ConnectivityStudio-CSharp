// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable CheckNamespace
// ReSharper disable AssignNullToNotNullAttribute
#region Example
// Shows how to browse for OPC UA files, using the file transfer client.
// Note: Consider using a higher-level abstraction, OPC UA file provider, instead.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Collections.Generic;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.Extensions;
using OpcLabs.EasyOpc.UA.FileTransfer;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples.FileTransfer._EasyUAFileTransferClient
{
    class BrowseFiles
    {
        public static void Main1()
        {
            // Unified Automation .NET based demo server (UaNETServer/UaServerNET.exe)
            var endpointDescriptor = new UAEndpointDescriptor("opc.tcp://localhost:48030")
                .WithUserNameIdentity("john", "master");

            // An object that aggregates an OPC UA file system.
            UANodeDescriptor objectDescriptor = "nsu=http://www.unifiedautomation.com/DemoServer/ ;s=Demo.Files";

            // Create a random number generator - will be used for file/directory names.
            var random = new Random();
            
            // Instantiate the file transfer client object
            var fileTransferClient = new EasyUAFileTransferClient();

            // Create two files, and then browse the directory that contains them.
            Dictionary<string, UANodeDescriptor> fileNodeDescriptorDictionary;
            try
            {
                // The file system node is a root directory of the file system.
                Console.WriteLine("Getting file system...");
                UANodeDescriptor fileSystemNodeDescriptor = fileTransferClient.GetFileSystem(endpointDescriptor, objectDescriptor);

                string fileName1 = "MyFile1-" + random.Next();
                Console.WriteLine($"Creating first file, '{fileName1}'...");
                fileTransferClient.CreateFile(endpointDescriptor, fileSystemNodeDescriptor, fileName1);

                string fileName2 = "MyFile2-" + random.Next();
                Console.WriteLine($"Creating second file, '{fileName2}'...");
                fileTransferClient.CreateFile(endpointDescriptor, fileSystemNodeDescriptor, fileName2);

                Console.WriteLine("Browsing for files...");
                fileNodeDescriptorDictionary = fileTransferClient.BrowseFiles(endpointDescriptor, fileSystemNodeDescriptor);
                // If you want browse for directories, use the BrowseDirectories method instead.
            }
            catch (UAException uaException)
            {
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
                return;
            }

            // Display result
            Console.WriteLine();
            foreach (KeyValuePair<string, UANodeDescriptor> pair in fileNodeDescriptorDictionary)
                Console.WriteLine(pair);
            
            Console.WriteLine();
            Console.WriteLine("Finished...");
        }
    }
}
#endregion
