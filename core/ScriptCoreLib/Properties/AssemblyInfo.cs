using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using ScriptCoreLib;
using System;
using ScriptCoreLib.Shared;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("ScriptCoreLib")]
[assembly: AssemblyProduct("ScriptCoreLib")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyConfiguration("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM componenets.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("ad497913-358e-4054-b2e3-98d7bd1e0031")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:

//20150103
[assembly: AssemblyVersion("4.6.0.0")]
[assembly: AssemblyFileVersion("4.6.0.0")]

[assembly:
    Script(IsCoreLib = true)
    //ScriptResources("assets/ScriptCoreLib")
]

[assembly:
    ScriptTypeFilter(ScriptType.GLSL, typeof(global::ScriptCoreLib.GLSL.vec2)),

    ScriptTypeFilter(ScriptType.ActionScript, typeof(global::ScriptCoreLib.ActionScript.Function)),
    ScriptTypeFilter(ScriptType.JavaScript, typeof(global::ScriptCoreLib.JavaScript.Native)),

    ScriptTypeFilter(ScriptType.PHP, "*.PHP"),

    ScriptTypeFilter(ScriptType.ActionScript, "*.Shared"),
    ScriptTypeFilter(ScriptType.JavaScript, "*.Shared"),
    ScriptTypeFilter(ScriptType.PHP, "*.Shared"),

    ScriptTypeFilter(ScriptType.Java, "*.Java.BCLImplementation"),
    ScriptTypeFilter(ScriptType.Java, "*.Shared.BCLImplementation"),

    ScriptTypeFilter(ScriptType.ActionScript, "*.Extensions"),
    ScriptTypeFilter(ScriptType.JavaScript, "*.Extensions"),
    ScriptTypeFilter(ScriptType.PHP, "*.Extensions"),
    ScriptTypeFilter(ScriptType.Java, "*.Extensions"),


    // some namespace mangling
    ScriptNamespaceRename(NativeNamespaceName = "ScriptCoreLib.ActionScript", VirtualNamespaceName = "", FilterToIsNative = true),
    //ScriptNamespaceRename(NativeNamespaceName = "ScriptCoreLib.Shared", VirtualNamespaceName = ""),
]



//[assembly: InternalsVisibleTo("ScriptCoreLibJava")]
//[assembly: InternalsVisibleTo("ScriptCoreLibJava.Web")]
//[assembly: InternalsVisibleTo("ScriptCoreLibJava.Windows.Forms")]

//to be reenabled part of build process
//[assembly: InternalsVisibleTo("ScriptCoreLibAndroid")]

[assembly: InternalsVisibleTo("ScriptCoreLib.Query")]
[assembly: InternalsVisibleTo("ScriptCoreLib.Windows.Forms")]
[assembly: InternalsVisibleTo("ScriptCoreLib.Drawing")]
[assembly: InternalsVisibleTo("ScriptCoreLib.Avalon")]
[assembly: InternalsVisibleTo("ScriptCoreLib.Web.Services")]
//[assembly: InternalsVisibleTo("ScriptCoreLib.Ultra")]
//[assembly: InternalsVisibleTo("ScriptCoreLib.Ultra.BCLImplementation")]

// should remove this?
[assembly: InternalsVisibleTo("jsc.meta")]

[assembly: InternalsVisibleTo("TestSelectMany")]
//[assembly: InternalsVisibleTo("ScriptCoreLib.Archive")]


namespace ScriptCoreLib.Shared
{
    [Script, Obsolete]
    public class AssemblyInfo : IAssemblyInfo
    {
        public static AssemblyInfo Current = new AssemblyInfo();

        #region BuildDateTimeString
        /// <summary>
        /// date when library was compiled
        /// </summary>
        public string BuildDateTimeString
        {
            [Script(
                UseCompilerConstants = true,
                OptimizedCode = @"return '{BuildDate} UTC';"
                )]
            get
            {
                return default(string);
            }
        }
        #endregion

        #region ModuleName
        public string ModuleName
        {
            [Script(
                UseCompilerConstants = true,
                OptimizedCode = @"return '{Module.Name}';"
                )]
            get
            {
                return default(string);
            }
        }
        #endregion

    }
}
