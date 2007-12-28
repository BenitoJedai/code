using ScriptCoreLib;
using ScriptCoreLib.Shared;

using global::System.Collections;
using global::System.Collections.Generic;


using IDisposable = global::System.IDisposable;
using System;
using System.Linq;

namespace ScriptCoreLib.Shared.Query
{




    [Script(Implements = typeof(global::System.Linq.Enumerable))]
    internal static partial class __Enumerable
    {




        public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> p)
        {
            var r = false;

            foreach (var v in source.AsEnumerable())
            {
                if (p(v))
                {
                    r = true;

                    break;
                }
            }

            return r;
        }

        public static bool Contains<TSource>(this IEnumerable<TSource> source, TSource value)
        {
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

        #region Min
        public static int Min(this IEnumerable<int> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            int num2 = 0;
            bool flag2 = false;
            foreach (int num3 in source.AsEnumerable())
            {
                if (flag2)
                {
                    if (num3 < num2)
                    {
                        num2 = num3;
                    }
                    continue;
                }
                num2 = num3;
                flag2 = true;
            }
            if (!flag2)
            {
                throw Error.NoElements();
            }
            return num2;
        }





        #endregion

        #region Max



        public static int Max(this IEnumerable<int> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            int num2 = 0;
            bool flag2 = false;
            foreach (int num3 in source.AsEnumerable())
            {
                if (flag2)
                {
                    if (num3 > num2)
                    {
                        num2 = num3;
                    }
                    continue;
                }
                num2 = num3;
                flag2 = true;
            }
            if (!flag2)
            {
                throw Error.NoElements();
            }
            return num2;
        }
        #endregion

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
                throw Error.ArgumentNull("source");
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
                throw Error.ArgumentNull("source");
            }
            if (predicate == null)
            {
                throw Error.ArgumentNull("predicate");
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

        //public static void ForEach<T, R>(this IEnumerable<T> array, Func<T, R> func)
        //{
        //    array.ForEach(func.AsAction());
        //}







        public static TSource[] ToArray<TSource>(this IEnumerable<TSource> source)
        {
            return source.ToList().ToArray();
        }

        public static List<TSource> ToList<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
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
