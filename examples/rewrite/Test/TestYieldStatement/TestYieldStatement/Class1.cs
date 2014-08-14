using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestYieldStatement
{
    public class Class1 : ScriptCoreLibJava.IAssemblyReferenceToken
    {
        // RewriteToAssembly /Output:xTestYieldStatement.dll /AssemblyMerge:TestYieldStatement.dll /xEnableSwitchRewrite:true


        // _arg0.__this.__2__current = Class1.<TElement>ReadToElement(_arg0.__this.r, _arg0.__this.source, (__Tuple_2<__MemberInfo, Integer>[])__Array.CreateInstance(__Type.GetTypeFromHandle(__RuntimeTypeHandle.op_Explicit(__Tuple_2.class)), 0));

        public static IEnumerable<TElement> ReadToElements<TElement>(DbDataReader r, IEnumerable<TElement> source)
        {
            // X:\jsc.svn\examples\rewrite\Test\TestYieldStatement\TestYieldStatement\Class1.cs
            // x:\jsc.svn\examples\java\hybrid\test\testjvmclryieldstatement\testjvmclryieldstatement\program.cs

            //Console.WriteLine("enter AsEnumerable ");

            //while (r.Read())
            //{
            // what the flip jsc java?
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140813
            yield return ReadToElement<TElement>(new Tuple<MemberInfo, int>[0]);
            //}

            //Console.WriteLine("exit AsEnumerable ");
        }

        public static TElement ReadToElement<TElement>(Tuple<MemberInfo, int>[] Target)
        {
            return default(TElement);
        }
    }
}
