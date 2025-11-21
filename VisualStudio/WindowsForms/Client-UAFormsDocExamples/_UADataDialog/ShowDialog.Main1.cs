// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable StringLiteralTypo
#region Example
// This example shows how to let the user browse for an OPC-UA data node (a Data Variable or a Property). 
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System.Windows.Forms;
using OpcLabs.EasyOpc.UA.Forms.Browsing;

namespace UAFormsDocExamples._UADataDialog
{
    static partial class ShowDialog
    {
        public static void Main1(IWin32Window owner)
        {
            var dataDialog = new UADataDialog
            {
                EndpointDescriptor = {UrlString = "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer" },
                // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
                // or "https://opcua.demo-this.com:51212/UA/SampleServer/"
                UserPickEndpoint = true
            };

            DialogResult dialogResult = dataDialog.ShowDialog(owner);
            if (dialogResult != DialogResult.OK)
                return;

            // Display results
            MessageBox.Show(owner, 
                $"EndpointDescriptor: {dataDialog.EndpointDescriptor}\r\n" +
                $"NodeElement: {dataDialog.NodeElement}");
        }
    }
}
#endregion
