// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Windows.Forms;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.OperationModel;

namespace ValueToMessageBox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create EasyOPC-DA component
            var client = new EasyDAClient();

            // Read item value and display it in a message box
            try
            {
                object value = client.ReadItemValue("", "OPCLabs.KitServer.2", "Demo.Single");
                MessageBox.Show(value?.ToString());
            }
            catch (OpcException opcException)
            {
                MessageBox.Show($"*** {opcException.Message}");
            }
        }
    }
}
