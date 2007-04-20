using ScriptCoreLib;
using ScriptCoreLib.Shared;

using global::System.Collections;
using global::System.Collections.Generic;

using IDisposable = global::System.IDisposable;
using ScriptException = global::ScriptCoreLib.JavaScript.System.ScriptException;

namespace ScriptCoreLib.Shared.Query
{


    [Script]
    public class SZArrayEnumerator<T> :
        IEnumerable<T>, IEnumerator<T>,
        IEnumerable, IEnumerator, IDisposable
    {
        T[] _array;
        int _index;
        int _endIndex;

        public SZArrayEnumerator(T[] array)
        {
            this._array = array;
            this._index = -1;
            this._endIndex = array.Length;
        }

        #region IEnumerable<T> Members

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            if (_index == -1)
            {
                return this;
            }
            else
            {
                return new SZArrayEnumerator<T>(this._array);
            }
        }

        #endregion

        IEnumerator IEnumerable.GetEnumerator()
        {
            if (_index == -1)
            {
                return this;
            }
            else
            {
                return new SZArrayEnumerator<T>(this._array);
            }
        }

        #region IEnumerator<T> Members

        public T Current
        {
            get
            {

                if (this._index < 0)
                    throw new ScriptException("InvalidOperation_EnumNotStarted");
                if (this._index >= this._endIndex)
                    throw new ScriptException("InvalidOperation_EnumEnded");

                return this._array[this._index];
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            this._index = -1;
        }

        #endregion

        #region IEnumerator Members

        object IEnumerator.Current
        {
            get { return this.Current; }
        }

        public bool MoveNext()
        {
            if (this._index < this._endIndex)
            {
                this._index++;
                return (this._index < this._endIndex);
            }

            return false;
        }

        public void Reset()
        {
            throw new ScriptException("The method or operation is not implemented.");
        }

        #endregion


        public static implicit operator SZArrayEnumerator<T>(T[] e)
        {
            return new SZArrayEnumerator<T>(e);
        }
    }


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
            foreach (int num3 in source)
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
            foreach (int num3 in source)
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

            foreach (var v in e) c++;

            return c;
        }

        public static T ElementAt<T>(this IEnumerable<T> e, int index)
        {
            int i = -1;

            T r = default(T);

            foreach (var v in e)
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

        public static IEnumerable<TSource> AsEnumerable<TSource>(this IEnumerable<TSource> source)
        {
            return source;
        }






        public static TSource Last<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }

            TSource current;

            using (IEnumerator<TSource> enumerator = source.GetEnumerator())
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


            using (IEnumerator<TSource> enumerator = source.GetEnumerator())
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

            using (IEnumerator<TSource> enumerator = source.GetEnumerator())
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
