// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
#region Example
// This example shows information available about OPC event category.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Diagnostics;
using OpcLabs.EasyOpc.AlarmsAndEvents;
using OpcLabs.EasyOpc.AlarmsAndEvents.AddressSpace;
using OpcLabs.EasyOpc.OperationModel;

namespace DocExamples.AlarmsAndEvents._AECategoryElement 
{ 
    class Properties 
    { 
        public static void Main1()
        {
            // Instantiate the client object.
            var client = new EasyAEClient();

            AECategoryElementCollection categoryElements;
            try
            {
                categoryElements = client.QueryEventCategories("", "OPCLabs.KitEventServer.2");
            }
            catch (OpcException opcException)
            {
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message);
                return;
            }

            foreach (AECategoryElement categoryElement in categoryElements)
            {
                Debug.Assert(!(categoryElement is null));

                Console.WriteLine("Information about category {0}:", categoryElement);
                Console.WriteLine("    .CategoryId: {0}", categoryElement.CategoryId);
                Console.WriteLine("    .Description: {0}", categoryElement.Description);
                Console.WriteLine("    .ConditionElements:");
                if (!(categoryElement.ConditionElements.Keys is null))
                    foreach (string conditionKey in categoryElement.ConditionElements.Keys)
                        Console.WriteLine("        {0}", conditionKey);
                Console.WriteLine("    .AttributeElements:");
                if (!(categoryElement.AttributeElements.Keys is null))
                    foreach (long attributeKey in categoryElement.AttributeElements.Keys)
                        Console.WriteLine("        {0}", attributeKey);
            }
        }
    } 
}
#endregion
