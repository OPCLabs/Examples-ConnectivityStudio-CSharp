// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

#pragma warning disable IDE0017 // Simplify object initialization
// ReSharper disable InconsistentNaming
// ReSharper disable InlineOutVariableDeclaration
// ReSharper disable LocalizableElement
// ReSharper disable UseObjectOrCollectionInitializer
#region Example
// This example shows different ways of constructing OPC UA node IDs.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.BaseLib;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.AddressSpace;
using OpcLabs.EasyOpc.UA.AddressSpace.Parsing;
using OpcLabs.EasyOpc.UA.AddressSpace.Parsing.Extensions;
using OpcLabs.EasyOpc.UA.AddressSpace.Standard;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace UACommonDocExamples._UANodeId
{
    class _Construction
    {
        // A node ID specifies a namespace (either by an URI or by an index), and an identifier.
        // The identifier can be numeric (an integer), string, GUID, or opaque.
        public static void Main1()
        {

            // A node ID can be specified in string form (so-called expanded text). 
            // The code below specifies a namespace URI (nsu=...), and an integer identifier (i=...).
            UANodeId nodeId1 = new UANodeId("nsu=http://test.org/UA/Data/ ;i=10853");
            Console.WriteLine(nodeId1);


            // Similarly, with a string identifier (s=...).
            UANodeId nodeId2 = new UANodeId("nsu=http://test.org/UA/Data/ ;s=someIdentifier");
            Console.WriteLine(nodeId2);


            // Actually, "s=" can be omitted (not recommended, though).
            UANodeId nodeId3 = new UANodeId("nsu=http://test.org/UA/Data/ ;someIdentifier");
            Console.WriteLine(nodeId3);
            // Notice that the output is normalized - the "s=" is added again.


            // Similarly, with a GUID identifier (g=...).
            UANodeId nodeId4 = new UANodeId("nsu=http://test.org/UA/Data/ ;g=BAEAF004-1E43-4A06-9EF0-E52010D5CD10");
            Console.WriteLine(nodeId4);
            // Notice that the output is normalized - uppercase letters in the GUI are converted to lowercase, etc.


            // Similarly, with an opaque identifier (b=..., in Base64 encoding).
            UANodeId nodeId5 = new UANodeId("nsu=http://test.org/UA/Data/ ;b=AP8=");
            Console.WriteLine(nodeId5);


            // Namespace index can be used instead of namespace URI. The server is allowed to change the namespace 
            // indices between sessions (except for namespace 0), and for this reason, you should avoid the use of
            // namespace indices, and rather use the namespace URIs whenever possible.
            UANodeId nodeId6 = new UANodeId("ns=2;i=10853");
            Console.WriteLine(nodeId6);


            // Namespace index can be also specified together with namespace URI. This is still safe, but may be 
            // a bit quicker to perform, because the client can just verify the namespace URI instead of looking 
            // it up.
            UANodeId nodeId7 = new UANodeId("nsu=http://test.org/UA/Data/ ;ns=2;i=10853");
            Console.WriteLine(nodeId7);


            // When neither namespace URI nor namespace index are given, the node ID is assumed to be in namespace
            // with index 0 and URI "http://opcfoundation.org/UA/", which is reserved by OPC UA standard. There are 
            // many standard nodes that live in this reserved namespace, but no nodes specific to your servers will 
            // be in the reserved namespace, and hence the need to specify the namespace with server-specific nodes.
            UANodeId nodeId8 = new UANodeId("i=2254");
            Console.WriteLine(nodeId8);


            // If you attempt to pass in a string that does not conform to the syntax rules, 
            // a UANodeIdFormatException is thrown.
            try
            {
                UANodeId nodeId9 = new UANodeId("nsu=http://test.org/UA/Data/ ;i=notAnInteger");
                Console.WriteLine(nodeId9);
            }
            catch (UANodeIdFormatException nodeIdFormatException)
            {
                Console.WriteLine($"*** Failure {nodeIdFormatException.Message}");
            }


            // There is a parser object that can be used to parse the expanded texts of node IDs. 
            UANodeIdParser nodeIdParser10 = new UANodeIdParser();
            UANodeId nodeId10 = nodeIdParser10.Parse("nsu=http://test.org/UA/Data/ ;i=10853");
            Console.WriteLine(nodeId10);


            // The parser can be used if you want to parse the expanded text of the node ID but do not want 
            // exceptions be thrown.
            UANodeIdParser nodeIdParser11 = new UANodeIdParser();
            IStringParsingError stringParsingError =
                nodeIdParser11.TryParse("nsu=http://test.org/UA/Data/ ;i=notAnInteger", out UANodeId nodeId11);
            if (stringParsingError is null)
                Console.WriteLine(nodeId11);
            else
                Console.WriteLine($"*** Failure: {stringParsingError.Message}");


            // You can also use the parser if you have node IDs where you want the default namespace be different 
            // from the standard "http://opcfoundation.org/UA/".
            UANodeIdParser nodeIdParser12 = new UANodeIdParser("http://test.org/UA/Data/");
            UANodeId nodeId12 = nodeIdParser12.Parse("i=10853");
            Console.WriteLine(nodeId12);


            // The namespace URI string (or the namespace index, or both) and the identifier can be passed to the
            // constructor separately.
            UANodeId nodeId13 = new UANodeId("http://test.org/UA/Data/", 10853);
            Console.WriteLine(nodeId13);


            // You can create a "null" node ID. Such node ID does not actually identify any valid node in OPC UA, but 
            // is useful as a placeholder or as a starting point for further modifications of its properties.
            UANodeId nodeId14 = new UANodeId();
            Console.WriteLine(nodeId14);


            // Properties of a node ID can be modified individually. The advantage of this approach is that you do 
            // not have to care about syntax of the node ID expanded text.
            UANodeId nodeId15 = new UANodeId();
            nodeId15.NamespaceUriString = "http://test.org/UA/Data/";
            nodeId15.Identifier = 10853;
            Console.WriteLine(nodeId15);


            // The same as above, but using an object initializer list.
            UANodeId nodeId16 = new UANodeId
            {
                NamespaceUriString = "http://test.org/UA/Data/",
                Identifier = 10853
            };
            Console.WriteLine(nodeId16);


            // If you know the type of the identifier upfront, it is safer to use typed properties that correspond 
            // to specific types of identifier. Here, with an integer identifier.
            UANodeId nodeId17 = new UANodeId();
            nodeId17.NamespaceUriString = "http://test.org/UA/Data/";
            nodeId17.NumericIdentifier = 10853;
            Console.WriteLine(nodeId17);


            // Similarly, with a string identifier.
            UANodeId nodeId18 = new UANodeId();
            nodeId18.NamespaceUriString = "http://test.org/UA/Data/";
            nodeId18.StringIdentifier = "someIdentifier";
            Console.WriteLine(nodeId18);


            // Similarly, with a GUID identifier.
            UANodeId nodeId19 = new UANodeId();
            nodeId19.NamespaceUriString = "http://test.org/UA/Data/";
            nodeId19.GuidIdentifier = Guid.Parse("BAEAF004-1E43-4A06-9EF0-E52010D5CD10");
            Console.WriteLine(nodeId19);


            // If you have GUID in its string form, the node ID object can parse it for you.
            UANodeId nodeId20 = new UANodeId();
            nodeId20.NamespaceUriString = "http://test.org/UA/Data/";
            nodeId20.GuidIdentifierString = "BAEAF004-1E43-4A06-9EF0-E52010D5CD10";
            Console.WriteLine(nodeId20);


            // And, with an opaque identifier.
            UANodeId nodeId21 = new UANodeId();
            nodeId21.NamespaceUriString = "http://test.org/UA/Data/";
            nodeId21.OpaqueIdentifier = new byte[] {0x00, 0xFF};
            Console.WriteLine(nodeId21);


            // Assigning an expanded text to a node ID parses the value being assigned and sets all corresponding
            // properties accordingly.
            UANodeId nodeId22 = new UANodeId();
            nodeId22.ExpandedText = "nsu=http://test.org/UA/Data/ ;i=10853";
            Console.WriteLine(nodeId22);


            // There is an implicit conversion from a string (representing the expanded text) to a node ID.
            // You can therefore use the expanded text (string) in place of any node ID object directly.
            UANodeId nodeId23 = "nsu=http://test.org/UA/Data/ ;i=10853";
            Console.WriteLine(nodeId23);


            // There is a copy constructor as well, creating a clone of an existing node ID.
            UANodeId nodeId24a = new UANodeId("nsu=http://test.org/UA/Data/ ;i=10853");
            Console.WriteLine(nodeId24a);
            UANodeId nodeId24b = new UANodeId(nodeId24a);
            Console.WriteLine(nodeId24b);


            // We have provided static classes with properties that correspond to all standard nodes specified by 
            // OPC UA. You can simply refer to these node IDs in your code.
            // The class names are UADataTypeIds, UAMethodIds, UAObjectIds, UAObjectTypeIds, UAReferenceTypeIds, 
            // UAVariableIds and UAVariableTypeIds.
            UANodeId nodeId25 = UAObjectIds.TypesFolder;
            Console.WriteLine(nodeId25);
            // When the UANodeId equals to one of the standard nodes, it is output in the shortened form - as the standard
            // name only.


            // You can also refer to any standard node using its name (in a string form).
            // Note that assigning a non-existing standard name is not allowed, and throws ArgumentException.
            UANodeId nodeId26 = new UANodeId();
            nodeId26.StandardName = "TypesFolder";
            Console.WriteLine(nodeId26);


            // When you browse for nodes in the OPC UA server, every returned node element contains a node ID that
            // you can use further.
            var client27 = new EasyUAClient();
            try
            {
                UANodeElementCollection nodeElementCollection27 = client27.Browse(
                        "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer",
                        UAObjectIds.Server,
                        new UABrowseParameters(UANodeClass.All, new[] { UAReferenceTypeIds.References }));
                if (nodeElementCollection27.Count != 0)
                {
                    UANodeId nodeId27 = nodeElementCollection27[0].NodeId;
                    Console.WriteLine(nodeId27);
                }
            }
            catch (UAException uaException)
            {
                Console.WriteLine("Failure: {0}", uaException.GetBaseException().Message);
            }


            // As above, but using a constructor that takes a node element as an input.
            var client28 = new EasyUAClient();
            try
            {
                UANodeElementCollection nodeElementCollection28 = client28.Browse(
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer",
                    UAObjectIds.Server,
                    new UABrowseParameters(UANodeClass.All, new[] { UAReferenceTypeIds.References }));
                if (nodeElementCollection28.Count != 0)
                {
                    UANodeId nodeId28 = new UANodeId(nodeElementCollection28[0]);
                    Console.WriteLine(nodeId28);
                }
            }
            catch (UAException uaException)
            {
                Console.WriteLine("Failure: {0}", uaException.GetBaseException().Message);
            }


            // Or, there is an explicit conversion from a node descriptor as well.
            var client29 = new EasyUAClient();
            try
            {
                UANodeElementCollection nodeElementCollection29 = client29.Browse(
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer",
                    UAObjectIds.Server,
                    new UABrowseParameters(UANodeClass.All, new[] { UAReferenceTypeIds.References }));
                if (nodeElementCollection29.Count != 0)
                {
                    UANodeId nodeId29 = (UANodeId) nodeElementCollection29[0];
                    Console.WriteLine(nodeId29);
                }
            }
            catch (UAException uaException)
            {
                Console.WriteLine("Failure: {0}", uaException.GetBaseException().Message);
            }
        }
    }
}
#endregion
