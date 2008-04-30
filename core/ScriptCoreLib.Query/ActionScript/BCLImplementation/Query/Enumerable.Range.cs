using ScriptCoreLib;
using ScriptCoreLib.Shared;

using global::System.Collections;
using global::System.Collections.Generic;

using IDisposable = global::System.IDisposable;

namespace ScriptCoreLib.ActionScript.BCLImplementation.Query
{

    internal static partial class __Enumerable
    {

        public static IEnumerable<int> Range(int start, int count)
        {
            long num;
            num = (((long)start) + ((long)count)) - ((long)1);
            if (count < 0)
            {

            }
            else if (num <= ((long)0x7fffffff))
            {
                return RangeIterator(start, count);
            }

            throw DefinedError.ArgumentOutOfRange("count");
        }


        [Script]
        sealed partial class _RangeIterator_d__91 : IEnumerable<int>, IEnumerable, IEnumerator<int>, IEnumerator, IDisposable
        {
            private int __1__state;



            public int __3__start;
            public int __3__count;

            public int start;
            public int count;


            private int __2__current;



            public int _i_5__92;
 




            public _RangeIterator_d__91(int __1__state)
            {
                this.__1__state = __1__state;
                return;
            }






            #region IEnumerable<int> Members

            public IEnumerator<int> GetEnumerator()
            {
                _RangeIterator_d__91 _ret = null;

                if (this.__1__state == -2)
                {
                    this.__1__state = 0;
                    _ret = this;
                }
                else
                {
                    _ret = new _RangeIterator_d__91(0);
                }



                _ret.start = this.__3__start;
                _ret.count = this.__3__count;

                return _ret;
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion

            #region IEnumerator<int> Members

            public int Current
            {
                get { return this.__2__current; }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
            }

            #endregion

            #region IEnumerator Members

            object IEnumerator.Current
            {
                get { return this.Current; }
            }

            

            public void Reset()
            {
                throw DefinedError.NotImplemented();
            }

            #endregion
        }


        private static IEnumerable<int> RangeIterator(int start, int count)
        {
            _RangeIterator_d__91 d__;
            d__ = new _RangeIterator_d__91(-2);
            d__.__3__start = start;
            d__.__3__count = count;
            return d__;
        }





    }
}
