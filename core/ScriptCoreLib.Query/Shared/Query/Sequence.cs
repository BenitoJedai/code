using ScriptCoreLib;
using ScriptCoreLib.Shared;

using global::System.Collections;
using global::System.Collections.Generic;


using IDisposable = global::System.IDisposable;
using ScriptException = global::ScriptCoreLib.JavaScript.System.ScriptException;

namespace ScriptCoreLib.Shared.Query
{




    [Script]
    public static partial class Sequence
    {

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

        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
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


        public static void ForEach<T>(this IEnumerable<T> array, Action<T> action)
        {
            if (array == null)
            {
                throw Error.ArgumentNull("array");
            }
            if (action == null)
            {
                throw Error.ArgumentNull("action");
            }

            foreach (var v in array.AsEnumerable())
            {
                action(v);
            }
      
        }

 

 






    }





}
