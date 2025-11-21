// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable NotNullMemberIsNotInitialized
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
using JetBrains.Annotations;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.AddressSpace;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UASubscribeToMany
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int _changeCount;
        private int _displayTickCount;
        private int _startTickCount;

        double GetElapsedTime()
        {
            return (Environment.TickCount - _startTickCount) / 1000.0;
        }
        
        private void startButton_Click(object sender, EventArgs e)
        {
            var numberOfItems = (int)numberOfItemsNumericUpDown.Value;

            startButton.Enabled = false;
            numberOfItemsNumericUpDown.Enabled = false;

            var argumentArray = new UAMonitoredItemArguments[numberOfItems];
            for (int i = 0; i < numberOfItems; i++)
            {
                var listViewItem = new ListViewItem();
                valuesListView.Items.Add(listViewItem);
                string identifier = (i < 100)
                    ? FormattableString.Invariant($"UInt32[{i * 4}]")
                    : FormattableString.Invariant($"Double[{(i - 100) * 8}]");
                argumentArray[i] = new UAMonitoredItemArguments(
                    state: listViewItem, 
                    endpointDescriptor:"opc.tcp://opcua.demo-this.com:51210/UA/SampleServer", 
                    nodeDescriptor: new UANodeId("http://samples.org/UA/memorybuffer/Instance", identifier), 
                    monitoringParameters:50);
            }

            _changeCount = 0;
            _startTickCount = Environment.TickCount;
            timer1.Start();

            easyUAClient1.SubscribeMultipleMonitoredItems(argumentArray);

        }

        private void easyUAClient1_DataChangeNotification(object sender, EasyUADataChangeNotificationEventArgs e)
        {
            _changeCount++;

            var listViewItem = (ListViewItem)e.Arguments.State;
            Debug.Assert(listViewItem != null);

            string text;
            if (e.Exception is null)
            {
                Debug.Assert(e.AttributeData != null);
                text = e.AttributeData.DisplayValue();
            }
            else
                text = "*Error*";
            listViewItem.Text = text;

            if (Environment.TickCount >= _displayTickCount + 5*1000)
                UpdateTimings();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateTimings();
        }

        private void UpdateTimings()
        {
            double elapsedTime = GetElapsedTime();
            if (elapsedTime == 0.0d)
                return;

            int changeCount = _changeCount;
            double changesPerSecond = changeCount / elapsedTime;

            changeCountTextBox.Text = changeCount.ToString(CultureInfo.CurrentCulture);
            elapsedTimeTextBox.Text = $"{elapsedTime:0.0}";
            changesPerSecondTextBox.Text = $"{changesPerSecond:0.0}";

            _displayTickCount = Environment.TickCount;
        }
    }
}
