// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable LocalizableElement
// ReSharper disable InconsistentNaming
// ReSharper disable PossibleNullReferenceException
// ReSharper disable StringLiteralTypo
// ReSharper disable CommentTypo
#region Example
// This example shows how to subscribe to multiple events.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.AddressSpace;
using OpcLabs.EasyOpc.UA.AddressSpace.Standard;
using OpcLabs.EasyOpc.UA.AlarmsAndConditions;
using OpcLabs.EasyOpc.UA.Filtering;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UADocExamples.AlarmsAndConditions
{
    class SubscribeMultipleMonitoredItems
    {
        public static void Events()
        {
            // Define which server we will work with.
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:62544/Quickstarts/AlarmConditionServer";

            // Instantiate the client object and hook events.
            var client = new EasyUAClient();
            client.EventNotification += client_EventNotification;

            Console.WriteLine("Subscribing...");
            client.SubscribeMultipleMonitoredItems(new[]
                {
                    new EasyUAMonitoredItemArguments("firstState", 
                        endpointDescriptor, 
                        UAObjectIds.Server,
                        new UAMonitoringParameters(1000, new UAEventFilterBuilder(
                            UAFilterElements.GreaterThanOrEqual(UABaseEventObject.Operands.Severity, 500),
                            UABaseEventObject.AllFields)))
                        { AttributeId = UAAttributeId.EventNotifier },
                    new EasyUAMonitoredItemArguments("secondState",
                        endpointDescriptor, 
                        UAObjectIds.Server,
                        new UAMonitoringParameters(2000, new UAEventFilterBuilder(
                            UAFilterElements.Equals(
                                UABaseEventObject.Operands.SourceNode, 
                                new UANodeId("nsu=http://opcfoundation.org/Quickstarts/AlarmCondition ;ns=2;s=1:Metals/SouthMotor")),
                            UABaseEventObject.AllFields)))
                        { AttributeId = UAAttributeId.EventNotifier }
                });

            Console.WriteLine("Processing event notifications for 30 seconds...");
            System.Threading.Thread.Sleep(30 * 1000);

            Console.WriteLine("Unsubscribing...");
            client.UnsubscribeAllMonitoredItems();

            Console.WriteLine("Waiting for 5 seconds...");
            System.Threading.Thread.Sleep(5 * 1000);

            Console.WriteLine("Finished.");
        }

        static void client_EventNotification(object sender, EasyUAEventNotificationEventArgs e)
        {
            // Display the event
            Console.WriteLine(e);
        }
        

        // Example output (truncated):
        //Subscribing...
        //Processing monitored item changed events for 30 seconds...
        //[firstState] Success
        //[secondState] Success
        //[firstState] Success; Refresh; RefreshInitiated
        //[firstState] Success; Refresh; (10 field results) [EastTank] 500! "The alarm was acknoweledged." @10/14/2019 4:00:13 PM
        //[firstState] Success; Refresh; (10 field results) [EastTank] 500! "The alarm was acknoweledged." @10/14/2019 4:00:17 PM
        //[firstState] Success; Refresh; (10 field results) [NorthMotor] 500! "The alarm was acknoweledged." @10/14/2019 4:00:02 PM
        //[firstState] Success; Refresh; (10 field results) [NorthMotor] 500! "The alarm was acknoweledged." @10/14/2019 4:00:16 PM
        //[firstState] Success; Refresh; (10 field results) [SouthMotor] 700! "The alarm was acknoweledged." @10/14/2019 4:00:21 PM
        //[firstState] Success; Refresh; (10 field results) [SouthMotor] 500! "The alarm was acknoweledged." @10/14/2019 4:00:03 PM
        //[firstState] Success; Refresh; RefreshComplete
        //[firstState] Success; (10 field results) [Internal] 500! "Raising Events" @11/8/2019 7:48:08 PM
        //[firstState] Success; (10 field results) [Internal] 500! "Events Raised" @11/8/2019 7:48:08 PM
        //[secondState] Success; Refresh; RefreshInitiated
        //[secondState] Success; Refresh; (10 field results) [SouthMotor] 100! "The dialog was activated" @9/10/2019 8:08:25 PM
        //[secondState] Success; Refresh; (10 field results) [SouthMotor] 100! "The alarm is active." @11/8/2019 7:48:07 PM
        //[secondState] Success; Refresh; (10 field results) [SouthMotor] 700! "The alarm was acknoweledged." @10/14/2019 4:00:21 PM
        //[secondState] Success; Refresh; (10 field results) [SouthMotor] 500! "The alarm was acknoweledged." @10/14/2019 4:00:03 PM
        //[secondState] Success; Refresh; (10 field results) [SouthMotor] 100! "The alarm severity has increased." @9/10/2019 8:09:02 PM
        //[secondState] Success; Refresh; (10 field results) [SouthMotor] 100! "The alarm severity has increased." @9/10/2019 8:09:59 PM
        //[secondState] Success; Refresh; RefreshComplete
        //[firstState] Success; (10 field results) [Internal] 500! "Raising Events" @11/8/2019 7:48:09 PM
        //[firstState] Success; (10 field results) [Internal] 500! "Events Raised" @11/8/2019 7:48:09 PM
        //[firstState] Success; (10 field results) [Internal] 500! "Raising Events" @11/8/2019 7:48:10 PM
        //[firstState] Success; (10 field results) [Internal] 500! "Events Raised" @11/8/2019 7:48:10 PM
        //[firstState] Success; (10 field results) [Internal] 500! "Raising Events" @11/8/2019 7:48:11 PM
        //[firstState] Success; (10 field results) [Internal] 500! "Events Raised" @11/8/2019 7:48:11 PM
        //[firstState] Success; (10 field results) [Internal] 500! "Raising Events" @11/8/2019 7:48:12 PM
        //[firstState] Success; (10 field results) [Internal] 500! "Events Raised" @11/8/2019 7:48:12 PM
        //[firstState] Success; (10 field results) [EastTank] 500! "The alarm severity has increased." @11/8/2019 7:48:13 PM
        //[firstState] Success; (10 field results) [Internal] 500! "Raising Events" @11/8/2019 7:48:13 PM
        //[firstState] Success; (10 field results) [Internal] 500! "Events Raised" @11/8/2019 7:48:13 PM
        //[firstState] Success; (10 field results) [Internal] 500! "Raising Events" @11/8/2019 7:48:14 PM
        //[firstState] Success; (10 field results) [Internal] 500! "Events Raised" @11/8/2019 7:48:14 PM
        //[firstState] Success; (10 field results) [Internal] 500! "Raising Events" @11/8/2019 7:48:15 PM
        //[firstState] Success; (10 field results) [Internal] 500! "Events Raised" @11/8/2019 7:48:15 PM
        //[firstState] Success; (10 field results) [Internal] 500! "Raising Events" @11/8/2019 7:48:16 PM
        //[firstState] Success; (10 field results) [Internal] 500! "Events Raised" @11/8/2019 7:48:16 PM
        //[firstState] Success; (10 field results) [Internal] 500! "Raising Events" @11/8/2019 7:48:17 PM
        //[firstState] Success; (10 field results) [Internal] 500! "Events Raised" @11/8/2019 7:48:17 PM
        //[firstState] Success; (10 field results) [Internal] 500! "Raising Events" @11/8/2019 7:48:18 PM
        //[firstState] Success; (10 field results) [Internal] 500! "Events Raised" @11/8/2019 7:48:18 PM
        //[secondState] Success; (10 field results) [SouthMotor] 300! "The alarm severity has increased." @11/8/2019 7:48:18 PM
        //...
    }
}
#endregion
