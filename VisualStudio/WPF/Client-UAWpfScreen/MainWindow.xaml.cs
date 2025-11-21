// ReSharper disable PossibleNullReferenceException

//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System.Windows;
using System.Windows.Controls;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UAWpfScreen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow/* : Window*/
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClientOnDataChangeNotification(object sender, EasyUADataChangeNotificationEventArgs eventArgs)
        {
            // We have passed in a reference to the target TextBox as the State property in EasyUAMonitoredItemArguments.
            var textBox = (TextBox) eventArgs.Arguments.State;

            if (eventArgs.Succeeded)
                textBox.Text = eventArgs.AttributeData.DisplayValue();
            else
                textBox.Text = "*** Error: " + eventArgs.Exception;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Define the target controls, the OPC data we want to monitor, and how fast.
            var arguments = new []
            {
                new EasyUAMonitoredItemArguments(TextBox1, "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer", "nsu=http://test.org/UA/Data/ ;i=10845", 1000),
                new EasyUAMonitoredItemArguments(TextBox2, "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer", "nsu=http://test.org/UA/Data/ ;i=10853", 1000),
                new EasyUAMonitoredItemArguments(TextBox3, "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer", "nsu=http://test.org/UA/Data/ ;i=10855", 1000)
                // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
                // or "https://opcua.demo-this.com:51212/UA/SampleServer/"
            };

            // Hook the event handler, and subscribe to OPC data
            _client.DataChangeNotification += ClientOnDataChangeNotification;
            _handles = _client.SubscribeMultipleMonitoredItems(arguments);
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            // Unsubscribe from OPC data, and unhook the event handler
            _client.UnsubscribeMultipleMonitoredItems(_handles);
            _client.DataChangeNotification -= ClientOnDataChangeNotification;
        }

        private readonly EasyUAClient _client = new EasyUAClient();

        private int[] _handles;
    }
}
