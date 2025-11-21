// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
#region Example
// This example shows how change the update rate of an existing OPC XML-DA subscription.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Threading;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.OperationModel;

namespace DocExamples.DataAccess.Xml
{
    class ChangeItemSubscription
    {
        public static void Main1Xml()
        {
            // Instantiate the client object.
            using (var client = new EasyDAClient())
            {
                client.ItemChanged += client_ItemChanged;

                Console.WriteLine("Subscribing...");
                int handle = client.SubscribeItem(
                    "http://opcxml.demo-this.com/XmlDaSampleServer/Service.asmx",
                    "Dynamic/Analog Types/Int",
                    2000,
                    state: null);

                Console.WriteLine("Waiting for 20 seconds...");
                Thread.Sleep(20 * 1000);

                Console.WriteLine("Changing subscription...");
                client.ChangeItemSubscription(handle, new DAGroupParameters(500));

                Console.WriteLine("Waiting for 10 seconds...");
                Thread.Sleep(10 * 1000);

                Console.WriteLine("Unsubscribing...");
                client.UnsubscribeAllItems();

                Console.WriteLine("Waiting for 10 seconds...");
                Thread.Sleep(10 * 1000);
            }
        }

        // Item changed event handler
        static void client_ItemChanged(object sender, EasyDAItemChangedEventArgs e)
        {
            if (e.Succeeded)
                Console.WriteLine(e.Vtq);
            else
                Console.WriteLine("*** Failure: {0}", e.ErrorMessageBrief);
        }
    }
}
#endregion
