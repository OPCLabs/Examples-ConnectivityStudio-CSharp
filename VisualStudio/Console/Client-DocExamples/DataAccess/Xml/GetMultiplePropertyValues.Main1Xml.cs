// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable PossibleNullReferenceException
// ReSharper disable ConvertIfStatementToConditionalTernaryExpression
#region Example
// This example shows how to get value of multiple OPC XML-DA properties, and handle errors.
//
// Note that some properties may not have a useful value initially (e.g. until the item is activated in a group), which also the
// case with Timestamp property as implemented by the demo server. This behavior is server-dependent, and normal. You can run 
// IEasyDAClient.ReadMultipleItemValues.Main.vbs shortly before this example, in order to obtain better property values. Your 
// code may also subscribe to the items in order to assure that they remain active.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.BaseLib.OperationModel;
using OpcLabs.EasyOpc;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.OperationModel;

namespace DocExamples.DataAccess.Xml
{
    partial class GetMultiplePropertyValues
    {
        public static void Main1Xml()
        {
            // Instantiate the client object.
            var client = new EasyDAClient();

            ServerDescriptor serverDescriptor = "http://opcxml.demo-this.com/XmlDaSampleServer/Service.asmx";

            // Get the values of Timestamp and AccessRights properties of two items.
            ValueResult[] results = client.GetMultiplePropertyValues(new[]
            {
                new DAPropertyArguments(serverDescriptor, "Dynamic/Analog Types/Int", DAPropertyDescriptor.Timestamp),
                new DAPropertyArguments(serverDescriptor, "Dynamic/Analog Types/Int", DAPropertyDescriptor.AccessRights),
                new DAPropertyArguments(serverDescriptor, "Static/Analog Types/Double", DAPropertyDescriptor.Timestamp),
                new DAPropertyArguments(serverDescriptor, "Static/Analog Types/Double", DAPropertyDescriptor.AccessRights)
            });

            for (int i = 0; i < results.Length; i++)
            {
                ValueResult valueResult = results[i];
                if (valueResult.Exception is null)
                    Console.WriteLine($"results({i}).Value: {valueResult.Value}");
                else
                    Console.WriteLine($"results({i}).Exception.Message: {valueResult.Exception.Message}");
            }
        }
    }
}
#endregion
