using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestIntPostfixIncrement
{
    public static class Class1
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201502/20150226
        // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Convert.FromBase64String.cs

        public static void FromBase64String(byte[] input)
        {
            int i = 0;
            var enc1 = 64;

            enc1 = Base64Key_IndexOf(input[i++]);

            // roslyn v14
            //c = (((e + 1)));
            //d = AgAABmVvpzSgZUB5TmcESg(b[e]);

            // roslyn v12:
            //c = (((e + 1)));
            //d = AgAABmVvpzSgZUB5TmcESg(b[e]);

            // v11 broken
            //c = (((c++ + 1)));
            //d = AgAABmVvpzSgZUB5TmcESg(b[c++]);

            // v11 fixed
            // d = AgAABmVvpzSgZUB5TmcESg(b[c++]);

        }

        private static int Base64Key_IndexOf(byte v)
        {
            return 0;
        }
    }
}
