// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

#region Example
// This example shows how to write a value into a single node, specifying a type explicitly.
//
// Reasons for specifying the type explicitly might be:
// - The data type in the server has subtypes, and the client therefore needs to pick the subtype to be written.
// - The data type that the server reports is incorrect.
// - Writing with an explicitly specified type is more efficient.
//
// For a selected subset of most common types, you can also use a different overload of the WriteValue method, and specify 
// a value from the TypeCode enumeration instead.
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
    partial class WriteValue
    {
        public static void Type()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client object
            var client = new EasyUAClient();

            // Modify value of a node
            try
            {
                client.WriteValue(
                    endpointDescriptor,
                    "nsu=http://test.org/UA/Data/ ;i=10221",
                    12345,
                    typeof(Int32));
            }
            catch (UAException uaException)
            {
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
            }
        }
    }
}
#endregion
