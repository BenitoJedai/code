using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]

namespace Test435AnonymousToString
{
    public  class Class1 : ScriptCoreLib.Shared.IAssemblyReferenceToken
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150103/test435anonymoustostring

        public static string Invoke(string foo)
        {
            //new Array(1)[0] = a[0].foo;
            //return dR8ABtNdQz66ZYUODttTfw('{{ foo = {0} }}', new Array(1));

            return new { foo }.ToString();
        }
    }
}
