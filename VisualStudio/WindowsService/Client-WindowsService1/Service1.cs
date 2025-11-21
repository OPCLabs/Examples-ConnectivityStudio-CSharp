// ReSharper disable StringLiteralTypo

// WindowsService1: A Windows Service that subscribes to items from the simulation server, and logs their changes into 
// a file.
//
// Install the service by running:
//      C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe /i WindowsService1.exe
// If you get "Access denied" error when starting the service, change its configuration to run under Local System account.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using JetBrains.Annotations;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.OperationModel;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        const string FilePath = "C:\\Service1.txt";

        // ReSharper disable once NotNullMemberIsNotInitialized
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            File.Create(FilePath).Close();

            easyDAClient1.SubscribeMultipleItems(
                new[]
                    {
                        new DAItemGroupArguments("", "OPCLabs.KitServer.2", "Simulation.Incrementing (1 s)", 100, null),
                        new DAItemGroupArguments("", "OPCLabs.KitServer.2", "Simulation.Ramp (10 s)", 1000, null),
                        new DAItemGroupArguments("", "OPCLabs.KitServer.2", "Simulation.Register_BSTR", 1000, null),
                        new DAItemGroupArguments("", "OPCLabs.KitServer.2", "Simulation.Register_BOOL", 1000, null)
                    });
        }

        protected override void OnStop()
        {
            easyDAClient1.UnsubscribeAllItems();
        }

        // ReSharper disable InconsistentNaming
        private void easyDAClient1_ItemChanged(object sender, EasyDAItemChangedEventArgs e)
        // ReSharper restore InconsistentNaming
        {
            string line;
            if (e.Exception is null)
            {
                Debug.Assert(!(e.Vtq is null));
                line = $"{e.Arguments.ItemDescriptor.ItemId}: {e.Vtq.DisplayValue()}";
            }
            else
                line = $"{e.Arguments.ItemDescriptor.ItemId}: ** {e.Exception.GetBaseException()} **";

            using (var textWriter = File.AppendText(FilePath))
                textWriter.WriteLine(line);
        }
    }
}
