// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
#region Example
// This example shows how subscribe to changes of a single item with percent deadband.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Diagnostics;
using System.Threading;
using OpcLabs.BaseLib.ComInterop;
using OpcLabs.EasyOpc.DataAccess;

namespace DocExamples.DataAccess.Xml
{
    partial class SubscribeItem
    {
        public static void PercentDeadbandXml()
        {
            // Instantiate the client object.
            var client = new EasyDAClient();

            const float percentDeadband = 5.0f;
            Console.WriteLine($"Subscribing with {percentDeadband}% deadband...");
            // The callback is a lambda expression the displays the value
            client.SubscribeItem("http://opcxml.demo-this.com/XmlDaSampleServer/Service.asmx", new DAItemDescriptor("Dynamic/Analog Types/Double", 
                VarTypes.Empty), new DAGroupParameters(requestedUpdateRate:100, percentDeadband:percentDeadband), 
                (sender, eventArgs) =>
                {
                    Debug.Assert(!(eventArgs is null));

                    if (eventArgs.Succeeded)
                    {
                        Debug.Assert(!(eventArgs.Vtq is null));
                        Console.WriteLine(eventArgs.Vtq.ToString());
                    }
                    else
                        Console.WriteLine("*** Failure: {0}", eventArgs.ErrorMessageBrief);
                },
                state:null);

            Console.WriteLine("Processing item changed events for 10 seconds...");
            Thread.Sleep(10 * 1000);

            Console.WriteLine("Unsubscribing...");
            client.UnsubscribeAllItems();

            Console.WriteLine("Waiting for 2 seconds...");
            Thread.Sleep(2 * 1000);
        }
    }
}
#endregion
