// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
// ReSharper disable CheckNamespace
// ReSharper disable ConvertIfStatementToConditionalTernaryExpression
#region Example
// This example shows how to read values of 4 items at once, and display them.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Diagnostics;
using OpcLabs.BaseLib.OperationModel;
using OpcLabs.EasyOpc.DataAccess;

namespace DocExamples.DataAccess.Xml
{
    class ReadMultipleItemValues
    {
        public static void Main1Xml()
        {
            // Instantiate the client object.
            var client = new EasyDAClient();

            ValueResult[] valueResults = client.ReadMultipleItemValues("http://opcxml.demo-this.com/XmlDaSampleServer/Service.asmx",
                new DAItemDescriptor[]
                {
                    "Dynamic/Analog Types/Double", "Dynamic/Analog Types/Double[]", "Dynamic/Analog Types/Int", "Static/Analog Types/Int"
                });

            for (int i = 0; i < valueResults.Length; i++)
            {
                ValueResult valueResult = valueResults[i];
                Debug.Assert(!(valueResult is null));

                if (valueResult.Succeeded)
                    Console.WriteLine($"valueResults[{i}].Value: {valueResult.Value}");
                else
                    Console.WriteLine($"valueResults[{i}] *** Failure: {valueResult.ErrorMessageBrief}");
            }
        }


        // Example output:
        //
        //valueResults[0].Value: 50
        //valueResults[1].Value: System.Double[]
        //valueResults[2].Value: 100
        //valueResults[3].Value: 12345
    }
}
#endregion
