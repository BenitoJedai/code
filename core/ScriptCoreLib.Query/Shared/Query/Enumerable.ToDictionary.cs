using ScriptCoreLib;
using ScriptCoreLib.Shared;

using global::System.Collections;
using global::System.Collections.Generic;

using IDisposable = global::System.IDisposable;

namespace ScriptCoreLib.Shared.Query
{

    internal static partial class __Enumerable
    {
        public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(this IEnumerable<TSource> source, global::System.Linq.Func<TSource, TKey> keySelector)
        {
            return source.ToDictionary<TSource, TKey, TSource>(keySelector, IdentityFunction<TSource>.Instance, null);
        }

        public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(this IEnumerable<TSource> source, global::System.Linq.Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            return source.ToDictionary<TSource, TKey, TSource>(keySelector, IdentityFunction<TSource>.Instance, comparer);
        }

        public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, global::System.Linq.Func<TSource, TKey> keySelector, global::System.Linq.Func<TSource, TElement> elementSelector)
        {
            return source.ToDictionary<TSource, TKey, TElement>(keySelector, elementSelector, null);
        }

        public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, global::System.Linq.Func<TSource, TKey> keySelector, global::System.Linq.Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            if (keySelector == null)
            {
                throw Error.ArgumentNull("keySelector");
            }
            if (elementSelector == null)
            {
                throw Error.ArgumentNull("elementSelector");
            }
            Dictionary<TKey, TElement> dictionary = new Dictionary<TKey, TElement>(comparer);
            foreach (TSource local in source.AsEnumerable())
            {
                dictionary.Add(keySelector(local), elementSelector(local));
            }
            return dictionary;
        }

    }
}
