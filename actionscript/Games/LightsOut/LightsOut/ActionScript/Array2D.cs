using ScriptCoreLib;
using ScriptCoreLib.Shared;

using System.Linq;
using System;

namespace LightsOut.ActionScript
{
    [Script]
    public class Array2D<T> : System.Collections.Generic.IEnumerable<T>
    {
        readonly T[] items;

        readonly int _XLength;
        readonly int _YLength;

        public int Length
        {
            get { return items.Length; }
        }

        public int XLength
        {
            get { return _XLength; }
        }

        public int YLength
        {
            get { return _YLength; }
        }
	

        public Array2D(int x, int y)
        {
            this._XLength = x;
            this._YLength = y;

            this.items = new T[x * y];
        }

        public void ForEach(Action<int, int> a)
        {
            for (int i = 0; i < this._XLength; i++)
                for (int j = 0; j < this._YLength; j++)
                    a(i, j);

        }

        public Array2D<bool> ToBooleanArray()
        {
            return new Array2D<bool>(_XLength, _YLength);
        }

        public readonly T EmptyValue;

        public T this[int x, int y]
        {
            get
            {
                if (x < 0) return EmptyValue;
                if (y < 0) return EmptyValue;
                if (x >= this._XLength) return EmptyValue;
                if (y >= this._YLength) return EmptyValue;

                return this.items[this._XLength * y + x];
            }
            set
            {
                if (x < 0) return;
                if (y < 0) return;
                if (x >= this._XLength) return;
                if (y >= this._YLength) return;

                this.items[this._XLength * y + x] = value;
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
