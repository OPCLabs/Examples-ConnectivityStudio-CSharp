// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

// ReSharper disable CheckNamespace
// ReSharper disable CommentTypo
#region Example
// This example shows how to obtain all leaves under the "Simulation" branch of the address space. For each leaf, it displays 
// the ItemID of the node.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.AddressSpace;
using OpcLabs.EasyOpc.OperationModel;

namespace DocExamples.DataAccess._EasyDAClient
{
    class BrowseLeaves
    {
        public static void Main1()
        {
            // Instantiate the client object.
            var client = new EasyDAClient();
            DANodeElementCollection leafElements;
            try
            {
                leafElements = client.BrowseLeaves("", "OPCLabs.KitServer.2", "Simulation");
            }
            catch (OpcException opcException)
            {
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message);
                return;
            }

            foreach (DANodeElement leafElement in leafElements)
                Console.WriteLine($"LeafElements(\"{leafElement.Name}\").ItemId: {leafElement.ItemId}");
        }


        // Example output:
        //
        //LeafElements("Register_ArrayOfI2").ItemId: Simulation.Register_ArrayOfI2
        //LeafElements("Register_ArrayOfI4").ItemId: Simulation.Register_ArrayOfI4
        //LeafElements("Staircase 0:2 (10 s)").ItemId: Simulation.Staircase 0:2 (10 s)
        //LeafElements("Constant_VARIANT").ItemId: Simulation.Constant_VARIANT
        //LeafElements("Staircase 0:10 (1 s)").ItemId: Simulation.Staircase 0:10 (1 s)
        //LeafElements("Register_DATE").ItemId: Simulation.Register_DATE
        //LeafElements("Constant_RECORD").ItemId: Simulation.Constant_RECORD
        //LeafElements("ReadValue_DECIMAL").ItemId: Simulation.ReadValue_DECIMAL
        //LeafElements("Ramp 0:360 (1 s)").ItemId: Simulation.Ramp 0:360 (1 s)
        //LeafElements("Constant_NULL").ItemId: Simulation.Constant_NULL
        //LeafElements("ReadValue_ArrayOfUI2").ItemId: Simulation.ReadValue_ArrayOfUI2
        //LeafElements("ReadValue_ArrayOfUI1").ItemId: Simulation.ReadValue_ArrayOfUI1
        //LeafElements("ReadValue_ArrayOfUI4").ItemId: Simulation.ReadValue_ArrayOfUI4
        //LeafElements("Constant_CY").ItemId: Simulation.Constant_CY
        //LeafElements("Staircase 0:2 (1 min)").ItemId: Simulation.Staircase 0:2 (1 min)
        //LeafElements("Staircase 0:2 (10 min)").ItemId: Simulation.Staircase 0:2 (10 min)
        //LeafElements("Square (10 s)").ItemId: Simulation.Square(10 s)
        //LeafElements("Register_ArrayOfBSTR").ItemId: Simulation.Register_ArrayOfBSTR
        //LeafElements("ReadValue_I2").ItemId: Simulation.ReadValue_I2
        //LeafElements("ReadValue_I1").ItemId: Simulation.ReadValue_I1
        //LeafElements("ReadValue_I4").ItemId: Simulation.ReadValue_I4
        //LeafElements("Ramp (1 s)").ItemId: Simulation.Ramp(1 s)
        //LeafElements("ReadValue_ArrayOfDATE").ItemId: Simulation.ReadValue_ArrayOfDATE
        //LeafElements("OnOff (1 s)").ItemId: Simulation.OnOff(1 s)
        //LeafElements("AlternatingQuality Uncertain (1 s)").ItemId: Simulation.AlternatingQuality Uncertain(1 s)
        //LeafElements("Register_NULL").ItemId: Simulation.Register_NULL
        //LeafElements("Random (1 min)").ItemId: Simulation.Random(1 min)
        //LeafElements("Random (10 min)").ItemId: Simulation.Random(10 min)
        //LeafElements("AlternatingError (10 s)").ItemId: Simulation.AlternatingError(10 s)
        //LeafElements("ReadValue_ArrayOfI1").ItemId: Simulation.ReadValue_ArrayOfI1
        //LeafElements("ReadValue_ArrayOfI2").ItemId: Simulation.ReadValue_ArrayOfI2
        //LeafElements("ReadValue_UI2").ItemId: Simulation.ReadValue_UI2
        //LeafElements("ReadValue_ArrayOfI4").ItemId: Simulation.ReadValue_ArrayOfI4
        //LeafElements("ReadValue_UI1").ItemId: Simulation.ReadValue_UI1
        //LeafElements("ReadValue_UI4").ItemId: Simulation.ReadValue_UI4
        //LeafElements("Weekdays (1 s)").ItemId: Simulation.Weekdays(1 s)
        //LeafElements("AlternatingQuality Uncertain (1 min)").ItemId: Simulation.AlternatingQuality Uncertain(1 min)
        //LeafElements("AlternatingQuality Uncertain (10 min)").ItemId: Simulation.AlternatingQuality Uncertain(10 min)
        //LeafElements("Weekdays (1 min)").ItemId: Simulation.Weekdays(1 min)
        //LeafElements("Weekdays (10 min)").ItemId: Simulation.Weekdays(10 min)
        //LeafElements("OnOff (10 s)").ItemId: Simulation.OnOff(10 s)
        //LeafElements("ReadWriteCount").ItemId: Simulation.ReadWriteCount
        //LeafElements("Register_UNKNOWN").ItemId: Simulation.Register_UNKNOWN
        //LeafElements("AlternatingQuality Uncertain (10 s)").ItemId: Simulation.AlternatingQuality Uncertain(10 s)
        //LeafElements("Constant_BSTR").ItemId: Simulation.Constant_BSTR
        //LeafElements("Constant_ERROR").ItemId: Simulation.Constant_ERROR
        //LeafElements("Constant_UI2").ItemId: Simulation.Constant_UI2
        //LeafElements("Constant_UI1").ItemId: Simulation.Constant_UI1
        //LeafElements("Constant_UI4").ItemId: Simulation.Constant_UI4
        //LeafElements("Constant_R4").ItemId: Simulation.Constant_R4
        //LeafElements("Constant_R8").ItemId: Simulation.Constant_R8
        //LeafElements("ReadValue_ArrayOfBSTR").ItemId: Simulation.ReadValue_ArrayOfBSTR
        //LeafElements("Register_ArrayOfR4").ItemId: Simulation.Register_ArrayOfR4
        //LeafElements("Register_ArrayOfR8").ItemId: Simulation.Register_ArrayOfR8
        //LeafElements("Ramp 0:360 (1 min)").ItemId: Simulation.Ramp 0:360 (1 min)
        //LeafElements("Ramp 0:360 (10 min)").ItemId: Simulation.Ramp 0:360 (10 min)
        //LeafElements("RegisterSet_n").ItemId: 
        //LeafElements("Register_ArrayOfUI4").ItemId: Simulation.Register_ArrayOfUI4
        //LeafElements("Register_ArrayOfUI1").ItemId: Simulation.Register_ArrayOfUI1
        //LeafElements("Register_ArrayOfUI2").ItemId: Simulation.Register_ArrayOfUI2
        //LeafElements("Register").ItemId: Simulation.Register
        //LeafElements("Constant_EMPTY").ItemId: Simulation.Constant_EMPTY
        //LeafElements("Register_RECORD").ItemId: Simulation.Register_RECORD
        //LeafElements("ReadValue_ArrayOfBOOL").ItemId: Simulation.ReadValue_ArrayOfBOOL
        //LeafElements("AlternatingError (1 min)").ItemId: Simulation.AlternatingError(1 min)
        //LeafElements("AlternatingError (10 min)").ItemId: Simulation.AlternatingError(10 min)
        //LeafElements("OnOff (1 min)").ItemId: Simulation.OnOff(1 min)
        //LeafElements("ReadValue_DATE").ItemId: Simulation.ReadValue_DATE
        //LeafElements("Register_ERROR").ItemId: Simulation.Register_ERROR
        //LeafElements("ReadValue_ArrayOfUINT").ItemId: Simulation.ReadValue_ArrayOfUINT
        //LeafElements("Incrementing (10 s)").ItemId: Simulation.Incrementing(10 s)
        //LeafElements("ReadValue_ArrayOfINT").ItemId: Simulation.ReadValue_ArrayOfINT
        //LeafElements("ReadValue_BOOL").ItemId: Simulation.ReadValue_BOOL
        //LeafElements("Register_ArrayOfCY").ItemId: Simulation.Register_ArrayOfCY
        //LeafElements("Incrementing (1 s)").ItemId: Simulation.Incrementing(1 s)
        //LeafElements("Constant_UINT").ItemId: Simulation.Constant_UINT
        //LeafElements("ReadValue_ArrayOfR4").ItemId: Simulation.ReadValue_ArrayOfR4
        //LeafElements("ReadValue_ArrayOfR8").ItemId: Simulation.ReadValue_ArrayOfR8
        //LeafElements("Constant_I4").ItemId: Simulation.Constant_I4
        //LeafElements("Constant_I2").ItemId: Simulation.Constant_I2
        //LeafElements("Constant_I1").ItemId: Simulation.Constant_I1
        //LeafElements("Register_BOOL").ItemId: Simulation.Register_BOOL
        //LeafElements("Constant_UNKNOWN").ItemId: Simulation.Constant_UNKNOWN
        //LeafElements("Ramp 0:100 (1 s)").ItemId: Simulation.Ramp 0:100 (1 s)
        //LeafElements("Register_UI4").ItemId: Simulation.Register_UI4
        //LeafElements("Register_UI2").ItemId: Simulation.Register_UI2
        //LeafElements("Register_UI1").ItemId: Simulation.Register_UI1
        //LeafElements("AlternatingError (1 s)").ItemId: Simulation.AlternatingError(1 s)
        //LeafElements("Sine (10 s)").ItemId: Simulation.Sine(10 s)
        //LeafElements("Constant_BOOL").ItemId: Simulation.Constant_BOOL
        //LeafElements("Sine -100:100 (10 s)").ItemId: Simulation.Sine -100:100 (10 s)
        //LeafElements("Register_UINT").ItemId: Simulation.Register_UINT
        //LeafElements("Constant").ItemId: Simulation.Constant
        //LeafElements("Sine (1 s)").ItemId: Simulation.Sine(1 s)
        //LeafElements("Register_I1").ItemId: Simulation.Register_I1
        //LeafElements("Register_I2").ItemId: Simulation.Register_I2
        //LeafElements("Register_I4").ItemId: Simulation.Register_I4
        //LeafElements("ReadValue_ArrayOfCY").ItemId: Simulation.ReadValue_ArrayOfCY
        //LeafElements("Register_ArrayOfUINT").ItemId: Simulation.Register_ArrayOfUINT
        //LeafElements("Incrementing (1 min)").ItemId: Simulation.Incrementing(1 min)
        //LeafElements("Weekdays (10 s)").ItemId: Simulation.Weekdays(10 s)
        //LeafElements("Square (1 s)").ItemId: Simulation.Square(1 s)
        //LeafElements("Sine (1 min)").ItemId: Simulation.Sine(1 min)
        //LeafElements("Sine (10 min)").ItemId: Simulation.Sine(10 min)
        //LeafElements("Constant_DISPATCH").ItemId: Simulation.Constant_DISPATCH
        //LeafElements("Ramp 0:100 (1 min)").ItemId: Simulation.Ramp 0:100 (1 min)
        //LeafElements("Ramp 0:100 (10 min)").ItemId: Simulation.Ramp 0:100 (10 min)
        //LeafElements("Random (1 s)").ItemId: Simulation.Random(1 s)
        //LeafElements("Staircase 0:10 (1 min)").ItemId: Simulation.Staircase 0:10 (1 min)
        //LeafElements("Staircase 0:10 (10 min)").ItemId: Simulation.Staircase 0:10 (10 min)
        //LeafElements("AlternatingQuality Bad (1 s)").ItemId: Simulation.AlternatingQuality Bad(1 s)
        //LeafElements("Sine -100:100 (1 s)").ItemId: Simulation.Sine -100:100 (1 s)
        //LeafElements("Register_ArrayOfDATE").ItemId: Simulation.Register_ArrayOfDATE
        //LeafElements("AlternatingQuality Bad (10 s)").ItemId: Simulation.AlternatingQuality Bad(10 s)
        //LeafElements("ReadValue_R4").ItemId: Simulation.ReadValue_R4
        //LeafElements("ReadValue_R8").ItemId: Simulation.ReadValue_R8
        //LeafElements("Ramp (1 min)").ItemId: Simulation.Ramp(1 min)
        //LeafElements("Ramp (10 min)").ItemId: Simulation.Ramp(10 min)
        //LeafElements("Register_DISPATCH").ItemId: Simulation.Register_DISPATCH
        //LeafElements("OnOff (10 min)").ItemId: Simulation.OnOff(10 min)
        //LeafElements("ReadValue_BSTR").ItemId: Simulation.ReadValue_BSTR
        //LeafElements("Staircase 0:10 (10 s)").ItemId: Simulation.Staircase 0:10 (10 s)
        //LeafElements("Random (10 s)").ItemId: Simulation.Random(10 s)
        //LeafElements("Incrementing").ItemId: Simulation.Incrementing
        //LeafElements("Register_BSTR").ItemId: Simulation.Register_BSTR
        //LeafElements("ReadValue_UINT").ItemId: Simulation.ReadValue_UINT
        //LeafElements("Register_CY").ItemId: Simulation.Register_CY
        //LeafElements("AlternatingQuality Bad (1 min)").ItemId: Simulation.AlternatingQuality Bad(1 min)
        //LeafElements("AlternatingQuality Bad (10 min)").ItemId: Simulation.AlternatingQuality Bad(10 min)
        //LeafElements("Random").ItemId: Simulation.Random
        //LeafElements("Sine -100:100 (1 min)").ItemId: Simulation.Sine -100:100 (1 min)
        //LeafElements("Sine -100:100 (10 min)").ItemId: Simulation.Sine -100:100 (10 min)
        //LeafElements("Ramp (10 s)").ItemId: Simulation.Ramp(10 s)
        //LeafElements("ReadValue_INT").ItemId: Simulation.ReadValue_INT
        //LeafElements("Staircase 0:2 (1 s)").ItemId: Simulation.Staircase 0:2 (1 s)
        //LeafElements("ReadValue_CY").ItemId: Simulation.ReadValue_CY
        //LeafElements("Register_R8").ItemId: Simulation.Register_R8
        //LeafElements("Register_R4").ItemId: Simulation.Register_R4
        //LeafElements("Register_DECIMAL").ItemId: Simulation.Register_DECIMAL
        //LeafElements("Incrementing (10 min)").ItemId: Simulation.Incrementing(10 min)
        //LeafElements("Register_EMPTY").ItemId: Simulation.Register_EMPTY
        //LeafElements("Constant_INT").ItemId: Simulation.Constant_INT
        //LeafElements("Register_INT").ItemId: Simulation.Register_INT
        //LeafElements("Register_ArrayOfBOOL").ItemId: Simulation.Register_ArrayOfBOOL
        //LeafElements("Ramp 0:100 (10 s)").ItemId: Simulation.Ramp 0:100 (10 s)
        //LeafElements("Ramp 0:360 (10 s)").ItemId: Simulation.Ramp 0:360 (10 s)
        //LeafElements("Square (1 min)").ItemId: Simulation.Square(1 min)
        //LeafElements("Square (10 min)").ItemId: Simulation.Square(10 min)
        //LeafElements("Constant_DECIMAL").ItemId: Simulation.Constant_DECIMAL
        //LeafElements("Register_VARIANT").ItemId: Simulation.Register_VARIANT
        //LeafElements("Constant_DATE").ItemId: Simulation.Constant_DATE
        //LeafElements("Register_ArrayOfINT").ItemId: Simulation.Register_ArrayOfINT
    }
}
#endregion
