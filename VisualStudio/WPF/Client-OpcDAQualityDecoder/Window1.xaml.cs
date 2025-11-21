
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using OpcLabs.EasyOpc.DataAccess;
using System;
using System.Windows;

namespace OpcDAQualityDecoder
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1
    {
        public Window1()
        {
            InitializeComponent();
        }

        // ReSharper disable InconsistentNaming
        private void DecodeButton_Click(object sender, RoutedEventArgs e)
        // ReSharper restore InconsistentNaming
        {
            int value;
            try
            {
                value = Convert.ToUInt16(valueTextBox.Text);
            }
            catch (FormatException formatException)
            {
                MessageBox.Show(formatException.Message);
                qualityChoiceStringTextBox.Text = "";
                statusStringTextBox.Text = "";
                limitStringTextBox.Text = "";
                return;
            }
            catch (OverflowException overflowException)
            {
                MessageBox.Show(overflowException.Message);
                qualityChoiceStringTextBox.Text = "";
                statusStringTextBox.Text = "";
                limitStringTextBox.Text = "";
                return;
            }
            var quality = new DAQuality(value);

            qualityChoiceStringTextBox.Text = quality.QualityChoiceBitField.ToString();
            statusStringTextBox.Text = quality.StatusBitField.ToString();
            limitStringTextBox.Text = quality.LimitBitField.ToString();
        }
    }
}
