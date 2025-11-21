// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable PossibleNullReferenceException
#region Example
// A fully functional OPC UA demo server running in Windows Forms host.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client, server and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System.Linq;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.Forms.Application;
using OpcLabs.EasyOpc.UA.OperationModel;
using OpcLabs.EasyOpc.UA.Services;
using UAServerDemoLibrary;

namespace UAServerWindowsFormsDemo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            // Instantiating the EasyUAServer here, and not inline where the field is declared, is important for it to
            // acquire the proper synchronization context.
            _server = new EasyUAServer();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            // Extend the form's system menu by a command for OPC UA application management.
            EasyUAFormsApplication.Instance.AddToSystemMenu(this);

            // Define various nodes.
            DataNodes.AddToParent(_server.Objects);
            DemoNodes.AddToParent(_server.Objects);

            // Hook events to the server object.
            _server.EndpointStateChanged += ServerOnEndpointStateChanged;

            // Obtain the server connection monitoring service.
            IEasyUAServerConnectionMonitoring serverConnectionMonitoring = _server.GetService<IEasyUAServerConnectionMonitoring>();
            if (!(serverConnectionMonitoring is null))
            {
                // Hook events to the connection monitoring service.
                serverConnectionMonitoring.ClientSessionConnected += ServerConnectionMonitoringOnClientSessionConnected;
                serverConnectionMonitoring.ClientSessionDisconnected += ServerConnectionMonitoringOnClientSessionDisconnected;
            }

            _server.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _server.Stop();
        }

        private void ServerOnEndpointStateChanged(object sender, EasyUAServerEndpointStateChangedEventArgs e)
        {
            // Note that since we have created the EasyUAServer on the UI thread, we can access the UI controls in its
            // event handlers directly, because the events are raised using the synchronization context acquired at time of
            // the creation.

            string endpointUrlString = e.EndpointUrlString;
            ListViewItem listViewItem = endpointStateListView.Items.Cast<ListViewItem>().FirstOrDefault(item => 
                item.Text == endpointUrlString);
            if (listViewItem is null)
            {
                listViewItem = new ListViewItem(endpointUrlString);
                endpointStateListView.Items.Add(listViewItem);
                listViewItem.SubItems.Add("");
                listViewItem.SubItems.Add("");
            }
            listViewItem.SubItems[1].Text = e.ConnectionState.ToString();
            listViewItem.SubItems[2].Text = e.Exception?.Message ?? "";
        }

        private void ServerConnectionMonitoringOnClientSessionConnected(object sender, EasyUAClientSessionConnectionEventArgs e)
        {
            // Note that since we have created the EasyUAServer on the UI thread, we can access the UI controls in its
            // event handlers directly, because the events are raised using the synchronization context acquired at time of
            // the creation.

            var listViewItem = new ListViewItem(e.SessionId.Identifier.ToString());
            listViewItem.SubItems.Add(e.SessionName);
            connectionsListView.Items.Add(listViewItem);
        }

        private void ServerConnectionMonitoringOnClientSessionDisconnected(object sender, EasyUAClientSessionConnectionEventArgs e)
        {
            // Note that since we have created the EasyUAServer on the UI thread, we can access the UI controls in its
            // event handlers directly, because the events are raised using the synchronization context acquired at time of
            // the creation.

            string identifierString = e.SessionId.Identifier.ToString();
            ListViewItem listViewItem = connectionsListView.Items.Cast<ListViewItem>().FirstOrDefault(item =>
                item.Text == identifierString);
            if (!(listViewItem is null))
                connectionsListView.Items.Remove(listViewItem);
        }

        private readonly EasyUAServer _server;
    }
}
#endregion
