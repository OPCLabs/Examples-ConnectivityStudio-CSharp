// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
#region Example
// UAServerWorkerServiceDemo: Shows how to use the component to create an OPC UA server hosted in a worker service. It
// provides readable and writable nodes of various types.
// See also: https://learn.microsoft.com/en-us/dotnet/core/extensions/windows-service
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client, server and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;

namespace UAServerWorkerServiceDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddWindowsService(options =>
            {
                options.ServiceName = "OpcWizardDemo";
            });

            LoggerProviderOptions.RegisterProviderOptions<
                EventLogSettings, EventLogLoggerProvider>(builder.Services);

            builder.Services.AddHostedService<ServerDemoBackgroundService>();

            IHost host = builder.Build();
            host.Run();
        }
    }
}
#endregion
