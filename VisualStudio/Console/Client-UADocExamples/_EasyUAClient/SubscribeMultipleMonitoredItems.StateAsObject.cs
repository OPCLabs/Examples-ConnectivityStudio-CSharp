// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable LocalizableElement
// ReSharper disable InconsistentNaming
// ReSharper disable PossibleNullReferenceException
// ReSharper disable ConvertIfStatementToConditionalTernaryExpression
#region Example
// This example shows how to subscribe to changes of multiple monitored items
// and display each change, identifying the different subscriptions by an
// object.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Threading;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples._EasyUAClient
{
    partial class SubscribeMultipleMonitoredItems
    {
        class CustomObject
        {
            public CustomObject(string name)
            {
                Name = name;
            }

            public string Name { get; }
        }
        

        public static void StateAsObject()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client object and hook events
            var client = new EasyUAClient();
            client.DataChangeNotification += ClientOnDataChangeNotification_StateAsObject;

            Console.WriteLine("Subscribing...");
            int[] handleArray = client.SubscribeMultipleMonitoredItems(new[]
                {
                    new EasyUAMonitoredItemArguments(null, endpointDescriptor, 
                        "nsu=http://test.org/UA/Data/ ;i=10845", 1000)
                        {State = new CustomObject("First")},    // A custom object that corresponds to the subscription
                    new EasyUAMonitoredItemArguments(null, endpointDescriptor, 
                        "nsu=http://test.org/UA/Data/ ;i=10853", 1000)
                        {State = new CustomObject("Second")},    // A custom object that corresponds to the subscription
                    new EasyUAMonitoredItemArguments(null, endpointDescriptor, 
                        "nsu=http://test.org/UA/Data/ ;i=10855", 1000)
                        {State = new CustomObject("Third")},    // A custom object that corresponds to the subscription
                });

            for (int i = 0; i < handleArray.Length; i++)
                Console.WriteLine($"handleArray[{i}]: {handleArray[i]}");

            Console.WriteLine("Processing monitored item changed events for 10 seconds...");
            Thread.Sleep(10 * 1000);

            Console.WriteLine("Unsubscribing...");
            client.UnsubscribeAllMonitoredItems();

            Console.WriteLine("Waiting for 5 seconds...");
            Thread.Sleep(5 * 1000);

            Console.WriteLine("Finished.");
        }

        static void ClientOnDataChangeNotification_StateAsObject(object sender, EasyUADataChangeNotificationEventArgs eventArgs)
        {
            // Obtain the custom object we have passed in.
            var stateAsObject = (CustomObject) eventArgs.Arguments.State;

            // Display the data
            if (eventArgs.Succeeded)
                Console.WriteLine($"{stateAsObject.Name}: {eventArgs.AttributeData}");
            else
                Console.WriteLine($"{stateAsObject.Name} *** Failure: {eventArgs.ErrorMessageBrief}");
        }
    }
}
#endregion
