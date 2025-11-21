// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
#region Example
// This example shows how to subscribe to OPC XML-DA item changes and obtain the events by pulling them.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.OperationModel;

namespace DocExamples.DataAccess.Xml
{
    class PullItemChanged
    {
        public static void Main1Xml()
        {
            // Instantiate the client object.
            // In order to use event pull, you must set a non-zero queue capacity upfront.
            var client = new EasyDAClient { PullItemChangedQueueCapacity = 1000 };

            Console.WriteLine("Subscribing item changes...");
            client.SubscribeItem(
                "http://opcxml.demo-this.com/XmlDaSampleServer/Service.asmx",
                "Dynamic/Analog Types/Int",
                1000,
                state: null);

            Console.WriteLine("Processing item changes for 1 minute...");
            int endTick = Environment.TickCount + 60 * 1000;
            do
            {
                EasyDAItemChangedEventArgs eventArgs = client.PullItemChanged(2 * 1000);
                if (!(eventArgs is null))
                    // Handle the notification event
                    Console.WriteLine(eventArgs);
            } while (Environment.TickCount < endTick);

            Console.WriteLine("Unsubscribing item changes...");
            client.UnsubscribeAllItems();

            Console.WriteLine("Finished.");
        }
    }
}
#endregion
