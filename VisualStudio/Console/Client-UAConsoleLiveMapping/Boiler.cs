//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.BaseLib.LiveMapping;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.LiveMapping;

namespace UAConsoleLiveMapping
{
    // The Boiler and its constituents are described in our application domain terms, the way we want to work with them.
    // Attributes are used to describe the correspondence between our types and members, and OPC nodes.

    // This is how the boiler looks in OPC address space:
    //  - Boiler #1
    //      - CC1001                    (CustomController)
    //          - ControlOut
    //          - Description
    //          - Input1
    //          - Input2
    //          - Input3
    //      - Drum1001                  (BoilerDrum)
    //          - LIX001                (LevelIndicator)
    //              - Output
    //      - FC1001                    (FlowController)
    //          - ControlOut
    //          - Measurement
    //          - SetPoint
    //      - LC1001                    (LevelController)
    //          - ControlOut
    //          - Measurement
    //          - SetPoint
    //      - Pipe1001                  (BoilerInputPipe)
    //          - FTX001                (FlowTransmitter)
    //              - Output
    //      - Pipe1002                  (BoilerOutputPipe)
    //          - FTX002                (FlowTransmitter)
    //              - Output

    [UANamespace("http://opcfoundation.org/UA/Boiler/")]
    [UAType]
    class Boiler
    {
        // Specifying BrowsePath-s here only because we have named the class members differently from OPC node names.

        [UANode(BrowsePath = "/PipeX001")]
        public BoilerInputPipe InputPipe = new BoilerInputPipe();

        [UANode(BrowsePath = "/DrumX001")]
        public BoilerDrum Drum = new BoilerDrum();

        [UANode(BrowsePath = "/PipeX002")]
        public BoilerOutputPipe OutputPipe = new BoilerOutputPipe();

        [UANode(BrowsePath = "/FCX001")]
        public FlowController FlowController = new FlowController();

        [UANode(BrowsePath = "/LCX001")]
        public LevelController LevelController = new LevelController();

        [UANode(BrowsePath = "/CCX001")]
        public CustomController CustomController = new CustomController();
    }

    [UAType]
    class BoilerInputPipe
    {
        // Specifying BrowsePath-s here only because we have named the class members differently from OPC node names.

        [UANode(BrowsePath = "/FTX001")]
        public FlowTransmitter FlowTransmitter1 = new FlowTransmitter();

        [UANode(BrowsePath = "/ValveX001")]
        public Valve Valve = new Valve();
    }

    [UAType]
    class BoilerDrum
    {
        // Specifying BrowsePath-s here only because we have named the class members differently from OPC node names.

        [UANode(BrowsePath = "/LIX001")]
        public LevelIndicator LevelIndicator = new LevelIndicator();
    }

    [UAType]
    class BoilerOutputPipe
    {
        // Specifying BrowsePath-s here only because we have named the class members differently from OPC node names.

        [UANode(BrowsePath = "/FTX002")]
        public FlowTransmitter FlowTransmitter2 = new FlowTransmitter();
    }

    [UAType]
    class FlowController : GenericController
    {
    }

    [UAType]
    class LevelController : GenericController
    {
    }

    [UAType]
    class CustomController
    {
        [UANode, UAData(Operations = UADataMappingOperations.Write)]    // not readable
        public double Input1 { get; set; }

        [UANode, UAData(Operations = UADataMappingOperations.Write)]    // not readable
        public double Input2 { get; set; }

        [UANode, UAData(Operations = UADataMappingOperations.Write)]    // not readable
        public double Input3 { get; set; }

        [UANode, UAData(Operations = UADataMappingOperations.ReadAndSubscribe)] // no OPC writing
        public double ControlOut { get; set; }

        [UANode, UAData]
        public string Description { get; set; }
    }

    [UAType]
    class FlowTransmitter : GenericSensor
    {
    }

    [UAType]
    class Valve : GenericActuator
    {
    }

    [UAType]
    class LevelIndicator : GenericSensor
    {
    }

    [UAType]
    class GenericController
    {
        [UANode, UAData(Operations = UADataMappingOperations.ReadAndSubscribe)] // no OPC writing
        public double Measurement { get; set; }

        [UANode, UAData]
        public double SetPoint { get; set; }

        [UANode, UAData(Operations = UADataMappingOperations.ReadAndSubscribe)] // no OPC writing
        public double ControlOut { get; set; }
    }

    [UAType]
    class GenericSensor
    {
        // Meta-members are filled in by information collected during mapping, and allow access to it later from your code.
        // Alternatively, you can derive your class from UAMappedNode, which will bring in many meta-members automatically.
        [MetaMember("NodeDescriptor")]
        public UANodeDescriptor NodeDescriptor { get; set; }

        [UANode, UAData(Operations = UADataMappingOperations.ReadAndSubscribe)] // no OPC writing
        public double Output
        {
            get => _output;
            set
            {
                _output = value;
                Console.WriteLine($"Sensor \"{NodeDescriptor}\" output is now {value}.");
            }
        }

        private double _output;
    }

    [UAType]
    class GenericActuator
    {
        [UANode, UAData(Operations = UADataMappingOperations.Write)]    // generic actuator input is not readable
        public double Input { get; set; }
    }
}
