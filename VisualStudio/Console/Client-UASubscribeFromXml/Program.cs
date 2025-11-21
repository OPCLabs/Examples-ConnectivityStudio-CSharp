
// This example shows how to load a list of OPC Unified Architecture items from an XML file and subscribe to them.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UASubscribeFromXml
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Loading items from XML file...");
            var xmlSerializer = new XmlSerializer(typeof(UAMonitoredItemArguments[]));
            var xmlReader = XmlReader.Create("UAItems.xml", new XmlReaderSettings { IgnoreWhitespace = true });
            var argArray = (UAMonitoredItemArguments[])(xmlSerializer.Deserialize(xmlReader));

            if (!(argArray is null))
            {
                Console.WriteLine();
                Console.WriteLine("Arguments loaded:");
                foreach (UAMonitoredItemArguments monitoredItemArguments in argArray)
                    Console.WriteLine($"  {monitoredItemArguments}");

                Console.WriteLine();
                Console.WriteLine("Subscribing for 30 seconds...");
                EasyUAClient.SharedInstance.SubscribeMultipleDataChanges(argArray,
                    (_, eventArgs) => Console.WriteLine(
                        $"[{eventArgs.Arguments.State}] {eventArgs.Arguments.NodeDescriptor.NodeId}: {eventArgs.AttributeData}"));
                Thread.Sleep(30 * 1000);

                Console.WriteLine();
                Console.WriteLine("Unsubscribing...");
                EasyUAClient.SharedInstance.UnsubscribeAllMonitoredItems();
            }

            Console.WriteLine();
            Console.WriteLine("Finished.");
        }
    }
}
