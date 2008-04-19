using ScriptCoreLib;
using ScriptCoreLib.Shared;

using global::System.Collections;
using global::System.Collections.Generic;


using IDisposable = global::System.IDisposable;
using System;
using System.Linq;

namespace ScriptCoreLib.ActionScript.Query
{

    internal static partial class __Enumerable
    {
        public static U Aggregate<T, U>(this IEnumerable<T> source,
                         U seed, global::System.Func<U, T, U> func)
        {
            U result = seed;

            foreach (T element in source.AsEnumerable())
                result = func(result, element);

            return result;
        }

        public static bool Any<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw DefinedErrors.ArgumentNull("source");
            }


            var r = false;

            foreach (var v in source.AsEnumerable())
            {
                r = true;

                break;
            }

            return r;
        }

        public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw DefinedErrors.ArgumentNull("source");
            }

            if (predicate == null)
            {
                throw DefinedErrors.ArgumentNull("predicate");
            }

            var r = false;

            foreach (var v in source.AsEnumerable())
            {
                if (predicate(v))
                {
                    r = true;

                    break;
                }
            }

            return r;
        }

        public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw DefinedErrors.ArgumentNull("source");
            }

            if (predicate == null)
            {
                throw DefinedErrors.ArgumentNull("predicate");
            }

            var r = true;

            foreach (var v in source.AsEnumerable())
            {
                if (!predicate(v))
                {
                    r = false;

                    break;
                }
            }

            return r;
        }


        public static IEnumerable<TSource> AsEnumerable<TSource>(this IEnumerable<TSource> source)
        {
            return Query.InternalSequenceImplementation.AsEnumerable(source);
        }


        public static int Count<T>(this IEnumerable<T> e)
        {
            int c = 0;

            foreach (var v in e.AsEnumerable()) c++;

            return c;
        }

    }





}
