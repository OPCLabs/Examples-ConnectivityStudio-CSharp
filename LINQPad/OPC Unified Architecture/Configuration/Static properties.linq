<Query Kind="Statements">
  <NuGetReference>OpcLabs.QuickOpc</NuGetReference>
  <Namespace>OpcLabs.EasyOpc.UA</Namespace>
</Query>
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

// Parameters that are always shared by all instances of the component.
EasyUAClient.SharedParameters
.Dump("SharedParameters");

// Adaptable parameters for non-isolated client objects.
// When the <see cref="Isolated"/> property of the <see cref="EasyUAClient"/> is <c>false</c> (the default), these 
// adaptable parameters are used. When the <see cref="Isolated"/> property is <c>true</c>, each such instance has 
// its own set of adaptable parameters, taken from the <see cref="IsolatedParameters"/> property instead.
EasyUAClient.AdaptableParameters
	.Dump("AdaptableParameters");
