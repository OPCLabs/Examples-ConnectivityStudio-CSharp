// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable ArrangeModifiersOrder
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedAutoPropertyAccessor.Local

// UADataGridWebApplication: Demonstrates how easily can GridView be populated with data read from OPC UA server.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Collections.Generic;
using OpcLabs.BaseLib.OperationModel;
using OpcLabs.EasyOpc.UA;

namespace UADataGridWebApplication
{
    public partial class _Default : System.Web.UI.Page
    {
        class Row
        {
            public string NodeId { get; set; }
            public string Value { get; set; }
        }

        // Use a shared client instance to allow for better optimization.
        static private readonly EasyUAClient Client = new EasyUAClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            var nodeDescriptors = new UANodeDescriptor[]
                {
                    "nsu=http://test.org/UA/Data/ ;i=10844",
                    "nsu=http://test.org/UA/Data/ ;i=10853",
                    "nsu=http://test.org/UA/Data/ ;i=11017",
                    "nsu=http://test.org/UA/Data/ ;i=10845"
                };

            ValueResult[] valueResults = Client.ReadMultipleValues(
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer", 
                nodeDescriptors);

            var data = new List<Row>();
            for (int i = 0; i < nodeDescriptors.Length; i++)
                data.Add(new Row
                    {
                        NodeId = nodeDescriptors[i].NodeId, 
                        Value = valueResults[i].Value?.ToString()
                    });

            GridView1.DataSource = data;
            GridView1.DataBind();
        }
    }
}
