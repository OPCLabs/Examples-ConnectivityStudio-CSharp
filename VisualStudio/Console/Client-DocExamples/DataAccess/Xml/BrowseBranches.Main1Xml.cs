// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
#region Example
// This example shows how to obtain all branches at the root of the address space. For each branch, it displays whether 
// it may have child nodes.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.AddressSpace;
using OpcLabs.EasyOpc.OperationModel;

namespace DocExamples.DataAccess.Xml
{
    class BrowseBranches
    {
        public static void Main1Xml()
        {
            // Instantiate the client object.
            var client = new EasyDAClient();
            DANodeElementCollection branchElements;
            try
            {
                branchElements = client.BrowseBranches("http://opcxml.demo-this.com/XmlDaSampleServer/Service.asmx");
            }
            catch (OpcException opcException)
            {
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message);
                return;
            }

            foreach (DANodeElement branchElement in branchElements)
                Console.WriteLine($"BranchElements(\"{branchElement.Name}\").HasChildren: {branchElement.HasChildren}");
        }


        // Example output:
        //
        //BranchElements("Static").HasChildren: True
        //BranchElements("Dynamic").HasChildren: True
    }
}
#endregion
