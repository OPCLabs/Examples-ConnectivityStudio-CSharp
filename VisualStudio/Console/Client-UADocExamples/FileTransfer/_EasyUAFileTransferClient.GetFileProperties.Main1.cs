// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable CheckNamespace
// ReSharper disable AssignNullToNotNullAttribute
#region Example
// Shows how to get OPC UA file properties (such as its size or writable status), using the file transfer client.
// Note: Consider using a higher-level abstraction, OPC UA file provider, instead.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.FileTransfer;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples.FileTransfer._EasyUAFileTransferClient
{
    class GetFileProperties
    {
        public static void Main1()
        {
            // Unified Automation .NET based demo server (UaNETServer/UaServerNET.exe)
            UAEndpointDescriptor endpointDescriptor = "opc.tcp://localhost:48030";

            // A node that represents an instance of OPC UA FileType object.
            UANodeDescriptor fileNodeDescriptor = "nsu=http://www.unifiedautomation.com/DemoServer/ ;s=Demo.Files.TextFile";
            
            // Instantiate the file transfer client object
            var fileTransferClient = new EasyUAFileTransferClient();

            // Get properties of a specified file.
            UAFileProperties fileProperties;
            Console.WriteLine("Getting file properties...");
            try
            {
                fileProperties = fileTransferClient.GetFileProperties(endpointDescriptor, fileNodeDescriptor);
            }
            catch (UAException uaException)
            {
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
                return;
            }

            // Display result
            Console.WriteLine();
            Console.WriteLine($"MimeType: {fileProperties.MimeType}");
            Console.WriteLine($"OpenCount: {fileProperties.OpenCount}");
            Console.WriteLine($"Size: {fileProperties.Size}");
            Console.WriteLine($"UserWritable: {fileProperties.UserWritable}");
            Console.WriteLine($"Writable: {fileProperties.Writable}");
            Console.WriteLine($"Timestamp: {fileProperties.Timestamp}");

            Console.WriteLine();
            Console.WriteLine("Finished...");
        }
    }
}
#endregion
