using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using ScriptCoreLib;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("ScriptApplication")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("neutronic")]
[assembly: AssemblyProduct("ScriptApplication")]
[assembly: AssemblyCopyright("Copyright ? neutronic 2007")]
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
[assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyFileVersion("1.0.*")]

[assembly:
	Script,
	ScriptTypeFilter(ScriptType.JavaScript, typeof(global::WebApplication.Client.JavaScript.AvalonDocument)),
	ScriptTypeFilter(ScriptType.JavaScript, typeof(global::WebApplication.Client.Avalon.AvalonCanvas)),
	ScriptTypeFilter(ScriptType.JavaScript, typeof(global::WebApplication.Shared.SharedExtensions)),

	ScriptTypeFilter(ScriptType.ActionScript, typeof(global::WebApplication.Client.ActionScript.AvalonFlash)),
	ScriptTypeFilter(ScriptType.ActionScript, typeof(global::WebApplication.Client.Avalon.AvalonCanvas)),
	ScriptTypeFilter(ScriptType.ActionScript, typeof(global::WebApplication.Shared.SharedExtensions)),


	ScriptTypeFilter(ScriptType.Java, typeof(global::WebApplication.Client.Java.Applet)),
	ScriptTypeFilter(ScriptType.Java, typeof(global::WebApplication.Shared.SharedExtensions)),

	ScriptTypeFilter(ScriptType.PHP, typeof(global::WebApplication.Server.Application)),
	ScriptTypeFilter(ScriptType.PHP, typeof(global::WebApplication.Shared.SharedExtensions)),
]
