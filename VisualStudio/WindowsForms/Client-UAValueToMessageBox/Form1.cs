// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Windows.Forms;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UAValueToMessageBox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create EasyOPC-UA component
            var client = new EasyUAClient();

            // Read value and display it in a message box
            try
            {
                object value = client.ReadValue(
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer", 
                    "nsu=http://test.org/UA/Data/ ;i=10853");
                MessageBox.Show(value?.ToString());
            }
            catch (UAException uaException)
            {
                MessageBox.Show($"*** {uaException.Message}");
            }
        }
    }
}
