// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder

// UAWebService1: A simple Web service using ASMX technology. Provides "Hello World" method to read a value of an OPC UA
// item. The web service can be tested using the integrated test facility.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System.Web.Services;
using OpcLabs.EasyOpc.UA;

namespace UAWebService1
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service1 : WebService
    {
        // Use a shared client instance to allow for better optimization.
        static private readonly EasyUAClient Client = new EasyUAClient();

        [WebMethod]
        public string HelloWorld()
        {
            // The following call may throw an UAException, which would be reported to the client.
            // Production-quality code may need to catch the UAException and handle it differently.
            object value = Client.ReadValue(
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer", 
                "nsu=http://test.org/UA/Data/ ;i=10845");
            return value?.ToString() ?? "";
        }
    }
}
