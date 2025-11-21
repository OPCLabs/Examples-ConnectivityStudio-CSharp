// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable StringLiteralTypo
#region Example
// This example shows how the current node and selected nodes can be persisted between dialog invocations.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System.Windows.Forms;
using OpcLabs.EasyOpc.UA.Forms.Browsing;

namespace UAFormsDocExamples._UABrowseDialog
{
    static partial class ShowDialog
    {
        public static void SelectionDescriptors(IWin32Window owner)
        {
            // The variables that persist the current and selected nodes.

            var currentNodeDescriptor = new UABrowseNodeDescriptor();
            var selectionDescriptors = new UABrowseNodeDescriptorCollection();

            // The initial current node (optional).

            currentNodeDescriptor.EndpointDescriptor = "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";

            // Repeatedly show the dialog until the user cancels it.

            do
            {
                var browseDialog = new UABrowseDialog();
                browseDialog.Mode.MultiSelect = true;

                // Set the dialog inputs from the persistence variables.

                browseDialog.InputsOutputs.CurrentNodeDescriptor = currentNodeDescriptor;
                browseDialog.InputsOutputs.SelectionDescriptors.Clear();
                foreach (UABrowseNodeDescriptor browseNodeDescriptor in selectionDescriptors)
                    browseDialog.InputsOutputs.SelectionDescriptors.Add(browseNodeDescriptor);

                DialogResult dialogResult = browseDialog.ShowDialog(owner);
                if (dialogResult != DialogResult.OK)
                    break;

                // Update the persistence variables with the dialog output.

                currentNodeDescriptor = browseDialog.InputsOutputs.CurrentNodeDescriptor;
                selectionDescriptors.Clear();
                foreach (UABrowseNodeDescriptor browseNodeDescriptor in browseDialog.InputsOutputs.SelectionDescriptors)
                    selectionDescriptors.Add(browseNodeDescriptor);

            } while (true);
        }
    }
}
#endregion
