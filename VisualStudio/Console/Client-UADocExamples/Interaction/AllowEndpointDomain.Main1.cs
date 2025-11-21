// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable LocalizableElement
#region Example
// This example shows how in a console application, the user is asked to allow a server instance certificate with
// mismatched domain name.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples.Interaction
{
    class AllowEndpointDomain
    {
        public static void Main1()
        {
            // Define which server we will work with.
            // Note that extra '.' at the end of the domain name. For the purpose of this example, it allows us to address
            // the same domain, but cause a mismatch with what the names that are listed in the server instance certificate.
            UAEndpointDescriptor endpointDescriptor = "opc.tcp://opcua.demo-this.com.:51210/UA/SampleServer";
            
            // Instantiate the client object.
            var client = new EasyUAClient()
            {
                // Enforce the endpoint domain check.
                Isolated = true,
                IsolatedParameters = {SessionParameters = {CheckEndpointDomain = true}}
            };

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
