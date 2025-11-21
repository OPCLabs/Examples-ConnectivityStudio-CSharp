// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable PossibleNullReferenceException
// ReSharper disable StringLiteralTypo
#region Example
// This example shows how to let the user browse for an OPC-UA server endpoint.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System.Windows.Forms;
using OpcLabs.EasyOpc.UA.Forms.Browsing;

namespace UAFormsDocExamples._UAEndpointDialog
{
    static class ShowDialog
    {
        public static void Main1(IWin32Window owner)
        {
            var endpointDialog = new UAEndpointDialog
            {
                DiscoveryHost = "opcua.demo-this.com"
            };

            DialogResult dialogResult = endpointDialog.ShowDialog(owner);
            if (dialogResult != DialogResult.OK)
                return;

            // Display results
            MessageBox.Show(owner, endpointDialog.DiscoveryElement.ToString());
        }
    }
}
#endregion
