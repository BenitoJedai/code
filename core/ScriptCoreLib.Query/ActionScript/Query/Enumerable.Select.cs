using ScriptCoreLib;
using ScriptCoreLib.Shared;

using global::System.Collections;
using global::System.Collections.Generic;

using IDisposable = global::System.IDisposable;
using System;

namespace ScriptCoreLib.ActionScript.Query
{

    internal static partial class __Enumerable
    {


        #region Select

        //public static IEnumerable<S> Select<T, S>(this T[] source, Func<T, S> selector)
        //{
        //    return Select( (SZArrayEnumerator<T>) source, selector);
        //}

        public static IEnumerable<S> Select<T, S>(this IEnumerable<T> source, global::System.Func<T, S> selector)
        {
            return SelectIterator<T, S>(ScriptCoreLib.Shared.Query.InternalSequence.AsEnumerable(source), selector);
        }

        #region yield return e.Select(f);

        [Script]
        sealed class _SelectIterator_d__b<T, S> :
            IEnumerable<S>, IEnumerator<S>,
            IEnumerable, IEnumerator, IDisposable
        {
            int _1_state;

            private S _2_current;

            public IEnumerable<T> _3_source;
            public global::System.Func<T, S> _3_selector;

            public T _e_5;

            public IEnumerator<T> _7_wrap;


            public IEnumerable<T> source;
            public global::System.Func<T, S> selector;

            public _SelectIterator_d__b(int _1_state)
            {
                this._1_state = _1_state;
            }


            #region IDisposable Members

            public void Dispose()
            {
                if (this._1_state == 1) return;
                if (this._1_state == 2) return;

                this._1_state = -1;

                if (this._7_wrap != null)
                {
                    this._7_wrap.Dispose();
                }


            }

            #endregion

            #region IEnumerable<S> Members

            IEnumerator<S> IEnumerable<S>.GetEnumerator()
            {
                _SelectIterator_d__b<T, S> _ret = null;

                if (this._1_state == -2)
                {
                    this._1_state = 0;
                    _ret = this;
                }
                else
                {
                    _ret = new _SelectIterator_d__b<T, S>(0);
                }



                _ret.source = this._3_source;
                _ret.selector = this._3_selector;

                return _ret;
            }

            #endregion

            #region IEnumerator<S> Members

            public S Current
            {
                get { return this._2_current; }
            }

            #endregion

            #region IEnumerator Members



            public bool MoveNext()
            {
                if (this._1_state == 0 || this._1_state == 2)
                {
                    if (this._1_state == 0)
                    {
                        this._1_state = -1;
                        this._7_wrap = this.source.GetEnumerator();
                    }

                    this._1_state = 1;

                    while (this._7_wrap.MoveNext())
                    {
                        this._e_5 = this._7_wrap.Current;

                        this._2_current = this.selector(this._e_5);
                        this._1_state = 2;

                        return true;
                    }

                    this._1_state = -1;
                }

                return false;
            }

            object IEnumerator.Current
            {
                get { return this.Current; }
            }

            public void Reset()
            {
                throw new Exception("The method or operation is not implemented.");
            }

            #endregion

            #region IEnumerable Members


            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable<S>)this).GetEnumerator();
            }

            #endregion
        }

        #endregion

        private static IEnumerable<S> SelectIterator<T, S>(IEnumerable<T> source, global::System.Func<T, S> selector)
        {
            return new _SelectIterator_d__b<T, S>(-2)
            {
                _3_source = source,
                _3_selector = selector
            };
        }

        #endregion
    }
}
