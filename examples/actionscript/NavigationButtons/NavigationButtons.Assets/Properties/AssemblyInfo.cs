using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ScriptCoreLib;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("NavigationButtons.Assets")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("NavigationButtons.Assets")]
[assembly: AssemblyCopyright("Copyright ©  2008")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("4048ec08-20be-4119-a9fb-c7453387149c")]

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
	ScriptTypeFilter(ScriptType.ActionScript, "NavigationButtons.Assets.Shared"),
	ScriptTypeFilter(ScriptType.ActionScript, "NavigationButtons.Assets.ActionScript"),
	ScriptTypeFilter(ScriptType.JavaScript, "NavigationButtons.Assets.Shared"),
	ScriptTypeFilter(ScriptType.JavaScript, "NavigationButtons.Assets.JavaScript"),
]