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
            if (array == null)
                throw Error.ArgumentNull("array");

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
            if (e == null)
                return null;

            return new SZArrayEnumerator<T>(e);
        }
    }


}
