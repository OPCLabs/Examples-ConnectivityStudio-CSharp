// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable InconsistentNaming
// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to read a range of values from an array.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UACommonDocExamples._UAIndexRangeList
{
    partial class Usage
    {
        public static void ReadValue()
        {
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client object
            var client = new EasyUAClient();

            // Obtain the value, indicating that just the elements 2 to 4 should be returned
            object value;
            try
            {
                value = client.ReadValue(
                    new UAReadArguments(
                        endpointDescriptor,
                        "nsu=http://test.org/UA/Data/ ;ns=2;i=10305",   // /Data.Static.Array.Int32Value
                        UAIndexRangeList.OneDimension(2, 4)));
            }
            catch (UAException uaException)
            {
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
                return;
            }

            // Cast to typed array
            var arrayValue = (Int32[]) value;

            // Display results
            for (int i = 0; i < 3; i++)
                Console.WriteLine("arrayValue[{0}]: {1}", i, arrayValue[i]);


            // Example output:
            //arrayValue[0]: 180410224
            //arrayValue[1]: 1919239969
            //arrayValue[2]: 1700185172
        }
    }
}
#endregion
