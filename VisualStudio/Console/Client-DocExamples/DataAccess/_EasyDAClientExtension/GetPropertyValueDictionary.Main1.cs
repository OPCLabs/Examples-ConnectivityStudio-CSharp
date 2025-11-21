// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
// ReSharper disable CheckNamespace
// ReSharper disable PossibleNullReferenceException
#region Example 
// This example shows how to obtain a dictionary of OPC property values for an OPC item, and extract property values.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.Extensions;
using OpcLabs.EasyOpc.OperationModel;

namespace DocExamples.DataAccess._EasyDAClientExtension
{
    class GetPropertyValueDictionary
    {
        public static void Main1()
        {
            // Instantiate the client object.
            var client = new EasyDAClient();

            // Get dictionary of property values, for all well-known properties
            DAPropertyValueDictionary propertyValueDictionary;
            try
            {
                propertyValueDictionary = client.GetPropertyValueDictionary("", "OPCLabs.KitServer.2", "Simulation.Random");
            }
            catch (OpcException opcException)
            {
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message);
                return;
            }

            // Display some of the obtained property values
            // The production code should also check for the .Exception first, before getting .Value
            Console.WriteLine("propertyValueDictionary[DAPropertyId.AccessRights].Value: {0}",
                propertyValueDictionary[DAPropertyIds.AccessRights].Value);
            Console.WriteLine("propertyValueDictionary[DAPropertyId.DataType].Value: {0}",
                propertyValueDictionary[DAPropertyIds.DataType].Value);
            Console.WriteLine("propertyValueDictionary[DAPropertyId.Timestamp].Value: {0}",
                propertyValueDictionary[DAPropertyIds.Timestamp].Value);
        }
    }
}
#endregion
