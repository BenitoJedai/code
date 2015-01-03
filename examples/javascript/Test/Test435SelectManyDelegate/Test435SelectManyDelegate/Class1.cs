using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Test435SelectManyDelegate;
[assembly: Obfuscation(Feature = "script")]
namespace Test435SelectManyDelegate
{
    public static class xe
    {
        public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
        {
            return null;
        }
    }
    public class XContainer
    {
        internal IEnumerable<XElement> Elements()
        {
            return null;
        }
    }
    public class XElement : XContainer
    { }

    public class Class1
    {
        public static IEnumerable<XElement> xElements<T>(IEnumerable<T> source) where T : XContainer
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150102/linq
            // X:\jsc.svn\examples\javascript\Test\Test435SelectManyDelegate\Test435SelectManyDelegate\Class1.cs
            return source.SelectMany(k => k.Elements());
        }
    }
}
