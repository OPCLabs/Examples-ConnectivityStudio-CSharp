<Query Kind="Statements">
  <NuGetReference>OpcLabs.QuickOpc</NuGetReference>
  <Namespace>OpcLabs.EasyOpc.UA</Namespace>
</Query>

// Note that this only works once in each application domain.
// Re-running the query in LINQPad results in no dump, until the appdomain is unloaded.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

EasyUAClient.SharedParameters.EngineParameters.ApplicationParameters.ApplicationManifest.ApplicationName = "My Application";
EasyUAClient.SharedParameters.EngineParameters.ApplicationParameters.ApplicationManifest.ApplicationUriString = "http://opcua.demo-this.com/MyApplication";	// optional

// Dump the loggable entry containing client certificate parameters.
EasyUAClient.LogEntry += (sender, e) => { if (e.EventId == 161/*UAEngineEventIds.ClientInstanceCertificateParameters*/) e.Dump(); };

// Do something - invoke an OPC read, to trigger loggable entries.
new EasyUAClient().ReadValue("opc.tcp://opcua.demo-this.com:51210/UA/SampleServer", "nsu=http://test.org/UA/Data/;i=10853");

// The certificate will be located or created in a directory similar to:
// C:\ProgramData\OPC Foundation\CertificateStores\MachineDefault\certs
// or
// C:\Users\All Users\OPC Foundation\CertificateStores\UA Applications\certs\
// and its subject will be as given by the application name.