// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

#region Example
// This example shows how to allow browsing for an OPC Data Access node by placing a browsing control on the form.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using OpcLabs.EasyOpc.Forms.Browsing;
using System;
using System.Windows.Forms;
using OpcLabs.BaseLib;

namespace FormsDocExamples._OpcBrowseControl
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
            OpcBrowseNodeElement currentNodeElement = opcBrowseControl1.Outputs.CurrentNodeElement;

            // Display the present parts of the current node element in the outputs text text box.
            outputsTextBox.Text = "";
            if (!(currentNodeElement.ComputerElement is null))
                outputsTextBox.Text += $"{nameof(OpcBrowseNodeElement.ComputerElement)}: {currentNodeElement.ComputerElement}\r\n";
            if (!(currentNodeElement.ServerElement is null))
                outputsTextBox.Text += $"{nameof(OpcBrowseNodeElement.ServerElement)}: {currentNodeElement.ServerElement}\r\n";
            if (!(currentNodeElement.DANodeElement is null))
                outputsTextBox.Text += $"{nameof(OpcBrowseNodeElement.DANodeElement)}: {currentNodeElement.DANodeElement}\r\n";
        }

        private void opcBrowseControl1_BrowseFailure(object sender, FailureEventArgs e)
        {
            // Append the event name and its arguments to the browsing events text box.
            browsingEventsTextBox.Text += $"{nameof(OpcBrowseControl.BrowseFailure)}: {e}\r\n";
        }

        private void opcBrowseControl1_CurrentNodeChanged(object sender, EventArgs e)
        {
            // Append the event name and the current node element to the browsing events text box.
            browsingEventsTextBox.Text += $"{nameof(OpcBrowseControl.CurrentNodeChanged)}; {opcBrowseControl1.Outputs.CurrentNodeElement}\r\n";
        }

        private void opcBrowseControl1_NodeDoubleClick(object sender, EventArgs e)
        {
            // Append the event name to the browsing events text box.
            browsingEventsTextBox.Text += $"{nameof(OpcBrowseControl.NodeDoubleClick)}\r\n";
        }

        private void opcBrowseControl1_SelectionChanged(object sender, EventArgs e)
        {
            // Append the event name to the browsing events text box.
            browsingEventsTextBox.Text += $"{nameof(OpcBrowseControl.SelectionChanged)}\r\n";
        }

        private void setInputsButton_Click(object sender, EventArgs e)
        {
            // Set the current node to a pre-defined OPC DA item on our server.
            opcBrowseControl1.InputsOutputs.CurrentNodeDescriptor.ServerDescriptor = "OPCLabs.KitServer.2";
            opcBrowseControl1.InputsOutputs.CurrentNodeDescriptor.DANodeDescriptor = "Demo.Ramp";
        }
    }
}
#endregion
