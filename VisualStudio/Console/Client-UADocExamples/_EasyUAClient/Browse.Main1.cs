// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable LocalizableElement
#region Example
// This example shows how to obtain nodes under a given node of the OPC-UA address space. 
// For each node, it displays its browse name and node ID.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Diagnostics;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.AddressSpace;
using OpcLabs.EasyOpc.UA.Navigation.Parsing;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples._EasyUAClient
{
    partial class Browse
    {
        public static void Main1()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            var browsePathParser = new UABrowsePathParser("http://test.org/UA/Data/");
            UANodeDescriptor nodeDescriptor = browsePathParser.Parse("[ObjectsFolder]/Data/Static/UserScalar");

            // Instantiate the client object
            var client = new EasyUAClient();

            // perform the operation
            UANodeElementCollection nodeElementCollection;
            try
            {
                nodeElementCollection = client.Browse(
                    endpointDescriptor, 
                    nodeDescriptor, 
                    UABrowseParameters.AllForwardReferences);
            }
            catch (UAException uaException)
            {
                Console.WriteLine($"*** Failure: {uaException.GetBaseException().Message}");
                return;
            }

            // Display results
            foreach (UANodeElement nodeElement in nodeElementCollection)
            {
                Debug.Assert(!(nodeElement is null));
                Console.WriteLine($"{nodeElement.BrowseName}: {nodeElement.NodeId}");
            }
        }

        // Example output:
        //
        //BooleanValue: nsu=http://test.org/UA/Data/ ;ns=2;i=10384
        //SByteValue: nsu=http://test.org/UA/Data/ ;ns=2;i=10385
        //ByteValue: nsu=http://test.org/UA/Data/ ;ns=2;i=10386
        //Int16Value: nsu=http://test.org/UA/Data/ ;ns=2;i=10387
        //UInt16Value: nsu=http://test.org/UA/Data/ ;ns=2;i=10388
        //Int32Value: nsu=http://test.org/UA/Data/ ;ns=2;i=10389
        //UInt32Value: nsu=http://test.org/UA/Data/ ;ns=2;i=10390
        //Int64Value: nsu=http://test.org/UA/Data/ ;ns=2;i=10391
        //UInt64Value: nsu=http://test.org/UA/Data/ ;ns=2;i=10392
        //FloatValue: nsu=http://test.org/UA/Data/ ;ns=2;i=10393
        //DoubleValue: nsu=http://test.org/UA/Data/ ;ns=2;i=10394
        //StringValue: nsu=http://test.org/UA/Data/ ;ns=2;i=10395
        //DateTimeValue: nsu=http://test.org/UA/Data/ ;ns=2;i=10396
        //GuidValue: nsu=http://test.org/UA/Data/ ;ns=2;i=10397
        //ByteStringValue: nsu=http://test.org/UA/Data/ ;ns=2;i=10398
        //XmlElementValue: nsu=http://test.org/UA/Data/ ;ns=2;i=10399
        //NodeIdValue: nsu=http://test.org/UA/Data/ ;ns=2;i=10400
        //ExpandedNodeIdValue: nsu=http://test.org/UA/Data/ ;ns=2;i=10401
        //QualifiedNameValue: nsu=http://test.org/UA/Data/ ;ns=2;i=10402
        //LocalizedTextValue: nsu=http://test.org/UA/Data/ ;ns=2;i=10403
        //StatusCodeValue: nsu=http://test.org/UA/Data/ ;ns=2;i=10404
        //VariantValue: nsu=http://test.org/UA/Data/ ;ns=2;i=10405
        //SimulationActive: nsu=http://test.org/UA/Data/ ;ns=2;i=10328
        //GenerateValues: nsu=http://test.org/UA/Data/ ;ns=2;i=10329
        //CycleComplete: nsu=http://test.org/UA/Data/ ;ns=2;i=10331
        //UserScalarValueObjectType: nsu=http://test.org/UA/Data/ ;ns=2;i=9921
    }
}
#endregion
