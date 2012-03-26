﻿using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ScriptCoreLib;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("ScriptCoreLibJava")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyProduct("ScriptCoreLibJava")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("8a018a81-c976-4a13-bc99-834f97decab8")]

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

[assembly: ScriptTypeFilter(ScriptType.Java)]
[assembly: Script(IsCoreLib = true)]

[assembly: InternalsVisibleTo("ScriptCoreLibJava.jni")]
[assembly: InternalsVisibleTo("ScriptCoreLibJava.Threading")]
[assembly: InternalsVisibleTo("ScriptCoreLibJava.Web")]
[assembly: InternalsVisibleTo("ScriptCoreLibJava.Web.Services")]
[assembly: InternalsVisibleTo("ScriptCoreLibJava.Windows.Forms")]