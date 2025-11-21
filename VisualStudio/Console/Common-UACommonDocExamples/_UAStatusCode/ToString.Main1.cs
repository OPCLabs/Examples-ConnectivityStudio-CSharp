// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException
#region Example
// This example shows how the OPC UA status codes are formatted to a string containing their symbolic name.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.UA;

namespace UACommonDocExamples._UAStatusCode
{
    class ToString
    {
        public static void Main1()
        {
            long[] internalValueArray = { 0, 0x80010000, 2147614720, 0x80340000 };

            foreach (long internalValue in internalValueArray)
                Console.WriteLine($"{internalValue}: {new UAStatusCode(internalValue)}");


            // Example output:
            //0: Good
            //2147549184: BadUnexpectedError
            //2147614720: BadInternalError
            //2150891520: BadNodeIdUnknown
        }
    }
}
#endregion
