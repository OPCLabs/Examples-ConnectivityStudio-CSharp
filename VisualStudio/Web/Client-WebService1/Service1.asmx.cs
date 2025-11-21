// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder

// WebService1: A simple Web service using ASMX technology. Provides "Hello World" method to read a value of an OPC
// "Classic" item. The web service can be tested using the integrated test facility.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System.Web.Services;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.Extensions;

namespace WebService1
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
        static Service1()
        {
            // Enable auto-subscribing optimization (not necessary), which can improve performance with repeated Read requests.
            Client.TryEnableAutoSubscribingOptimization();
        }

        // Use a shared client instance to allow for better optimization.
        static private readonly EasyDAClient Client = new EasyDAClient();

        [WebMethod]
        public string HelloWorld()
        {
            // The following call may throw an OpcException, which would be reported to the client.
            // Production-quality code may need to catch the OpcException and handle it differently.
            object value = Client.ReadItemValue("", "OPCLabs.KitServer.2", "Demo.Ramp");
            return value?.ToString() ?? "";
        }
    }
}
