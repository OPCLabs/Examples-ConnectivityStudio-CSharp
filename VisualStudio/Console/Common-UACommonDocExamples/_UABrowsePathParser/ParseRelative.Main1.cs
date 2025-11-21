// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

#region Example
// Parses a relative OPC-UA browse path and displays its elements.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA.Navigation;
using OpcLabs.EasyOpc.UA.Navigation.Parsing;

namespace UACommonDocExamples._UABrowsePathParser
{
    class ParseRelative
    {
        public static void Main1()
        {
            var browsePathParser = new UABrowsePathParser();
            UABrowsePathElementCollection browsePathElements;
            try
            {
                browsePathElements = browsePathParser.ParseRelative("/Data.Dynamic.Scalar.CycleComplete");
            }
            catch (UABrowsePathFormatException browsePathFormatException)
            {
                Console.WriteLine("*** Failure: {0}", browsePathFormatException.GetBaseException().Message);
                return;
            }

            // Display results
            foreach (UABrowsePathElement browsePathElement in browsePathElements)
                Console.WriteLine(browsePathElement);

            // Example output:
            // /Data
            // .Dynamic
            // .Scalar
            // .CycleComplete        
        }
    }
}
#endregion
