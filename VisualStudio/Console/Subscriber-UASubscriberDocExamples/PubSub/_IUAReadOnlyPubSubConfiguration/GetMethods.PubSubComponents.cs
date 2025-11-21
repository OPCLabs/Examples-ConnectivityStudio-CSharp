// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

#pragma warning disable IDE1006 // Naming Styles
// ReSharper disable PossibleNullReferenceException
#region Example
// This example obtains and prints out information about PubSub connections, writer groups, and dataset writers in the
// OPC UA PubSub configuration.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.BaseLib.Collections.Specialized;
using OpcLabs.EasyOpc.UA.OperationModel;
using OpcLabs.EasyOpc.UA.PubSub.Configuration;
using OpcLabs.EasyOpc.UA.PubSub.InformationModel;
using OpcLabs.EasyOpc.UA.PubSub.InformationModel.Extensions;

namespace UASubscriberDocExamples.PubSub._IUAReadOnlyPubSubConfiguration
{
    partial class GetMethods
    {
        public static void PubSubComponents()
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

                // Alternatively, using the statement below, you can access a live configuration residing in an OPC UA Server
                // with appropriate information model.
                //IUAReadOnlyPubSubConfiguration pubSubConfiguration =
                //    publishSubscribeClient.AccessReadOnlyConfiguration("opc.tcp://localhost:48010");

                // Get the names of PubSub connections in the configuration, regardless of the folder they reside in.
                StringCollection pubSubConnectionNames = pubSubConfiguration.ListConnectionNames();
                foreach (string pubSubConnectionName in pubSubConnectionNames)
                {
                    Console.WriteLine($"PubSub connection: {pubSubConnectionName}");

                    // You can use the statement below to obtain parameters of the PubSub connection.
                    //UAPubSubConnectionElement connectionElement = 
                    //    pubSubConfiguration.GetConnectionElement(pubSubConnectionName);

                    // Get names of the writer groups on this PubSub connection.
                    StringCollection writerGroupNames = pubSubConfiguration.ListWriterGroupNames(pubSubConnectionName);
                    foreach (string writerGroupName in writerGroupNames)
                    {
                        Console.WriteLine($"  Writer group: {writerGroupName}");

                        // You can use the statement below to obtain parameters of the writer group.
                        //UAWriterGroupElement writerGroupElement = 
                        //    pubSubConfiguration.GetWriterGroupElement(pubSubConnectionName, writerGroupName);

                        // Get names of the dataset writers on this writer group.
                        StringCollection dataSetWriterNames =
                            pubSubConfiguration.ListDataSetWriterNames(pubSubConnectionName, writerGroupName);
                        foreach (string dataSetWriterName in dataSetWriterNames)
                        {
                            Console.WriteLine($"    Dataset writer: {dataSetWriterName}");

                            // You can use the statement below to obtain parameters of the dataset writer.
                            //UADataSetWriterElement dataSetWriterElement = pubSubConfiguration.GetDataSetWriterElement(
                            //    pubSubConnectionName, writerGroupName, dataSetWriterName);
                        }
                    }
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
        //PubSub connection: FixedLayoutConnection
        //  Writer group: FixedLayoutGroup
        //    Dataset writer: SimpleWriter
        //    Dataset writer: AllTypesWriter
        //    Dataset writer: MassTestWriter
        //PubSub connection: DynamicLayoutConnection
        //  Writer group: DynamicLayoutGroup
        //    Dataset writer: SimpleWriter
        //    Dataset writer: MassTestWriter
        //    Dataset writer: AllTypes-DynamicWriter
        //    Dataset writer: EventSimpleWriter
        //PubSub connection: FlexibleLayoutConnection
        //  Writer group: FlexibleLayoutGroup
        //    Dataset writer: SimpleWriter
        //    Dataset writer: MassTestWriter
        //    Dataset writer: AllTypes-DynamicWriter
        //Finished.
    }
}
#endregion
