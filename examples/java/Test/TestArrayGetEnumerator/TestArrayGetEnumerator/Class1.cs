using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]
namespace ScriptCoreLibJava.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Array), IsArray = true)]
    internal class __Array
    {
        [Script]
        class __Enumerator
        {
            public object[] Target;

        }

        [Script(DefineAsStatic = true)]
        public object GetEnumerator()
        {
            // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Array.cs


            //            [javac] Compiling 479 source files to T:\bin\classes
            //[javac] T:\src\ScriptCoreLibJava\BCLImplementation\System\__Array.java:31: incompatible types
            //[javac] found   : java.lang.Object
            //[javac] required: java.lang.Object[]
            //[javac]         enumerator0.Target = that;
            //[javac]                              ^

            //public static  __IEnumerator GetEnumerator(Object that)
            //{
            //    __Array___Enumerator enumerator0;

            //    enumerator0 = new __Array___Enumerator();
            //    enumerator0.Target = that;
            //    return  enumerator0;
            //}


            //GetEnumerator() : IEnumerator
            //Analysis
            //Attributes
            //Signature Types
            //Declaring Module
            //Declaring Type
            //loc.0 <- 0x0001 newobj       [ScriptCoreLibAndroid] ScriptCoreLibJava.BCLImplementation.System.__Array+__Enumerator..ctor()
            //loc.1 <- 0x0013 ldloc.0      loc.0 : __Enumerator
            //maxstack 3 (used 2)
            //IL Code (12)
            //0x0000 nop 
            //0x0001 . newobj         [ScriptCoreLibAndroid] ScriptCoreLibJava.BCLImplementation.System.__Array+__Enumerator..ctor()
            //0x0006 stloc.0          loc.0 : __Enumerator
            //0x0007 . ldloc.0        loc.0 : __Enumerator
            //0x0008 . . ldarg.0      this [ScriptCoreLibAndroid] ScriptCoreLibJava.BCLImplementation.System.__Array
            //0x0009 . . castclass    [mscorlib] System.Object[]
            //0x000e stfld            [ScriptCoreLibAndroid] ScriptCoreLibJava.BCLImplementation.System.__Array+__Enumerator.Target : object[]
            //0x0013 . ldloc.0        loc.0 : __Enumerator
            //0x0014 stloc.1          loc.1 : IEnumerator
            //0x0015 br.s 
            //0x0017 . ldloc.1        loc.1 : IEnumerator
            //0x0018 ret 




            return new __Enumerator { Target = (object[])(object)this };
        }
    }
}