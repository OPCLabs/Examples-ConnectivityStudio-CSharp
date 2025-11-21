// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

#region Example
// This example shows how to allow browsing for an OPC Unified Architecture node by placing a browsing control on the form.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Windows.Forms;
using OpcLabs.BaseLib;
using OpcLabs.EasyOpc.UA.Forms.Browsing;

namespace UAFormsDocExamples._UABrowseControl
{
    public partial class UsageForm : Form
    {
        public UsageForm()
        {
            InitializeComponent();
        }

        private void getOutputsButton_Click(object sender, EventArgs e)
        {
            // Obtain the current node element.
            UABrowseNodeElement currentNodeElement = uaBrowseControl1.Outputs.CurrentNodeElement;

            // Display the present parts of the current node element in the outputs text text box.
            outputsTextBox.Text = "";
            if (!(currentNodeElement.HostElement is null))
                outputsTextBox.Text += $"{nameof(UABrowseNodeElement.HostElement)}: {currentNodeElement.HostElement}\r\n";
            if (!(currentNodeElement.DiscoveryElement is null))
                outputsTextBox.Text += $"{nameof(UABrowseNodeElement.DiscoveryElement)}: {currentNodeElement.DiscoveryElement}\r\n";
            if (!(currentNodeElement.NodeElement is null))
                outputsTextBox.Text += $"{nameof(UABrowseNodeElement.NodeElement)}: {currentNodeElement.NodeElement}\r\n";
        }

        private void uaBrowseControl1_BrowseFailure(object sender, FailureEventArgs e)
        {
            // Append the event name and its arguments to the browsing events text box.
            browsingEventsTextBox.Text += $"{nameof(UABrowseControl.BrowseFailure)}: {e}\r\n";
        }

        private void uaBrowseControl1_CurrentNodeChanged(object sender, EventArgs e)
        {
            // Append the event name and the current node element to the browsing events text box.
            browsingEventsTextBox.Text += $"{nameof(UABrowseControl.CurrentNodeChanged)}; {uaBrowseControl1.Outputs.CurrentNodeElement}\r\n";
        }

        private void uaBrowseControl1_NodeDoubleClick(object sender, EventArgs e)
        {
            // Append the event name to the browsing events text box.
            browsingEventsTextBox.Text += $"{nameof(UABrowseControl.NodeDoubleClick)}\r\n";
        }

        private void uaBrowseControl1_SelectionChanged(object sender, EventArgs e)
        {
            // Append the event name to the browsing events text box.
            browsingEventsTextBox.Text += $"{nameof(UABrowseControl.SelectionChanged)}\r\n";
        }

        private void setInputsButton_Click(object sender, EventArgs e)
        {
            // Set the current node to our pre-defined OPC UA server.
            uaBrowseControl1.InputsOutputs.CurrentNodeDescriptor.EndpointDescriptor = "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
        }
    }
}
#endregion
