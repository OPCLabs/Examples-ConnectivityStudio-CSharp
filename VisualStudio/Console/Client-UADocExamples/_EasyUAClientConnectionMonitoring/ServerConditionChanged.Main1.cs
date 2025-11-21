// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ConvertIfStatementToConditionalTernaryExpression
// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to monitor connections to and disconnections from the OPC UA server.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using Microsoft.Extensions.DependencyInjection;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.OperationModel;
using OpcLabs.EasyOpc.UA.Services;

namespace UADocExamples._EasyUAClientConnectionMonitoring
{
    class ServerConditionChanged
    {
        public static void Main1()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client object.
            using (var client = new EasyUAClient())
            {
                // Obtain the client connection monitoring service.
                IEasyUAClientConnectionMonitoring clientConnectionMonitoring = client.GetService<IEasyUAClientConnectionMonitoring>();
                if (clientConnectionMonitoring is null)
                {
                    Console.WriteLine("The client connection monitoring service is not available.");
                    return;
                }

                // Hook events.
                client.DataChangeNotification += client_DataChangeNotification;
                clientConnectionMonitoring.ServerConditionChanged += clientConnectionMonitoring_OnServerConditionChanged;

                try
                {
                    Console.WriteLine("Reading (1)");
                    // The first read will cause a connection to the server.
                    UAAttributeData attributeData1 = client.Read(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10853");
                    Console.WriteLine(attributeData1);

                    Console.WriteLine("Reading (2)");
                    // The second read, because it closely follows the first one, will reuse the connection that is already
                    // open.
                    UAAttributeData attributeData2 = client.Read(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10853");
                    Console.WriteLine(attributeData2);
                }
                catch (UAException uaException)
                {
                    Console.WriteLine($"*** Failure: {uaException.GetBaseException().Message}");
                }

                Console.WriteLine("Waiting for 10 seconds...");
                // Since the connection is now not used for some time, it will be closed.
                System.Threading.Thread.Sleep(10 * 1000);

                Console.WriteLine("Subscribing...");
                // Subscribing to a monitored item will cause a connection to the server, if one is not yet open.
                client.SubscribeDataChange(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10853", 1000);

                Console.WriteLine("Waiting for 10 seconds...");
                // The connection will not be closed as long as there are any subscriptions to the server.
                System.Threading.Thread.Sleep(10 * 1000);

                Console.WriteLine("Unsubscribing...");
                client.UnsubscribeAllMonitoredItems();

                Console.WriteLine("Waiting for 10 seconds...");
                // After some delay, the connection will be closed, because there are no subscriptions to the server.
                System.Threading.Thread.Sleep(10 * 1000);

                Console.WriteLine("Finished.");
            }
        }

        static void client_DataChangeNotification(object sender, EasyUADataChangeNotificationEventArgs e)
        {
            // Display value
            if (e.Succeeded)
                Console.WriteLine($"Value: {e.AttributeData.Value}");
            else
                Console.WriteLine($"*** Failure: {e.ErrorMessageBrief}");
        }

        static void clientConnectionMonitoring_OnServerConditionChanged(object sender, EasyUAServerConditionChangedEventArgs e)
        {
            // Display the event
            Console.WriteLine(e);
        }


        // Example output:
        //
        //Reading(1)
        //"opc.tcp://opcua.demo-this.com:51210/UA/SampleServer" Connecting; Success
        //"opc.tcp://opcua.demo-this.com:51210/UA/SampleServer" Connected; Success
        //-8.095801E+32 {System.Single} @2021-06-24T07:09:46.062 @@2021-06-24T07:09:46.062; Good
        //Reading(2)
        //-3.11389E-18 {System.Single} @2021 - 06 - 24T07: 09:46.125 @@2021 - 06 - 24T07: 09:46.125; Good
        //Waiting for 10 seconds...
        //"opc.tcp://opcua.demo-this.com:51210/UA/SampleServer" Disconnecting; Success
        //"opc.tcp://opcua.demo-this.com:51210/UA/SampleServer" Disconnected(Infinite); Success
        //Subscribing...
        //"opc.tcp://opcua.demo-this.com:51210/UA/SampleServer" Connecting; Success
        //Waiting for 10 seconds...
        //"opc.tcp://opcua.demo-this.com:51210/UA/SampleServer" Connected; Success
        //Value: 3.293034E-31
        //Value: 6.838126E+37
        //Value: 5.837702E-29
        //Value: 0.02940081
        //Value: -9.653872E-17
        //Value: -1.012749E+29
        //Value: -1.422391E+20
        //Value: -3.56571E-22
        //Unsubscribing...
        //Waiting for 10 seconds...
        //"opc.tcp://opcua.demo-this.com:51210/UA/SampleServer" Disconnecting; Success
        //"opc.tcp://opcua.demo-this.com:51210/UA/SampleServer" Disconnected(Infinite); Success
        //Finished.
    }
}
#endregion
