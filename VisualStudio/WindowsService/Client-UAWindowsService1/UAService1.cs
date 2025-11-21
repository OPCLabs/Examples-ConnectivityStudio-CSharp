// UAWindowsService1: A Windows Service that subscribes to variables from the OPC UA sample server, and logs their changes
// into a file.
//
// Install the service by running:
//      C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe /i UAWindowsService1.exe
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using JetBrains.Annotations;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UAWindowsService1
{
    public partial class UAService1 : ServiceBase
    {
        const string FilePath = "C:\\UAService1.txt";

        public UAService1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            File.Create(FilePath).Close();

            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
            // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            easyUAClient1.SubscribeMultipleMonitoredItems(
                new[]
                {
                    new EasyUAMonitoredItemArguments(null, endpointDescriptor,
                        "nsu=http://test.org/UA/Data/ ;i=10845", 1000),
                    new EasyUAMonitoredItemArguments(null, endpointDescriptor,
                        "nsu=http://test.org/UA/Data/ ;i=10853", 1000),
                    new EasyUAMonitoredItemArguments(null, endpointDescriptor,
                        "nsu=http://test.org/UA/Data/ ;i=10855", 1000)
                });
        }

        protected override void OnStop()
        {
            easyUAClient1.UnsubscribeAllMonitoredItems();
        }

        private void easyUAClient1_DataChangeNotification(
            object sender, 
            EasyUADataChangeNotificationEventArgs e)
        {
            string line;
            if (e.Exception is null)
            {
                Debug.Assert(!(e.AttributeData is null));
                line = $"{e.Arguments.NodeDescriptor.NodeId}: {e.AttributeData.DisplayValue()}";
            }
            else
                line = $"{e.Arguments.NodeDescriptor.NodeId}: ** {e.Exception.GetBaseException()} **";

            using (var textWriter = File.AppendText(FilePath))
                textWriter.WriteLine(line);
        }
    }
}
