using ScriptCoreLib;
using ScriptCoreLib.Shared;

using global::System.Collections;
using global::System.Collections.Generic;

using IDisposable = global::System.IDisposable;
using ScriptCoreLib.Shared.Query;
using ScriptCoreLib.Shared.BCLImplementation.System.Linq;

namespace ScriptCoreLib.Shared.Query
{

	internal static partial class __Enumerable
    {

        public static IEnumerable<TSource> Take<TSource>(this IEnumerable<TSource> source, int count)
        {
            if (source == null)
            {
				throw __DefinedError.ArgumentNull("source");
            }
            return TakeIterator<TSource>(source, count);
        }






        [Script]
        private sealed partial class _TakeIterator_d__40<TSource> : IEnumerable<TSource>, IEnumerable, IEnumerator<TSource>, IEnumerator, IDisposable
        {
            // Fields
            private int __1__state;
            private TSource __2__current;
            public int __3__count;
            public IEnumerable<TSource> __3__source;
            public IEnumerator<TSource> __7__wrap42;
            public TSource _element_5__41;
            public int count;
            public IEnumerable<TSource> source;


            public _TakeIterator_d__40(int __1__state)
            {
                this.__1__state = __1__state;
                return;
            }






            #region IEnumerable<int> Members

            public IEnumerator<TSource> GetEnumerator()
            {

                _TakeIterator_d__40<TSource> _ret = null;

                if (this.__1__state == -2)
                {
                    this.__1__state = 0;
                    _ret = this;
                }
                else
                {
                    _ret = new _TakeIterator_d__40<TSource>(0);
                }



                _ret.source = this.__3__source;
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

            public TSource Current
            {
                get { return this.__2__current; }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                __m__Finally43();
            }

            #endregion

            #region IEnumerator Members

            object IEnumerator.Current
            {
                get { return this.Current; }
            }



            public void Reset()
            {
				throw __DefinedError.NotImplemented();
            }

            #endregion
        }


        private static IEnumerable<TSource> TakeIterator<TSource>(IEnumerable<TSource> source, int count)
        {
            return new _TakeIterator_d__40<TSource>(-2) { __3__source = source, __3__count = count };
        }









    }
}
