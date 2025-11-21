// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable LocalizableElement
// ReSharper disable StringLiteralTypo
#region Example
// This example shows that either a single client object, or multiple client objects can be used to read values from two
// servers.
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
    partial class ReadValue
    {
        public static void MultipleServers()
        {
            // Define which servers we will work with.
            UAEndpointDescriptor endpointDescriptor1 =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"
            UAEndpointDescriptor endpointDescriptor2 =
                "opc.tcp://opcua.demo-this.com:62544/Quickstarts/AlarmConditionServer";



            // Part 1: Use a single client object.
            // This demonstrates the fact that the client objects do *not* represent connections to individual servers.
            // Instead, they are able to maintain connections to multiple servers internally. API method calls on the client
            // object include the server's endpoint descriptor in their arguments, so you can specify a different endpoint
            // with each operation.
            Console.WriteLine();

            // Instantiate the client object
            var client = new EasyUAClient();

            Console.WriteLine("Obtaining values of nodes using a single client object...");
            object value1, value2;
            try
            {
                // The node Id we are reading returns the product name of the server.
                value1 = client.ReadValue(endpointDescriptor1, "nsu=http://opcfoundation.org/UA/ ;i=2261");
                value2 = client.ReadValue(endpointDescriptor2, "nsu=http://opcfoundation.org/UA/ ;i=2261");
                // Note: For efficiency (reading from the two servers in parallel), it would be better to use the
                // ReadMultipleValues method here, but this example is made for code clarity.
            }
            catch (UAException uaException)
            {
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
                return;
            }

            // Display results
            Console.WriteLine("value1: {0}", value1);
            Console.WriteLine("value2: {0}", value2);



            // Part 2: Use multiple client objects.
            // This demonstrates the fact that it is also possible to use multiple client objects, and on the OPC side, the
            // behavior will be the same as if you had used a single client object. Multiple client objects consume somewhat
            // more resources on the client side, but they come handy if, for example,
            // - you cannot easily pass around the single pre-created client object to various parts in your code, or
            // - you are using subscriptions, and you want to hook separate event handlers for different purposes, or
            // - you need to set something in the instance parameters of the client object differently for different
            // connections.
            Console.WriteLine();

            // Instantiate the client objects.
            var client1 = new EasyUAClient();
            var client2 = new EasyUAClient();

            Console.WriteLine("Obtaining values of nodes using multiple client objects...");
            try
            {
                // The node Id we are reading returns the product name of the server.
                value1 = client1.ReadValue(endpointDescriptor1, "nsu=http://opcfoundation.org/UA/ ;i=2261");
                value2 = client2.ReadValue(endpointDescriptor2, "nsu=http://opcfoundation.org/UA/ ;i=2261");
            }
            catch (UAException uaException)
            {
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
                return;
            }

            // Display results
            Console.WriteLine("value1: {0}", value1);
            Console.WriteLine("value2: {0}", value2);



            // Example output:
            //
            //Obtaining values of nodes using a single client object...
            //value1: OPC UA SDK Samples
            //value2: OPC UA Workshop Samples
            //
            //Obtaining values of nodes using multiple client objects...
            //value1: OPC UA SDK Samples
            //value2: OPC UA Workshop Samples
        }
    }
}
#endregion
