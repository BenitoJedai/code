using ScriptCoreLib;

using global::System.Collections;
using global::System.Collections.Generic;

using IDisposable = global::System.IDisposable;
using System;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Linq
{

    internal static partial class __Enumerable
    {
      

        #region Where

        //public static IEnumerable<T> Where<T>(this T[] source, Func<T, bool> predicate)
        //{
        //    return Where( (SZArrayEnumerator<T>)source, predicate);
        //}

        public static IEnumerable<T> Where<T>(this IEnumerable<T> source, global::System.Func<T, bool> predicate)
        {
            if (predicate == null) throw new ArgumentNullException("predicate");
            if (source == null) throw new ArgumentNullException("source");

            return WhereIterator<T>(source, predicate);
        }

        [Script]
        sealed class _WhereIterator_d__0<T> :
            IEnumerable<T>, IEnumerator<T>,
            IEnumerable, IEnumerator, IDisposable
        {
            int _1_state;

            public _WhereIterator_d__0(int state)
            {
                this._1_state = state;
            }

            public IEnumerable<T> _3_source;
            public global::System.Func<T, bool> _3_predicate;

            public IEnumerable<T> source;
            public global::System.Func<T, bool> predicate;

            #region IEnumerable<S> Members

            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                _WhereIterator_d__0<T> _ret = null;

                if (this._1_state == -2)
                {
                    this._1_state = 0;
                    _ret = this;
                }
                else
                {
                    _ret = new _WhereIterator_d__0<T>(0);
                }



                _ret.source = this._3_source;
                _ret.predicate = this._3_predicate;

                return _ret;
            }

            #endregion

            #region IEnumerator<S> Members

            public T Current
            {
                get { return this._2_current; }
            }

            #endregion


            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable<T>)this).GetEnumerator();
            }

            object IEnumerator.Current
            {
                get { return this.Current; }
            }

            public void Reset()
            {
                throw __DefinedError.NotImplemented();
            }


            #endregion

            private T _2_current;

            public T _e_5;

            public IEnumerator<T> _7_wrap;


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

                        if (!this.predicate(this._e_5))
                            continue;

                        this._2_current = this._e_5;
                        this._1_state = 2;

                        return true;
                    }

                    this._1_state = -1;
                }

                return false;
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
        }

        private static IEnumerable<T> WhereIterator<T>(IEnumerable<T> source, global::System.Func<T, bool> predicate)
        {
            return new _WhereIterator_d__0<T>(-2) 
            { 
                _3_source = source, 
                _3_predicate = predicate 
            };
        }

        #endregion
    }
}
