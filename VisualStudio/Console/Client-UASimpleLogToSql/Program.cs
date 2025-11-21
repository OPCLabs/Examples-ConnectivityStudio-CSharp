// UASimpleLogToSql: Logs OPC Unified Architecture data changes into an SQL database, using a subscription. Values of all 
// data types are stored in a single SQL_VARIANT column.
//
// The database creation script is in the Examples-NET\MSSQL\Client\QuickOPCExamples.sql file under the product installation
// directory. The example assumes that the database is already created.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Threading;
using OpcLabs.EasyOpc.UA;

namespace UASimpleLogToSql
{
    class Program
    {
        static void Main()
        {
            const string connectionString =
                "Data Source=(local);Initial Catalog=QuickOPCExamples;Integrated Security=true";

            // Define which server we will work with.
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            Console.WriteLine("Starting up...");
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create all necessary ADO.NET objects.
                var adapter = new SqlDataAdapter("SELECT * FROM UASimpleLog", connection);
                var dataSet = new DataSet();
                adapter.FillSchema(dataSet, SchemaType.Source, "UASimpleLog");
                adapter.InsertCommand = new SqlCommandBuilder(adapter).GetInsertCommand();
                DataTable table = dataSet.Tables["UASimpleLog"];
                Debug.Assert(!(table is null));

                Console.WriteLine("Logging for 30 seconds...");
                // Subscribe to an OPC item, using an anonymous method to process the notifications.
                int handle = EasyUAClient.SharedInstance.SubscribeDataChange(
                    endpointDescriptor,
                    "nsu=http://test.org/UA/Data/ ;i=10853",
                    100,
                    (_, eventArgs) =>
                    {
                        Debug.Assert(!(eventArgs is null));
                        Console.Write(".");

                        // In this example, we only log valid data. Production logger would also log errors.
                        if (!(eventArgs.AttributeData is null))
                        {
                            // Fill a DataRow with the OPC data, and add it to a DataTable.
                            Debug.Assert(!(table.Rows is null));
                            table.Rows.Clear();
                            DataRow row = table.NewRow();
                            row["NodeID"] = eventArgs.Arguments.NodeDescriptor.NodeId;
                            row["Value"] = eventArgs.AttributeData.Value;
                            row["SourceTimestamp"] = (eventArgs.AttributeData.SourceTimestamp < (DateTime)SqlDateTime.MinValue)
                                                   ? (DateTime)SqlDateTime.MinValue
                                                   : eventArgs.AttributeData.SourceTimestamp;
                            row["ServerTimestamp"] = (eventArgs.AttributeData.ServerTimestamp < (DateTime)SqlDateTime.MinValue)
                                                   ? (DateTime)SqlDateTime.MinValue
                                                   : eventArgs.AttributeData.ServerTimestamp;
                            row["StatusCode"] = eventArgs.AttributeData.StatusCode.InternalValue;

                            Debug.Assert(!(table.Rows is null));
                            table.Rows.Add(row);

                            // Update the underlying DataSet using an insert command.
                            adapter.Update(dataSet, "UASimpleLog");
                        }
                    }
                    );
                Thread.Sleep(30 * 1000);
                Console.WriteLine();

                Console.WriteLine("Shutting down...");
                EasyUAClient.SharedInstance.UnsubscribeMonitoredItem(handle);
            }

            Console.WriteLine("Finished.");
        }
    }
}
