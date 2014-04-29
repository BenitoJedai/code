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



        public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(
            this IEnumerable<TOuter> outer,
            IEnumerable<TInner> inner,
            Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector,
            Func<TOuter, TInner, TResult> resultSelector)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140429
            // X:\jsc.svn\examples\javascript\test\TestLINQJoin\TestLINQJoin\Application.cs

            var c = Comparer<TKey>.Default;
            var s = Stopwatch.StartNew();

            //Console.WriteLine("enter Join");


            var o = outer.ToArray();
            var i = inner.ToArray();

            return
                from jo in outer
                from ji in inner
                let ko = outerKeySelector(jo)
                let ki = innerKeySelector(ji)
                where c.Compare(ko, ki) == 0
                let r = resultSelector(jo, ji)
                select r;


            //Console.WriteLine("exit Join " + new { s.ElapsedMilliseconds });
            //return null;
        }

        //public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(
        //    this IEnumerable<TOuter> outer, 
        //    IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, 
        //    Func<TInner, TKey> innerKeySelector, 
        //    Func<TOuter, TInner, TResult> resultSelector, 
        //    IEqualityComparer<TKey> comparer)
        //{

        //}



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







        public static TSource[] ToArray<TSource>(this IEnumerable<TSource> source)
        {
            return source.AsEnumerable().ToList().ToArray();
        }

        public static List<TSource> ToList<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            return new List<TSource>(source);
        }


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








    }



}
