// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable PossibleNullReferenceException
#region Example
// UAServerWindowsServiceDemo: Shows how to use the component to create an OPC UA server hosted in a Windows service. It
// provides readable and writable nodes of various types.
//
// Install the service by running:
//      C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe /i UAServerWindowsServiceDemo.exe
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client, server and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System.ServiceProcess;
using OpcLabs.EasyOpc.UA;
using UAServerDemoLibrary;

namespace UAServerWindowsServiceDemo
{
    public partial class UAService1 : ServiceBase
    {
        public UAService1()
        {
            InitializeComponent();

            // Define various nodes.
            ConsoleNodes.AddToParent(_server.Objects);
            DataNodes.AddToParent(_server.Objects);
            DemoNodes.AddToParent(_server.Objects);
        }

        protected override void OnStart(string[] args)
        {
            // Start the server.
            _server.Start();
        }

        protected override void OnStop()
        {
            // Stop the server.
            _server.Stop();
        }

        // The server object.
        // By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
        private readonly EasyUAServer _server = new EasyUAServer();
    }
}
#endregion
