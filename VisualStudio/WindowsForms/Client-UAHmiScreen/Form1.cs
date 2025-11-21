// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// This example shows how to implement an HMI screen by storing OPC Unified Architecture node IDs in the Tag property of
// screen controls, and animate the controls by subscribing to all items at once. Also shows a possibility how to write to
// an OPC item from the screen.
//
// Note that the Live Binding programming model can provide similar - and more - features, without a need for coding.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System.Diagnostics;
using JetBrains.Annotations;
using OpcLabs.EasyOpc.UA;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UAHmiScreen
{
    public partial class Form1 : Form
    {
        // ReSharper disable once NotNullMemberIsNotInitialized
        public Form1()
        {
            InitializeComponent();
        }

        // Define which server we will work with.
        private readonly UAEndpointDescriptor _endpointDescriptor =
            "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
        // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
        // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            easyUAClient1.UnsubscribeAllMonitoredItems();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // We have configured the read-only controls on the form in the designer by specifying the node Ids of the items
            // they should subscribe to and display in their Tag properties.

            var argumentsList = new List<UAMonitoredItemArguments>();
            foreach (Control control in Controls)
            {
                Debug.Assert(!(control is null));

                if (control.Tag is string nodeIdExpandedText)
                    // The State argument of the subscription will be the reference to the control itself.
                    argumentsList.Add(new UAMonitoredItemArguments(
                        control, _endpointDescriptor, nodeIdExpandedText, monitoringParameters:50));
            }

            // Subscribe to the assembled list.
            easyUAClient1.SubscribeMultipleMonitoredItems(argumentsList.ToArray());
        }

        private void easyDAClient1_DataChangeNotification(object sender, EasyUADataChangeNotificationEventArgs e)
        {
            // The State argument in the incoming notification now holds the reference to the control that should be
            // updated.
            if ((e.Arguments.State is TextBox textBox) && textBox.ReadOnly)
            {
                if (e.Exception is null)
                {
                    Debug.Assert(!(e.AttributeData is null));
                    textBox.Text = e.AttributeData.DisplayValue();
                }
                else
                    textBox.Text = "** Error **";
            }
        }

        private void writeButton_Click(object sender, EventArgs e)
        {
            // We have configured the writable control on the form in the designer by specifying the node ID of the item it
            // should write to in its Tag property.

            TextBox textBox = writeValueTextBox;
            var nodeIdExpandedText = (string) textBox.Tag;
            Debug.Assert(!(nodeIdExpandedText is null));

            try
            {
                easyUAClient1.WriteValue(_endpointDescriptor, nodeIdExpandedText, textBox.Text);
            }
            catch (UAException)
            {
                Console.Beep();
            }
        }
    }
}
