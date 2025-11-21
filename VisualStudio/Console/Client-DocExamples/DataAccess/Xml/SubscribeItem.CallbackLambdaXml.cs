// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
#region Example
// This example shows how subscribe to changes of a single item in an OPC XML-DA server and display the value of the item 
// with each change, using a callback method specified using lambda expression.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Threading;
using System.Diagnostics;
using OpcLabs.EasyOpc.DataAccess;

namespace DocExamples.DataAccess.Xml
{
    partial class SubscribeItem
    {
        public static void CallbackLambdaXml()
        {
            // Instantiate the client object.
            var client = new EasyDAClient();

            Console.WriteLine("Subscribing item...");
            // The callback is a lambda expression the displays the value
            client.SubscribeItem(
                "http://opcxml.demo-this.com/XmlDaSampleServer/Service.asmx",
                "Dynamic/Analog Types/Int",
                1000,
                (sender, eventArgs) =>
                {
                    Debug.Assert(eventArgs != null);

                    if (eventArgs.Succeeded)
                    {
                        Debug.Assert(eventArgs.Vtq != null);
                        Console.WriteLine(eventArgs.Vtq.ToString());
                    }
                    else
                        Console.WriteLine($"*** Failure: {eventArgs.ErrorMessageBrief}");
                },
                state: null);

            Console.WriteLine("Processing item changed events for 30 seconds...");
            Thread.Sleep(30 * 1000);

            Console.WriteLine("Unsubscribing items...");
            client.UnsubscribeAllItems();

            Console.WriteLine("Waiting for 2 seconds...");
            Thread.Sleep(2 * 1000);

            Console.WriteLine("Finished.");
        }
    }
}
#endregion
