// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
// ReSharper disable LocalizableElement
#region Example
// This example shows how to wait on an item until a value with "good" status severity becomes available.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.Extensions;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples._EasyUAClientExtension
{
    class WaitForValue
    {
        public static void Main1()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client object
            var client = new EasyUAClient();

            Console.WriteLine("Waiting until an item value with \"good\" status severity becomes available...");
            object value;
            try
            {
                value = client.WaitForValue(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10221",
                    monitoringParameters: 100,  // this is the sampling rate
                    millisecondsTimeout: 60*1000);
            }
            catch (UAException uaException)
            {
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
                return;
            }

            // Display the obtained value.
            Console.WriteLine("value: {0}", value);
        }
    }
}
#endregion
