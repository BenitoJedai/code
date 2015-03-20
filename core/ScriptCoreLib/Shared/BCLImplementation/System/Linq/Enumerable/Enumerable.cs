using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq
{
	// http://referencesource.microsoft.com/#System.Core/System/Linq/Enumerable.cs
	// https://github.com/dotnet/corefx/blob/master/src/System.Linq/src/System/Linq/Enumerable.cs

	[Script(Implements = typeof(global::System.Linq.Enumerable))]
    public static partial class __Enumerable
    {
        //script: error JSC1000: No implementation found for this native method, please implement [static System.Linq.Enumerable.Cast(System.Collections.IEnumerable)]

        //public static IEnumerable<TResult> Cast<TResult>(this IEnumerable source)
        //{
        //    return 
        //}

      
        // script: error JSC1000: No implementation found for this native method, please implement [static System.Linq.Enumerable.SequenceEqual(System.Collections.Generic.IEnumerable`1[[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], System.Collections.Generic.IEnumerable`1[[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]])]


        public static bool SequenceEqual<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            var a = first.ToArray();
            var b = second.ToArray();

            if (a.Length != b.Length)
                return false;

            var comparer = Comparer<TSource>.Default;

            return Enumerable.Range(0, a.Length).All(i => comparer.Compare(a[i], b[i]) == 0);

        }



        public static IEnumerable<TSource> TakeWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            var c = true;

            // naive mplementation
            return source.Where(
                x =>
                {
                    if (c)
                    {
                        c = predicate(x);
                    }

                    return c;
                }
            );
        }
    }

}
