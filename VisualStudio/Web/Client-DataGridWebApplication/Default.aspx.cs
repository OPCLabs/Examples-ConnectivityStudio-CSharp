// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedAutoPropertyAccessor.Local

// DataGridWebApplication: Demonstrates how easily can GridView be populated with data read from OPC Data Access server.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Collections.Generic;
using OpcLabs.BaseLib.OperationModel;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.Extensions;

namespace DataGridWebApplication
{
    public partial class _Default : System.Web.UI.Page
    {
        class Row
        {
            public string ItemId { get; set; }
            public string Value { get; set; }
        }

        static _Default()
        {
            // Enable auto-subscribing optimization (not necessary), which can improve performance with repeated Read requests.
            Client.TryEnableAutoSubscribingOptimization();
        }

        // Use a shared client instance to allow for better optimization.
        static private readonly EasyDAClient Client = new EasyDAClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            var itemDescriptors = new DAItemDescriptor[]
                {
                    "Simulation.Register_BOOL",
                    "Simulation.Register_I2",
                    "Demo.Ramp",
                    "Demo.Single"                                                         
                };

            ValueResult[] valueResults = Client.ReadMultipleItemValues("OPCLabs.KitServer.2", itemDescriptors);

            var data = new List<Row>();
            for (int i = 0; i < itemDescriptors.Length; i++)
                data.Add(new Row
                    {
                        ItemId = itemDescriptors[i].ItemId, 
                        Value = valueResults[i].Value?.ToString()
                    });

            GridView1.DataSource = data;
            GridView1.DataBind();
        }
    }
}
