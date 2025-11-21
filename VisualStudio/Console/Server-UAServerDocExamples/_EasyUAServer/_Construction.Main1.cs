// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable InconsistentNaming
// ReSharper disable RedundantExplicitParamsArrayCreation
// ReSharper disable UnusedVariable
#region Example
// This example shows different ways of constructing the EasyUAServer object.
// You can use any OPC UA client, including our Connectivity Explorer and OpcCmd utility, to connect to the server. 
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client, server and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.Engine;
using OpcLabs.EasyOpc.UA.NodeSpace;

namespace UAServerDocExamples._EasyUAServer
{
    class _Construction
    {
        public static void Main1()
        {
            // The toolkit provides a ready-made shared instance of the server object which you can use without even having
            // to construct it. Not recommended for use in library code, because it is a shared instance, and its usage may
            // therefore conflict with other code using the same instance.
            var server0 = EasyUAServer.SharedInstance;


            // The simplest way to construct the server object is to use the default constructor. The server will run on its
            // default endpoint URL "opc.tcp://localhost:48040/".
            var server1 = new EasyUAServer();


            // The server object can be constructed with a specific single endpoint URL string passed as an argument to the
            // constructor.
            var server2 = new EasyUAServer("opc.tcp://localhost:38444");


            // The server object can also be constructed with multiple endpoint URL strings passed as an array to the
            // constructor.
            var server3 = new EasyUAServer(new []
            {
                "opc.tcp://localhost:38444", "opc.tcp://localhost:38445", "opc.tcp://localhost:38446"
            });


            // If the language supports variable number of arguments (such as C# or VB.NET), the multiple endpoint URL
            // strings can be passed to it as separate arguments, instead of having to create an array of them.
            var server4 = new EasyUAServer(
                "opc.tcp://localhost:38444", "opc.tcp://localhost:38445", "opc.tcp://localhost:38446");


            // The server object can be constructed with specific message security modes.
            var server5 = new EasyUAServer(UAMessageSecurityModes.Secure);


            // The message security modes can be combined with the endpoint URL string.
            var server6 = new EasyUAServer(UAMessageSecurityModes.Secure, "opc.tcp://localhost:38444");


            // The message security modes can also be combined with multiple endpoint URL strings in an array.
            var server7 = new EasyUAServer(UAMessageSecurityModes.Secure, new[]
            {
                "opc.tcp://localhost:38444", "opc.tcp://localhost:38445", "opc.tcp://localhost:38446"
            });


            // If the language supports variable number of arguments (such as C# or VB.NET), the message security modes can
            // be combined with multiple endpoint URL strings passed to it as separate arguments, instead of having to create
            // an array of them.
            var server8 = new EasyUAServer(
                UAMessageSecurityModes.Secure, 
                "opc.tcp://localhost:38444", "opc.tcp://localhost:38445", "opc.tcp://localhost:38446");


            // The endpoint can be specified using the Uri object.
            var server9 = new EasyUAServer(new Uri("opc.tcp://localhost:38444"));


            // The server object can also be constructed with multiple Uri objects for server endpoints, passed as an array
            // to the constructor.
            var server10 = new EasyUAServer(new[]
            {
                new Uri("opc.tcp://localhost:38444"), 
                new Uri("opc.tcp://localhost:38445"), 
                new Uri("opc.tcp://localhost:38446")
            });


            // If the language supports variable number of arguments (such as C# or VB.NET), the multiple endpoint Uri
            // objects can be passed to it as separate arguments, instead of having to create an array of them.
            var server11 = new EasyUAServer(
                new Uri("opc.tcp://localhost:38444"),
                new Uri("opc.tcp://localhost:38445"),
                new Uri("opc.tcp://localhost:38446")
            );


            // The message security modes can be combined with the endpoint Uri object.
            var server12 = new EasyUAServer(UAMessageSecurityModes.Secure, new Uri("opc.tcp://localhost:38444"));


            // The message security modes can also be combined with multiple endpoint Uri objects in an array.
            var server13 = new EasyUAServer(UAMessageSecurityModes.Secure, new[]
            {
                new Uri("opc.tcp://localhost:38444"),
                new Uri("opc.tcp://localhost:38445"),
                new Uri("opc.tcp://localhost:38446")
            });


            // If the language supports variable number of arguments (such as C# or VB.NET), the message security modes can
            // be combined with multiple endpoint Uri objects passed to it as separate arguments, instead of having to create
            // an array of them.
            var server14 = new EasyUAServer(
                UAMessageSecurityModes.Secure,
                new Uri("opc.tcp://localhost:38444"),
                new Uri("opc.tcp://localhost:38445"),
                new Uri("opc.tcp://localhost:38446")
            );


            // The message security modes and the endpoint URL string can be set after the server object is constructed.
            var server15 = new EasyUAServer();
            server15.MessageSecurityModes = UAMessageSecurityModes.Secure;
            server15.EndpointUrlString = "opc.tcp://localhost:38444";


            // If the language supports property initializers (such as C# or VB.NET), the above code can be written more
            // concisely.
            var server16 = new EasyUAServer
            {
                MessageSecurityModes = UAMessageSecurityModes.Secure,
                EndpointUrlString = "opc.tcp://localhost:38444"
            };


            // If the language supports collection initializers (such as C# or VB.NET), the server object can be constructed
            // with the contents of the Objects folder, such as the data variables, in a single statement.
            var server17 = new EasyUAServer
            {
                new UADataVariable("Constant1").ConstantValue(42),
                new UADataVariable("Constant2").ConstantValue("abc")
            };
        }
    }
}
#endregion
