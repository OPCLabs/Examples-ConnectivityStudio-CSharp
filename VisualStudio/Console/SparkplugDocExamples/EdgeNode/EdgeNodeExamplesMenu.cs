// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable LocalizableElement
// ReSharper disable RedundantCommaInArrayInitializer

using System;
using System.Collections.Generic;
using OpcLabs.BaseLib.Console;

namespace SparkplugDocExamples.EdgeNode
{
    static class EdgeNodeExamplesMenu
    {
        public static void Main1()
        {
            var actionArray = new Action[] {
                _EasySparkplugEdgeNode.AutoConnectSystem.Main1,
                _EasySparkplugEdgeNode.Construction.Main1,
                _EasySparkplugEdgeNode.DataSourceConnectionMode.Main1,
                _EasySparkplugEdgeNode.Dispose.Main1,
                _EasySparkplugEdgeNode.DisposableLockPublishing.Main1,
                _EasySparkplugEdgeNode.OnRead.Main1,
                _EasySparkplugEdgeNode.OnWrite.Main1,
                _EasySparkplugEdgeNode.PrimaryHostId.Main1,
                _EasySparkplugEdgeNode.PublishingError.Main1,
                _EasySparkplugEdgeNode.PublishingInterval.Main1,
                _EasySparkplugEdgeNode.Read.Main1,
                _EasySparkplugEdgeNode.ReportByException.Main1,
                _EasySparkplugEdgeNode.Start_Stop.Authentication,
                _EasySparkplugEdgeNode.Start_Stop.ClientId,
                _EasySparkplugEdgeNode.Start_Stop.Main1,
                _EasySparkplugEdgeNode.Start_Stop.Mqtt5,
                _EasySparkplugEdgeNode.Start_Stop.Simplest,
                _EasySparkplugEdgeNode.Start_Stop.Tls,
                _EasySparkplugEdgeNode.Start_Stop.WebSocket,
                _EasySparkplugEdgeNode.SystemConnectionParameters.Main1,
                _EasySparkplugEdgeNode.Write.Main1,
                
                _SparkplugDevice.ConnectDataSource.Main1,
                
                _SparkplugMetric.ConstantValue.Main1,
                _SparkplugMetric.CreateIn.Main1,
                _SparkplugMetric.ReadData.Main1,
                _SparkplugMetric.ReadFunction.Main1,
                _SparkplugMetric.ReadValueFunction.Array,
                _SparkplugMetric.ReadValueFunction.Bytes,
                _SparkplugMetric.ReadValueFunction.Main1,
                _SparkplugMetric.ReadValueFunction.UInt16,
                _SparkplugMetric.ReadWrite.Main1,
                _SparkplugMetric.ReadWriteValue.Array,
                _SparkplugMetric.ReadWriteValue.Bytes,
                _SparkplugMetric.ReadWriteValue.FullyWritable,
                _SparkplugMetric.ReadWriteValue.Main1,
                _SparkplugMetric.Starting_Stopped.Main1,
                _SparkplugMetric.UpdateReadData.Main1,
                _SparkplugMetric.WriteData.Main1,
                _SparkplugMetric.WriteFunction.Main1,
                _SparkplugMetric.WriteValueAction.Bytes,
                _SparkplugMetric.WriteValueAction.Main1,
                _SparkplugMetric.WriteValueAction.UInt16,
                _SparkplugMetric.WriteValueAction.WriteOnly1,
                _SparkplugMetric.WriteValueFunction.Main1,

                _SparkplugProducerMonitoring.EdgeNodeAndDevices.Main1,
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
