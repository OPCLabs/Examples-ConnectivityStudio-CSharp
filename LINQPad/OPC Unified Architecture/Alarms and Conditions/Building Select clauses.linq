<Query Kind="Statements">
  <NuGetReference>OpcLabs.QuickOpc</NuGetReference>
  <Namespace>OpcLabs.EasyOpc.UA</Namespace>
  <Namespace>OpcLabs.EasyOpc.UA.AddressSpace</Namespace>
  <Namespace>OpcLabs.EasyOpc.UA.AlarmsAndConditions</Namespace>
  <Namespace>OpcLabs.EasyOpc.UA.Filtering</Namespace>
</Query>
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

new EasyUAClient()
	.SubscribeEvent(
		"opc.tcp://opcua.demo-this.com:62544/Quickstarts/AlarmConditionServer",
		UAObjectIds.Server,
		1000,
		new UAAttributeFieldCollection
		{
			// Select specific fields using standard operand symbols
			UABaseEventObject.Operands.NodeId,
			UABaseEventObject.Operands.SourceNode,
			UABaseEventObject.Operands.SourceName,
			UABaseEventObject.Operands.Time,
	
			// Select specific fields using an event type ID and a simple relative path
			UAFilterElements.SimpleAttribute(UAObjectTypeIds.BaseEventType, "/Message"),
			UAFilterElements.SimpleAttribute(UAObjectTypeIds.BaseEventType, "/Severity"),
		},
		(sender, eventArgs) => eventArgs.Dump(1),
		state:null);
Thread.Sleep(30*1000);