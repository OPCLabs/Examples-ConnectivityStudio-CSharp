// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to retrieve all sub-types of a given data type, recursively.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.BaseLib.OperationModel.Generic;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.AddressSpace.Standard;
using OpcLabs.EasyOpc.UA.Extensions;

namespace UADocExamples._EasyUAClient
{
    class RetrieveAllDataTypes
    {
        public static void Main1()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client object
            var client = new EasyUAClient();

            // Retrieve all sub-types of the 'Structure' data type.
            ValueResult<UANodeDescriptor[]> result = client.RetrieveAllDataTypes(
                endpointDescriptor, UADataTypeIds.Structure);
            // Check if the operation succeeded. Use the ThrowIfFailed method instead if you want exception be thrown.
            if (!result.Succeeded)
            {
                Console.WriteLine("*** Failure: {0}", result.ErrorMessageBrief);
                return;
            }

            // Display results. Note that all node descriptors have node IDs in them; but the default display format shows 
            // the browse paths, which are more readable, when they are available.
            foreach (UANodeDescriptor nodeDescriptor in result.Value)
                Console.WriteLine(nodeDescriptor);

            // Example output:
            //
            //NodeId="Structure"
            //BrowsePath="[Structure]/Argument"
            //BrowsePath="[Structure]/StatusResult"
            //BrowsePath="[Structure]/UserTokenPolicy"
            //BrowsePath="[Structure]/ApplicationDescription"
            //BrowsePath="[Structure]/EndpointDescription"
            //BrowsePath="[Structure]/UserIdentityToken"
            //BrowsePath="[Structure]/EndpointConfiguration"
            //BrowsePath="[Structure]/SupportedProfile"
            //BrowsePath="[Structure]/BuildInfo"
            //BrowsePath="[Structure]/SoftwareCertificate"
            //BrowsePath="[Structure]/SignedSoftwareCertificate"
            //BrowsePath="[Structure]/AddNodesItem"
            //BrowsePath="[Structure]/AddReferencesItem"
            //BrowsePath="[Structure]/DeleteNodesItem"
            //BrowsePath="[Structure]/DeleteReferencesItem"
            //BrowsePath="[Structure]/ScalarTestType"
            //BrowsePath="[Structure]/ArrayTestType"
            //BrowsePath="[Structure]/CompositeTestType"
            //BrowsePath="[Structure]/RegisteredServer"
            //BrowsePath="[Structure]/ContentFilterElement"
            //BrowsePath="[Structure]/ContentFilter"
            //BrowsePath="[Structure]/FilterOperand"
            //BrowsePath="[Structure]/HistoryEvent"
            //BrowsePath="[Structure]/MonitoringFilter"
            //BrowsePath="[Structure]/RedundantServerDataType"
            //BrowsePath="[Structure]/SamplingIntervalDiagnosticsDataType"
            //BrowsePath="[Structure]/ServerDiagnosticsSummaryDataType"
            //BrowsePath="[Structure]/ServerStatusDataType"
            //BrowsePath="[Structure]/SessionDiagnosticsDataType"
            //BrowsePath="[Structure]/SessionSecurityDiagnosticsDataType"
            //BrowsePath="[Structure]/ServiceCounterDataType"
            //BrowsePath="[Structure]/SubscriptionDiagnosticsDataType"
            //BrowsePath="[Structure]/ModelChangeStructureDataType"
            //BrowsePath="[Structure]/Range"
            //BrowsePath="[Structure]/EUInformation"
            //BrowsePath="[Structure]/Annotation"
            //BrowsePath="[Structure]/ProgramDiagnosticDataType"
            //BrowsePath="[Structure]/SemanticChangeStructureDataType"
            //BrowsePath="[Structure]/HistoryEventFieldList"
            //BrowsePath="[Structure]/AggregateConfiguration"
            //BrowsePath="[Structure]/EnumValueType"
            //BrowsePath="[Structure]/TimeZoneDataType"
            //BrowsePath="[Structure]/EndpointUrlListDataType"
            //BrowsePath="[Structure]/NetworkGroupDataType"
            //BrowsePath="[Structure]/AxisInformation"
            //BrowsePath="[Structure]/XVType"
            //BrowsePath="[Structure]/ComplexNumberType"
            //BrowsePath="[Structure]/DoubleComplexNumberType"
            //BrowsePath="[Structure]/ScalarValueDataType"
            //BrowsePath="[Structure]/ArrayValueDataType"
            //BrowsePath="[Structure]/UserScalarValueDataType"
            //BrowsePath="[Structure]/UserArrayValueDataType"
            //BrowsePath="[Structure]/UserIdentityToken/AnonymousIdentityToken"
            //BrowsePath="[Structure]/UserIdentityToken/UserNameIdentityToken"
            //BrowsePath="[Structure]/UserIdentityToken/X509IdentityToken"
            //BrowsePath="[Structure]/UserIdentityToken/IssuedIdentityToken"
            //BrowsePath="[Structure]/FilterOperand/ElementOperand"
            //BrowsePath="[Structure]/FilterOperand/LiteralOperand"
            //BrowsePath="[Structure]/FilterOperand/AttributeOperand"
            //BrowsePath="[Structure]/FilterOperand/SimpleAttributeOperand"
            //BrowsePath="[Structure]/MonitoringFilter/EventFilter"
        }
    }
}
#endregion
