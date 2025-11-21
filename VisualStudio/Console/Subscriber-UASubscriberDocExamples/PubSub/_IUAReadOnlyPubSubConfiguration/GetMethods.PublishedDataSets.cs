// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

#pragma warning disable IDE1006 // Naming Styles
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable PossibleNullReferenceException
#region Example
// This example obtains and prints out information about all published datasets in the OPC UA PubSub configuration.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.BaseLib.Collections.Specialized;
using OpcLabs.EasyOpc.UA.OperationModel;
using OpcLabs.EasyOpc.UA.PubSub.Configuration;
using OpcLabs.EasyOpc.UA.PubSub.Configuration.Extensions;
using OpcLabs.EasyOpc.UA.PubSub.InformationModel;
using OpcLabs.EasyOpc.UA.PubSub.InformationModel.Extensions;

namespace UASubscriberDocExamples.PubSub._IUAReadOnlyPubSubConfiguration
{
    partial class GetMethods
    {
        public static void PublishedDataSets()
        {
            // Instantiate the publish-subscribe client object.
            var publishSubscribeClient = new EasyUAPublishSubscribeClient();

            try
            {
                Console.WriteLine("Loading the configuration...");
                // Load the PubSub configuration from a file. The file itself is at the root of the project, and we have
                // specified that it has to be copied to the project's output directory.
                IUAReadOnlyPubSubConfiguration pubSubConfiguration = 
                    publishSubscribeClient.LoadReadOnlyConfiguration("UADemoPublisher-Default.uabinary");

                // Alternatively, using the statement below, you can access a live configuration residing in an OPC UA
                // Server with appropriate information model.
                //IUAReadOnlyPubSubConfiguration pubSubConfiguration =
                //    publishSubscribeClient.AccessReadOnlyConfiguration("opc.tcp://localhost:48010");

                // Get the names of all published datasets in the PubSub configuration.
                StringCollection publishedDataSetNames = pubSubConfiguration.ListAllPublishedDataSetNames();

                foreach (string publishedDataSetName in publishedDataSetNames)
                {
                    Console.WriteLine($"Published dataset: {publishedDataSetName}");

                    // You can use the statement below to obtain parameters of the published dataset.
                    //UAPublishedDataSetElement publishedDataSetElement = 
                    //    pubSubConfiguration.GetPublishedDataSetElement(publishedDataSetName);
                }
            }
            catch (UAException uaException)
            {
                Console.WriteLine($"*** Failure: {uaException.InnerException.Message}");
            }

            Console.WriteLine("Finished.");
        }

        // Example output:
        //
        //Loading the configuration...
        //Published dataset: Simple
        //Published dataset: AllTypes
        //Published dataset: MassTest
        //Published dataset: AllTypes-Dynamic
        //Finished.
    }
}
#endregion
