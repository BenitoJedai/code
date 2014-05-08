using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq
{

     static partial class __Enumerable
    {
        public static T ElementAt<T>(this IEnumerable<T> e, int index)
        {
            int i = -1;

            T r = default(T);

            foreach (var v in e.AsEnumerable())
            {
                i++;

                if (i == index)
                {
                    r = v;
                    break;
                }
            }

            return r;
        }


        public static TSource ElementAtOrDefault<TSource>(this IEnumerable<TSource> source, int index)
        {
            var r = default(TSource);
            var i = -1;
            foreach (var item in source.AsEnumerable())
            {
                i++;
                if (i == index)
                {
                    r = item;
                    break;
                }
            }
            return r;
        }
    }
}
