// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
// ReSharper disable CheckNamespace
#region Example 
// This example shows how to get a value of a single OPC XML-DA property.
//
// Note that some properties may not have a useful value initially (e.g. until the item is activated in a group), which also the
// case with Timestamp property as implemented by the demo server. This behavior is server-dependent, and normal. You can run 
// IEasyDAClient.ReadItemValue.Main.vbs shortly before this example, in order to obtain better property values. Your code may 
// also subscribe to the item in order to assure that it remains active.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.OperationModel;

namespace DocExamples.DataAccess.Xml
{
    partial class GetPropertyValue
    {
        public static void Main1Xml()
        {
            // Instantiate the client object.
            var client = new EasyDAClient();

            object value;
            try
            {
                value = client.GetPropertyValue("http://opcxml.demo-this.com/XmlDaSampleServer/Service.asmx", "Dynamic/Analog Types/Int",
                    DAPropertyDescriptor.Timestamp);
            }
            catch (OpcException opcException)
            {
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message);
                return;
            }

            Console.WriteLine(value);
        }
    }
}
#endregion
