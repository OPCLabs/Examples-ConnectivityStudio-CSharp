// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable CheckNamespace
// ReSharper disable AssignNullToNotNullAttribute
#region Example
// Shows how to write the full contents of an OPC UA file at once, using the file transfer client.
// Note: Consider using a higher-level abstraction, OPC UA file provider, instead.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.IO;
using System.Text;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.FileTransfer;
using OpcLabs.EasyOpc.UA.IO.Extensions;

namespace UADocExamples.FileTransfer._EasyUAFileTransferClient
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

            // Write all contents into a specified file node.
            byte[] bytes = Encoding.UTF8.GetBytes("TEXT FROM FILE TRANSFER CLIENT EXAMPLE. Demonstrates writing the whole contents of a file at once.");
            try
            {
                Console.WriteLine("Writing the whole file...");
                fileTransferClient.WriteAllBytes(endpointDescriptor, fileNodeDescriptor, bytes);

                // Due to an issue in the server, the file might not be readable now, without server restart.
                //Console.WriteLine("Reading the data back...");
                //byte[] data = fileTransferClient.ReadAllFileBytes(endpointDescriptor, fileNodeDescriptor);
                //Console.WriteLine(Encoding.UTF8.GetString(data));
            }
            // Beware that WriteAllFileBytes throws IOException and not UAException.
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
