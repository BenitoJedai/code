using ScriptCoreLib;
using ScriptCoreLib.Shared;

using global::System.Collections;
using global::System.Collections.Generic;

using IDisposable = global::System.IDisposable;

using System;

namespace ScriptCoreLib.ActionScript.BCLImplementation.Query
{

    internal static partial class __Enumerable
    {

        public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            if (source == null)
            {
                throw DefinedError.ArgumentNull("source");
            }
            if (collectionSelector == null)
            {
                throw DefinedError.ArgumentNull("collectionSelector");
            }
            if (resultSelector == null)
            {
                throw DefinedError.ArgumentNull("resultSelector");
            }

            return SelectManyIterator<TSource, TCollection, TResult>(source, collectionSelector, resultSelector);
        }

        private static IEnumerable<TResult> SelectManyIterator<TSource, TCollection, TResult>(IEnumerable<TSource> source, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            return new _SelectManyIterator_d__37<TSource, TCollection, TResult>(-2) { __3__source = source, __3__collectionSelector = collectionSelector, __3__resultSelector = resultSelector };
        }

        [Script]
        private sealed class _SelectManyIterator_d__37<TSource, TCollection, TResult> : IEnumerable<TResult>, IEnumerable, IEnumerator<TResult>, IEnumerator, IDisposable
        {
            private int __1__state;
            private TResult __2__current;
            public Func<TSource, IEnumerable<TCollection>> __3__collectionSelector;
            public Func<TSource, TCollection, TResult> __3__resultSelector;
            public IEnumerable<TSource> __3__source;
            public IEnumerator<TSource> __7__wrap3a;
            public IEnumerator<TCollection> __7__wrap3c;
            public TSource _element_5__38;
            public TCollection _subElement_5__39;
            public Func<TSource, IEnumerable<TCollection>> collectionSelector;
            public Func<TSource, TCollection, TResult> resultSelector;
            public IEnumerable<TSource> source;





            public _SelectManyIterator_d__37(int __1__state)
            {
                this.__1__state = __1__state;
            }



            IEnumerator<TResult> IEnumerable<TResult>.GetEnumerator()
            {
                __Enumerable._SelectManyIterator_d__37<TSource, TCollection, TResult> d__ = null;

                if (this.__1__state == -2)
                {
                    this.__1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new __Enumerable._SelectManyIterator_d__37<TSource, TCollection, TResult>(0);
                }
                d__.source = this.__3__source;
                d__.collectionSelector = this.__3__collectionSelector;
                d__.resultSelector = this.__3__resultSelector;
                return d__;
            }



            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable<TResult>)this).GetEnumerator();
            }


            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }


            private void __m__Finally3b()
            {
                this.__1__state = -1;
                if (this.__7__wrap3a != null)
                {
                    this.__7__wrap3a.Dispose();
                }
            }


            private void __m__Finally3d()
            {
                this.__1__state = 1;
                if (this.__7__wrap3c != null)
                {
                    this.__7__wrap3c.Dispose();
                }
            }

            void IDisposable.Dispose()
            {
                try
                {
                    this.__m__Finally3d();

                }
                finally
                {
                    this.__m__Finally3b();
                }
            }


            TResult IEnumerator<TResult>.Current
            {
                get
                {
                    return this.__2__current;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return (TResult)this.__2__current;
                }
            }

            public bool MoveNext()
            {
                try
                {
                    var Label_00AA = false;

                    var __1__state_eq_0 = this.__1__state == 0;
                    var __1__state_eq_3 = this.__1__state == 3;

                    if (__1__state_eq_0 || __1__state_eq_3)
                    {
                        if (__1__state_eq_3)
                            Label_00AA = true;

                        if (Label_00AA || __1__state_eq_0)
                        {
                            if (!Label_00AA)
                            {
                                this.__1__state = -1;
                                this.__7__wrap3a = this.source.AsEnumerable().GetEnumerator();
                                this.__1__state = 1;
                            }

                            while (Label_00AA || this.__7__wrap3a.MoveNext())
                            {
                                if (!Label_00AA)
                                {
                                    this._element_5__38 = this.__7__wrap3a.Current;
                                    this.__7__wrap3c = this.collectionSelector(this._element_5__38).AsEnumerable().GetEnumerator();
                                }
                                Label_00AA = false;
                                this.__1__state = 2;
                                while (this.__7__wrap3c.MoveNext())
                                {
                                    this._subElement_5__39 = this.__7__wrap3c.Current;
                                    this.__2__current = this.resultSelector(this._element_5__38, this._subElement_5__39);
                                    this.__1__state = 3;
                                    return true;
                                }
                                this.__m__Finally3d();
                            }
                            this.__m__Finally3b();
                        }
                    }

                    return false;
                }
                catch
                {
                    ((IDisposable)this).Dispose();

                    throw;
                }
            }





        }



        public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, global::System.Func<TSource, IEnumerable<TResult>> selector)
        {
            if (source == null)
            {
                throw DefinedError.ArgumentNull("source");
            }
            if (selector == null)
            {
                throw DefinedError.ArgumentNull("selector");
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
