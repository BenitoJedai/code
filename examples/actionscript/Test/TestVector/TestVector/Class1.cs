using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.CodeDom.Compiler;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;

[assembly: Obfuscation(Feature = "script")]


// Why was this class created?
// http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/Vector.html
// http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/package.html#Vector()
// http://riaoo.com/?p=1852

[GenericTypeDefinition]
[Script(IsNative = true)]
public class Vector<T>
{
    public static implicit operator Vector<T>(T[] e)
    {
        return default(Vector<T>);
    }

    public T this[int i]
    {
        get
        {
            return default(T);
        }
        set
        {

        }
    }
}

namespace TestVector
{


    public class Class1
    {
        public static void Foo()
        {
            var i = new Vector<int>();
            var o = new Vector<Class1>();

            i = new int[] { 4, 3, 2, 1 };

            i[3] = 0;

            // C#: vector_10 = new Vector<int>();
            // as: vector_10 = new Vector_1();

            // W:\jsc.svn\compiler\jsc\Languages\CSharp2\CSharp2Compiler.WriteDecoratedTypeName.cs
            // WriteGenericTypeName
        }
    }
}
