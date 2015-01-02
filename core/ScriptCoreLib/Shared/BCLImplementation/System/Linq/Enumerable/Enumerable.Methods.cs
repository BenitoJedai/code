using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq
{

    static partial class __Enumerable
    {







        public static IEnumerable<TSource> Reverse<TSource>(this IEnumerable<TSource> source)
        {
            var a = source.ToList();

            a.Reverse();

            return a;
        }


        public static bool Any<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
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
                throw new ArgumentNullException("source");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
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
                throw new ArgumentNullException("source");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
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

        public static bool Contains<TSource>(this IEnumerable<TSource> source, TSource value)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }


            var r = false;

            foreach (var v in source.AsEnumerable())
            {
                if (value != null)
                {
                    // order by comparator?
                    // http://bugs.sun.com/view_bug.do?bug_id=6608961
                    var comparer = Comparer<TSource>.Default;

                    if (comparer.Compare(v, value) == 0)
                    {
                        r = true;

                        break;
                    }
                }

                if (object.ReferenceEquals(v, value))
                {
                    r = true;

                    break;
                }
            }

            return r;
        }


        public static int Count<T>(this IEnumerable<T> e, global::System.Func<T, bool> predicate)
        {
            int c = 0;

            foreach (var v in e.AsEnumerable()) if (predicate(v)) c++;

            return c;
        }

        public static int Count<T>(this IEnumerable<T> e)
        {
            int c = 0;

            foreach (var v in e.AsEnumerable()) c++;

            return c;
        }






        public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }


            var value = default(TSource);

            foreach (TSource local in source.AsEnumerable())
            {
                value = local;

            }

            return value;
        }

        public static TSource Last<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
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
                    throw __DefinedError.NoElements();
            }

            return current;
        }


        public static TSource First<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> filter)
        {
            return source.Where(filter).First();
        }

        public static TSource First<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            TSource current;


            using (IEnumerator<TSource> enumerator = source.AsEnumerable().GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    current = enumerator.Current;
                }
                else
                    throw __DefinedError.NoElements();

            }

            return current;

        }


        // X:\jsc.svn\examples\javascript\Test\TestNullConditionalOperator\TestNullConditionalOperator\Application.cs

        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            var current = default(TSource);

            using (IEnumerator<TSource> enumerator = source.AsEnumerable().GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    current = enumerator.Current;
                }
            }

            return current;
        }

        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source, global::System.Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
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



        public static TSource Single<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return source.Where(predicate).Single();
        }

        public static TSource Single<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            TSource current;

            using (IEnumerator<TSource> enumerator = source.AsEnumerable().GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    throw __DefinedError.NoElements();
                }
                current = enumerator.Current;

                if (enumerator.MoveNext())
                {
                    throw __DefinedError.MoreThanOneElement();


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
                throw new ArgumentNullException("source");
            }

            var current = default(TSource);

            using (IEnumerator<TSource> enumerator = source.AsEnumerable().GetEnumerator())
            {
                if (enumerator.MoveNext())
                    current = enumerator.Current;


            }

            return current;
        }

        //public static void ForEach<T, R>(this IEnumerable<T> array, Func<T, R> func)
        //{
        //    array.ForEach(func.AsAction());
        //}










        public static U Aggregate<T, U>(this IEnumerable<T> source,
                                U seed, global::System.Func<U, T, U> func)
        {
            U result = seed;

            foreach (T element in source.AsEnumerable())
                result = func(result, element);

            return result;
        }

        //public static U Aggregate<T, U>(this IEnumerable<T> source,
        //                        U seed, Action<U, T> func)
        //{
        //    return source.Aggregate(seed, (u, t) => { func(u, t); return u; });
        //}






        public static IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
        {
            // X:\jsc.svn\examples\javascript\test\TestEnumerableZip\TestEnumerableZip\Application.cs

            //var x = second.GetEnumerator();
            var x = second.AsEnumerable().GetEnumerator();

            return first.TakeWhile(z => x.MoveNext()).Select(
                z =>
                {
                    return resultSelector(z, x.Current);
                }
            );
        }

    }



}
