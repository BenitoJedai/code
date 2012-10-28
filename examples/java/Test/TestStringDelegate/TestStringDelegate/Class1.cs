using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]
namespace TestStringDelegate
{
    using t = G<Action<G<int>>>;

    public class String
    {
    }

    public class __String
    {
    }

    class G<T>
    { }

    public class Class
    {
    }

    public class Integer
    {
    }

    class Class1 : ScriptCoreLibJava.IAssemblyReferenceToken
    {
        //    System.Action`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] :: Void .ctor(System.Object, IntPtr)

        String x;
        __String y;
        Class z;
        Integer zi;


        public static void foo(string e, t g)
        {
            Action<string, t> y = foo;
        }
    }
}
