// SimpleLogToSql: Logs OPC Data Access item changes into an SQL database, using a subscription. Values of all data types are
// stored in a single SQL_VARIANT column.
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

namespace SimpleLogToSql
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
                var adapter = new SqlDataAdapter("SELECT * FROM SimpleLog", connection);
                var dataSet = new DataSet();
                adapter.FillSchema(dataSet, SchemaType.Source, "SimpleLog");
                adapter.InsertCommand = new SqlCommandBuilder(adapter).GetInsertCommand();
                DataTable table = dataSet.Tables["SimpleLog"];
                Debug.Assert(!(table is null));

                Console.WriteLine("Logging for 30 seconds...");
                // Subscribe to an OPC item, using an anonymous method to process the notifications.
                int handle = EasyDAClient.SharedInstance.SubscribeItem(
                    "", 
                    "OPCLabs.KitServer.2",
                    "Simulation.Incrementing (1 s)", 
                    100,
                    (_, eventArgs) =>
                        {
                            Debug.Assert(!(eventArgs is null));
                            
                            Console.Write(".");
                            // In this example, we only log valid data. Production logger would also log errors.
                            if (!(eventArgs.Vtq is null))
                            {
                                // Fill a DataRow with the OPC data, and add it to a DataTable.
                                Debug.Assert(!(table.Rows is null));
                                table.Rows.Clear();
                                DataRow row = table.NewRow();
                                row["ItemID"] = eventArgs.Arguments.ItemDescriptor.ItemId;
                                row["Value"] = eventArgs.Vtq.Value;
                                row["Timestamp"] = (eventArgs.Vtq.Timestamp < (DateTime)SqlDateTime.MinValue)
                                                       ? (DateTime)SqlDateTime.MinValue
                                                       : eventArgs.Vtq.Timestamp;
                                row["Quality"] = (short)eventArgs.Vtq.Quality;

                                Debug.Assert(!(table.Rows is null));
                                table.Rows.Add(row);

                                // Update the underlying DataSet using an insert command.
                                adapter.Update(dataSet, "SimpleLog");
                            }
                        }
                    );
                System.Threading.Thread.Sleep(30*1000);
                Console.WriteLine();

                Console.WriteLine("Shutting down...");
                EasyDAClient.SharedInstance.UnsubscribeItem(handle);
            }

            Console.WriteLine("Finished.");
        }
    }
}
