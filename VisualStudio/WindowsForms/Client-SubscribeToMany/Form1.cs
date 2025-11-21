// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable NotNullMemberIsNotInitialized

//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System.Diagnostics;
using System.Globalization;
using JetBrains.Annotations;
using OpcLabs.EasyOpc.DataAccess;
using System;
using System.Windows.Forms;
using OpcLabs.EasyOpc.DataAccess.OperationModel;

namespace SubscribeToMany
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int _changeCount;
        int _startTickCount;

        double GetElapsedTime()
        {
            return (Environment.TickCount - _startTickCount) / 1000.0;
        }
        
        private void startButton_Click(object sender, EventArgs e)
        {
            var numberOfItems = (int)numberOfItemsNumericUpDown.Value;

            startButton.Enabled = false;
            numberOfItemsNumericUpDown.Enabled = false;

            var argumentArray = new DAItemGroupArguments[numberOfItems];
            for (int i = 0; i < numberOfItems; i++)
            {
                var listViewItem = new ListViewItem();
                valuesListView.Items.Add(listViewItem);
                int copy = (i / 100) + 1;
                int phase = (i%100) + 1;
                string itemId = FormattableString.Invariant($"Simulation.Incrementing.Copy_{copy}.Phase_{phase}");
                argumentArray[i] = new DAItemGroupArguments("", "OPCLabs.KitServer.2", itemId, 50, listViewItem);
            }

            _changeCount = 0;
            _startTickCount = Environment.TickCount;
            timer1.Start();

            easyDAClient1.SubscribeMultipleItems(argumentArray);

        }

        private void easyDAClient1_ItemChanged(object sender, EasyDAItemChangedEventArgs e)
        {
            _changeCount++;

            var listViewItem = (ListViewItem)e.Arguments.State;
            Debug.Assert(listViewItem != null);

            string text;
            if (e.Exception is null)
            {
                Debug.Assert(e.Vtq != null);
                text = e.Vtq.DisplayValue();
            }
            else
                text = "*Error*";
            listViewItem.Text = text;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double elapsedTime = GetElapsedTime();
            // ReSharper disable CompareOfFloatsByEqualityOperator
            if (elapsedTime == 0.0d)
            // ReSharper restore CompareOfFloatsByEqualityOperator
                return;

            int changeCount = _changeCount;
            double changesPerSecond = changeCount / elapsedTime;

            changeCountTextBox.Text = changeCount.ToString(CultureInfo.CurrentCulture);
            elapsedTimeTextBox.Text = $"{elapsedTime:0.0}";
            changesPerSecondTextBox.Text = $"{changesPerSecond:0.0}";
        }
    }
}
