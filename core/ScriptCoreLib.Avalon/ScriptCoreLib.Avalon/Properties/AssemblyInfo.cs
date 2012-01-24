﻿using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ScriptCoreLib;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("ScriptCoreLib.Avalon")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Abstractatech OÜ")]
[assembly: AssemblyProduct("ScriptCoreLib.Avalon")]
[assembly: AssemblyCopyright("Copyright © Abstractatech OÜ 2012")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("351fb015-8a71-4e9b-ba42-fd819f94abfa")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]


[assembly:
	Script,
	ScriptTypeFilter(ScriptType.ActionScript, "*.ActionScript"),
	ScriptTypeFilter(ScriptType.ActionScript, "*.Shared"),
	ScriptTypeFilter(ScriptType.JavaScript, "*.JavaScript"),
	ScriptTypeFilter(ScriptType.JavaScript, "*.Shared"),
	// ScriptTypeFilter(ScriptType.CSharp2, "*.CSharp"),
	//ScriptNamespaceRename(NativeNamespaceName = "ScriptCoreLib.ActionScript", VirtualNamespaceName = ""),
	//ScriptNamespaceRename(NativeNamespaceName = "ScriptCoreLib.Shared", VirtualNamespaceName = "")
    // some namespace mangling, why bother?

    InternalsVisibleTo("ScriptCoreLib.Redux.Avalon"),
    InternalsVisibleTo("ScriptCoreLib.Avalon.Integration"),
	InternalsVisibleTo("jsc.meta"),
]
