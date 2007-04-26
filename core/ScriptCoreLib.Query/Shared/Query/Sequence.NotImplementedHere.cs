using ScriptCoreLib;
using ScriptCoreLib.Shared;

using global::System.Collections;
using global::System.Collections.Generic;

using IDisposable = global::System.IDisposable;
using ScriptException = global::ScriptCoreLib.JavaScript.System.ScriptException;

namespace ScriptCoreLib.Shared.Query
{
    internal static class InternalSequence
    {
        public static TSource[] ToArray<TSource>(IEnumerable<TSource> source)
        {
            return default(TSource[]);
        }

        public static IEnumerable<TSource> AsEnumerable<TSource>(IEnumerable<TSource> source)
        {
            return default(IEnumerable<TSource>);

        }
    }

    public static partial class Sequence
    {
        static void NoOptimization() { }

        [Script(NotImplementedHere = true)]
        public static TSource[] ToArray<TSource>(this IEnumerable<TSource> source)
        {
            NoOptimization();

            return InternalSequence.ToArray(source);
        }



        [Script(NotImplementedHere = true)]
        public static IEnumerable<TSource> AsEnumerable<TSource>(this IEnumerable<TSource> source)
        {
            // the runtime should create a SZArray for arrays but it does not for the moment

            NoOptimization();

            return InternalSequence.AsEnumerable(source);
        }


    }
}
