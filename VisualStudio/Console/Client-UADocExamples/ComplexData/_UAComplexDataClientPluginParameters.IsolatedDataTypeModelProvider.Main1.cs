// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable LocalizableElement
#region Example
// Shows how to configure the OPC UA Complex Data plug-in to use a shared data type model provider.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Diagnostics;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.OperationModel;
using OpcLabs.EasyOpc.UA.Plugins.ComplexData;

namespace UADocExamples.ComplexData._UAComplexDataClientPluginParameters
{
    class IsolatedDataTypeModelProvider
    {
        public static void Main1()
        {
            // Define which server and node we will work with.
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"
            UANodeDescriptor nodeDescriptor =
                "nsu=http://test.org/UA/Data/ ;i=10239"; // [ObjectsFolder]/Data.Static.Scalar.StructureValue


            // We will create two instances of EasyUAClient class, and configure each of them to use the shared data type
            // model provider.

            // Configure the first client object.
            var client1 = new EasyUAClient();
            UAComplexDataClientPluginParameters complexDataClientPluginParameters1 =
                client1.InstanceParameters.PluginConfigurations.Find<UAComplexDataClientPluginParameters>();
            Debug.Assert(complexDataClientPluginParameters1 != null);
            complexDataClientPluginParameters1.IsolatedDataTypeModelProvider = false;

            // Configure the second client object.
            var client2 = new EasyUAClient();
            UAComplexDataClientPluginParameters complexDataClientPluginParameters2 =
                client2.InstanceParameters.PluginConfigurations.Find<UAComplexDataClientPluginParameters>();
            Debug.Assert(complexDataClientPluginParameters2 != null);
            complexDataClientPluginParameters2.IsolatedDataTypeModelProvider = false;


            // We will now read the same complex data node using the two client objects.
            //
            // There is no noticeable difference in the results from the default state in which the client objects are
            // set to use per-instance data type model provider. But, with the shared data type model provider, the metadata
            // obtained during the read on the first client object and cached inside the data type model provider are reused
            // during the read on the second client object, making this and the subsequent operations more efficient.

            // Read the complex data node using the first client.
            object value1;
            try
            {
                value1 = client1.ReadValue(endpointDescriptor, nodeDescriptor);
            }
            catch (UAException uaException)
            {
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
                return;
            }
            Console.WriteLine(value1);

            // Read the complex data node using the second client.
            object value2;
            try
            {
                value2 = client2.ReadValue(endpointDescriptor, nodeDescriptor);
            }
            catch (UAException uaException)
            {
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
                return;
            }
            Console.WriteLine(value2);
        }
    }
}
#endregion
