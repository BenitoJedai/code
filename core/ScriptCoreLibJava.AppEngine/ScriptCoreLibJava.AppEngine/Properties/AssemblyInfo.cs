using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ScriptCoreLib;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("ScriptCoreLibJava.AppEngine")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("ScriptCoreLibJava.AppEngine")]
[assembly: AssemblyCopyright("Copyright ©  2009")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("22a6e63e-a0cc-4a40-b361-612a94a10488")]

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



// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140406
// um. dont change version numbers while using jsc :)
[assembly: AssemblyVersion("4.1.0.0")]
[assembly: AssemblyFileVersion("4.1.0.0")]

//Unhandled Exception: System.TypeLoadException: Could not load type 'com.google.apphosting.client.datastoreservice.app.DatastoreRpcHandler' from assembly 'ScriptCoreLibJava.AppEngine, Version=4.1.0.0, Culture=neutral, PublicKeyToken=null'.


[assembly: ScriptTypeFilter(ScriptType.Java)]
[assembly: Script]
[assembly: ScriptNamespaceRename(
    NativeNamespaceName = "ScriptCoreLibJava.AppEngine.API",
    VirtualNamespaceName = "com.google.appengine.api"
)]