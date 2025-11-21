// UAWcfClient1: Using a Web service provided by the UAWcfService1 project (under Web folder), gets and displays a value of
// an OPC UA variable.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;

namespace UAWcfClient1
{
    class Program
    {
        static void Main()
        {
            var client = new Service1Client();

            // Use the 'client' variable to call operations on the service.
            // The following call may throw an exception in case of an error. Production-quality code should catch and
            // handle the exceptions appropriately.
            string data = client.GetData();
            Console.WriteLine(data);
            Console.ReadLine();

            // Always close the client.
            client.Close();
        }
    }
}
