// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// This example shows how to implement an HMI screen by storing OPC "Classic" Item IDs in the Tag property of screen
// controls, and animate the controls by subscribing to all items at once. Also shows a possibility how to write to an OPC
// item from the screen.
//
// Note that the Live Binding programming model can provide similar - and more - features, without a need for coding.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System.Diagnostics;
using JetBrains.Annotations;
using OpcLabs.EasyOpc.DataAccess;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OpcLabs.EasyOpc.DataAccess.OperationModel;
using OpcLabs.EasyOpc.OperationModel;

namespace HmiScreen
{
    public partial class Form1 : Form
    {
        // ReSharper disable once NotNullMemberIsNotInitialized
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            easyDAClient1.UnsubscribeAllItems();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // We have configured the read-only controls on the form in the designer by specifying the ItemIDs of the items
            // they should subscribe to and display in their Tag properties.

            var argumentsList = new List<DAItemGroupArguments>();
            foreach (Control control in Controls)
            {
                Debug.Assert(!(control is null));

                if (control.Tag is string itemId)
                    // The State argument of the subscription will be the reference to the control itself.
                    argumentsList.Add(new DAItemGroupArguments(
                        "", "OPCLabs.KitServer.2", itemId, requestedUpdateRate:50, control));
            }

            // Subscribe to the assembled list.
            easyDAClient1.SubscribeMultipleItems(argumentsList.ToArray());
        }

        private void easyDAClient1_ItemChanged(object sender, EasyDAItemChangedEventArgs e)
        {
            // The State argument in the incoming notification now holds the reference to the control that should be
            // updated.
            if ((e.Arguments.State is TextBox textBox) && textBox.ReadOnly)
            {
                if (e.Exception is null)
                {
                    Debug.Assert(!(e.Vtq is null));
                    textBox.Text = e.Vtq.DisplayValue();
                }
                else
                    textBox.Text = "** Error **";
            }
        }

        private void writeButton_Click(object sender, EventArgs e)
        {
            // We have configured the writable control on the form in the designer by specifying the ItemID of the item it
            // should write to in its Tag property.

            TextBox textBox = writeValueTextBox;
            var itemId = (string) textBox.Tag;
            Debug.Assert(!(itemId is null));

            try
            {
                easyDAClient1.WriteItemValue("", "OPCLabs.KitServer.2", itemId, textBox.Text);
            }
            catch (OpcException)
            {
                Console.Beep();
            }
        }
    }
}
