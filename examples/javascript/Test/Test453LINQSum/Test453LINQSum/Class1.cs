using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]

namespace Test453LINQSum
{
    public class Class1 : ScriptCoreLib.Shared.IAssemblyReferenceToken
    {
        // X:\jsc.svn\examples\javascript\Test\Test435SelectManyDelegate\Test435SelectManyDelegate\Class1.cs

        class foo { public int goo; }

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150102/linq

        static int Invoke(IEnumerable<foo> f) => (from x in f select x.goo).Sum();


    }
}
