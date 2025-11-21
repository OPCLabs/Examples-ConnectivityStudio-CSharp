// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable InconsistentNaming
// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to write a value into a single node that is an array of Int32.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples._EasyUAClient
{
    partial class WriteValue
    {
        public static void ArrayOfInt32()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client object
            var client = new EasyUAClient();

            Console.WriteLine("Modifying value of a node...");
            try
            {
                var arrayValue = new Int32[] { 11111, 22222, 33333, 44444, 55555, 66666, 77777 };
                client.WriteValue(endpointDescriptor,
                    "nsu=http://test.org/UA/Data/ ;ns=2;i=10305",   // /Data.Static.Array.Int32Value
                    arrayValue);
            }
            catch (UAException uaException)
            {
                Console.WriteLine($"*** Failure: {uaException.GetBaseException().Message}");
                return;
            }

            Console.WriteLine("Finished.");
        }
    }
}
#endregion
