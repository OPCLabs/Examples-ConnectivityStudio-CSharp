// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
#region Example
// This example shows how to unsubscribe from changes of multiple items.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Threading;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.OperationModel;

namespace DocExamples.DataAccess._EasyDAClient
{
    class UnsubscribeMultipleItems
    {
        public static void Main1()
        {
            // Instantiate the client object.
            using (var client = new EasyDAClient())
            {
                client.ItemChanged += client_ItemChanged;

                Console.WriteLine("Subscribing...");
                int[] handleArray = client.SubscribeMultipleItems(
                    new[] {
                            new DAItemGroupArguments("", "OPCLabs.KitServer.2", "Simulation.Random", 1000, null), 
                            new DAItemGroupArguments("", "OPCLabs.KitServer.2", "Trends.Ramp (1 min)", 1000, null), 
                            new DAItemGroupArguments("", "OPCLabs.KitServer.2", "Trends.Sine (1 min)", 1000, null),  
                            new DAItemGroupArguments("", "OPCLabs.KitServer.2", "Simulation.Register_I4", 1000, null)
                        });

                for (int i = 0; i < handleArray.Length; i++)
                    Console.WriteLine($"handleArray({i}): {handleArray[i]}");

                Console.WriteLine("Processing item changed events for 10 seconds...");
                Thread.Sleep(10 * 1000);

                Console.WriteLine("Unsubscribing from two items...");
                client.UnsubscribeMultipleItems(new [] {handleArray[1], handleArray[2]});

                Console.WriteLine("Processing item changed events for 10 seconds...");
                Thread.Sleep(10 * 1000);

                Console.WriteLine("Unsubscribing from all remaining items...");
                client.UnsubscribeAllItems();

                Console.WriteLine("Waiting for 5 seconds...");
                Thread.Sleep(5 * 1000);
            }
        }

        // Item changed event handler
        static void client_ItemChanged(object sender, EasyDAItemChangedEventArgs e)
        {
            if (e.Succeeded)
                Console.WriteLine("{0}: {1}", e.Arguments.ItemDescriptor.ItemId, e.Vtq);
            else
                Console.WriteLine("{0} *** Failure: {1}", e.Arguments.ItemDescriptor.ItemId, e.ErrorMessageBrief);
        }


