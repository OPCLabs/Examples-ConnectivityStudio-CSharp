// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
#region Example 
// Shows how different data types can be read, including rare types and arrays of values.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.OperationModel;

namespace DocExamples.DataAccess._EasyDAClient
{
    partial class ReadItem
    {
        // Instantiate the client object.
        static readonly EasyDAClient Client = new EasyDAClient();

        static void ReadAndDisplay(string itemId)
        {
            Console.WriteLine();
            Console.WriteLine("Reading \"{0}\"...", itemId);

            DAVtq vtq;
            try
            {
                vtq = Client.ReadItem("OPCLabs.KitServer.2", itemId);
            }
            catch (OpcException opcException)
            {
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message);
                return;
            }
            Console.WriteLine("Vtq: {0}", vtq);
        }

        public static void DataTypes()
        {
            ReadAndDisplay("Simulation.Register_EMPTY");
            ReadAndDisplay("Simulation.Register_NULL");
            ReadAndDisplay("Simulation.Register_DISPATCH");

            ReadAndDisplay("Simulation.ReadValue_I2");
            ReadAndDisplay("Simulation.ReadValue_I4");
            ReadAndDisplay("Simulation.ReadValue_R4");
            ReadAndDisplay("Simulation.ReadValue_R8");
            ReadAndDisplay("Simulation.ReadValue_CY");
            ReadAndDisplay("Simulation.ReadValue_DATE");
            ReadAndDisplay("Simulation.ReadValue_BSTR");
            ReadAndDisplay("Simulation.ReadValue_BOOL");
            ReadAndDisplay("Simulation.ReadValue_DECIMAL");
            ReadAndDisplay("Simulation.ReadValue_I1");
            ReadAndDisplay("Simulation.ReadValue_UI1");
            ReadAndDisplay("Simulation.ReadValue_UI2");
            ReadAndDisplay("Simulation.ReadValue_UI4");
            ReadAndDisplay("Simulation.ReadValue_INT");
            ReadAndDisplay("Simulation.ReadValue_UINT");

            ReadAndDisplay("Simulation.ReadValue_ArrayOfI2");
            ReadAndDisplay("Simulation.ReadValue_ArrayOfI4");
            ReadAndDisplay("Simulation.ReadValue_ArrayOfR4");
            ReadAndDisplay("Simulation.ReadValue_ArrayOfR8");
            ReadAndDisplay("Simulation.ReadValue_ArrayOfCY");
            ReadAndDisplay("Simulation.ReadValue_ArrayOfDATE");
            ReadAndDisplay("Simulation.ReadValue_ArrayOfBSTR");
            ReadAndDisplay("Simulation.ReadValue_ArrayOfBOOL");
            //ReadAndDisplay("Simulation.ReadValue_ArrayOfDECIMAL");
            ReadAndDisplay("Simulation.ReadValue_ArrayOfI1");
            ReadAndDisplay("Simulation.ReadValue_ArrayOfUI1");
            ReadAndDisplay("Simulation.ReadValue_ArrayOfUI2");
            ReadAndDisplay("Simulation.ReadValue_ArrayOfUI4");
            ReadAndDisplay("Simulation.ReadValue_ArrayOfINT");
            ReadAndDisplay("Simulation.ReadValue_ArrayOfUINT");
        }
    }
}
#endregion
