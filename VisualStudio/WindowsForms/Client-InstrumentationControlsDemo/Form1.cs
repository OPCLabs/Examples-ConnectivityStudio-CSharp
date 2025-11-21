
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Diagnostics;
using System.Windows.Forms;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.OperationModel;

namespace InstrumentationControlsDemo
{
    public partial class Form1 : Form
    {
        // ReSharper disable once NotNullMemberIsNotInitialized
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            easyDAClient1.SubscribeItem("", "OPCLabs.KitServer.2", "Simulation.Ramp 0:100 (10 s)", 100, null, 0);
            easyDAClient1.SubscribeItem("", "OPCLabs.KitServer.2", "Simulation.Sine -100:100 (10 s)", 100, null, 1);
            easyDAClient1.SubscribeItem("", "OPCLabs.KitServer.2", "Simulation.Register_R8", 100, null, 2);
        }

        private void easyDAClient1_ItemChanged(object sender, EasyDAItemChangedEventArgs e)
        {
            // ReSharper disable once PossibleNullReferenceException
            var index = (int)e.Arguments.State;
            if ((e.Vtq != null) && (e.Vtq.HasValue))
            {
                // ReSharper disable once PossibleNullReferenceException
                var d = (double) e.Vtq.Value;
                plot1.Channels[index].AddXY(e.Vtq.Timestamp, d);
            }
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }
    }
}
