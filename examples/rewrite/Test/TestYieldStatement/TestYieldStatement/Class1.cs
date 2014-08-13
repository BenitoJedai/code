using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestYieldStatement
{
    public class Class1
    {
        public static IEnumerable<TElement> ReadToElements<TElement>(DbDataReader r, IEnumerable<TElement> source)
        {
            // X:\jsc.svn\examples\rewrite\Test\TestYieldStatement\TestYieldStatement\Class1.cs
            // x:\jsc.svn\examples\java\hybrid\test\testjvmclryieldstatement\testjvmclryieldstatement\program.cs

            Console.WriteLine("enter AsEnumerable ");

            //while (r.Read())
            //{
            // what the flip jsc java?
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140813
            yield return ReadToElement<TElement>(r, source, new Tuple<MemberInfo, int>[0]);
            //}

            Console.WriteLine("exit AsEnumerable ");
        }

        public static TElement ReadToElement<TElement>(DbDataReader r, IEnumerable source, Tuple<MemberInfo, int>[] Target)
        {
            return default(TElement);
        }
    }
}
