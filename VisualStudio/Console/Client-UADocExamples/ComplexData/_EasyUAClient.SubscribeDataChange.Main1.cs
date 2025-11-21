// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// Shows how to subscribe to complex data with OPC UA Complex Data plug-in.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples.ComplexData._EasyUAClient
{
    class SubscribeDataChange
    {
        public static void Main1()
        {
            // Define which server and node we will work with.
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"
            UANodeDescriptor nodeDescriptor =
                "nsu=http://test.org/UA/Data/ ;i=10867"; // [ObjectsFolder]/Data.Dynamic.Scalar.StructureValue

            // Instantiate the client object and hook the event handler.
            var client = new EasyUAClient();
            client.DataChangeNotification += client_DataChangeNotification;

            // Subscribe to a node which returns complex data. This is done in the same way as regular subscribes - just 
            // the data returned is different.
            Console.WriteLine("Subscribing...");
            client.SubscribeDataChange(endpointDescriptor, nodeDescriptor, samplingInterval:1000);

            Console.WriteLine("Processing data change events for 20 seconds...");
            System.Threading.Thread.Sleep(20 * 1000);

            Console.WriteLine("Unsubscribing...");
            client.UnsubscribeAllMonitoredItems();

            Console.WriteLine("Waiting for 5 seconds...");
            System.Threading.Thread.Sleep(5 * 1000);

            // Unhook the event handler.
            client.DataChangeNotification -= client_DataChangeNotification;


            // Example output:
            //
            //Subscribing...
            //Processing data change events for 20 seconds...
            //(ScalarValueDataType) structured
            //(ArrayValueDataType) structured
            //(ArrayValueDataType) structured
            //(ArrayValueDataType) structured
            //(ArrayValueDataType) structured
            //(ArrayValueDataType) structured
            //(ArrayValueDataType) structured
            //(ArrayValueDataType) structured
            //(ScalarValueDataType) structured
            //(ScalarValueDataType) structured
            //(ArrayValueDataType) structured
            //(ArrayValueDataType) structured
            //(ScalarValueDataType) structured
            //(ArrayValueDataType) structured
            //(ArrayValueDataType) structured
            //(ScalarValueDataType) structured
            //(ScalarValueDataType) structured
            //(ScalarValueDataType) structured
            //Unsubscribing...
            //Waiting for 5 seconds...
        }

        static void client_DataChangeNotification(object sender, EasyUADataChangeNotificationEventArgs e)
        {
            // Display value
            if (e.Succeeded)
                Console.WriteLine("Value: {0}", e.AttributeData.Value);
            else
                Console.WriteLine("*** Failure: {0}", e.ErrorMessageBrief);

            // For processing the internals of the data, refer to examples for GenericData and DataType classes.
        }
    }
}
#endregion
