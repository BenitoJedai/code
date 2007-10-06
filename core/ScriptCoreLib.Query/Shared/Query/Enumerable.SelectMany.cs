using ScriptCoreLib;
using ScriptCoreLib.Shared;

using global::System.Collections;
using global::System.Collections.Generic;

using IDisposable = global::System.IDisposable;

using System;

namespace ScriptCoreLib.Shared.Query
{

    internal static partial class __Enumerable
    {

        public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, global::System.Func<TSource, IEnumerable<TResult>> selector)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            if (selector == null)
            {
                throw Error.ArgumentNull("selector");
            }
            return SelectManyIterator<TSource, TResult>(source, selector);
        }


        private static IEnumerable<TResult> SelectManyIterator<TSource, TResult>(IEnumerable<TSource> source, global::System.Func<TSource, IEnumerable<TResult>> selector)
        {
            return new _SelectManyIterator_d__16<TSource, TResult>(-2) { __3__source = source.AsEnumerable(), __3__selector = selector };
        }


        [Script]
        private sealed class _SelectManyIterator_d__16<TSource, TResult> : IEnumerable<TResult>, IEnumerable, IEnumerator<TResult>, IEnumerator, IDisposable
        {
            private int __1__state;


            private TResult __2__current;



            public IEnumerable<TSource> __3__source;
            public global::System.Func<TSource, IEnumerable<TResult>> __3__selector;

            public IEnumerator<TSource> __7__wrap19;
            public IEnumerator<TResult> __7__wrap1a;


            public IEnumerable<TSource> source;
            public global::System.Func<TSource, IEnumerable<TResult>> selector;

            public _SelectManyIterator_d__16(int _state)
            {
                this.__1__state = _state;

            }


            #region IEnumerable<TResult> Members

            public IEnumerator<TResult> GetEnumerator()
            {
                _SelectManyIterator_d__16<TSource, TResult> _ret = null;

                if (this.__1__state == -2)
                {
                    this.__1__state = 0;
                    _ret = this;
                }
                else
                {
                    _ret = new _SelectManyIterator_d__16<TSource, TResult>(0);
                }


                _ret.source = this.__3__source;
                _ret.selector = this.__3__selector;

                return _ret;
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable<TResult>)this).GetEnumerator();
            }

            #endregion

            #region IEnumerator<TResult> Members

            public TResult Current
            {
                get { return this.__2__current; }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                try
                {
                    this.__1__state = 1;

                    if (this.__7__wrap1a != null)
                    {
                        this.__7__wrap1a.Dispose();
                    }
                }
                finally
                {
                    this.__1__state = -1;
                    if (this.__7__wrap19 != null)
                    {
                        this.__7__wrap19.Dispose();
                    }
                }
            }

            #endregion

            #region IEnumerator Members

            object IEnumerator.Current
            {
                get { return this.Current; }
            }

            public bool MoveNext()
            {
                var Label_00A4 = false;

                if (this.__1__state != 0)
                {
                    if (this.__1__state == 3)
                    {
                        Label_00A4 = true;

                    }
                    else
                    {
                        return false;
                    }
                }

                if (!Label_00A4)
                {
                    this.__1__state = -1;
                    this.__7__wrap19 = this.source.GetEnumerator();
                    this.__1__state = 1;
                }

                while (Label_00A4 || this.__7__wrap19.MoveNext())
                {
                    if (!Label_00A4)
                    {
                        this.__7__wrap1a = this.selector(this.__7__wrap19.Current).AsEnumerable().GetEnumerator();
                        this.__1__state = 2;
                    }
                    else
                    {
                        Label_00A4 = false;
                        this.__1__state = 2;
                    }
                    while (this.__7__wrap1a.MoveNext())
                    {
                        this.__2__current = this.__7__wrap1a.Current;

                        this.__1__state = 3;
                        return true;
                    }
                    this.__1__state = 1;

                }
                this.__1__state = -1;


                return false;
            }

            public void Reset()
            {
                throw new Exception("The method or operation is not implemented.");
            }

            #endregion
        }







    }
}
