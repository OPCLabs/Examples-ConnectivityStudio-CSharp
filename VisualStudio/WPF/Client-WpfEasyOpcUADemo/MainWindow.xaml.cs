
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.Forms.Browsing;
using OpcLabs.EasyOpc.UA.OperationModel;
using System;
using System.Globalization;
using System.Reflection;
using System.Windows;

// ReSharper disable InconsistentNaming
// ReSharper disable PossibleNullReferenceException
// ReSharper disable RedundantExtendsListEntry
// ReSharper disable UnusedMember.Global

namespace WpfEasyOpcUADemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            easyUAClient1.DataChangeNotification += easyUAClient1_DataChangeNotification;
        }

        private void readButton_Click(object sender, RoutedEventArgs e)
        {
            UAAttributeData attributeData = null;
            Exception exception = null;
            try
            {
                attributeData = easyUAClient1.Read(serverUriTextBox.Text, nodeIdTextBox.Text);
            }
            catch (UAException ex)
            {
                exception = ex;
            }
            DisplayAttributeData(attributeData);
            DisplayException(exception);
        }

        private void DisplayAttributeData(UAAttributeData attributeData)
        {
            if (attributeData is null)
            {
                valueTextBox.Text = "";
                statusTextBox.Text = "";
                sourceTimestampTextBox.Text = "";
                serverTimestampTextBox.Text = "";
            }
            else
            {
                valueTextBox.Text = attributeData.DisplayValue();
                statusTextBox.Text = attributeData.StatusCode.ToString();
                sourceTimestampTextBox.Text = attributeData.SourceTimestamp.ToString(CultureInfo.CurrentCulture);
                serverTimestampTextBox.Text = attributeData.ServerTimestamp.ToString(CultureInfo.CurrentCulture);
            }
        }

        private void DisplayException(Exception exception)
        {
            exceptionTextBox.Text = (exception is null) ? "" : exception.GetBaseException().Message;
        }

        private bool _isSubscribed/* = false*/;
        private int _handle/* = 0*/;

        public bool IsSubscribed
        {
            get => _isSubscribed;
            set
            {
                _isSubscribed = value;
                subscribeMonitoredItemButton.IsEnabled = !_isSubscribed;
                changeMonitoredItemSubscriptionButton.IsEnabled = _isSubscribed;
                unsubscribeMonitoredItemButton.IsEnabled = _isSubscribed;
            }
        }

        private void subscribeMonitoredItemButton_Click(object sender, RoutedEventArgs e)
        {
            _handle = easyUAClient1.SubscribeDataChange(serverUriTextBox.Text, nodeIdTextBox.Text,
                                               (int)samplingIntervalNumericUpDown.Value);
            IsSubscribed = true;
        }

        private void changeMonitoredItemSubscriptionButton_Click(object sender, RoutedEventArgs e)
        {
            easyUAClient1.ChangeMonitoredItemSubscription(_handle, (int)samplingIntervalNumericUpDown.Value);
        }

        private void unsubscribeMonitoredItemButton_Click(object sender, RoutedEventArgs e)
        {
            Unsubscribe();
        }

        private void Unsubscribe()
        {
            easyUAClient1.UnsubscribeMonitoredItem(_handle);
            _handle = 0;
            IsSubscribed = false;
        }

        private void easyUAClient1_DataChangeNotification(object sender, EasyUADataChangeNotificationEventArgs e)
        {
            DisplayAttributeData((e.Exception is null) ? e.AttributeData : null);
            DisplayException(e.Exception);
        }

        private void aboutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Assembly.GetExecutingAssembly().FullName, "Assembly Name", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsSubscribed)
                Unsubscribe();
            Close();
        }

        private void discoverServerButton_Click(object sender, RoutedEventArgs e)
        {
            uaHostAndEndpointDialog1.EndpointDescriptor = serverUriTextBox.Text;
            if (uaHostAndEndpointDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                serverUriTextBox.Text = uaHostAndEndpointDialog1.EndpointDescriptor.UrlString;
        }

        private void browseDataButton_Click(object sender, RoutedEventArgs e)
        {
            uaDataDialog1.EndpointDescriptor = serverUriTextBox.Text;
            if (uaDataDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                nodeIdTextBox.Text = uaDataDialog1.NodeDescriptor.NodeId;
        }

        private void writeValueButton_Click(object sender, RoutedEventArgs e)
        {
            Exception exception = null;
            try
            {
                easyUAClient1.WriteValue(serverUriTextBox.Text, nodeIdTextBox.Text, valueToWriteTextBox.Text);
            }
            catch (UAException ex)
            {
                exception = ex;
            }
            DisplayException(exception);
        }

        private readonly EasyUAClient easyUAClient1 = new EasyUAClient();
        private readonly UADataDialog uaDataDialog1 = new UADataDialog();
        private readonly UAHostAndEndpointDialog uaHostAndEndpointDialog1 = new UAHostAndEndpointDialog();
    }
}
