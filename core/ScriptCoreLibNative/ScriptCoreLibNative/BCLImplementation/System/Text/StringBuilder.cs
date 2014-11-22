using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;
using System.IO;
using ScriptCoreLibNative.BCLImplementation.System.IO;

namespace ScriptCoreLibNative.BCLImplementation.System.Text
{
    // http://referencesource.microsoft.com/#mscorlib/system/text/stringbuilder.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Text/StringBuilder.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Text\StringBuilder.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Text\StringBuilder.cs
    // X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\Text\StringBuilder.cs
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Text\StringBuilder.cs

    [Script(Implements = typeof(global::System.Text.StringBuilder))]
    internal class __StringBuilder
    {
        //MemoryStream InternalMemory = new MemoryStream(0xff);
        MemoryStream InternalMemory = new MemoryStream();

        public __StringBuilder Append(object e)
        {
            // we cannot recover type info can we
            // anonymous type is generic and thats why it may not exactly know the type

            // jsc rewriter should bake generics

            // what if we knew which opcode loaded the value an where?
            Append("?");

            return this;
        }

        public __StringBuilder Append(string e)
        {

            for (int i = 0; i < e.Length; i++)
            {
                var c = e[i];

                InternalMemory.WriteByte((byte)c);
            }


            //Console.WriteLine("Append");
            //Console.WriteLine(e);


            return this;
        }

        public override string ToString()
        {
            // memory terminator
            InternalMemory.WriteByte((byte)0);
            // how much capacity did we leak?


            object p = ((__MemoryStream)(object)InternalMemory).InternalBuffer;

            //Console.WriteLine("enter __StringBuilder.ToString InternalMemory");
            //Console.WriteLine((int)InternalMemory.Length);

            return (string)p;
        }
    }

}
