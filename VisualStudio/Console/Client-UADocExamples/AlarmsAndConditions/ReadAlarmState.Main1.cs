// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to read a Low state of a limit alarm. Note that you should not normally read a state of an alarm
// from inside its event notification, because the state might have already changed. Instead, include the information you
// need in the Select clauses when subscribing for the event.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.AddressSpace;
using OpcLabs.EasyOpc.UA.Navigation;

namespace UADocExamples.AlarmsAndConditions
{
    class ReadAlarmState
    {
        public static void Main1()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:62544/Quickstarts/AlarmConditionServer";

            UANodeDescriptor alarmNodeDescriptor = new UANodeId(
                namespaceUriString:"http://opcfoundation.org/Quickstarts/AlarmCondition", 
                identifier:"1:Colours/EastTank?Yellow");

            // Knowing the alarm node, and the fact that is an instance of NonExclusiveLevelAlarmType (or its subtype),
            // determine what is its LowState/Id node.
            UANodeDescriptor lowStateIdNodeDescriptor = new UABrowsePath(alarmNodeDescriptor,
                new []
                {
                    UABrowsePathElement.CreateSimple("ns=0;s=LowState"),
                    UABrowsePathElement.CreateSimple("ns=0;s=Id")
                });

            // Instantiate the client object.
            var client = new EasyUAClient();

            Console.WriteLine("Reading alarm state...");
            var lowStateId = (bool)client.ReadValue(endpointDescriptor, lowStateIdNodeDescriptor);

            Console.WriteLine($"Id of LowState: {lowStateId}");
        }
    }
}
#endregion
