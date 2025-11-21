// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable InconsistentNaming

//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using OpcLabs.EasyOpc.DataAccess;
using System;
using System.Web.UI.WebControls;
using OpcLabs.EasyOpc.DataAccess.Extensions;
using OpcLabs.EasyOpc.OperationModel;

namespace AutoRefreshWeb
{
    public partial class _Default : System.Web.UI.Page
    {
        static _Default()
        {
            // Enable auto-subscribing optimization (not necessary), which can improve performance with repeated Read
            // requests.
            Client.TryEnableAutoSubscribingOptimization();
        }

        // Use a shared client instance to allow for better optimization.
        static private readonly EasyDAClient Client = new EasyDAClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            Read(TextBox1, "Simulation.Ramp (10 s)");
            Read(TextBox2, "Simulation.Random");
            Read(TextBox3, "Simulation.Incrementing (1 s)");
        }

        protected void Read(TextBox textBox, string itemId)
        {
            try
            {
                object value = Client.ReadItemValue("", "OPCLabs.KitServer.2", itemId);
                textBox.Text = value?.ToString() ?? "";
            }
            catch (OpcException e)
            {
                textBox.Text = $"*** {e.GetBaseException().Message}"; 
            }
        }
    }
}
