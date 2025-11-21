// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
// ReSharper disable ConvertIfStatementToConditionalTernaryExpression
#region Example
// This example shows how to read the build information of the server.
// See also: https://reference.opcfoundation.org/Core/Part5/v105/docs/7.7 and
// https://reference.opcfoundation.org/Core/Part5/v104/docs/12.4 .
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.BaseLib.OperationModel;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.AddressSpace.Standard;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples.InformationModel
{
    partial class BuildInfoType
    {
        public static void Main1()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client object.
            var client = new EasyUAClient();

            // Obtain values. By default, the Value attributes of the nodes will be read.
            var readArgumentsArray = new[]
            {
                new UAReadArguments(endpointDescriptor, UAVariableIds.ServerStatusType_BuildInfo_BuildDate),
                new UAReadArguments(endpointDescriptor, UAVariableIds.ServerStatusType_BuildInfo_BuildNumber),
                new UAReadArguments(endpointDescriptor, UAVariableIds.ServerStatusType_BuildInfo_ManufacturerName),
                new UAReadArguments(endpointDescriptor, UAVariableIds.ServerStatusType_BuildInfo_ProductName),
                new UAReadArguments(endpointDescriptor, UAVariableIds.ServerStatusType_BuildInfo_ProductUri),
                new UAReadArguments(endpointDescriptor, UAVariableIds.ServerStatusType_BuildInfo_SoftwareVersion),
            };
            ValueResult[] valueResultArray = client.ReadMultipleValues(readArgumentsArray);

            // Display results.
            for (int i = 0; i < valueResultArray.Length; i++)
            {
                UAReadArguments readArguments = readArgumentsArray[i];
                ValueResult valueResult = valueResultArray[i];
                if (valueResult.Succeeded)
                    Console.WriteLine($"{readArguments.NodeDescriptor.NodeId}: {valueResult.Value}");
                else
                    Console.WriteLine($"{readArguments.NodeDescriptor.NodeId}: *** Failure: {valueResult.ErrorMessageBrief}");
            }
            
            // Note that as of time of writing this code, the public sample server does not have the build information
            // filled in, and the displayed information is thus largely empty (but correct). The example gives better
            // results with servers that provide meaningful build information.
        }
    }
}
#endregion
