using ScriptCoreLib;
using ScriptCoreLib.Shared.Query;

using System.Collections.Generic;


namespace CardGames.source.js
{
    [Script]
    internal static class Extensions
    {
        public static T[] Range<T>(this IList<T> e, int index, int count)
        {
            if (index < 0)
            {
                count -= index;
                index = 0;
            }

            var a = new T[count];

            for (int i = 0; i < count; i++)
            {
                var z = index + i;


                if (z < e.Count)
                    a[i] = e[z];
            }

            return a;
        }

        public static T Pop<T>(this IList<T> e)
        {
            var u = e.Last();

            e.RemoveLast();

            return u;
        }

        public static T Shift<T>(this IList<T> e)
        {
            var u = e.First();

            e.RemoveFirst();

            return u;
        }

        public static void RemoveFirst<T>(this IList<T> e)
        {
            if (e.Count > 0)
                e.RemoveAt(0);
        }

        public static void RemoveLast<T>(this IList<T> e)
        {
            if (e.Count > 0)
                e.RemoveAt(e.Count - 1);
        }

        public static void Add<T>(this IList<T> e, params T[] x)
        {
            foreach (var v in x)
            {
                e.Add(v);
            }
        }
    }
}
