// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
// ReSharper disable CheckNamespace
#region Example
// This example shows how to obtain current state information for the condition instance corresponding to a Source and 
// certain ConditionName.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.AlarmsAndEvents;
using OpcLabs.EasyOpc.OperationModel;

namespace DocExamples.AlarmsAndEvents._EasyAEClient
{
    class GetConditionState 
    { 
        public static void Main1()
        {
            // Instantiate the client object.
            var client = new EasyAEClient();

            AEConditionState conditionState;
            try
            {
                conditionState = client.GetConditionState("", "OPCLabs.KitEventServer.2",
                    "Simulation.ConditionState1", "Simulated");
            }
            catch (OpcException opcException)
            {
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message);
                return;
            }

            Console.WriteLine("ConditionState:");
            Console.WriteLine("    .ActiveSubcondition: {0}", conditionState.ActiveSubcondition);
            Console.WriteLine("    .Enabled: {0}", conditionState.Enabled);
            Console.WriteLine("    .Active: {0}", conditionState.Active);
            Console.WriteLine("    .Acknowledged: {0}", conditionState.Acknowledged);
            Console.WriteLine("    .Quality: {0}", conditionState.Quality);
            // Remark: IAEConditionState has many more properties
        }
	} 
}
#endregion
