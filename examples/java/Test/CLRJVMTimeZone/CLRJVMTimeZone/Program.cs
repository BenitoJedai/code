using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using ScriptCoreLibJava.Extensions;
using System.Xml.Linq;

namespace CLRJVMTimeZone
{

    public class Program
    {
        public static void Main(string[] args)
        {
            //            java.lang.Object
            //{ Ticks = 72000000000, z = 0.02:00:00 }
            //{ utc = 26.12.2013 23:22:25, Kind = 1 }
            //{ loc = 27.12.2013 01:22:25, Kind = 2 }
            //running inside CLR


            // { Ticks = 72000000000, z = 02:00:00, utc = 12/26/2013 11:14:40 PM, loc = 12/27/2013 1:14:40 AM }
            Console.WriteLine(typeof(object));
            var now = DateTime.Now;
            var z = TimeZone.CurrentTimeZone.GetUtcOffset(now);

            //System.Object
            //{ Ticks = 72000000000, z = 02:00:00 }
            //{ utc = 12/26/2013 11:17:02 PM, Kind = Utc }
            //{ loc = 12/27/2013 1:17:02 AM, Kind = Local }
            //running inside CLR


            Console.WriteLine(
                new
                {
                    z.Ticks,
                    z,
                }
            );

            var utc = now.ToUniversalTime();
            Console.WriteLine(new { utc, utc.Kind });
            var loc = utc.ToLocalTime();
            Console.WriteLine(new { loc, loc.Kind });

            var xml = new XElement("xml");
            Console.WriteLine(new { xml });

            //1fd8:02:01 0024:0001 __clr__CLRJVMTimeZone define jsc.meta::ScriptCoreLib.Desktop.JVM.JVMLauncher
            //1fd8:02:01 0025:0002 __clr__CLRJVMTimeZone define jsc.meta::ScriptCoreLib.Desktop.JVM.JVMLauncher+__Invoke
            //1fd8:02:01 0025:0003 __clr__CLRJVMTimeZone define jsc.meta::ScriptCoreLib.Desktop.JVM.JVMLauncher+
            //1fd8:02:01 RewriteToAssembly error: System.InvalidOperationException:   ---> System.ArgumentException: Duplicate type name within an assembly.
            //   at System.Reflection.Emit.TypeBuilder.DefineType(RuntimeModule module, String fullname, Int32 tkParent, TypeAttributes attributes, Int32 tkEnclosingType, Int32[] interfaceTokens)
            //   at System.Reflection.Emit.TypeBuilder.Init(String fullname, TypeAttributes attr, Type parent, Type[] interfaces, ModuleBuilder module, PackingSize iPackingSize, Int32 iTypeSize, TypeBuilder enclosingType)
            //   at System.Reflection.Emit.TypeBuilder.DefineNestedType(String name, TypeAttributes attr, Type parent, Type[] interfaces)

            //- javac
            //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\CLRJVMTimeZone\Program.java
            //Y:\staging\web\java\ScriptCoreLibJava\BCLImplementation\System\__Type.java:279: error: cannot find symbol
            //                    if (!infoArray0[num2].get_DeclaringType().Equals_0600104e(type1))
            //                                                             ^
            //  symbol:   method Equals_0600104e(__Type)
            //  location: class __Type
            //Y:\staging\web\java\ScriptCoreLibJava\BCLImplementation\System\Xml\Linq\__XContainer.java:165: error: cannot find symbol
            //            __Console.WriteLine("__XContainer.Add Type "+ __Object.System_Object_GetType_06000007(content));
            //                                                                  ^
            //  symbol:   method System_Object_GetType_06000007(Object)
            //  location: class __Object
            //Y:\staging\web\java\ScriptCoreLibJava\BCLImplementation\System\Collections\__Comparer.java:154: error: cannot find symbol
            //            throw new RuntimeException(__String.Concat("Implement IComparable for ", __Object.System_Object_GetType_06000007(a).get_FullName(), " vs ", __Object.System_Object_GetType_06000007(b).get_FullName()));
            //                                                                                             ^
            //  symbol:   method System_Object_GetType_06000007(Object)
            //  location: class __Object
            //Y:\staging\web\java\ScriptCoreLibJava\BCLImplementation\System\Collections\__Comparer.java:154: error: cannot find symbol
            //            throw new RuntimeException(__String.Concat("Implement IComparable for ", __Object.System_Object_GetType_06000007(a).get_FullName(), " vs ", __Object.System_Object_GetType_06000007(b).get_FullName()));
            //                                                                                                                                                                ^
            //  symbol:   method System_Object_GetType_06000007(Object)
            //  location: class __Object
            //Y:\staging\web\java\ScriptCoreLibJava\BCLImplementation\System\Collections\__Comparer.java:209: error: cannot find symbol
            //        if (!__Object.System_Object_GetType_06000007(a).Equals_0600104e(t))
            //                     ^
            //  symbol:   method System_Object_GetType_06000007(Object)
            //  location: class __Object
            //Y:\staging\web\java\ScriptCoreLibJava\BCLImplementation\System\Collections\__Comparer.java:215: error: cannot find symbol
            //        if (!__Object.System_Object_GetType_06000007(b).Equals_0600104e(t))
            //                     ^
            //  symbol:   method System_Object_GetType_06000007(Object)
            //  location: class __Object

            CLRProgram.CLRMain();
        }


    }

    [SwitchToCLRContext]
    static class CLRProgram
    {
        [STAThread]
        public static void CLRMain()
        {
            Console.WriteLine("running inside CLR");

            Console.ReadKey();
        }
    }
}
