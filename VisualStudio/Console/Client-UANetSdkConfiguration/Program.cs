// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// Shows how to configure the OPC UA .NET stack and SDK using its configuration file (XML). For more information, see
// https://kb.opclabs.com/OPC_UA_.NET_SDK_Configuration .
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.BaseLib.Instrumentation;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.Engine;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UANetSdkConfiguration
{
    class Program
    {
        static void Main()
        {
            // Set the source of OPC UA .NET SDK configuration to the "App.config" file only. In "old-style" Visual Studio
            // projects (for .NET Framework only), during the build, Visual Studio copies the "App.config" file from the
            // project to the output directory, and renames it to match the name of the executable. In this example, the
            // "App.config" from the project becomes "UANetSdkConfiguration.exe.config" in the build output directory. The
            // runtime looks for this file by appending ".config" to the name of the currently running executable.
            //
            // In "new-style" Visual Studio projects (mainly for .NET Core/.NET 5+), this automatic mechanism is not
            // present. To get around it, simply include a properly named "App.config" file (in our case, it is
            // "UANetSdkConfiguration.dll.config") in the project's root directory, and set "Copy to Output Directory" in
            // its Properties to either "Copy always" or "Copy if newer". Notice the ".dll.config" file extension used
            // under .NET Core/.NET 5+, as opposed to ".exe.config" under .NET Framework.
            // 
            // The "App.config" mechanism is a standard configuration mechanism in .NET Framework. In our case, the
            // "App.config" contains a references to *another* XML configuration file, whose format is specific to the OPC
            // UA .NET SDK. This example calls this file "MyApplication.Config.xml", but it can have any name, as long as it
            // matches the information provided in the (properly renamed) "App.config".
            //
            // The "MyApplication.Config.xml" file in this example specifies somewhat lower value for the MaxMessageSize
            // transport quota than the default.
            //
            // If the "App.config" file does not specify the file path of the OPC UA .NET SDK configuration, the component
            // tries to load it from a default file, residing in the same directory as the application's executable
            // assembly, and with the same name, but with a file extension ".Config.xml". This means that for this project,
            // it would be named UANetSdkConfiguration.Config.xml.

            EasyUAClient.SharedParameters.EngineParameters.ConfigurationSources = UAConfigurationSources.AppConfig;


            // Hook static events.
            // Uncomment the statement below for troubleshooting.
            //EasyUAClient.LogEntry += EasyUAClientOnLogEntry;


            // Perform some OPC operations.

            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            // Instantiate the client object
            var client = new EasyUAClient();

            Console.WriteLine("Obtaining value of a node...");
            try
            {
                object value = client.ReadValue(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10853");

                // Display results
                Console.WriteLine("value: {0}", value);
            }
            catch (UAException uaException)
            {
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
            }

            Console.WriteLine();
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        // Event handler for the LogEntry event.
        // Display the loggable entries related to UA .NET SDK configuration.
        private static void EasyUAClientOnLogEntry(object sender, LogEntryEventArgs logEntryEventArgs)
        {
            if ((130 <= logEntryEventArgs.EventId) && (logEntryEventArgs.EventId <= 139))
                Console.WriteLine(logEntryEventArgs);
        }
    }
}
#endregion
