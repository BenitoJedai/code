using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using ScriptCoreLib;

using EntryPointProgram = OrcasNativeJavaConsoleApplication.source.java.Program;
using EntryPointSettings = OrcasNativeJavaConsoleApplication.source.java.Settings;


[assembly:
    Script,
    ScriptTypeFilter(ScriptType.Java, typeof(OrcasNativeJavaConsoleApplication.source.java.Program))
]

[assembly:
    /*
     todo: if a referenced assembly already defines renames then they must be merged
     
    ScriptNamespaceRename(
        NativeNamespaceName = "javax.common.runtime",
        VirtualNamespaceName = EntryPointSettings.AliasNamespace + ".util"
    ),
    ScriptNamespaceRename(
        NativeNamespaceName = "javax.common.wrapper",
        VirtualNamespaceName = EntryPointSettings.AliasNamespace + ".util"
    ),*/
    ScriptNamespaceRename(
        NativeNamespaceName = "OrcasNativeJavaConsoleApplication.source.java",
        VirtualNamespaceName = EntryPointSettings.AliasNamespace
    )
]


// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("OrcasNativeJavaConsoleApplication")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("OrcasNativeJavaConsoleApplication")]
[assembly: AssemblyCopyright("Copyright ?  2005")]
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
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
