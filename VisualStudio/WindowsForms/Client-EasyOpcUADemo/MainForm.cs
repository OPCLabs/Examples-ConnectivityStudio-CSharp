// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using EasyOpcUADemo.Properties;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.Forms.Application;
using OpcLabs.EasyOpc.UA.OperationModel;

// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable InconsistentNaming
// ReSharper disable NotNullMemberIsNotInitialized
// ReSharper disable PossibleNullReferenceException

[assembly:CLSCompliant(true)]
namespace EasyOpcUADemo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// A handle for the OPC subscription, returned by the client component. The handle is used to change the
        /// parameters of the subscription, or to unsubscribe.
        /// </summary>
        private int _handle/* = 0*/;

        private bool _isSubscribed/* = false*/;


        /// <summary>
        /// The user has pressed the "About" button. Show a message box with information about the executing assembly.
        /// </summary>
        private void aboutButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, Assembly.GetExecutingAssembly().FullName, Resources.MainForm_AssemblyName,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// The user has pressed the "Browse" button. Let the user select the OPC data node (from nodes available in the
        /// given OPC server) in a modal dialog.
        /// </summary>
        private void browseDataButton_Click(object sender, EventArgs e)
        {
            uaDataDialog1.EndpointDescriptor = serverUriTextBox.Text;
            if (uaDataDialog1.ShowDialog() == DialogResult.OK)
                nodeIdTextBox.Text = uaDataDialog1.NodeDescriptor.NodeId;
        }

        /// <summary>
        /// The user has pressed the "Change subscription" button. Change the parameters of the current subscription to
        /// the sampling interval currently given on the form.
        /// </summary>
        private void changeSubscriptionButton_Click(object sender, EventArgs e)
        {
            easyUAClient1.ChangeMonitoredItemSubscription(_handle, (int)samplingIntervalNumericUpDown.Value);
        }

        /// <summary>
        /// The user has pressed the "Close" button. Close the form.
        /// </summary>
        private void closeButton_Click(object sender, EventArgs e)
        {
            if (IsSubscribed)
                Unsubscribe();
            Close();
        }

        /// <summary>
        /// The user has pressed the "Discover servers" button. Let the user enter or select the host and an OPC server in
        /// a modal dialog.
        /// </summary>
        private void discoverServersButton_Click(object sender, EventArgs e)
        {
            uaHostAndEndpointDialog1.EndpointDescriptor = serverUriTextBox.Text;
            if (uaHostAndEndpointDialog1.ShowDialog() == DialogResult.OK)
                serverUriTextBox.Text = uaHostAndEndpointDialog1.EndpointDescriptor.UrlString;
        }

        /// <summary>
        /// Updates the "Value", "Status", "Source timestamp" and "Server timestamp" text boxes with data from the OPC
        /// server, or clears them if no data is available.
        /// </summary>
        private void DisplayAttributeData(UAAttributeData attributeData)
        {
            if (attributeData is null)
            {
                valueTextBox.Text = "";
                statusTextBox.Text = "";
                sourceTimestampTextBox.Text = "";
                serverTimestampTextBox.Text = "";
            }
            else
            {
                valueTextBox.Text = attributeData.DisplayValue();
                statusTextBox.Text = attributeData.StatusCode.ToString();
                sourceTimestampTextBox.Text = attributeData.SourceTimestamp.ToString(CultureInfo.CurrentCulture);
                serverTimestampTextBox.Text = attributeData.ServerTimestamp.ToString(CultureInfo.CurrentCulture);
            }
        }

        /// <summary>
        /// Updates the "Exception" text box with the text of the error, or clears it if there is no exception.
        /// </summary>
        private void DisplayException(Exception exception)
        {
            exceptionTextBox.Text = (exception is null) ? "" : exception.GetBaseException().Message;
        }

        /// <summary>
        /// Event handler for the <see cref="EasyUAClient.DataChangeNotification"/> event. It is invoked for every
        /// significant change related to the OPC attribute subscribed. We display the data received (or the error) on the
        /// form.
        /// </summary>
        private void easyUAClient1_DataChangeNotification(object sender, EasyUADataChangeNotificationEventArgs e)
        {
            DisplayAttributeData((e.Exception is null) ? e.AttributeData : null);
            DisplayException(e.Exception);
        }

        /// <summary>
        /// Gets or sets whether there is currently a subscription to an OPC monitored item.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The method enables or disables corresponding controls on the form.</para>
        /// </remarks>
        public bool IsSubscribed
        {
            get => _isSubscribed;
            set
            {
                _isSubscribed = value;
                subscribeMonitoredItemButton.Enabled = !_isSubscribed;
                changeMonitoredItemSubscriptionButton.Enabled = _isSubscribed;
                unsubscribeMonitoredItemButton.Enabled = _isSubscribed;
            }
        }

        /// <summary>
        /// When the form loads, extend its system menu by a command for OPC UA application management.
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            EasyUAFormsApplication.Instance.AddToSystemMenu(this);
        }

        /// <summary>
        /// The user has pressed the "Read" button. Attempt to read the Value attribute of the given OPC node from the given
        /// OPC server, and display either the attribute data, or the error received.
        /// </summary>
        private void readButton_Click(object sender, EventArgs e)
        {
            UAAttributeData attributeData = null;
            Exception exception = null;
            try
            {
                attributeData = easyUAClient1.Read(serverUriTextBox.Text, nodeIdTextBox.Text);
            }
            catch (UAException ex)
            {
                exception = ex;
            }
            DisplayAttributeData(attributeData);
            DisplayException(exception);
        }

        /// <summary>
        /// The user has pressed the "Subscribe" button. Subscribe to the Value attribute of the given OPC node. Data will
        /// flow into the form by means of the <see cref="easyUAClient1_DataChangeNotification"/> event handler.
        /// </summary>
        private void subscribeButton_Click(object sender, EventArgs e)
        {
            _handle = easyUAClient1.SubscribeDataChange(serverUriTextBox.Text, nodeIdTextBox.Text,
                                                           (int)samplingIntervalNumericUpDown.Value);
            IsSubscribed = true;
        }

        /// <summary>
        /// Unsubscribe from OPC monitored item we have subscribed earlier in <see cref="subscribeButton_Click"/>. Data will
        /// no longer flow through the <see cref="easyUAClient1_DataChangeNotification"/> event handler.
        /// </summary>
        private void Unsubscribe()
        {
            easyUAClient1.UnsubscribeMonitoredItem(_handle);
            _handle = 0;
            IsSubscribed = false;
        }

        /// <summary>
        /// The user has pressed the "Unsubscribe" button.
        /// </summary>
        private void unsubscribeButton_Click(object sender, EventArgs e) => Unsubscribe();

        /// <summary>
        /// The user has pressed the "Write value" button. Attempt to write the value entered on the form to the given OPC
        /// node. If an error was received, display it.
        /// </summary>
        private void writeValueButton_Click(object sender, EventArgs e)
        {
            Exception exception = null;
            try
            {
                easyUAClient1.WriteValue(serverUriTextBox.Text, nodeIdTextBox.Text, valueToWriteTextBox.Text);
            }
            catch (UAException ex)
            {
                exception = ex;
            }
            DisplayException(exception);
        }
    }
}
