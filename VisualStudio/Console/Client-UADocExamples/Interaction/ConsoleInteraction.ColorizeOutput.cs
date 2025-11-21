// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable LocalizableElement
#region Example
// Shows how to configure the OPC UA Console Interaction plug-in by turning off the output colorization.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Diagnostics;
using OpcLabs.BaseLib.Console.Interaction;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.Engine;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples.Interaction
{
    partial class ConsoleInteraction
    {
        public static void ColorizeOutput()
        {
            // Configure the shared plug-in.

            // Find the parameters object of the plug-in.
            ConsoleInteractionParameters consoleInteractionPluginParameters =
                EasyUAClient.SharedParameters.PluginConfigurations.Find<ConsoleInteractionParameters>();
            Debug.Assert(consoleInteractionPluginParameters != null);
            // Change the parameter.
            consoleInteractionPluginParameters.ColorizeOutput = false;


            // Do not implicitly trust any endpoint URLs. We want the user be asked explicitly.
            EasyUAClient.SharedParameters.EngineParameters.CertificateAcceptancePolicy.TrustedEndpointUrlStrings.Clear();

            // Define which server we will work with.
            UAEndpointDescriptor endpointDescriptor = "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // Require secure connection, in order to enforce the certificate check.
            endpointDescriptor.EndpointSelectionPolicy = new UAEndpointSelectionPolicy(UAMessageSecurityModes.Secure);
            
            // Instantiate the client object.
            var client = new EasyUAClient();

            UAAttributeData attributeData;
            try
            {
                // Obtain attribute data.
                // The component automatically triggers the necessary user interaction during the first operation.
                attributeData = client.Read(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10853");
            }
            catch (UAException uaException)
            {
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
                return;
            }

            // Display results.
            Console.WriteLine("Value: {0}", attributeData.Value);
            Console.WriteLine("ServerTimestamp: {0}", attributeData.ServerTimestamp);
            Console.WriteLine("SourceTimestamp: {0}", attributeData.SourceTimestamp);
            Console.WriteLine("StatusCode: {0}", attributeData.StatusCode);
        }
    }
}
#endregion
