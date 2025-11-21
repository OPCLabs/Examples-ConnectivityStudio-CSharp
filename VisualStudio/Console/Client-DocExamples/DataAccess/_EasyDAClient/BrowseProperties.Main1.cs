// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
#region Example
// This example shows how to enumerate all properties of an OPC item. For each property, it displays its Id and description.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.AddressSpace;
using OpcLabs.EasyOpc.OperationModel;

namespace DocExamples.DataAccess._EasyDAClient
{
    class BrowseProperties
    {
        public static void Main1()
        {
            // Instantiate the client object.
            var client = new EasyDAClient();
            DAPropertyElementCollection propertyElements;
            try
            {
                propertyElements = client.BrowseProperties("OPCLabs.KitServer.2", "Simulation.Random");
            }
            catch (OpcException opcException)
            {
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message);
                return;
            }

            foreach (DAPropertyElement propertyElement in propertyElements)
                Console.WriteLine($"PropertyElements(\"{propertyElement.PropertyId.NumericalValue}\").Description: {propertyElement.Description}");
        }


        // Example output:
        //
        //PropertyElements("15008").Description: Visible
        //PropertyElements("5").Description: Item Access Rights
        //PropertyElements("2").Description: Item Value
        //PropertyElements("7").Description: Item EU Type
        //PropertyElements("15001").Description: Item Name
        //PropertyElements("4").Description: Item Timestamp
        //PropertyElements("1").Description: Item Canonical Data Type
        //PropertyElements("103").Description: Low EU
        //PropertyElements("15009").Description: Addable
        //PropertyElements("6").Description: Server Scan Rate
        //PropertyElements("15000").Description: Item ID
        //PropertyElements("3").Description: Item Quality
    }
}
#endregion
