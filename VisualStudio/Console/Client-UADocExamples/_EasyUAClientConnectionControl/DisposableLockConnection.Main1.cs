// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to obtain lock the OPC UA connection and obtain a disposable object which unlocks the connection
// when it is disposed.
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
using OpcLabs.EasyOpc.UA.Services.Extensions;

namespace UADocExamples._EasyUAClientConnectionControl
{
    class DisposableLockConnection
    {
        public static void Main1()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client object and hook events
            using (var client = new EasyUAClient())
            {
                // Obtain the client connection monitoring service.
                IEasyUAClientConnectionMonitoring clientConnectionMonitoring = client.GetService<IEasyUAClientConnectionMonitoring>();
                if (clientConnectionMonitoring is null)
                {
                    Console.WriteLine("The client connection monitoring service is not available.");
                    return;
                }

                // Obtain the client connection control service.
                IEasyUAClientConnectionControl clientConnectionControl = client.GetService<IEasyUAClientConnectionControl>();
                if (clientConnectionControl is null)
                {
                    Console.WriteLine("The client connection control service is not available.");
                    return;
                }

                // Display the server condition changed events.
                clientConnectionMonitoring.ServerConditionChanged += (sender, args) => Console.WriteLine(args);

                try
                {
                    Console.WriteLine("Reading (1)");
                    // The first read will cause a connection to the server.
                    UAAttributeData attributeData1 =
                        client.Read(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10853");
                    Console.WriteLine(attributeData1);

                    Console.WriteLine("Waiting for 10 seconds...");
                    // Since the connection is now not used for some time, and it is not locked, it will be closed.
                    System.Threading.Thread.Sleep(10 * 1000);

                    Console.WriteLine("Locking");
                    // Locking the connection causes it to open, if possible.
                    using (clientConnectionControl.DisposableLockConnection(endpointDescriptor))
                    {
                        Console.WriteLine("Waiting for 10 seconds...");
                        // The connection is locked, it will not be closed now.
                        System.Threading.Thread.Sleep(10 * 1000);

                        Console.WriteLine("Reading (2)");
                        UAAttributeData attributeData2 =
                            client.Read(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10853");
                        Console.WriteLine(attributeData2);

                        Console.WriteLine("Waiting for 10 seconds...");
                        // The connection is still locked, it will not be closed now.
                        System.Threading.Thread.Sleep(10 * 1000);

                        // Unlocking will occur now.
                    }
                    Console.WriteLine("Unlocked");
                }
                catch (UAException uaException)
                {
                    Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
                }

                Console.WriteLine("Waiting for 10 seconds...");
                // After some delay, the connection will be closed, because there are no subscriptions to the server and no
                // connection locks.
                System.Threading.Thread.Sleep(10 * 1000);

                Console.WriteLine("Finished.");
            }
        }


        // Example output:
        //
        //Reading (1)
        //"opc.tcp://opcua.demo-this.com:51210/UA/SampleServer" Connecting; Success; Attempt #1
        //"opc.tcp://opcua.demo-this.com:51210/UA/SampleServer" Connected; Success
        //-1.034588E+18 {Single} @2021-11-15T15:26:39.169 @@2021-11-15T15:26:39.169; Good
        //Waiting for 10 seconds...
        //"opc.tcp://opcua.demo-this.com:51210/UA/SampleServer" Disconnecting; Success
        //"opc.tcp://opcua.demo-this.com:51210/UA/SampleServer" Disconnected(RetrialDelay=Infinite); Success
        //Locking
        //"opc.tcp://opcua.demo-this.com:51210/UA/SampleServer" Connecting; Success; Attempt #1
        //"opc.tcp://opcua.demo-this.com:51210/UA/SampleServer" Connected; Success
        //Waiting for 10 seconds...
        //Reading (2)
        //2.288872E+21 {Single} @2021-11-15T15:26:59.836 @@2021-11-15T15:26:59.836; Good
        //Waiting for 10 seconds...
        //Unlocked
        //Waiting for 10 seconds...
        //"opc.tcp://opcua.demo-this.com:51210/UA/SampleServer" Disconnecting; Success
        //"opc.tcp://opcua.demo-this.com:51210/UA/SampleServer" Disconnected(RetrialDelay=Infinite); Success
        //Finished.
    }
}
#endregion
