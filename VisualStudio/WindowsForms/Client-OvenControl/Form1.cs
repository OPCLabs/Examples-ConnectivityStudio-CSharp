// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CommentTypo

//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using JetBrains.Annotations;
using OpcLabs.EasyOpc;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.OperationModel;
using OpcLabs.EasyOpc.OperationModel;

namespace OvenControl
{
    public partial class Form1 : Form
    {
        private const string MachineName = "";
        private const string ServerClass = "SWToolbox.TOPServer.V5";    // or "Kepware.KEPServerEX.V5"

        private DAVtq _fanPower;
        private DAVtq _heaterPower;
        private DAVtq _ovenTemperature;
        private DAVtq _heaterTemperature;
        private DAVtq _fanSpeed;
        private DAVtq _temperatureSetpoint;


        // ReSharper disable once NotNullMemberIsNotInitialized
        public Form1()
        {
            InitializeComponent();
        }

        private Color _defaultBackColor = Color.White;

        
        // ReSharper disable InconsistentNaming
        private void Form1_Load(object sender, EventArgs e)
        // ReSharper restore InconsistentNaming
        {
            SetDefaults();
            _defaultBackColor = txbOvenTemperature.BackColor;
        }

        private void SetDefaults()
        {
            nudUpdateRate.Value = 10;
        }

        // ReSharper disable InconsistentNaming
        private void btnStart_Click(object sender, EventArgs e)
        // ReSharper restore InconsistentNaming
        {
            btnStart.Enabled = false;
            timer1.Interval = Decimal.ToInt32(nudUpdateRate.Value * 1000);
            timer1.Start();
            UpdateValuesAndLog();
            btnStop.Enabled = true;
        }

        // ReSharper disable InconsistentNaming
        private void btnStop_Click(object sender, EventArgs e)
        // ReSharper restore InconsistentNaming
        {
            btnStop.Enabled = false;
            timer1.Stop();
            btnStart.Enabled = true;
        }

        // ReSharper disable InconsistentNaming
        private void timer1_Tick(object sender, EventArgs e)
        // ReSharper restore InconsistentNaming
        {
            UpdateValuesAndLog();
        }

        private static Int32? ConvertValueToInt32(DAVtq vtq)
        {
            if (vtq is null) return null;
            if (!vtq.HasValue) return null;
            return vtq.Value as Int32?;
        }

        private static string GetTextVtq(DAVtq vtq)
        {
            if ((vtq is null) || (!vtq.HasValue))
            {
                return "???";
            }
            return vtq.DisplayValue();
        }

        private void DisplayVtq(
            DAVtq vtq, 
            // ReSharper disable once SuggestBaseTypeForParameter
            TextBox txb, 
            int colorTestId)
        {
            if ((vtq is null) || (!vtq.HasValue))
            {
                // ReSharper disable LocalizableElement
                txb.Text = "???";
                // ReSharper restore LocalizableElement
                txb.BackColor = Color.Magenta;
            }
            else
            {
                txb.Text = vtq.DisplayValue();

                Int32? value;
                Color backColor = _defaultBackColor;
                switch (colorTestId)
                {
                    case 0: 
                        txb.BackColor = _defaultBackColor; 
                        break;
                
                    case 1: // oven temperature
                        value = ConvertValueToInt32(vtq);
                        if (value.HasValue)
                        {
                            Int32? range = ConvertValueToInt32(_temperatureSetpoint);
                            if (range.HasValue)
                            {
                                if (value <= (range - 5)) backColor = Color.Blue;
                                else
                                    if (value >= (range + 5)) backColor = Color.Red;
                            }
                        }
                        txb.BackColor = backColor;
                        break;

                    case 2: // oven temperature
                        value = ConvertValueToInt32(vtq);
                        // ReSharper disable once UseNullPropagation
                        if (value.HasValue)
                        {
                            if (value < 200) backColor = Color.Red;
                        }
                        txb.BackColor = backColor;
                        break;
                }
            }
        }

        private DAVtq ReadVtq(string itemId)
        {
            DAVtq vtq;
            Exception exception = null;
            try
            {
                vtq = easyDAClient1.ReadItem(MachineName, ServerClass, itemId);
            }
            catch (OpcException ex)
            {
                exception = ex;
                vtq = null;
            }
            DisplayException(exception);
            return vtq;
        }

        private DAVtqResult[] ReadMultipleVtq(string[] itemIds)
        {
            DAVtqResult[] results;
            Exception exception = null;
            var itemDescriptors = new DAItemDescriptor[itemIds.Length];
            for (int i = 0; i < itemIds.Length; i++)
            {
                Debug.Assert(itemIds[i] != null);
                itemDescriptors[i] = new DAItemDescriptor(itemIds[i]);
            }
            try
            {
                results = easyDAClient1.ReadMultipleItems(new ServerDescriptor(MachineName, ServerClass), 
                    itemDescriptors);
            }
            catch (OpcException ex)
            {
                exception = ex;
                results = null;
            }
            DisplayException(exception);
            return results;
        }

