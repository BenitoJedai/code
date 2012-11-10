using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Char)
        , ImplementationType = typeof(java.lang.Character)
        )]
    internal class __Char
    {
        public static bool IsWhiteSpace(char c)
        {
            return java.lang.Character.isWhitespace(c);
        }

        public static bool IsWhiteSpace(string e, int i)
        {
            var c = e[i];

            return char.IsWhiteSpace(c);
        }


        public static bool IsNumber(char c)
        {
            if (c == '0') return true;
            if (c == '1') return true;
            if (c == '2') return true;
            if (c == '3') return true;
            if (c == '4') return true;
            if (c == '5') return true;
            if (c == '6') return true;
            if (c == '7') return true;
            if (c == '8') return true;
            if (c == '9') return true;

            return false;
        }

        public static bool IsLetter(char c)
        {
            var gte_a = c >= 'a';
            var lte_z = c <= 'z';

            var gte_A = c >= 'A';
            var lte_Z = c <= 'Z';

            // cmon, its 2012 jsc. flying cars. why wont it work yet:)
            var flag0 = (gte_a && lte_z);
            var flag1 = (gte_A && lte_Z);

            return (flag0 || flag1);
        }
    }
}
