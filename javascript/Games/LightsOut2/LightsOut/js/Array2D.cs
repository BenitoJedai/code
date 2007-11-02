using ScriptCoreLib;
using ScriptCoreLib.Shared;

using System.Linq;
using System;

namespace LightsOut.js
{
    [Script]
    class Array2D<T> : System.Collections.Generic.IEnumerable<T>
    {
        readonly T[] items;

        readonly int x;
        readonly int y;

        public int Length
        {
            get { return items.Length; }
        }

        public int XLength
        {
            get { return x; }
        }

        public int YLength
        {
            get { return y; }
        }
	

        public Array2D(int x, int y)
        {
            this.x = x;
            this.y = y;

            this.items = new T[x * y];
        }

        public void ForEach(Action<int, int> a)
        {
            for (int i = 0; i < this.x; i++)
                for (int j = 0; j < this.y; j++)
                    a(i, j);

        }

        public Array2D<bool> ToBooleanArray()
        {
            return new Array2D<bool>(x, y);
        }

        public T this[int x, int y]
        {
            get
            {
                if (x < 0) return default(T);
                if (y < 0) return default(T);
                if (x >= this.x) return default(T);
                if (y >= this.y) return default(T);

                return this.items[this.x * y + x];
            }
            set
            {
                if (x < 0) return;
                if (y < 0) return;
                if (x >= this.x) return;
                if (y >= this.y) return;

                this.items[this.x * y + x] = value;
            }
        }

        #region IEnumerable<T> Members

        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
        {
            return this.items.AsEnumerable().GetEnumerator();
        }

        #endregion



        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.items.AsEnumerable().GetEnumerator();
        }

        #endregion
    }

}
