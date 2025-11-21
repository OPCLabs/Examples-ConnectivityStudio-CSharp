// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
// ReSharper disable LocalizableElement
#region Example
// This example shows how to read value of server's NamespaceArray, and display the namespace URIs in it.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.AddressSpace.Standard;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples._EasyUAClient
{
    partial class ReadValue
    {
        public static void NamespaceArray()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client object
            var client = new EasyUAClient();

            // Perform the operation: Obtain value of a node
            object value;
            try
            {
                value = client.ReadValue(endpointDescriptor, UAVariableIds.Server_NamespaceArray);  // i=2255
            }
            catch (UAException uaException)
            {
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
                return;
            }

            if (!(value is string[] arrayValue))
            {
                Console.WriteLine("*** Not a string array");
                return;
            }

            // Display results
            for (int i = 0; i < arrayValue.Length; i++)
                Console.WriteLine($"{i}: {arrayValue[i]}");
        }


        // Example output:
        //
        //0: http://opcfoundation.org/UA/
        //1: urn:DEMO-5:UA Sample Server
        //2: http://test.org/UA/Data/
        //3: http://test.org/UA/Data//Instance
        //4: http://opcfoundation.org/UA/Boiler/
        //5: http://opcfoundation.org/UA/Boiler//Instance
        //6: http://opcfoundation.org/UA/Diagnostics
        //7: http://samples.org/UA/memorybuffer
        //8: http://samples.org/UA/memorybuffer/Instance
    }
}
#endregion
