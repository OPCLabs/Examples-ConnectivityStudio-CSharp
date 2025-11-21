// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

#region Example
// UAServerWindowsServiceDemo: Shows how to use the component to create an OPC UA server hosted in a Windows service. It
// provides readable and writable nodes of various types.
//
// Install the service by running:
//      C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe /i UAServerWindowsServiceDemo.exe
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client, server and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

namespace UAServerWindowsServiceDemo
{
    partial class UAService1
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.easyUAServer1 = new OpcLabs.EasyOpc.UA.EasyUAServer(this.components);
            // 
            // easyUAServer1
            // 
            // UAService1
            // 
            this.ServiceName = "OpcWizardDemo";

        }

        #endregion

        private OpcLabs.EasyOpc.UA.EasyUAServer easyUAServer1;
    }
}
#endregion
