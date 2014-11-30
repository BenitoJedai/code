using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ScriptCoreLib;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("ScriptCoreLibAndroid")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyProduct("ScriptCoreLibAndroid")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("21a7fb29-075d-4a44-9d1a-09a195a164ef")]

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
// should the rewrite assemblies be versionless?

// what about native?
[assembly: ScriptTypeFilter(ScriptType.Java)]
[assembly: Script(IsCoreLib = true)]

// should not merge yet, as we will be rewriting all thos natives then.
// [assembly: Obfuscation(Feature = "merge")]

[assembly: InternalsVisibleTo("jsc.meta")]