        private string _lastExceptionMessage = "";
        private void DisplayException(Exception exception)
        {
            //txbExceptions.Text = (exception is null) ? "" : exception.GetBaseException().Message;
            if (!(exception is null))
            {
                string newMessage = $"{DateTime.Now} {exception.GetBaseException().Message}";
                if (_lastExceptionMessage != newMessage)
                {
                    _lastExceptionMessage = newMessage;
                    txbExceptions.AppendText(newMessage + Environment.NewLine);
                }
            }
        }

        private void ReadMultiple()
        {
            DAVtqResult[] results = ReadMultipleVtq(new[]
               {
                   "Channel1.Device1.FanPower", "Channel1.Device1.FanSpeed", "Channel1.Device1.HeaterPower", 
                   "Channel1.Device1.HeaterTemp", "Channel1.Device1.TempSetPoint", "Channel1.Device1.OvenTemp"
               });

            if (results is null)
                return;

            Debug.Assert(results[0] != null);
            Debug.Assert(results[1] != null);
            Debug.Assert(results[2] != null);
            Debug.Assert(results[3] != null);
            Debug.Assert(results[4] != null);
            Debug.Assert(results[5] != null);

            if (results[0].Exception is null) _fanPower = results[0].Vtq;
            else
            {
                _fanPower = null;
                DisplayException(results[0].Exception);
            }
            if (results[1].Exception is null) _fanSpeed = results[1].Vtq;
            else
            {
                _fanSpeed = null;
                DisplayException(results[1].Exception);
            }
            if (results[2].Exception is null) _heaterPower = results[2].Vtq;
            else
            {
                _heaterPower = null;
                DisplayException(results[2].Exception);
            }
            if (results[3].Exception is null) _heaterTemperature = results[3].Vtq;
            else
            {
                _heaterTemperature = null;
                DisplayException(results[3].Exception);
            }
            if (results[4].Exception is null) _temperatureSetpoint = results[4].Vtq;
            else
            {
                _temperatureSetpoint = null;
                DisplayException(results[4].Exception);
            }
            if (results[5].Exception is null) _ovenTemperature = results[5].Vtq;
            else
            {
                _ovenTemperature = null;
                DisplayException(results[5].Exception);
            }
        }


        private void UpdateValuesAndLog()
        {
            // read multiple item
            ReadMultiple();

            DisplayVtq(_fanPower, txbFanPower, 0);
            DisplayVtq(_fanSpeed, txbFanSpeed, 0);
            DisplayVtq(_heaterPower, txbHeaterPower, 0);
            DisplayVtq(_heaterTemperature, txbHeaterTemperature, 2);
            DisplayVtq(_temperatureSetpoint, txbTemperatureSetpoint, 0);
            DisplayVtq(_ovenTemperature, txbOvenTemperature, 1);

            WriteToLog();
        }

        // ReSharper disable InconsistentNaming
        private void btnSetTemperatureSetpoint_Click(object sender, EventArgs e)
        // ReSharper restore InconsistentNaming
        {
            object value = txbNewTemperatureSetpoint.Text;
            Exception exception = null;
            try
            {
                easyDAClient1.WriteItemValue(
                    MachineName,
                    ServerClass,
                    "Channel1.Device1.TempSetPoint",
                    value);
            }
            catch (OpcException ex)
            {
                exception = ex;
            }
            DisplayException(exception);

            _temperatureSetpoint = ReadVtq("Channel1.Device1.TempSetPoint");
            DisplayVtq(_temperatureSetpoint, txbTemperatureSetpoint, 0);
            DisplayVtq(_ovenTemperature, txbOvenTemperature, 1);

        }

        private void WriteToLog()
        {
            try
            {
                var sw = new System.IO.StreamWriter(Application.StartupPath + "\\OvenControl.csv", true);
                sw.WriteLine(
                    DateTime.Now.ToString(CultureInfo.InvariantCulture) + "," +  
                    GetTextVtq(_fanPower) + "," +  
                    GetTextVtq(_heaterPower) + "," +  
                    GetTextVtq(_ovenTemperature) + "," +  
                    GetTextVtq(_heaterTemperature) + "," +  
                    GetTextVtq(_fanSpeed) + "," +  
                    GetTextVtq(_temperatureSetpoint));

                sw.Close();
            }
            catch (Exception e)
            {
                DisplayException(e);
            }
        }

        // ReSharper disable InconsistentNaming
        private void btnClose_Click(object sender, EventArgs e)
        // ReSharper restore InconsistentNaming
        {
            Close();
        }
    }
}
