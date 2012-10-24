using ScriptCoreLib;
using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.BCLImplementation.System.Collections;
using ScriptCoreLib.Shared.BCLImplementation.System.Collections.Generic;
using System;
using System.Collections;
using System.Collections.Generic;

//namespace ScriptCoreLib.Shared.Query
namespace ScriptCoreLib.Shared.BCLImplementation.System
{

    // todo: IsArrayEnumerator should be replaced by Array.GetEnumerator<T>
    [Script(IsArrayEnumerator = true
        //, IsDebugCode = true
        )]
    internal class __SZArrayEnumerator<T> :
         __IEnumerator<T>,
         __IEnumerator,

        // special interfaces:
        IDisposable,
        IEnumerable<T>,
        IEnumerable
    {
        // http://igoro.com/archive/puzzling-over-arrays-and-enumerators-in-c/

        T[] _array;
        int _index;
        int _endIndex;

        public void __ref0()
        {
        }

        #region jsc is looking for this operator
        public static implicit operator __SZArrayEnumerator<T>(T[] e)
        {
            if (e == null)
                return null;

            return new __SZArrayEnumerator<T>(e);
        }
        #endregion

        public __SZArrayEnumerator(T[] array)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            this._array = array;
            this._index = -1;
            this._endIndex = array.Length;
        }

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            if (_index == -1)
            {
                return (IEnumerator<T>)(object)this;
            }
            else
            {
                return (IEnumerator<T>)(object)new __SZArrayEnumerator<T>(this._array);
            }
        }

        #endregion

        IEnumerator IEnumerable.GetEnumerator()
        {
            if (_index == -1)
            {
                return (IEnumerator<T>)(object)this;
            }
            else
            {
                return (IEnumerator)(object)new __SZArrayEnumerator<T>(this._array);
            }
        }

        #region IEnumerator<T> Members

        public T Current
        {
            get
            {

                if (this._index < 0)
                    throw new InvalidOperationException("InvalidOperation_EnumNotStarted");
                if (this._index >= this._endIndex)
                    throw new InvalidOperationException("InvalidOperation_EnumEnded");

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

        object __IEnumerator.Current
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
            throw new NotImplementedException();
        }

        #endregion



    }



}
