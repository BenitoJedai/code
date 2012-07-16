using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq
{

    internal static partial class __Enumerable
    {


        public static IEnumerable<T> SkipWhile<T>(this IEnumerable<T> source, global::System.Func<T, bool> predicate)
        {
            var _Where = false;

            return source.Where(
                k =>
                {
                    var r = _Where;

                    if (!_Where)
                        if (predicate(k))
                            _Where = true;

                    return r;
                }
            );
        }

    }
}
