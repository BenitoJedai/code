using ScriptCoreLib;
using ScriptCoreLib.Shared;

using global::System.Collections;
using global::System.Collections.Generic;

using IDisposable = global::System.IDisposable;
using System.Linq;
using System;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Linq
{



    [Script]
    internal class __OrderedEnumerable<TSource, TKey> : __OrderedEnumerable<TSource>
    {
        public Func<TSource, TKey> keySelector;
        public IComparer<TKey> comparer;
        public bool descending;


        public __OrderedEnumerable(
            IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer,
            bool descending)
        {
            this.keySelector = keySelector;

            if (comparer == null)
                this.comparer = Comparer<TKey>.Default;
            else
                this.comparer = comparer;


            this.descending = descending;
            this.source = source;
        }

        internal override __OrderedEnumerable<TSource> Clone()
        {
            return new __OrderedEnumerable<TSource, TKey>
            (
                this.source,
                this.keySelector,
                this.comparer,
                this.descending
            );
        }

        internal override int Compare(TSource a, TSource b)
        {
            return comparer.Compare(
                keySelector(a),
                keySelector(b)
            );
        }
    }

    [Script]
    internal abstract class __OrderedEnumerable<TSource> : IEnumerable<TSource>, IOrderedEnumerable<TSource>
    {
        // immutable 

        protected __OrderedEnumerable<TSource> prev;
        protected __OrderedEnumerable<TSource> next;

        protected IEnumerable<TSource> source;

        internal abstract __OrderedEnumerable<TSource> Clone();
        internal abstract int Compare(TSource a, TSource b);

        public IOrderedEnumerable<TSource> CreateOrderedEnumerable<TKey>(Func<TSource, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            var p = new __OrderedEnumerable<TSource, TKey>
            (
                null, // only the lowest has the source
                keySelector,
                comparer,
                descending
            );

            if (comparer == null)
                p.comparer = Comparer<TKey>.Default;
            else
                p.comparer = comparer;

            var _new = (__OrderedEnumerable<TSource>)p;
            var _old = this;

            while (_old != null)
            {
                var x = _old.Clone();
                _new.prev = x;
                x.next = _new;


                // level down
                _old = _old.prev;
                _new = _new.prev;
            }


            // deep clone current and set as parent
            return p;
        }



        public IEnumerator<TSource> GetEnumerator()
        {
            var p = this;

            while (p.prev != null) p = p.prev;

            TSource[] array = p.source.ToArray();

            global::System.Array.Sort(array,
                (a, b) =>
                {
                    int r = 0;
                    var x = p;

                    while (x != null)
                    {
                        r = x.Compare(a, b);

                        if (r != 0)
                            break;

                        x = x.next;
                    }

                    return r;
                }
            );

            return array.AsEnumerable().GetEnumerator();
        }


        global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }

}
