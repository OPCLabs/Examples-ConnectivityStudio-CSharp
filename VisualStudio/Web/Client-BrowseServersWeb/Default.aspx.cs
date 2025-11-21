// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable InconsistentNaming

//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Diagnostics;
using OpcLabs.EasyOpc;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.OperationModel;

namespace BrowseServersWeb
{
    public partial class _Default : System.Web.UI.Page
    {
        // Use a shared client instance to allow for better optimization.
        static private readonly EasyDAClient Client = new EasyDAClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ServerElementCollection serverElements = Client.BrowseServers("");
                Debug.Assert(!(serverElements.Keys is null));

                // ReSharper disable once LoopCanBePartlyConvertedToQuery
                foreach (ServerElement serverElement in serverElements)
                {
                    Debug.Assert(!(serverElement is null));
                    ListBox1.Items.Add(serverElement.ServerClass);
                }
                ListBox1.Visible = true;
            }
            catch (OpcException ex)
            {
                TextBox1.Text = ex.GetBaseException().Message;
                TextBox1.Visible = true;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
        }
    }
}