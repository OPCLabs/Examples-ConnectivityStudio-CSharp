// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to obtain all method nodes under a given node of the OPC-UA address space.
// For each node, it displays its browse name and node ID.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.AddressSpace;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples._EasyUAClient
{
    class BrowseMethods
    {
        public static void Overload2()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client object
            var client = new EasyUAClient();

            // Obtain methods under the specified node.
            UANodeElementCollection nodeElementCollection;
            try
            {
                nodeElementCollection = client.BrowseMethods(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10755");
            }
            catch (UAException uaException)
            {
                Console.WriteLine($"*** Failure: {uaException.GetBaseException().Message}");
                return;
            }

            // Display results
            foreach (UANodeElement nodeElement in nodeElementCollection)
                Console.WriteLine($"{nodeElement.BrowseName}: {nodeElement.NodeId}");
        }

        // Example output:
        //ScalarMethod1: nsu = http://test.org/UA/Data/ ;ns=2;i=10756
        //ScalarMethod2: nsu = http://test.org/UA/Data/ ;ns=2;i=10759
        //ScalarMethod3: nsu = http://test.org/UA/Data/ ;ns=2;i=10762
        //ArrayMethod1: nsu = http://test.org/UA/Data/ ;ns=2;i=10765
        //ArrayMethod2: nsu = http://test.org/UA/Data/ ;ns=2;i=10768
        //ArrayMethod3: nsu = http://test.org/UA/Data/ ;ns=2;i=10771
        //UserScalarMethod1: nsu = http://test.org/UA/Data/ ;ns=2;i=10774
        //UserScalarMethod2: nsu = http://test.org/UA/Data/ ;ns=2;i=10777
        //UserArrayMethod1: nsu = http://test.org/UA/Data/ ;ns=2;i=10780
        //UserArrayMethod2: nsu = http://test.org/UA/Data/ ;ns=2;i=10783
    }
}
#endregion
