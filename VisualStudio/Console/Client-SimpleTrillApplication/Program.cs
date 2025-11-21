// $Header: $
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

#region Example
// Shows how to query real-time OPC UA data using Trill. The query looks for event sequences where the previous value is
// less than 42, and the current value is greater than or equal to 42.
//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Reactive.Linq;
using Microsoft.StreamProcessing;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.Generic;
using OpcLabs.EasyOpc.UA.Reactive;

namespace SimpleTrillApplication
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            // Define which server we will work with.
            UAEndpointDescriptor endpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer";
                // or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
                // or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            Console.WriteLine("Creating source observable...");
            // Create a data change observable for specified OPC UA node value.
            IObservable<UAAttributeData<byte>> sourceObservable = UADataChangeNotificationObservable
                .Create<byte>(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10846", 200)
                .Where(eventArgs => eventArgs.Succeeded)    // ignore events that carry failures
                .Select(eventArgs => eventArgs.TypedAttributeData);

            Console.WriteLine("Creating input streamable...");
            // Load the observable as a stream in Trill, injecting a punctuation every second. Because we use
            // FlushPolicy.FlushOnPunctuation, this will also flush the data every second.
            IObservableIngressStreamable<UAAttributeData<byte>> inputStreamable =
                sourceObservable.Select(attributeData => 
                        StreamEvent.CreateStart(attributeData.SourceTimestamp.Ticks, attributeData))
                    .ToStreamable(
                        DisorderPolicy.Drop(),
                        FlushPolicy.FlushOnPunctuation,
                        PeriodicPunctuationPolicy.Time((ulong)TimeSpan.FromSeconds(1).Ticks));

            // We will be building a query that takes a stream of UAAttributeData<byte> events.
            Console.WriteLine("Creating the query...");
            // Query: look for pattern of [value < 42] --> [value >= 42]
            IStreamable<Empty, Tuple<byte, byte>> query = inputStreamable
                .AlterEventDuration(TimeSpan.FromSeconds(10).Ticks)
                .Detect(
                    default(Tuple<byte, byte>), // register to store the value
                    pattern => pattern
                        .SingleElement(e => e.TypedValue < 42, (ts, ev, reg) => new Tuple<byte, byte>(ev.TypedValue, 0))
                        .SingleElement(e => e.TypedValue >= 42, (ts, ev, reg) => new Tuple<byte, byte>(reg.Item1, ev.TypedValue)));

            Console.WriteLine("Running the query for 60 seconds...");
            query.ToStreamEventObservable()
                .Take(TimeSpan.FromSeconds(60))
                .ForEachAsync(streamEvent => Console.WriteLine(streamEvent)).Wait();

            Console.WriteLine("Finished.");
        }



        // Example output:
        //Creating source observable...
        //Creating input streamable...
        //Creating the query...
        //Running the query for 20 seconds...
        //[Punctuation: 637080168490000000]
        //[Interval: 637080168493266740 - 637080168591235679, (17, 76)]
        //[Punctuation: 637080168500000000]
        //[Interval: 637080168509585008 - 637080168607485434, (28, 197)]
        //[Punctuation: 637080168510000000]
        //[Punctuation: 637080168520000000]
        //[Punctuation: 637080168530000000]
        //[Punctuation: 637080168540000000]
        //[Interval: 637080168540136670 - 637080168638105248, (23, 108)]
        //[Punctuation: 637080168550000000]
        //[Interval: 637080168556412772 - 637080168654381015, (20, 157)]
        //[Punctuation: 637080168560000000]
        //[Interval: 637080168560631254 - 637080168658599730, (36, 53)]
        //[Punctuation: 637080168570000000]
        //[Interval: 637080168572974888 - 637080168670943594, (34, 113)]
        //[Punctuation: 637080168580000000]
        //[Interval: 637080168589224991 - 637080168687193550, (25, 176)]
        //[Punctuation: 637080168590000000]
        //[Punctuation: 637080168600000000]
        //[Interval: 637080168605474719 - 637080168703443726, (1, 161)]
        //[Punctuation: 637080168610000000]
        //[Interval: 637080168611568222 - 637080168709537000, (32, 222)]
        //[Punctuation: 637080168620000000]
        //[Punctuation: 637080168630000000]
        //[Punctuation: 637080168640000000]
        //[Interval: 637080168644224600 - 637080168742193693, (24, 87)]
        //[Punctuation: 637080168650000000]
        //[Punctuation: 637080168660000000]
        //[Interval: 637080168660475491 - 637080168758444908, (40, 230)]
        //[Punctuation: 637080168670000000]
        //Finished.
    }
}
#endregion
