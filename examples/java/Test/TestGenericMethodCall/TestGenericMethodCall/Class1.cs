using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]
namespace TestGenericMethodCall
{
    public class Class2<T>
    {
    }

    public class Class1
    {
        static void Bar(IEnumerable<Class2<IEnumerable<string>>> e)
        {
            var x = e.AsEnumerable();
            var y = x.FirstOrDefault();
            var z = x.FirstOrDefault(null);

            Class1.Foo(0, "");
        }

        public static void Foo<T, X>(T t, X x)
        {

        }
    }

    [Script(Implements = typeof(global::System.Collections.Generic.IEnumerable<>))]
    internal interface __IEnumerable<T>
    {
    }

    [Script(Implements = typeof(global::System.Linq.Enumerable))]
    internal static partial class __Enumerable
    {
        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> other)
        {
            return default(TSource);
        }

        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            return default(TSource);
        }

        public static IEnumerable<TSource> AsEnumerable<TSource>(this IEnumerable<TSource> source)
        {
            return source;
        }

    }
}
