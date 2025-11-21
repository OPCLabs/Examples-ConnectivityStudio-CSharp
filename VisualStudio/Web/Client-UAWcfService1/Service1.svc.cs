// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder

// UAWcfService1: A simple Web service using WCF technology. Provides a GetData method to read a value of an OPC UA 
// variable.
// Use UAWcfClient1 project (under Console folder) to test this Web service.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using OpcLabs.EasyOpc.UA;

namespace UAWcfService1
{
    // NOTE: If you change the class name "Service1" here, you must also update the reference to "Service1" in Web.config and in the associated .svc file.
    public class Service1 : IService1
    {
        // Use a shared client instance to allow for better optimization.
        static private readonly EasyUAClient Client = new EasyUAClient();

        public string GetData()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // The following call may throw a UAException, which would be propagated to the client as a FaultException.
            // Production-quality code may need to catch the UAException and handle it differently.
            object value = Client.ReadValue(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10845");
            return value?.ToString() ?? "";
        }
    }
}
