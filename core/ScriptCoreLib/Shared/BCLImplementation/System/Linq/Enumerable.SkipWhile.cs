using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq
{

     static partial class __Enumerable
    {


        public static IEnumerable<T> SkipWhile<T>(this IEnumerable<T> source, global::System.Func<T, bool> predicate)
        {
            //tested by X:\jsc.svn\examples\javascript\RuntimeHTMLDesignMode\RuntimeHTMLDesignMode\Application.cs

            var SkipModeActive = true;

            return source.Where(
                k =>
                {
                    if (SkipModeActive)
                    {
                        SkipModeActive = predicate(k);

                        if (SkipModeActive)
                        {
                            return false;
                        }
                    }

                    return true;
                }
            );
        }

    }
}
