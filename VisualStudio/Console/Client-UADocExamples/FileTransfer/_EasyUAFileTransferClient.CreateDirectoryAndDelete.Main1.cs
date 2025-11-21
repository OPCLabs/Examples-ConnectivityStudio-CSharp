// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable CheckNamespace
// ReSharper disable AssignNullToNotNullAttribute
#region Example
// Shows how to create and delete OPC UA directories, using the file transfer client.
// Note: Consider using a higher-level abstraction, OPC UA file provider, instead.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.AddressSpace;
using OpcLabs.EasyOpc.UA.Extensions;
using OpcLabs.EasyOpc.UA.FileTransfer;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples.FileTransfer._EasyUAFileTransferClient
{
    class CreateDirectoryAndDelete
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

            // Create two directories, and one nested directory, and delete the first one.
            try
            {
                // The file system node is a root directory of the file system.
                Console.WriteLine("Getting file system...");
                UANodeDescriptor fileSystemNodeDescriptor = fileTransferClient.GetFileSystem(endpointDescriptor, objectDescriptor);

                string directoryName1 = "MyDirectory1-" + random.Next();
                Console.WriteLine($"Creating first directory, '{directoryName1}'...");
                UANodeId directoryNodeId1 = fileTransferClient.CreateDirectory(endpointDescriptor, fileSystemNodeDescriptor, directoryName1);
                Console.WriteLine($"Node Id of the first directory: {directoryNodeId1}");

                string directoryName2 = "MyDirectory2-" + random.Next();
                Console.WriteLine($"Creating second directory, '{directoryName2}'...");
                UANodeId directoryNodeId2 = fileTransferClient.CreateDirectory(endpointDescriptor, fileSystemNodeDescriptor, directoryName2);
                Console.WriteLine($"Node Id of the second directory: {directoryNodeId2}");

                string nestedDirectoryName = "MyDirectory3-" + random.Next();
                Console.WriteLine($"Creating nested directory, '{nestedDirectoryName}'...");
                // Note how directoryNodeId2 (a parent directory) is passed as the 2nd argument to the CreateDirectory method.
                UANodeId nestedDirectoryNodeId = fileTransferClient.CreateDirectory(endpointDescriptor, directoryNodeId2, nestedDirectoryName);
                Console.WriteLine($"Node Id of the nested directory: {nestedDirectoryNodeId}");

                // At this moment, the directory structure we have created looks like this:
                // * MyDirectory1
                // * MyDirectory2
                // * * MyDirectory3

                Console.WriteLine("Deleting the first directory...");
                fileTransferClient.DeleteDirectory(endpointDescriptor, fileSystemNodeDescriptor, directoryName1);
            }
            catch (UAException uaException)
            {
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
                return;
            }

            Console.WriteLine("Finished...");
        }
    }
}
#endregion
