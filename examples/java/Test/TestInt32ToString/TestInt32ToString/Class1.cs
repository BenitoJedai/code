using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]
namespace TestInt32ToString
{
    public static class Class1
    {
        static string Invoke(int i)
        {
            return i.ToString();
        }
    }
}

namespace java.lang
{
    [Script(IsNative = true)]
    public sealed class Integer
    {
    }

    // http://developer.android.com/reference/java/lang/Object.html
    // http://docs.oracle.com/javase/1.5.0/docs/api/java/lang/Object.html
    [Script(IsNative = true)]
    public class Object
    { }
}



namespace ScriptCoreLibJava.BCLImplementation.System
{

    [Script(

        Implements = typeof(global::System.Object),
        ImplementationType = typeof(global::java.lang.Object)

        //Implements = typeof(global::System.Object),
        //ImplementationType = typeof(object)

        )]
    internal class __Object
    {
    }
}
namespace ScriptCoreLibJava.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Int32)
        // native type cast conflict: ,ExternalTarget="java.lang.Integer"
        , ImplementationType = typeof(java.lang.Integer)
        )]
    internal class __Int32
    {
    }
}