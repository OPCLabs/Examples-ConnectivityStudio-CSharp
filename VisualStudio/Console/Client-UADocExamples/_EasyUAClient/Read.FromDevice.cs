// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable LocalizableElement
#region Example
// This example shows how to read data from the device (data source) and display a value, timestamps, and status code.
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
    partial class Read
    {
        public static void FromDevice()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client object
            var client = new EasyUAClient();

            // Obtain attribute data. By default, the Value attribute of a node will be read.
            // The parameters specify reading from the device (data source), which may be slow but provides the very latest
            // data.
            UAAttributeData attributeData;
            try
            {
                attributeData = client.Read(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10853", 
                    UAReadParameters.FromDevice);
            }
            catch (UAException uaException)
            {
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
                return;
            }

            // Display results
            Console.WriteLine("Value: {0}", attributeData.Value);
            Console.WriteLine("ServerTimestamp: {0}", attributeData.ServerTimestamp);
            Console.WriteLine("SourceTimestamp: {0}", attributeData.SourceTimestamp);
            Console.WriteLine("StatusCode: {0}", attributeData.StatusCode);

            // Example output:
            //
            //Value: -2.230064E-31
            //ServerTimestamp: 11/6/2011 1:34:30 PM
            //SourceTimestamp: 11/6/2011 1:34:30 PM
            //StatusCode: Good
        }
    }
}
#endregion
