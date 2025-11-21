// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable UnusedMember.Local
#region Example
// This example for OPC DA type-less mapping shows how to define a mapping and perform subscribe and unsubscribe operations.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Threading;
using OpcLabs.BaseLib.ComponentModel.Linking;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.LiveMapping;

namespace DocExamples.DataAccess._DAClientMapper
{
    partial class DefineMapping
    {
        class MyClassSubscribe
        {
            public Double Value
            {
                set
                {
                    // Display the incoming value
                    Console.WriteLine(value);
                }
            }
        }

        public static void Subscribe()
        {
            // Instantiate the client mapper object.
            var mapper = new DAClientMapper();

            var target = new MyClassSubscribe();

            // Define a type-less mapping.

            mapper.DefineMapping(
                 new DAClientItemSource("OPCLabs.KitServer.2", "Demo.Ramp", 1000, DADataSource.Cache),
                 new DAClientItemMapping(typeof(Double)),
                 new ObjectMemberLinkingTarget(target.GetType(), target, "Value"));

            // Perform a subscribe operation.
            mapper.Subscribe(true);

            Thread.Sleep(30 * 1000);

            // Perform an unsubscribe operation.
            mapper.Subscribe(false);
        }
    }
}
#endregion
