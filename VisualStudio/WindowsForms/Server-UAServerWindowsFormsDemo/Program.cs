// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable AccessToStaticMemberViaDerivedType
// ReSharper disable ArrangeModifiersOrder
// ReSharper disable PossibleNullReferenceException
#region Example
// A fully functional OPC UA demo server running in Windows Forms host.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client, server and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Windows.Forms;
using Opc.Ua;
using OpcLabs.EasyOpc.UA;

namespace UAServerWindowsFormsDemo
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if (NET6_0_OR_GREATER)
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
#endif

            // Enable the Windows Forms interaction by the server.
            EasyUAServer.SharedParameters.PluginSetups.FindName("UAWindowsFormsInteraction").Enabled = true;

            Application.Run(new MainForm());
        }
    }
}
#endregion
