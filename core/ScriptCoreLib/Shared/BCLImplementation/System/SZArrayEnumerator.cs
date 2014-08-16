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
    // http://referencesource.microsoft.com/#mscorlib/system/array.cs

    // 2014 java still dont know how to instanceof generics?
    [Script]
    public class __SZArrayEnumerator
    {
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Collections\Generic\List.cs

        public virtual object GetArray()
        {
            return null;
        }
    }

    // todo: IsArrayEnumerator should be replaced by Array.GetEnumerator<T>
    [Script(IsArrayEnumerator = true
        //, IsDebugCode = true
        )]
    public class __SZArrayEnumerator<T> : __SZArrayEnumerator,
         __IEnumerator<T>,
         __IEnumerator,

        // special interfaces:
        IDisposable,
        IEnumerable<T>,
        IEnumerable
    {
        // X:\jsc.svn\examples\java\hybrid\Test\TestJVMCLRAssignArrayToEnumerable\TestJVMCLRAssignArrayToEnumerable\Program.cs

        // http://igoro.com/archive/puzzling-over-arrays-and-enumerators-in-c/

        // typeof(T) or a way to infer enumerable type.
        // X:\jsc.svn\examples\java\hybrid\Test\TestJVMCLRGenericConcat\TestJVMCLRGenericConcat\Program.cs

        public T[] _array;

        public override object GetArray()
        {
            return _array;
        }


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
