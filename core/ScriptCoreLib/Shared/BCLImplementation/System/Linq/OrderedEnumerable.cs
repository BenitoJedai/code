using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq
{
    // https://github.com/dotnet/corefx/blob/master/src/System.Xml.XDocument/System/Linq/Enumerable.cs

    [Script]
    internal class __OrderedEnumerable<TSource, TKey> : __OrderedEnumerable<TSource>, IOrderedEnumerable<TSource>
    {
        public Func<TSource, TKey> keySelector;
        public IComparer<TKey> comparer;
        public bool descending;

        internal __OrderedEnumerable()
            : this(null, null, null, false)
        {

        }

        public __OrderedEnumerable(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            this.keySelector = keySelector;

            if (comparer == null)
                this.comparer = Comparer<TKey>.Default;
            else
                this.comparer = comparer;


            this.descending = descending;
            this.source = source;
        }

        protected override __OrderedEnumerable<TSource> Clone()
        {
            return new __OrderedEnumerable<TSource, TKey>
            {
                keySelector = this.keySelector,
                comparer = this.comparer,
                descending = this.descending,
                source = this.source
            };
        }

        public override int Compare(TSource a, TSource b)
        {
            // broken this is a workaround
            var comparer = Comparer<TKey>.Default;

            if (descending)
                return comparer.Compare(
                    keySelector(b),
                    keySelector(a)
                );

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

        public __OrderedEnumerable<TSource> prev;
        public __OrderedEnumerable<TSource> next;

        public IEnumerable<TSource> source;

        protected abstract __OrderedEnumerable<TSource> Clone();
        public abstract int Compare(TSource a, TSource b);

        public IOrderedEnumerable<TSource> CreateOrderedEnumerable<TKey>(Func<TSource, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            var p = new __OrderedEnumerable<TSource, TKey>
            {
                keySelector = keySelector,
                comparer = comparer,
                descending = descending,
                source = null // only the lowest has the source
            };

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
            // X:\jsc.svn\examples\javascript\test\TestOrderedEnumerable1\TestOrderedEnumerable1\Application.cs

            var p = this;

            while (p.prev != null) p = p.prev;



            TSource[] array = p.source.ToArray();

            Array.Sort(array,
                (a, b) =>
                {
                    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201502/20150222
                    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150111/redux
                    // X:\jsc.svn\examples\javascript\Test\Test453WhileBreak\Test453WhileBreak\Program.cs

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

            var enumerable = array.AsEnumerable();
            var enumerator = enumerable.GetEnumerator();
            return enumerator;
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }

}
