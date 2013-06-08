using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Extensions
{
    public static class IEnumerableExtensions
    {
        static public IEnumerable<object> AsEnumerable(this ICollection source)
        {
            var r = source.GetEnumerator();

            return Enumerable.Range(0, source.Count).Select(i =>
                {
                    r.MoveNext();

                    return r.Current;
                }
            );
        }
    }
}
