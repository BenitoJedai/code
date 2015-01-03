using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test435SelectManyDelegate;

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
        public static IEnumerable<XElement> Elements<T>(IEnumerable<T> source) where T : XContainer
        {
            // X:\jsc.svn\examples\javascript\Test\Test435SelectManyDelegate\Test435SelectManyDelegate\Class1.cs
            return source.SelectMany(k => k.Elements());
        }
    }
}
