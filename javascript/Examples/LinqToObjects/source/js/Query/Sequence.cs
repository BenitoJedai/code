using ScriptCoreLib;
using ScriptCoreLib.Shared;

using global::System.Collections;
using global::System.Collections.Generic;

using IDisposable = global::System.IDisposable;
using ScriptException = global::ScriptCoreLib.JavaScript.System.ScriptException;

namespace LinqToObjects.source.js.Query
{
    [Script]
    public static class Sequence
    {
        [Script]
        class SZArrayEnumerator<T> :
            IEnumerable<T>, IEnumerator<T>,
            IEnumerable, IEnumerator, IDisposable
        {
            T[] _array;
            int _index;
            int _endIndex;

            public SZArrayEnumerator(T[] array)
            {
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
                        throw  new ScriptException("InvalidOperation_EnumNotStarted");
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
        }

        #region Select

        public static IEnumerable<S> Select<T, S>(this T[] source, Func<T, S> selector)
        {
            return Select(new SZArrayEnumerator<T>(source), selector);
        }

        public static IEnumerable<S> Select<T, S>(this IEnumerable<T> source, Func<T, S> selector)
        {
            return SelectIterator<T, S>(source, selector);
        }

        #region yield return e.Select(f);

        [Script]
        private sealed class _SelectIterator_d__b<T, S> :
            IEnumerable<S>, IEnumerator<S>,
            IEnumerable, IEnumerator, IDisposable
        {
            int _1_state;

            private S _2_current;

            public IEnumerable<T> _3_source;
            public Func<T, S> _3_selector;

            public T _e_5;

            public IEnumerator<T> _7_wrap;


            public IEnumerable<T> source;
            public Func<T, S> selector;

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
                throw new ScriptException("The method or operation is not implemented.");
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

        private static IEnumerable<S> SelectIterator<T, S>(IEnumerable<T> source, Func<T, S> selector)
        {
            return new _SelectIterator_d__b<T, S>(-2)
            {
                _3_source = source,
                _3_selector = selector,
            };
        }

        #endregion


        #region Where

        public static IEnumerable<T> Where<T>(this T[] source, Func<T, bool> predicate)
        {
            return Where(new SZArrayEnumerator<T>(source), predicate);
        }

        public static IEnumerable<T> Where<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            return WhereIterator<T>(source, predicate);
        }

        [Script]
        public class _WhereIterator_d__0<T> :
            IEnumerable<T>, IEnumerator<T>,
            IEnumerable, IEnumerator, IDisposable
        {
            int _1_state;

            public _WhereIterator_d__0(int state)
            {
                this._1_state = state;
            }

            public IEnumerable<T> _3_source;
            public Func<T, bool> _3_predicate;

            public IEnumerable<T> source;
            public Func<T, bool> predicate;

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
                throw new ScriptException("The method or operation is not implemented.");
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

        private static IEnumerable<T> WhereIterator<T>(IEnumerable<T> source, Func<T, bool> predicate)
        {
            return new _WhereIterator_d__0<T>(-2) { _3_source = source, _3_predicate = predicate };
        }

        #endregion
    }
}
