// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ConvertIfStatementToConditionalTernaryExpression
// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to read the attributes of 4 OPC-UA nodes specified by browse paths at once, and display the
// results.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.Navigation.Parsing;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples._EasyUAClient
{
    partial class ReadMultiple
    {
        public static void BrowsePath()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client object.
            var client = new EasyUAClient();

            // Instantiate the browse path parser.
            var browsePathParser = new UABrowsePathParser {DefaultNamespaceUriString = "http://test.org/UA/Data/"};

            // Prepare arguments.
            // Note: Add error handling around the following statement if the browse paths are not guaranteed to be
            // syntactically valid.
            var readArgumentsArray = new[]
            {
                new UAReadArguments(endpointDescriptor, 
                    browsePathParser.Parse("[ObjectsFolder]/Data/Dynamic/Scalar/FloatValue")),
                new UAReadArguments(endpointDescriptor,
                    browsePathParser.Parse("[ObjectsFolder]/Data/Dynamic/Scalar/SByteValue")),
                new UAReadArguments(endpointDescriptor,
                    browsePathParser.Parse("[ObjectsFolder]/Data/Static/Array/UInt16Value")),
                new UAReadArguments(endpointDescriptor,
                    browsePathParser.Parse("[ObjectsFolder]/Data/Static/UserScalar/Int32Value"))
            };

            // Obtain attribute data.
            UAAttributeDataResult[] attributeDataResultArray = client.ReadMultiple(readArgumentsArray);

            // Display results.
            for (int i = 0; i < attributeDataResultArray.Length; i++)
            {
                UAAttributeDataResult attributeDataResult = attributeDataResultArray[i];
                if (attributeDataResult.Succeeded)
                    Console.WriteLine($"results[{i}].AttributeData: {attributeDataResult.AttributeData}");
                else
                    Console.WriteLine($"results[{i}] *** Failure: {attributeDataResult.ErrorMessageBrief}");
            }
        }

        // Example output:
        //results[0].AttributeData: 4.187603E+21 {Single} @2019-11-09T14:05:46.268 @@2019-11-09T14:05:46.268; Good
        //results[1].AttributeData: -98 {Int16} @2019-11-09T14:05:46.268 @@2019-11-09T14:05:46.268; Good
        //results[2].AttributeData: [58] {38240, 11129, 64397, 22845, 30525, ...} {Int32[]} @2019-11-09T14:00:07.543 @@2019-11-09T14:05:46.268; Good
        //results[3].AttributeData: 1280120396 {Int32} @2019-11-09T14:00:07.590 @@2019-11-09T14:05:46.268; Good
    }
}
#endregion
