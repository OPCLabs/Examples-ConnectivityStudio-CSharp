// LogToSqlEnhanced: Logs OPC Data Access item changes into an SQL database, using a subscription. Item values and qualities
// are stored in their respective columns. Notifications with the same timestamp are merged into a single row.
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
using OpcLabs.BaseLib.Runtime.InteropServices;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.OperationModel;

namespace LogToSqlEnhanced
{
    class Program
    {
        static void Main()
        {
            ComManagement.Instance.AssureSecurityInitialization();

            const string connectionString =
                "Data Source=(local);Initial Catalog=QuickOPCExamples;Integrated Security=true";

            Console.WriteLine("Starting up...");
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create all necessary ADO.NET objects.
                var adapter = new SqlDataAdapter("SELECT * FROM ColumnarLog", connection);
                var dataSet = new DataSet();
                adapter.FillSchema(dataSet, SchemaType.Source, "ColumnarLog");
                adapter.InsertCommand = new SqlCommandBuilder(adapter).GetInsertCommand();
                adapter.UpdateCommand = new SqlCommandBuilder(adapter).GetUpdateCommand();
                DataTable table = dataSet.Tables["ColumnarLog"];

                Console.WriteLine("Logging for 30 seconds...");
                // Subscribe to OPC items, using an anonymous method to process the notifications.
                int[] handles = EasyDAClient.SharedInstance.SubscribeMultipleItems(
                    new[]
                        {
                            new DAItemGroupArguments("", "OPCLabs.KitServer.2", "Trends.Ramp (1 s)", 1000, null),
                            new DAItemGroupArguments("", "OPCLabs.KitServer.2", "Trends.Ramp (10 s)", 1000, null),
                            new DAItemGroupArguments("", "OPCLabs.KitServer.2", "Trends.Ramp (1 min)", 1000, null),
                            new DAItemGroupArguments("", "OPCLabs.KitServer.2", "Trends.Ramp (10 min)", 1000, null)
                        },
                    (_, eventArgs) =>
                    {
                        Debug.Assert(!(eventArgs is null));
                        
                        Console.Write(".");
                        // In this example, we only log valid data. Production logger would also log errors.
                        if (!(eventArgs.Vtq is null))
                        {
                            // Fill a DataRow with the OPC data, and add it to a DataTable.
                            DateTime timestamp = 
                                (eventArgs.Vtq.Timestamp < (DateTime) SqlDateTime.MinValue) 
                                ? (DateTime) SqlDateTime.MinValue 
                                : eventArgs.Vtq.Timestamp;

                            DataRow row = dataSet.Tables["ColumnarLog"].Rows.Find(timestamp);
                            bool adding = (row is null);
                            if (adding)
                            {
                                row = table.NewRow();
                                row["Timestamp"] = timestamp;
                            }

                            switch (eventArgs.Arguments.ItemDescriptor.ItemId)
                            {
                                case "Trends.Ramp (1 s)":
                                    row["Ramp1Value"] = eventArgs.Vtq.Value ?? DBNull.Value;
                                    row["Ramp1Quality"] = (short)eventArgs.Vtq.Quality;
                                    break;
                                case "Trends.Ramp (10 s)":
                                    row["Ramp2Value"] = eventArgs.Vtq.Value ?? DBNull.Value;
                                    row["Ramp2Quality"] = (short)eventArgs.Vtq.Quality;
                                    break;
                                case "Trends.Ramp (1 min)":
                                    row["Ramp3Value"] = eventArgs.Vtq.Value ?? DBNull.Value;
                                    row["Ramp3Quality"] = (short)eventArgs.Vtq.Quality;
                                    break;
                                case "Trends.Ramp (10 min)":
                                    row["Ramp4Value"] = eventArgs.Vtq.Value ?? DBNull.Value;
                                    row["Ramp4Quality"] = (short)eventArgs.Vtq.Quality;
                                    break;
                            }

                            if (adding)
                                table.Rows.Add(row);

                            // Update the underlying DataSet using an insert command.
                            adapter.Update(dataSet, "ColumnarLog");

                            // IMPROVE: For long-running logs, you may have to remove old DataTable.Rows from memory.
                        }
                    }
                    );
                System.Threading.Thread.Sleep(30 * 1000);
                Console.WriteLine();

                Console.WriteLine("Shutting down...");
                EasyDAClient.SharedInstance.UnsubscribeMultipleItems(handles);
            }

            Console.WriteLine("Finished.");
        }
    }
}
