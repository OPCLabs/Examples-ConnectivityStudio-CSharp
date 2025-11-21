// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ConvertIfStatementToConditionalTernaryExpression
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how to change the user identity (so-called "user switching") while keeping the same connection (OPC
// UA session).
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.BaseLib.Aliasing;
using OpcLabs.BaseLib.Aliasing.Extensions;
using OpcLabs.BaseLib.IdentityModel.User;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.OperationModel;
using OpcLabs.EasyOpc.UA.AddressSpace.Standard;
using OpcLabs.EasyOpc.UA.Engine;

namespace UADocExamples.Users
{
    class IdentityChange
    {
        public static void Main1()
        {
            // Note that this example demonstrates the ability to dynamically change the user identity while keeping the
            // same connection. If you want a single user identity, or even multiple user identities at the same time or
            // sequentially without the need to keep the same connection, this example is not for you. In such cases, you can
            // simply specify the user identity directly in the endpoint descriptor, without the use of aliases.


            // This example is designed to work with Unified Automation UaServerNET.exe, which is a demo server from their
            // UA .NET SDK.
            UAEndpointDescriptor endpointDescriptor = "opc.tcp://localhost:48030/";

            // Require a fully secured connection. Prevents passwords from being transferred in clear text.
            endpointDescriptor.EndpointSelectionPolicy = UAEndpointSelectionPolicy.FullySecured;

            // For identity changes, the user identity in the endpoint descriptor must be specified by an alias. The name of
            // the alias (here, "CurrentUser") is up to you.
            endpointDescriptor.UserIdentity.AliasName = "CurrentUser";

            // Set the initial value of the "CurrentUser" alias to an anonymous identity.
            IAliasRepository aliasRepository = AliasingManagement.Instance.AliasRepository;
            aliasRepository.SetAlias("CurrentUser", UserIdentity.CreateAnonymousIdentity());

            // Instantiate the client object.
            var client = new EasyUAClient();

            // Identity changes require an isolated client component, otherwise you will get an error.
            client.Isolated = true;

            // Hook events to the client object.
            client.DataChangeNotification += client_DataChangeNotification;

            Console.WriteLine("Subscribing with anonymous identity...");
            client.SubscribeDataChange(endpointDescriptor, UAVariableIds.Server_ServerStatus_CurrentTime, 1000);

            Console.WriteLine("Processing data change events for 10 seconds...");
            System.Threading.Thread.Sleep(10 * 1000);

            Console.WriteLine("Changing the user identity to 'john' (a known user)...");
            aliasRepository.SetAlias("CurrentUser", UserIdentity.CreateUserNameIdentity("john", "master"));

            Console.WriteLine("Processing data change events for 10 seconds...");
            System.Threading.Thread.Sleep(10 * 1000);

            Console.WriteLine("Attempting to change the user identity to 'unknown', a user that is not known to the server...");
            // This leads to errors in operations and notifications, because the server does not know the user.
            aliasRepository.SetAlias("CurrentUser", UserIdentity.CreateUserNameIdentity("unknown", "123"));

            Console.WriteLine("Processing data change events for 10 seconds...");
            System.Threading.Thread.Sleep(10 * 1000);

            Console.WriteLine("Changing the user identity back to 'john'...");
            // The operations and notifications will no longer give errors, because the server knows and authenticates the
            // user.
            aliasRepository.SetAlias("CurrentUser", UserIdentity.CreateUserNameIdentity("john", "master"));

            Console.WriteLine("Processing data change events for 10 seconds...");
            System.Threading.Thread.Sleep(10 * 1000);

            Console.WriteLine("Unsubscribing...");
            client.UnsubscribeAllMonitoredItems();

            Console.WriteLine("Waiting for 5 seconds...");
            System.Threading.Thread.Sleep(5 * 1000);

            Console.WriteLine("Finished.");
        }
        static void client_DataChangeNotification(object sender, EasyUADataChangeNotificationEventArgs e)
        {
            // Display value.
            if (e.Succeeded)
                Console.WriteLine($"Value: {e.AttributeData.Value}");
            else
                Console.WriteLine($"*** Failure: {e.ErrorMessageBrief}");
        }
    }
}
#endregion
