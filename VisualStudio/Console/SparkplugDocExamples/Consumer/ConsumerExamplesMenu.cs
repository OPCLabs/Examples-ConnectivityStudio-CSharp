// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable LocalizableElement
// ReSharper disable RedundantCommaInArrayInitializer

using System;
using System.Collections.Generic;
using OpcLabs.BaseLib.Console;

namespace SparkplugDocExamples.Consumer
{
    static class ConsumerExamplesMenu
    {
        public static void Main1()
        {
            var actionArray = new Action[] 
            {
                _EasySparkplugConsumer.DeliverCompleteDataSet.Main1,
                _EasySparkplugConsumer.ImplicitNodeDescriptor.Main1,
                _EasySparkplugConsumer.PublishDeviceMetric.Bytes,
                _EasySparkplugConsumer.PublishDeviceMetric.DataType,
                _EasySparkplugConsumer.PublishDeviceMetric.Int32Array,
                _EasySparkplugConsumer.PublishDeviceMetric.Overload1,
                _EasySparkplugConsumer.PublishEdgeNodeMetric.Incrementing,
                _EasySparkplugConsumer.PublishEdgeNodeMetric.Overload1,
                _EasySparkplugConsumer.PublishEdgeNodeMetric.Timestamp,
                _EasySparkplugConsumer.PublishEdgeNodePayload.Overload1,
                _EasySparkplugConsumer.SubscribeDeviceMetric.Overload1,
                _EasySparkplugConsumer.SubscribeEdgeNodeMetric.Authentication,
                _EasySparkplugConsumer.SubscribeEdgeNodeMetric.CallbackLambda,
                _EasySparkplugConsumer.SubscribeEdgeNodeMetric.ClientId,
                _EasySparkplugConsumer.SubscribeEdgeNodeMetric.Mqtt5,
                _EasySparkplugConsumer.SubscribeEdgeNodeMetric.Overload1,
                _EasySparkplugConsumer.SubscribeEdgeNodeMetric.Simplest,
                _EasySparkplugConsumer.SubscribeEdgeNodeMetric.Tls,
                _EasySparkplugConsumer.SubscribeEdgeNodeMetric.WebSocket,
                _EasySparkplugConsumer.SubscribeEdgeNodePayload.Overload1,
                _EasySparkplugConsumer.SubscribeMetric.StateAsInteger,
                _EasySparkplugConsumer.UnsubscribeMetric.Main1,

                _EasySparkplugHostApplication.Start_Stop.Main1,
                _EasySparkplugHostApplication.SystemConnectionParameters.Main1,
                
                _SparkplugHostDescriptor.HostId.Main1,
            };

            var actionList = new List<Action>(actionArray);

            do
            {
                Console.WriteLine();
                if (!ConsoleDialog.SelectAndPerformAction("Select action to perform", "Return", actionList))
                    break;

                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
            while (true);
        }
    }
}
