// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
#region Example
// This example shows how to work with Software Tolbox TOP Server 5 Alarms and Events.
// Use simdemo_WithA&E.opf configuration file and write a value above 1000 to Channel1.Device1.Tag1 or Channel1.Device1.Tag2.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Diagnostics;
using System.Threading;
using OpcLabs.EasyOpc.AlarmsAndEvents;
using OpcLabs.EasyOpc.AlarmsAndEvents.AddressSpace;
using OpcLabs.EasyOpc.AlarmsAndEvents.OperationModel;
using OpcLabs.EasyOpc.OperationModel;

namespace DocExamples.AlarmsAndEvents.SWToolbox
{
    class TOPServer_AE
    {
        public static void Main1()
        {
            // Instantiate the client object.
            var client = new EasyAEClient();

            // Browse for some areas and sources

            AENodeElementCollection areaElements;
            try
            {
                areaElements = client.BrowseAreas("", "SWToolbox.TOPServer_AE.V5", "");
            }
            catch (OpcException opcException)
            {
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message);
                return;
            }

            foreach (AENodeElement areaElement in areaElements)
            {
                Debug.Assert(!(areaElement is null));
                Debug.Assert(!(areaElement.QualifiedName is null));

                Console.WriteLine("areaElements[\"{0}\"]:", areaElement.Name);
                Console.WriteLine("    .QualifiedName: {0}", areaElement.QualifiedName);

                AENodeElementCollection sourceElements =
                    client.BrowseSources("", "SWToolbox.TOPServer_AE.V5", areaElement.QualifiedName);
                foreach (AENodeElement sourceElement in sourceElements)
                {
                    Debug.Assert(sourceElement != null);

                    Console.WriteLine("    sourceElements[\"{0}\"]:", sourceElement.Name);
                    Console.WriteLine("        .QualifiedName: {0}", sourceElement.QualifiedName);
                }
            }

            // Query for event categories

            AECategoryElementCollection categoryElements;
            try
            {
                categoryElements = client.QueryEventCategories("", "SWToolbox.TOPServer_AE.V5");
            }
            catch (OpcException opcException)
            {
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message);
                return;
            }

            foreach (AECategoryElement categoryElement in categoryElements)
            {
                Debug.Assert(categoryElement != null);
                Console.WriteLine("CategoryElements[\"{0}\"].Description: {1}", categoryElement.CategoryId,
                                  categoryElement.Description);
            }

            // Subscribe to events, wait, and unsubscribe

            var eventHandler = new EasyAENotificationEventHandler(client_Notification);
            client.Notification += eventHandler;

            int handle = client.SubscribeEvents("", "SWToolbox.TOPServer_AE.V5", 1000);

            Console.WriteLine("Processing event notifications for 1 minute...");
            Thread.Sleep(60 * 1000);

            client.UnsubscribeEvents(handle);
        }

        // Notification event handler
        static void client_Notification(object sender, EasyAENotificationEventArgs e)
        {
            if (!(e.Exception is null)) Console.WriteLine("e.Exception.Message: {0}", e.Exception.Message);
            if (!(e.Exception is null)) Console.WriteLine("e.Exception.Source: {0}", e.Exception.Source);
            Console.WriteLine("e.Refresh: {0}", e.Refresh);
            Console.WriteLine("e.RefreshComplete: {0}", e.RefreshComplete);
            if (!(e.EventData is null)) Console.WriteLine("e.EventData.QualifiedSourceName: {0}", e.EventData.QualifiedSourceName);
            if (!(e.EventData is null)) Console.WriteLine("e.EventData.Time: {0}", e.EventData.Time);
            if (!(e.EventData is null)) Console.WriteLine("e.EventData.Message: {0}", e.EventData.Message);
            if (!(e.EventData is null)) Console.WriteLine("e.EventData.EventType: {0}", e.EventData.EventType);
            if (!(e.EventData is null)) Console.WriteLine("e.EventData.CategoryId: {0}", e.EventData.CategoryId);
            if (!(e.EventData is null)) Console.WriteLine("e.EventData.Severity: {0}", e.EventData.Severity);
            if (!(e.EventData is null)) Console.WriteLine("e.EventData.ConditionName: {0}", e.EventData.ConditionName);
            if (!(e.EventData is null)) Console.WriteLine("e.EventData.SubconditionName: {0}", e.EventData.SubconditionName);
            if (!(e.EventData is null)) Console.WriteLine("e.EventData.Enabled: {0}", e.EventData.Enabled);
            if (!(e.EventData is null)) Console.WriteLine("e.EventData.Active: {0}", e.EventData.Active);
            if (!(e.EventData is null)) Console.WriteLine("e.EventData.Acknowledged: {0}", e.EventData.Acknowledged);
            if (!(e.EventData is null)) Console.WriteLine("e.EventData.Quality: {0}", e.EventData.Quality);
            if (!(e.EventData is null)) Console.WriteLine("e.EventData.AcknowledgeRequired: {0}", e.EventData.AcknowledgeRequired);
            if (!(e.EventData is null)) Console.WriteLine("e.EventData.ActiveTime: {0}", e.EventData.ActiveTime);
        }
    }
}
#endregion
