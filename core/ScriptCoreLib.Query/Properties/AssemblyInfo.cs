using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using ScriptCoreLib;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("ScriptCoreLib.Query")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("neutronic")]
[assembly: AssemblyProduct("ScriptCoreLib.Query")]
[assembly: AssemblyCopyright("Copyright ? neutronic 2008")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM componenets.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("0cae4ddb-abd5-4c2a-96c0-918ed1d736e4")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion("3.5.*")]
[assembly: AssemblyFileVersion("3.5.*")]

[assembly:
    Script,

    ScriptNamespaceRename(NativeNamespaceName = "ScriptCoreLib.ActionScript", VirtualNamespaceName = ""),
    ScriptNamespaceRename(NativeNamespaceName = "ScriptCoreLib.Shared", VirtualNamespaceName = ""),

    ScriptTypeFilter(ScriptType.ActionScript, "*.ActionScript"),
    ScriptTypeFilter(ScriptType.ActionScript, "*.Shared.Lambda"),
    ScriptTypeFilter(ScriptType.JavaScript, "*.JavaScript"),
    ScriptTypeFilter(ScriptType.JavaScript, "*.Shared"),
    ScriptTypeFilter(ScriptType.CSharp2, "*.CSharp2"),
    ScriptTypeFilter(ScriptType.PHP, "*.PHP")
]

