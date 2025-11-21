
// ReSharper disable ArrangeModifiersOrder

// WcfService1: A simple Web service using WCF technology. Provides a GetData method to read a value of an OPC item.
// Use WcfClient1 project (under Console folder) to test this Web service.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.Extensions;

namespace WcfService1
{
    // NOTE: If you change the class name "Service1" here, you must also update the reference to "Service1" in Web.config and in the associated .svc file.
    public class Service1 : IService1
    {
        static Service1()
        {
            // Enable auto-subscribing optimization (not necessary), which can improve performance with repeated Read requests.
            Client.TryEnableAutoSubscribingOptimization();
        }

        // Use a shared client instance to allow for better optimization.
        static private readonly EasyDAClient Client = new EasyDAClient();

        public string GetData()
        {
            // The following call may throw an OpcException, which would be propagated to the client as a FaultException.
            // Production-quality code may need to catch the OpcException and handle it differently.
            object value = Client.ReadItemValue("", "OPCLabs.KitServer.2", "Demo.Ramp");
            return value?.ToString() ?? "";
        }
    }
}
