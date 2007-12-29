using ScriptCoreLib;
using ScriptCoreLib.Shared;

using global::System.Collections;
using global::System.Collections.Generic;

using IDisposable = global::System.IDisposable;
using System.Linq;
using System;

namespace ScriptCoreLib.Shared.Query
{



    [Script]
    public class OrderedEnumerable<TSource, TKey> : OrderedEnumerable<TSource>
    {
        public Func<TSource, TKey> keySelector;
        public IComparer<TKey> comparer;
        public bool descending;

        internal OrderedEnumerable()
        {

        }

        public OrderedEnumerable(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            this.keySelector = keySelector;

            if (comparer == null)
                this.comparer = LocalInternalEnumerable.GetDefaultComparer<TKey>();
            else
                this.comparer = comparer;


            this.descending = descending;
            this.source = source;
        }

        protected override OrderedEnumerable<TSource> Clone()
        {
            return new OrderedEnumerable<TSource, TKey>
            {
                keySelector = this.keySelector,
                comparer = this.comparer,
                descending = this.descending,
                source = this.source
            };
        }

        protected override int Compare(TSource a, TSource b)
        {
            return comparer.Compare(
                keySelector(a),
                keySelector(b)
            );
        }
    }

    [Script]
    public abstract class OrderedEnumerable<TSource> : IEnumerable<TSource>, IOrderedEnumerable<TSource>
    {
        // immutable 

        protected OrderedEnumerable<TSource> prev;
        protected OrderedEnumerable<TSource> next;

        protected IEnumerable<TSource> source;

        protected abstract OrderedEnumerable<TSource> Clone();
        protected abstract int Compare(TSource a, TSource b);

        public IOrderedEnumerable<TSource> CreateOrderedEnumerable<TKey>(Func<TSource, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            var p = new OrderedEnumerable<TSource, TKey>
            {
                keySelector = keySelector,
                comparer = comparer,
                descending = descending,
                source = null // only the lowest has the source
            };

            if (comparer == null)
                p.comparer = LocalInternalEnumerable.GetDefaultComparer<TKey>();
            else
                p.comparer = comparer;

            var _new = (OrderedEnumerable<TSource>)p;
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

            Array.Sort(array,
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


        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }

}
