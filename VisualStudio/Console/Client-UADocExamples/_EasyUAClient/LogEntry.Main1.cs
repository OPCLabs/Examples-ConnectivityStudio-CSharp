// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

#region Example
// This example demonstrates the loggable entries originating in the OPC-UA client engine and the EasyUAClient component.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.BaseLib.Instrumentation;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples._EasyUAClient
{
    class LogEntry
    {
        public static void Main1()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Hook static events
            EasyUAClient.LogEntry += EasyUAClientOnLogEntry;

            try
            {
                // Do something - invoke an OPC read, to trigger some loggable entries.
                var client = new EasyUAClient();
                try
                {
                    client.ReadValue(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10853");
                }
                catch (UAException uaException)
                {
                    Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
                    return;
                }

                Console.WriteLine("Processing log entry events for 1 minute...");
                System.Threading.Thread.Sleep(60 * 1000);

                Console.WriteLine("Finished.");
            }
            finally
            {
                // Unhook static events
                EasyUAClient.LogEntry -= EasyUAClientOnLogEntry;
            }
        }

        // Event handler for the LogEntry event. It simply prints out the event.
        private static void EasyUAClientOnLogEntry(object sender, LogEntryEventArgs logEntryEventArgs)
        {
            Console.WriteLine(logEntryEventArgs);
        }
    }
}
#endregion
