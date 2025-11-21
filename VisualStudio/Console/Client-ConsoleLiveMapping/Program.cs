// ConsoleLiveMapping: Creates an object structure for a boiler, describes its mapping into OPC Data Access server using 
// attributes, and then performs the live mapping. Boiler data is then read, written and/or subscribed to using plain .NET 
// object access.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Diagnostics;
using System.Threading;
using OpcLabs.BaseLib.Runtime.InteropServices;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.LiveMapping;
using OpcLabs.EasyOpc.DataAccess.LiveMapping.Extensions;

namespace ConsoleLiveMapping
{
    class Program
    {
        static void Main()
        {
            ComManagement.Instance.AssureSecurityInitialization();

            Console.WriteLine();
            Console.WriteLine("Mapping our data structures to OPC...");
            var mapper = new DAClientMapper();
            var boiler1 = new Boiler();
            mapper.Map(boiler1, new DAMappingContext
                {
                    ServerDescriptor = "OPCLabs.KitServer.2",   // local OPC server
                    // The NodeDescriptor below determines where in the OPC address space we want to map our data to.
                    NodeDescriptor = new DANodeDescriptor { BrowsePath = "/Boilers/Boiler #1"},
                    GroupParameters = 1000,  // requested update rate (for subscriptions)
                });

            Console.WriteLine();
            Console.WriteLine("Reading all data of the boiler...");
            mapper.Read();
            Console.WriteLine($"Drum level is: {boiler1.Drum.LevelIndicator.Output}");

            Console.WriteLine();
            Console.WriteLine("Writing new setpoint value...");
            boiler1.LevelController.SetPoint = 50.0;
            Debug.Assert(!(boiler1.LevelController is null));
            mapper.WriteTarget(boiler1.LevelController, /*recurse:*/false);

            Console.WriteLine();
            Console.WriteLine("Subscribing to boiler data changes...");
            mapper.Subscribe(/*active:*/true);

            Thread.Sleep(30 * 1000);

            Console.WriteLine();
            Console.WriteLine("Unsubscribing from boiler data changes...");
            mapper.Subscribe(/*active:*/false);

            Console.WriteLine();
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}
