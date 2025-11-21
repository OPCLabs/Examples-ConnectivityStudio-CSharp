// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
#region Example
// This example shows how subscribe to changes of multiple items with percent deadband.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Threading;
using OpcLabs.BaseLib.ComInterop;
using OpcLabs.EasyOpc;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.OperationModel;

namespace DocExamples.DataAccess.Xml
{
    partial class SubscribeMultipleItems
    {
        public static void PercentDeadbandXml()
        {
            // Instantiate the client object.
            using (var client = new EasyDAClient())
            {
                client.ItemChanged += client_PercentDeadband_ItemChanged;

                Console.WriteLine("Subscribing with different percent deadbands...");
                client.SubscribeMultipleItems(
                    new[] {
                            new DAItemGroupArguments("http://opcxml.demo-this.com/XmlDaSampleServer/Service.asmx", new DAItemDescriptor("Dynamic/Analog Types/Int", 
                                VarTypes.Empty), new DAGroupParameters(requestedUpdateRate:100, percentDeadband:10.0f), null), 
                            new DAItemGroupArguments("http://opcxml.demo-this.com/XmlDaSampleServer/Service.asmx", new DAItemDescriptor("Dynamic/Analog Types/Double",
                                VarTypes.Empty), new DAGroupParameters(requestedUpdateRate:100, percentDeadband:5.0f), null),
                        });

                Console.WriteLine("Processing item changed events for 1 minute...");
                Thread.Sleep(60 * 1000);
            }
        }

        // Item changed event handler
        static void client_PercentDeadband_ItemChanged(object sender, EasyDAItemChangedEventArgs e)
        {
            if (e.Succeeded)
                Console.WriteLine("{0}: {1}", e.Arguments.ItemDescriptor.ItemId, e.Vtq);
            else
                Console.WriteLine("{0} *** Failure: {1}", e.Arguments.ItemDescriptor.ItemId, e.ErrorMessageBrief);
        }
    }
}
#endregion
