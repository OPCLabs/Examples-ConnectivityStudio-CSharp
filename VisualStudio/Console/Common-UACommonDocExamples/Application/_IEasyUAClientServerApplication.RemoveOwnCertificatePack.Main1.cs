// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable CheckNamespace
// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// Shows how to remove the own application certificate pack.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA.Application;
using OpcLabs.EasyOpc.UA.Application.Extensions;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UACommonDocExamples.Application._IEasyUAClientServerApplication
{
    class RemoveOwnCertificatePack
    {
        public static void Main1()
        {
            // Obtain the application interface.
            EasyUAApplication application = EasyUAApplication.Instance;

            try
            {
                Console.WriteLine("Removing the own application certificate pack...");
                application.RemoveOwnCertificatePack();
                Console.WriteLine("The application certificate pack has been removed.");
            }
            catch (UAException uaException)
            {
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message);
            }
        }
    }
}
#endregion

