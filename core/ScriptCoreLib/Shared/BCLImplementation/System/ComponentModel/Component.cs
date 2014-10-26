using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel
{
    // http://referencesource.microsoft.com/#System/compmod/system/componentmodel/Component.cs
    [Script(Implements = typeof(global::System.ComponentModel.Component))]
    public class __Component :
        __MarshalByRefObject,
         __IComponent
    {
        public event EventHandler Disposed;


        // tested by
        // x:\jsc.svn\examples\java\Test\TestBaseCall\TestBaseCall\Class1.cs
        // X:\jsc.svn\examples\java\forms\AppletAsyncWhenReady\AppletAsyncWhenReady\ApplicationApplet.cs
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Data\SQLite\SQLiteConnection.cs
        // X:\jsc.svn\examples\java\android\forms\FormsShowDialog\FormsShowDialog\Library\Form1.Designer.cs
        public virtual void Dispose(bool disposing)
        {
        }

        public bool DesignMode { get; set; }

        public void Dispose()
        {
            Dispose(true);
        }

        #region __IComponent Members

        public global::System.ComponentModel.ISite Site
        {
            get;
            set;
        }

        #endregion


        public override string ToString()
        {
            //ToString() : string
            //Analysis
            //Attributes
            //Base Definition
            //Signature Types
            //Declaring Module
            //Declaring Type
            //loc.0 <- 0x0001 ldfld        [System] System.ComponentModel.Component.site : ISite
            //maxstack 4
            //IL Code (18)
            //0x0000 . ldarg.0            this
            //0x0001 . ldfld              [System] System.ComponentModel.Component.site : ISite
            //0x0006 stloc.0              loc.0 : ISite
            //0x0007 . ldloc.0            loc.0 : ISite
            //0x0008 brfalse.s 
            //0x0008 -> 0x000a 0x002b 
            //0x0008 -> 0x000a ldloc.0 
            //0x000a . ldloc.0            loc.0 : ISite
            //0x000b . callvirt           str0 <- [System] System.ComponentModel.ISite.get_Name() : string
            //0x0010 . . ldstr            str1 <- " ["
            //0x0015 . . . ldarg.0        this
            //0x0016 . . . call           [mscorlib] System.Object.GetType() : Type
            //0x001b . . . callvirt       str2 <- [mscorlib] System.Type.get_FullName() : string
            //0x0020 . . . . ldstr        str3 <- "]"
            //0x0025 . call               [mscorlib] System.String.Concat(str0 : string, str1 : string, str2 : string, str3 : string) : String
            //0x002a ret 
            //0x0008 -> 0x002b ldarg.0 
            //0x002b . ldarg.0            this
            //0x002c . call               [mscorlib] System.Object.GetType() : Type
            //0x0031 . callvirt           [mscorlib] System.Type.get_FullName() : string
            //0x0036 ret 


            // do all languages support GetType yet?
            // do all languages support is interface yet?


            //[javac] V:\src\ScriptCoreLib\Shared\BCLImplementation\System\ComponentModel\__Component.java:67: error: cannot find symbol
            //[javac]         return  __Object.ScriptCoreLibJava_BCLImplementation_System___Object_GetType_0600263a(this).get_FullName();
            //[javac]                         ^
            //[javac]   symbol:   method ScriptCoreLibJava_BCLImplementation_System___Object_GetType_0600263a(__Component)
            //[javac]   location: class __Object

            // why would java not find object?
            var t = this.GetType();

            return t.FullName;
        }
    }
}
