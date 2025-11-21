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

using OpcLabs.EasyOpc.UA;
using UAServerDemoLibrary;

namespace UAServerWorkerServiceDemo
{
    public class ServerDemoBackgroundService : BackgroundService
    {
        private readonly ILogger<ServerDemoBackgroundService> _logger;

        // The server object.
        // By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
        private readonly EasyUAServer _server = new();

        public ServerDemoBackgroundService(ILogger<ServerDemoBackgroundService> logger)
        {
            _logger = logger;

            // Define various nodes.
            ConsoleNodes.AddToParent(_server.Objects);
            DataNodes.AddToParent(_server.Objects);
            DemoNodes.AddToParent(_server.Objects);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                // Start the server.
                _server.Start();

                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(1000, stoppingToken);
                }

                // Stop the server.
                _server.Stop();
            }
            catch (OperationCanceledException)
            {
                // When the stopping token is canceled, for example, a call made from services.msc,
                // we shouldn't exit with a non-zero exit code. In other words, this is expected...
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message}", ex.Message);

                // Terminates this process and returns an exit code to the operating system.
                // This is required to avoid the 'BackgroundServiceExceptionBehavior', which
                // performs one of two scenarios:
                // 1. When set to "Ignore": will do nothing at all, errors cause zombie services.
                // 2. When set to "StopHost": will cleanly stop the host, and log errors.
                //
                // In order for the Windows Service Management system to leverage configured
                // recovery options, we need to terminate the process with a non-zero exit code.
                Environment.Exit(1);
            }
        }
    }
}
#endregion
