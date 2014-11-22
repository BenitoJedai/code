using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;

namespace ScriptCoreLibNative.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/string.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System/String.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\String.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\String.cs
    // X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\String.cs
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\String.cs

    [Script(Implements = typeof(global::System.String))]
    internal class __String
    {
        // System.String Concat(System.String, System.String) used at
        public static string Concat(string e, string o)
        {
            // X:\jsc.svn\examples\c\Test\TestFunc\TestFunc\Program.cs
            var w = new StringBuilder();

            w.Append(e);
            w.Append(o);

            return w.ToString();
        }

        [Script(OptimizedCode = @"return e[o];")]
        internal static char StringChar(string e, int o)
        {
            return default(char);
        }

        public char get_Chars(int i)
        {
            return StringChar((string)(object)this, i);
        }

        public int Length
        {
            get
            {
                return string_h.strlen((string)(object)this);
            }
        }

        public char[] ToCharArray()
        {
            var that = (string)(object)this;

            var length = that.Length;
            var c = new char[length];

            for (int i = 0; i < length; i++)
            {
                c[i] = that[i];
            }

            return c;
        }
    }

}
