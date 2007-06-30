using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace jsx
{
    public class PEnumerator<T> : IEnumerable<T[]>
    {
        public readonly IEnumerable<T>[] Source;
        public readonly int Count;

        public PEnumerator(params IEnumerable<T>[] e)
        {
            if (e.Length == 0)
                throw new ArgumentOutOfRangeException();

            Count = e[0].Count();

            for (int i = 1; i < e.Length; i++)
            {
                if (e[i].Count() != Count)
                    throw new ArgumentOutOfRangeException();
                    
            }

            Source = e;
        }




        #region IEnumerable<T[]> Members

        IEnumerator<T[]> IEnumerable<T[]>.GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return this[i];
            }
        }

        public T[] this[int i]
        {
            get
            {
                var z = new T[Source.Length];

                for (int x = 0; x < Source.Length; x++)
                {
                    z[x] = Source[x].ElementAtOrDefault(i);
                }

                return z;
            }
        }

        #endregion

        #region IEnumerable Members


        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }

    [DebuggerDisplay("{Value}")]
    public struct OnlyOnce
    {
        public bool Value;

        public static implicit operator bool(OnlyOnce e)
        {
            return e.Value;
        }

        public static OnlyOnce operator ++(OnlyOnce e)
        {
            e.Value = true;

            return e;
        }
    }
}
