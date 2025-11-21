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
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.AddressSpace;
using OpcLabs.EasyOpc.UA.AddressSpace.Standard;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UABrowseNodesWeb
{
	public partial class _Default : System.Web.UI.Page
	{
        // Use a shared client instance to allow for better optimization.
        static private readonly EasyUAClient Client = new EasyUAClient();

        protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				UANodeElementCollection elementDictionary = Client.BrowseProperties(
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer", 
                    UAObjectIds.Server);
			    foreach (UANodeElement nodeElement in elementDictionary)
				    ListBox1.Items.Add(nodeElement.ToString());
				ListBox1.Visible = true;
			}
			catch (UAException ex)
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
