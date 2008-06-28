using ScriptCoreLib;
using ScriptCoreLib.Shared;

using global::System.Collections;
using global::System.Collections.Generic;

using IDisposable = global::System.IDisposable;
using System;

namespace ScriptCoreLib.Shared.Query
{

    internal  static partial class __Enumerable
    {
        public static IEnumerable<TSource> Concat<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            if (first == null)
            {
                throw DefinedError.ArgumentNull("first");
            }
            if (second == null)
            {
                throw DefinedError.ArgumentNull("second");
            }
            return ConcatIterator<TSource>(first, second);
        }

        private static IEnumerable<TSource> ConcatIterator<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            return new _ConcatIterator_d__5b<TSource>(-2) { __3__first = first.AsEnumerable(), __3__second = second.AsEnumerable() };
        }

        [Script]
        private sealed class _ConcatIterator_d__5b<TSource> : IEnumerable<TSource>, IEnumerable, IEnumerator<TSource>, IEnumerator, IDisposable
        {
            // Fields
            private int __1__state;
            private TSource __2__current;
            public IEnumerable<TSource> __3__first;
            public IEnumerable<TSource> __3__second;
            public IEnumerator<TSource> __7__wrap5e;
            public IEnumerator<TSource> __7__wrap5f;
            public TSource _element_5__5c;
            public TSource _element_5__5d;
            public IEnumerable<TSource> first;
            public IEnumerable<TSource> second;


            public _ConcatIterator_d__5b(int _state)
            {
                this.__1__state = _state;

            }

            #region IEnumerable<TSource> Members

            public IEnumerator<TSource> GetEnumerator()
            {
                var _ret = default(_ConcatIterator_d__5b<TSource>);

                if (this.__1__state == -2)
                {
                    this.__1__state = 0;
                    _ret = this;
                }
                else
                {
                    _ret = new _ConcatIterator_d__5b<TSource>(0);
                }



                _ret.first = this.__3__first;
                _ret.second = this.__3__second;

                return _ret;
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion

            #region IEnumerator<TSource> Members

            public TSource Current
            {
                get { return this.__2__current; }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                this.__1__state = -1;
                if (this.__7__wrap5e != null)
                {
                    this.__7__wrap5e.Dispose();
                }

                this.__1__state = -1;
                if (this.__7__wrap5f != null)
                {
                    this.__7__wrap5f.Dispose();
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
                if (this.__1__state == 0 ||
                    this.__1__state == 2 ||
                    this.__1__state == 4)
                {


                    if (this.__1__state != 4)
                    {
                        if (this.__1__state == 0)
                        {

                            this.__1__state = -1;
                            this.__7__wrap5e = this.first.GetEnumerator();
                            this.__1__state = 1;
                        }
                        else
                        {
                            this.__1__state = 1;
                        }

                        while (this.__7__wrap5e.MoveNext())
                        {
                            this._element_5__5c = this.__7__wrap5e.Current;

                            this.__2__current = this._element_5__5c;
                            this.__1__state = 2;

                            return true;
                        }

                        this.__1__state = -1;
                        this.__7__wrap5f = this.second.GetEnumerator();
                        this.__1__state = 3;
                    }
                    else
                    {
                        this.__1__state = 3;
                    }

                    while (this.__7__wrap5f.MoveNext())
                    {
                        this._element_5__5d = this.__7__wrap5f.Current;

                        this.__2__current = this._element_5__5d;
                        this.__1__state = 4;

                        return true;
                    }



                }

                return false;
            }



            //public bool MoveNext()
            //{ // to be supported with jsx 
            //    switch (this.__1__state)
            //    {
            //        default:
            //        case 1:
            //        case 3:
            //            return false;
            //        case 0:
            //        case 2:
            //        case 4:

            //            switch (this.__1__state)
            //            {
            //                case 0:
            //                case 2:
            //                    switch (this.__1__state)
            //                    {
            //                        case 0:
            //                            this.__1__state = -1;
            //                            this.__7__wrap5e = this.first.GetEnumerator();
            //                            this.__1__state = 1;
            //                            break;
            //                        case 2:
            //                            this.__1__state = 1;
            //                            break;
            //                    }

            //                    while (this.__7__wrap5e.MoveNext())
            //                    {
            //                        this._element_5__5c = this.__7__wrap5e.Current;

            //                        this.__2__current = this._element_5__5c;
            //                        this.__1__state = 2;

            //                        return true;
            //                    }

            //                    this.__1__state = -1;
            //                    this.__7__wrap5f = this.second.GetEnumerator();
            //                    this.__1__state = 3;
            //                    break;
            //                case 4:
            //                    this.__1__state = 3;
            //                    break;
            //            }

            //            while (this.__7__wrap5f.MoveNext())
            //            {
            //                this._element_5__5d = this.__7__wrap5f.Current;

            //                this.__2__current = this._element_5__5d;
            //                this.__1__state = 4;

            //                return true;
            //            }

            //            return false;
            //    }

            //    throw new ScriptException("Not supported.");
            //}

            public void Reset()
            {
                throw new Exception("The method or operation is not implemented.");
            }

            #endregion
        }


    }
}
