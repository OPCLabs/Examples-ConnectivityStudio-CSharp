
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Windows.Forms;
using LiveBindingDemo.Properties;

namespace LiveBindingDemo
{
    public partial class Form1 : Form
    {
        // ReSharper disable once NotNullMemberIsNotInitialized
        public Form1()
        {
            InitializeComponent();
        }

        // ReSharper disable InconsistentNaming
        private void tabPage3_Enter(object sender, EventArgs e)
        // ReSharper restore InconsistentNaming
        {
            helpRichTextBox.Text = "";
        }

        // ReSharper disable InconsistentNaming
        private void tabPage2_Enter(object sender, EventArgs e)
        // ReSharper restore InconsistentNaming
        {
            helpRichTextBox.Text = "";
        }

        // ReSharper disable InconsistentNaming
        private void tabPage1_Enter(object sender, EventArgs e)
        // ReSharper restore InconsistentNaming
        {
            helpRichTextBox.Text = "";
        }

        // ReSharper disable InconsistentNaming
        private void tabPage4_Enter(object sender, EventArgs e)
        // ReSharper restore InconsistentNaming
        {
            helpRichTextBox.Text = "";
        }

        // ReSharper disable InconsistentNaming
        private void getOnEventLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        // ReSharper restore InconsistentNaming
        {
            helpRichTextBox.Text = Resources.Form1_getOnEventLinkLabel_LinkClicked_HelpText;
        }

        // ReSharper disable InconsistentNaming
        private void automaticGetLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        // ReSharper restore InconsistentNaming
        {
            helpRichTextBox.Text = Resources.Form1_automaticGetLinkLabel_LinkClicked_HelpText;
        }

        // ReSharper disable InconsistentNaming
        private void automaticSubscriptionLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        // ReSharper restore InconsistentNaming
        {
            helpRichTextBox.Text = Resources.Form1_automaticSubscriptionLinkLabel_LinkClicked_HelpText;
        }

        // ReSharper disable InconsistentNaming
        private void automaticReadLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        // ReSharper restore InconsistentNaming
        {
            helpRichTextBox.Text = Resources.Form1_automaticReadLinkLabel_LinkClicked_HelpText;
        }

        // ReSharper disable InconsistentNaming
        private void readOnCustomEventLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        // ReSharper restore InconsistentNaming
        {
            helpRichTextBox.Text = Resources.Form1_readOnCustomEventLinkLabel_LinkClicked_HelpText;
        }

        // ReSharper disable InconsistentNaming
        private void errorProviderLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        // ReSharper restore InconsistentNaming
        {
            helpRichTextBox.Text = Resources.Form1_errorProviderLinkLabel_LinkClicked_HelpText;
        }

        // ReSharper disable InconsistentNaming
        private void stringFormattingLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        // ReSharper restore InconsistentNaming
        {
            helpRichTextBox.Text = Resources.Form1_stringFormattingLinkLabel_LinkClicked_HelpText;
        }

        // ReSharper disable InconsistentNaming
        private void changeBackgroundLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        // ReSharper restore InconsistentNaming
        {
            helpRichTextBox.Text = Resources.Form1_changeBackgroundLinkLabel_LinkClicked_HelpText;
        }

        // ReSharper disable InconsistentNaming
        private void bindingKindsLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        // ReSharper restore InconsistentNaming
        {
            helpRichTextBox.Text = Resources.Form1_bindingKindsLinkLabel_LinkClicked_HelpText;
        }

        // ReSharper disable InconsistentNaming
        private void extendersLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        // ReSharper restore InconsistentNaming
        {
            helpRichTextBox.Text = Resources.Form1_extendersLinkLabel_LinkClicked_HelpText;
        }

        // ReSharper disable InconsistentNaming
        private void subscribeAndWriteLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        // ReSharper restore InconsistentNaming
        {
            helpRichTextBox.Text = Resources.Form1_subscribeAndWriteLinkLabel_LinkClicked_HelpText;
        }

        // ReSharper disable InconsistentNaming
        private void writeSingleLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        // ReSharper restore InconsistentNaming
        {
            helpRichTextBox.Text = Resources.Form1_writeSingleLinkLabel_LinkClicked_HelpText;
        }

        // ReSharper disable InconsistentNaming
        private void writeGroupLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        // ReSharper restore InconsistentNaming
        {
            helpRichTextBox.Text = Resources.Form1_writeGroupLinkLabel_LinkClicked_HelpText;
        }

        // ReSharper disable InconsistentNaming
        private void displayWriteErrorsLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        // ReSharper restore InconsistentNaming
        {
            helpRichTextBox.Text = Resources.Form1_displayWriteErrorsLinkLabel_LinkClicked_HelpText;
        }
    }
}