        // Example output:
        //
        //Subscribing...
        //handleArray(0): 12586002
        //handleArray(1) : 12586004
        //handleArray(2) : 12586005
        //handleArray(3) : 12586006
        //Processing item changed events for 10 seconds...
        //Simulation.Register_I4: 0 {System.Int32} @1601-01-01T00:00:00.000; GoodNonspecific(192)
        //Trends.Ramp(1 min): 0.585198730230331 {System.Double} @2020-04-10T15:50:35.111; GoodNonspecific(192)
        //Simulation.Random: 0.00125125888851588 {System.Double} @2020-04-10T15:50:35.111; GoodNonspecific(192)
        //Trends.Sine(1 min): -0.51011579179648 {System.Double} @2020-04-10T15:50:35.111; GoodNonspecific(192)
        //Trends.Ramp(1 min): 0.601875007152557 {System.Double} @2020-04-10T15:50:36.112; GoodNonspecific(192)
        //Simulation.Random: 0.56358531449324 {System.Double} @2020-04-10T15:50:36.112; GoodNonspecific(192)
        //Trends.Sine(1 min): -0.597275285519755 {System.Double} @2020-04-10T15:50:36.112; GoodNonspecific(192)
        //Trends.Ramp(1 min): 0.618548572063446 {System.Double} @2020-04-10T15:50:37.112; GoodNonspecific(192)
        //Simulation.Random: 0.193304239020966 {System.Double} @2020-04-10T15:50:37.112; GoodNonspecific(192)
        //Trends.Sine(1 min): -0.677870836926452 {System.Double} @2020-04-10T15:50:37.112; GoodNonspecific(192)
        //Trends.Ramp(1 min): 0.635226935148239 {System.Double} @2020-04-10T15:50:38.113; GoodNonspecific(192)
        //Simulation.Random: 0.808740501113926 {System.Double} @2020-04-10T15:50:38.113; GoodNonspecific(192)
        //Trends.Sine(1 min): -0.751053255223256 {System.Double} @2020-04-10T15:50:38.113; GoodNonspecific(192)
        //Trends.Ramp(1 min): 0.65190464258194 {System.Double} @2020-04-10T15:50:39.114; GoodNonspecific(192)
        //Simulation.Random: 0.58500930814539 {System.Double} @2020-04-10T15:50:39.114; GoodNonspecific(192)
        //Trends.Sine(1 min): -0.815993052494079 {System.Double} @2020-04-10T15:50:39.114; GoodNonspecific(192)
        //Trends.Ramp(1 min): 0.668579548597336 {System.Double} @2020-04-10T15:50:40.114; GoodNonspecific(192)
        //Simulation.Random: 0.47987304300058 {System.Double} @2020-04-10T15:50:40.114; GoodNonspecific(192)
        //Trends.Sine(1 min): -0.87197220432209 {System.Double} @2020-04-10T15:50:40.114; GoodNonspecific(192)
        //Trends.Ramp(1 min): 0.68526017665863 {System.Double} @2020-04-10T15:50:41.115; GoodNonspecific(192)
        //Simulation.Random: 0.350291451765496 {System.Double} @2020-04-10T15:50:41.115; GoodNonspecific(192)
        //Trends.Sine(1 min): -0.918402631917152 {System.Double} @2020-04-10T15:50:41.115; GoodNonspecific(192)
        //Trends.Ramp(1 min): 0.701931446790695 {System.Double} @2020-04-10T15:50:42.115; GoodNonspecific(192)
        //Simulation.Random: 0.895962401196326 {System.Double} @2020-04-10T15:50:42.115; GoodNonspecific(192)
        //Trends.Sine(1 min): -0.954736510704127 {System.Double} @2020-04-10T15:50:42.115; GoodNonspecific(192)
        //Trends.Ramp(1 min): 0.718607157468796 {System.Double} @2020-04-10T15:50:43.116; GoodNonspecific(192)
        //Simulation.Random: 0.822840052491836 {System.Double} @2020-04-10T15:50:43.116; GoodNonspecific(192)
        //Trends.Sine(1 min): -0.98060979065431 {System.Double} @2020-04-10T15:50:43.116; GoodNonspecific(192)
        //Trends.Ramp(1 min): 0.735284030437469 {System.Double} @2020-04-10T15:50:44.117; GoodNonspecific(192)
        //Simulation.Random: 0.746604815820795 {System.Double} @2020-04-10T15:50:44.117; GoodNonspecific(192)
        //Trends.Sine(1 min): -0.995728326344284 {System.Double} @2020-04-10T15:50:44.117; GoodNonspecific(192)
        //Unsubscribing from two items...
        //Processing item changed events for 10 seconds...
        //Simulation.Random: 0.174108096560564 { System.Double} @2020-04-10T15:50:45.117; GoodNonspecific(192)
        //Simulation.Random: 0.858943449201941 {System.Double} @2020-04-10T15:50:46.118; GoodNonspecific(192)
        //Simulation.Random: 0.710501419110691 {System.Double} @2020-04-10T15:50:47.118; GoodNonspecific(192)
        //Simulation.Random: 0.513534958952605 {System.Double} @2020-04-10T15:50:48.120; GoodNonspecific(192)
        //Simulation.Random: 0.303994872890408 {System.Double} @2020-04-10T15:50:49.120; GoodNonspecific(192)
        //Simulation.Random: 0.0149845881527146 {System.Double} @2020-04-10T15:50:50.121; GoodNonspecific(192)
        //Simulation.Random: 0.0914029358806116 {System.Double} @2020-04-10T15:50:51.122; GoodNonspecific(192)
        //Simulation.Random: 0.364452040162358 {System.Double} @2020-04-10T15:50:52.124; GoodNonspecific(192)
        //Simulation.Random: 0.147312845240638 {System.Double} @2020-04-10T15:50:53.125; GoodNonspecific(192)
        //Unsubscribing from all remaining items...
        //Waiting for 5 seconds...
    }
}
#endregion
