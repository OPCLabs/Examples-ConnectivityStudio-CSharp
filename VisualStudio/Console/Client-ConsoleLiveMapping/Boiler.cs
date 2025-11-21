//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.BaseLib.LiveMapping;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.LiveMapping;

namespace ConsoleLiveMapping
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
    
    [DAType]
    class Boiler
    {
        // Specifying BrowsePath-s here only because we have named the class members differently from OPC node names.

        [DANode(BrowsePath = "Pipe1001")]
        public BoilerInputPipe InputPipe = new BoilerInputPipe();

        [DANode(BrowsePath = "Drum1001")]
        public BoilerDrum Drum = new BoilerDrum();

        [DANode(BrowsePath = "Pipe1002")]
        public BoilerOutputPipe OutputPipe = new BoilerOutputPipe();

        [DANode(BrowsePath = "FC1001")]
        public FlowController FlowController = new FlowController();

        [DANode(BrowsePath = "LC1001")]
        public LevelController LevelController = new LevelController();

        [DANode(BrowsePath = "CC1001")]
        public CustomController CustomController = new CustomController();
    }

    [DAType]
    class BoilerInputPipe
    {
        // Specifying BrowsePath-s here only because we have named the class members differently from OPC node names.

        [DANode(BrowsePath = "FTX001")]
        public FlowTransmitter FlowTransmitter1 = new FlowTransmitter();

        [DANode(BrowsePath = "ValveX001")]
        public Valve Valve = new Valve();
    }

    [DAType]
    class BoilerDrum
    {
        // Specifying BrowsePath-s here only because we have named the class members differently from OPC node names.

        [DANode(BrowsePath = "LIX001")]
        public LevelIndicator LevelIndicator = new LevelIndicator();
    }

    [DAType]
    class BoilerOutputPipe
    {
        // Specifying BrowsePath-s here only because we have named the class members differently from OPC node names.

        [DANode(BrowsePath = "FTX002")]
        public FlowTransmitter FlowTransmitter2 = new FlowTransmitter();
    }

    [DAType]
    class FlowController : GenericController
    {
    }

    [DAType]
    class LevelController : GenericController
    {
    }

    [DAType]
    class CustomController
    {
        [DANode, DAItem]
        public double Input1 { get; set; }

        [DANode, DAItem]
        public double Input2 { get; set; }

        [DANode, DAItem]
        public double Input3 { get; set; }

        [DANode, DAItem(Operations = DAItemMappingOperations.ReadAndSubscribe)] // no OPC writing
        public double ControlOut { get; set; }

        [DANode, DAItem]
        public string Description { get; set; }
    }

    [DAType]
    class FlowTransmitter : GenericSensor
    {
    }

    [DAType]
    class Valve : GenericActuator
    {
    }

    [DAType]
    class LevelIndicator : GenericSensor
    {
    }

    [DAType]
    class GenericController
    {
        [DANode, DAItem(Operations = DAItemMappingOperations.ReadAndSubscribe)] // no OPC writing
        public double Measurement { get; set; }

        [DANode, DAItem]
        public double SetPoint { get; set; }

        [DANode, DAItem(Operations = DAItemMappingOperations.ReadAndSubscribe)] // no OPC writing
        public double ControlOut { get; set; }
    }

    [DAType]
    class GenericSensor
    {
        // Meta-members are filled in by information collected during mapping, and allow access to it later from your code.
        // Alternatively, you can derive your class from DAMappedNode, which will bring in many meta-members automatically.
        [MetaMember("NodeDescriptor")]
        public DANodeDescriptor NodeDescriptor { get; set; }

        [DANode, DAItem(Operations = DAItemMappingOperations.ReadAndSubscribe)] // no OPC writing
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

    [DAType]
    class GenericActuator
    {
        [DANode, DAItem]
        public double Input { get; set; }
    }
}
