// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

#region Example
// Attempts to parse a relative OPC-UA browse path and displays its elements.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.BaseLib;
using OpcLabs.EasyOpc.UA.Navigation;
using OpcLabs.EasyOpc.UA.Navigation.Parsing;

namespace UACommonDocExamples._UABrowsePathParser
{
    class TryParseRelative
    {
        public static void Main1()
        {
            var browsePathElements = new UABrowsePathElementCollection();

            var browsePathParser = new UABrowsePathParser();
            IStringParsingError stringParsingError = browsePathParser.TryParseRelative("/Data.Dynamic.Scalar.CycleComplete", browsePathElements);

            // Display results
            if (!(stringParsingError is null))
            {
                Console.WriteLine("*** Error: {0}", stringParsingError);
                return;
            }

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
