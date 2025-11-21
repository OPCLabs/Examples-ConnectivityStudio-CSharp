// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable InconsistentNaming
// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to read a value from a single node that is an array of UInt16.
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
    partial class ReadValue
    {
        public static void ArrayOfUInt16()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client object
            var client = new EasyUAClient();

            Console.WriteLine("Obtaining value of a node...");
            int[] value;
            try
            {
                // UInt16 is returned as Int32, because UInt16 is not a CLS-compliant type (and is not supported in VB.NET).
                value = (int[])client.ReadValue(endpointDescriptor,
                    "nsu=http://test.org/UA/Data/ ;ns=2;i=10932");   // /Data.Dynamic.Array.UInt16Value
            }
            catch (UAException uaException)
            {
                Console.WriteLine($"*** Failure: {uaException.GetBaseException().Message}");
                return;
            }

            if (!(value is null))
            {
                Console.WriteLine(value[0]);
                Console.WriteLine(value[1]);
                Console.WriteLine(value[2]);
            }

            Console.WriteLine("Finished.");
        }
    }
}
#endregion
