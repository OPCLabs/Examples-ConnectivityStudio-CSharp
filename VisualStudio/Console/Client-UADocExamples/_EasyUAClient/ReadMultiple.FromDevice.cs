// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to read data value of 3 nodes at once, from the device (data source).
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
    partial class ReadMultiple
    {
        public static void FromDevice()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client object
            var client = new EasyUAClient();

            // Obtain attribute data. By default, the Value attributes of the nodes will be read.
            // The parameters specify reading from the device (data source), which may be slow but provides the very latest
            // data.
            UAAttributeDataResult[] attributeDataResultArray = client.ReadMultiple(new[]
                {
                    new UAReadArguments(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10845", 
                        UAReadParameters.FromDevice),
                    new UAReadArguments(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10853", 
                        UAReadParameters.FromDevice),
                    new UAReadArguments(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10855", 
                        UAReadParameters.FromDevice)
                });

            // Display results
            foreach (UAAttributeDataResult attributeDataResult in attributeDataResultArray)
            {
                if (attributeDataResult.Succeeded)
                    Console.WriteLine("AttributeData: {0}", attributeDataResult.AttributeData);
                else
                    Console.WriteLine("*** Failure: {0}", attributeDataResult.ErrorMessageBrief);
            }
        }

        // Example output:
        //
        //AttributeData: 51 {System.Int16} @11/6/2011 1:49:19 PM @11/6/2011 1:49:19 PM; Good
        //AttributeData: -1993984 {System.Single} @11/6/2011 1:49:19 PM @11/6/2011 1:49:19 PM; Good
        //AttributeData: Yellow% Dragon Cat) White Blue Dog# Green Banana- {System.String} @11/6/2011 1:49:19 PM @11/6/2011 1:49:19 PM; Good            
    }
}
#endregion
