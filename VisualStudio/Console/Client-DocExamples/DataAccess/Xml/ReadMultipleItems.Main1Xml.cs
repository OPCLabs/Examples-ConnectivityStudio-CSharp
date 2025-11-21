// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
#region Example
// This example shows how to read 4 items from an OPC XML-DA server at once, and display their values, timestamps 
// and qualities.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Diagnostics;
using OpcLabs.EasyOpc;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.OperationModel;

namespace DocExamples.DataAccess.Xml
{
    partial class ReadMultipleItems
    {
        public static void Main1Xml()
        {
            // Instantiate the client object.
            var client = new EasyDAClient();

            DAVtqResult[] vtqResults = client.ReadMultipleItems(
                new ServerDescriptor { UrlString = "http://opcxml.demo-this.com/XmlDaSampleServer/Service.asmx" },
                new DAItemDescriptor[]
                    {
                        "Dynamic/Analog Types/Double", 
                        "Dynamic/Analog Types/Double[]", 
                        "Dynamic/Analog Types/Int", 
                        "SomeUnknownItem"
                    });

            for (int i = 0; i < vtqResults.Length; i++)
            {
                Debug.Assert(vtqResults[i] != null);

                if (!(vtqResults[i].Exception is null))
                {
                    Console.WriteLine($"vtqResults[{i}] *** Failure: {vtqResults[i].ErrorMessageBrief}");
                    continue;
                }
                Console.WriteLine($"vtqResults[{i}].Vtq: {vtqResults[i].Vtq}");
            }
        }
    }
}
#endregion
