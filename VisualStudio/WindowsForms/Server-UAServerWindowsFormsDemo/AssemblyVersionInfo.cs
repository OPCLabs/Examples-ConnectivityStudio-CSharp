// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved. 									
#if !NO_CUSTOM_REFERENCES 																								
using OpcLabs.BaseLib.Reflection; 																						
#endif 																												
using System.Reflection; 																								
 																														
#pragma warning disable 0436 																							
#if !NO_CUSTOM_REFERENCES 																								
[assembly: AssemblyBranch(AssemblyProperties.AssemblyBranch)] 															
#endif 																												
[assembly: AssemblyVersion(AssemblyProperties.AssemblyVersion)] 														
[assembly: AssemblyFileVersion(AssemblyProperties.AssemblyFileVersion)] 												
#pragma warning restore 0436 																							
 																														
// ReSharper disable CheckNamespace 																					
// ReSharper disable PartialTypeWithSinglePart 																		
static partial class AssemblyProperties 																				
// ReSharper restore PartialTypeWithSinglePart 																		
// ReSharper restore CheckNamespace 																					
{ 																														
    public const string AssemblyBranch = "2025.2"; 																	
    public const string AssemblyVersion = "5.83.473.1"; 														
    public const string AssemblyFileVersion = "5.83.473.1"; 													
} 																														
