using ScriptCoreLib;
using ScriptCoreLib.Shared;

using global::System.Collections;
using global::System.Collections.Generic;


using IDisposable = global::System.IDisposable;
using System;
using System.Linq;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Linq
{

    using Error = __DefinedError;


    internal static partial class __Enumerable
    {
        //public static IEnumerable<TSource> Reverse<TSource>(this IEnumerable<TSource> source)
        //{
        //    var s = new Stack<TSource>();

        //    foreach (var v in source)
        //    {
        //        s.Push(v);
        //    }

        //    return s;
        //}

        public static TSource Single<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return source.Where(predicate).Single();
        }

        public static TSource Single<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }

            TSource current;

            using (IEnumerator<TSource> enumerator = source.AsEnumerable().GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    throw Error.NoElements();
                }
                current = enumerator.Current;

                if (enumerator.MoveNext())
                {
                    throw Error.MoreThanOneElement();


                }
            }

            return current;
        }


        public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return source.Where(predicate).SingleOrDefault();
        }

        public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }

            var current = default(TSource);

            using (IEnumerator<TSource> enumerator = source.AsEnumerable().GetEnumerator())
            {
                if (enumerator.MoveNext())
                    current = enumerator.Current;


            }

            return current;
        }


        public static bool Contains<TSource>(this IEnumerable<TSource> source, TSource value)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }


            var r = false;

            foreach (var v in source.AsEnumerable())
            {
                if (object.ReferenceEquals(v, value))
                {
                    r = true;

                    break;
                }
            }

            return r;
        }


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
                throw __DefinedError.ArgumentNull("source");
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
                throw __DefinedError.ArgumentNull("source");
            }

            if (predicate == null)
            {
                throw __DefinedError.ArgumentNull("predicate");
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
                throw __DefinedError.ArgumentNull("source");
            }

            if (predicate == null)
            {
                throw __DefinedError.ArgumentNull("predicate");
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
            // wrap native types/collections

            return source;
        }


        public static int Count<T>(this IEnumerable<T> e)
        {
            int c = 0;

            foreach (var v in e.AsEnumerable()) c++;

            return c;
        }

        public static int Count<T>(this IEnumerable<T> e, Func<T, bool> w)
        {
            return e.Where(w).Count();
        }

        public static TSource[] ToArray<TSource>(this IEnumerable<TSource> source)
        {
            return source.ToList().ToArray();
        }

        public static List<TSource> ToList<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw __DefinedError.ArgumentNull("source");
            }
            return new List<TSource>(source);
        }


        public static TSource Last<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }

            TSource current;

            using (IEnumerator<TSource> enumerator = source.AsEnumerable().GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    current = enumerator.Current;

                    while (enumerator.MoveNext())
                    {
                        current = enumerator.Current;
                    }

                }
                else
                    throw Error.NoElements();
            }

            return current;
        }



        public static TSource First<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }

            TSource current;


            using (IEnumerator<TSource> enumerator = source.AsEnumerable().GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    current = enumerator.Current;
                }
                else
                    throw Error.NoElements();

            }

            return current;

        }

        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw __DefinedError.ArgumentNull("source");
            }


            var value = default(TSource);

            foreach (TSource local in source.AsEnumerable())
            {
                value = local;

                break;
            }

            return value;
        }

        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source, global::System.Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw __DefinedError.ArgumentNull("source");
            }
            if (predicate == null)
            {
                throw __DefinedError.ArgumentNull("predicate");
            }

          

            var value = default(TSource);

            foreach (TSource local in source.AsEnumerable())
            {
                if (predicate(local))
                {
                    value = local;

                    break;
                }
            }

            return value;
        }


    }

}
