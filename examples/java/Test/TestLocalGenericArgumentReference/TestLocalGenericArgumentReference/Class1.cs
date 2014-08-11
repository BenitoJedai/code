using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]

namespace TestLocalGenericArgumentReference
{
    public class Class1 : ScriptCoreLibJava.IAssemblyReferenceToken
    {
        public Class1()
        {
            // X:\jsc.svn\examples\java\hybrid\Test\TestJVMCLRNewTupleArray\TestJVMCLRNewTupleArray\Program.cs

            var z = new Tuple<System.Reflection.MemberInfo, int>[] {
                // Tuple.Create(item.m, index)
            };
        }
    }
}
