// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
using System.Reflection;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("EasyOPC-DA.NET Demo (enabled for OPC-XML)")]
[assembly: AssemblyDescription("This client demo is written in .NET, and it shows how to use the component to work with OPC XML-DA servers. It allows reading and writing items, subscriptions to items, and getting item properties. It also shows the user interface for browsing OPC items and OPC properties.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("CODE Consulting and Development, s.r.o.")]
[assembly: AssemblyProduct("QuickOPC.NET")]
[assembly: AssemblyCopyright("© 2006-2025 CODE Consulting and Development, s.r.o., Plzen. All rights reserved.")]
[assembly: AssemblyTrademark("OPC Labs")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("05470B3E-F6DE-4959-A750-EA1AD85C127F")]

[assembly: System.Resources.NeutralResourcesLanguageAttribute("en-US")]

#if (!NETFRAMEWORK)
[assembly: System.Runtime.Versioning.SupportedOSPlatform("windows")]
#endif
