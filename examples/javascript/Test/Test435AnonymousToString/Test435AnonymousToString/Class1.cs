using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]

// 453!
namespace Test435AnonymousToString
{
    public class Class1
        : ScriptCoreLib.Shared.IAssemblyReferenceToken,
        ScriptCoreLibJava.IAssemblyReferenceToken

    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150103/test435anonymoustostring

        public static string Invoke(string foo, string bar)
        {
            //new Array(1)[0] = a[0].foo;
            //return dR8ABtNdQz66ZYUODttTfw('{{ foo = {0} }}', new Array(1));

            //public String toString()
            //    {
            //        new Object[2][0] = this._foo_i__Field;
            //        new Object[2][1] = this._bar_i__Field;
            //        return __String.Format("{{ foo = {0}, bar = {1} }}", new Object[2]);
            //    }

            return new { foo, bar }.ToString();
        }
    }
}
