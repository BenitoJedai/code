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

        /// <summary>
        /// target language shall override this
        /// </summary>
        static internal Func<IEnumerable, IEnumerable> InternalAsEnumerableImplementation;

        public static IEnumerable<TSource> AsEnumerable<TSource>(this IEnumerable<TSource> source)
        {
            // the runtime should create a SZArray for arrays but it does not for the moment

            if (InternalAsEnumerableImplementation != null)
                return (IEnumerable<TSource>)InternalAsEnumerableImplementation(source);


            return source;
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








    }





}
