// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable UnusedAutoPropertyAccessor.Local
#region Example
// This example for OPC UA type-less mapping shows how to define a mapping and perform a read operation.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using OpcLabs.BaseLib.ComponentModel.Linking;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.LiveMapping;

namespace UADocExamples._UAClientMapper
{
    class DefineMapping
    {
        class MyClass2
        {
            public object Value { get; set; }
        }

        public static void Main1()
        {
#region Example-DefineAndRead

            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            var mapper = new UAClientMapper();
            var target = new MyClass2();

            // Define a type-less mapping.

            MemberInfo memberInfo = target.GetType().GetMember("Value").SingleOrDefault();
            Debug.Assert(memberInfo != null);

            mapper.DefineMapping(
                new UAClientDataMappingSource(
                    endpointDescriptor,
                    "nsu=http://test.org/UA/Data/ ;i=10389",
                    UAAttributeId.Value,
                    UAIndexRangeList.Empty,
                    UAReadParameters.CacheMaximumAge),
                new UAClientDataMapping(typeof(Int32)),
                new ObjectMemberLinkingTarget(target.GetType(), target, memberInfo));

            // Perform a read operation.
            mapper.Read();
#endregion 

            // Display the result.
            Console.WriteLine(target.Value);
        }
    }
}
#endregion
