<Query Kind="Statements">
  <NuGetReference>OpcLabs.QuickOpc</NuGetReference>
  <Namespace>OpcLabs.EasyOpc.UA</Namespace>
</Query>
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

// Dump the loggable entries originating in the OPC-UA client engine and the EasyUAClient component.
EasyUAClient.LogEntry += (sender, e) => e.Dump();

// Do something - invoke an OPC read, to trigger some loggable entries.
new EasyUAClient().ReadValue("opc.tcp://opcua.demo-this.com:51210/UA/SampleServer", "nsu=http://test.org/UA/Data/;i=10853");

Thread.Sleep(10*1000);