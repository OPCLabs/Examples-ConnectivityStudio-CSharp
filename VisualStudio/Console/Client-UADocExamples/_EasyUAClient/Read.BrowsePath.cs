// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ConvertIfStatementToConditionalTernaryExpression
// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to read the node using a browse path.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.Navigation;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples._EasyUAClient
{
    partial class Read
    {
        public static void BrowsePath()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client object.
            var client = new EasyUAClient();

            // Create the node descriptor by parsing an absolute browse path.
            // The syntax below specifies a common default namespace URI for the elements of the browse path.
            UANodeDescriptor nodeDescriptor = UABrowsePath.Parse("[ObjectsFolder]/Data/Dynamic/Scalar/FloatValue", "http://test.org/UA/Data/");

            // Alternatively, the namespace URIs can be specified with each element of the browse separately.
            //UANodeDescriptor nodeDescriptor = UABrowsePath.Parse("[ObjectsFolder]/[nsu=http://test.org/UA/Data/;Data]/[nsu=http://test.org/UA/Data/;Dynamic]/[nsu=http://test.org/UA/Data/;Scalar]/[nsu=http://test.org/UA/Data/;FloatValue]", null);
            
            // Alternatively, the namespaces can be specified by indexes, separately with each element.
            //UANodeDescriptor nodeDescriptor = UABrowsePath.Parse("[ObjectsFolder]/2:Data/2:Dynamic/2:Scalar/2:FloatValue", null);

            // Obtain attribute data. By default, the Value attribute of a node will be read.
            UAAttributeData attributeData;
            try
            {
                attributeData = client.Read(endpointDescriptor, nodeDescriptor);
            }
            catch (UAException uaException)
            {
                Console.WriteLine($"*** Failure: {uaException.GetBaseException().Message}");
                return;
            }

            // Display results
            Console.WriteLine($"Value: {attributeData.Value}");
            Console.WriteLine($"ServerTimestamp: {attributeData.ServerTimestamp}");
            Console.WriteLine($"SourceTimestamp: {attributeData.SourceTimestamp}");
            Console.WriteLine($"StatusCode: {attributeData.StatusCode}");
        }
    }
}
#endregion
