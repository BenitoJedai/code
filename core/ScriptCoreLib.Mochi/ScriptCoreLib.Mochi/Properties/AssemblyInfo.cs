using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ScriptCoreLib;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("ScriptCoreLib.Mochi")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("ScriptCoreLib.Mochi")]
[assembly: AssemblyCopyright("Copyright ©  2008")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("f0393a45-f7bc-49fd-ab2b-ad5f78a82aa7")]

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
    ScriptTypeFilter(ScriptType.ActionScript, "*.Shared.Query"),

    // some namespace mangling
    ScriptNamespaceRename(NativeNamespaceName = "ScriptCoreLib.ActionScript", VirtualNamespaceName = ""),
    ScriptNamespaceRename(NativeNamespaceName = "ScriptCoreLib.Shared", VirtualNamespaceName = ""),

    ScriptNamespaceRename(NativeNamespaceName = "ScriptCoreLib.ActionScript.MochiLibrary.Score", VirtualNamespaceName = "mochi"),
    ScriptNamespaceRename(NativeNamespaceName = "ScriptCoreLib.ActionScript.MochiLibrary.Ad", VirtualNamespaceName = ""),

    ScriptResources("mochi"),
    ScriptResources(""),
]
