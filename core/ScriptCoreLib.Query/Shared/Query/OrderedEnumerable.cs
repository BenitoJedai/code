using ScriptCoreLib;
using ScriptCoreLib.Shared;

using global::System.Collections;
using global::System.Collections.Generic;

using IDisposable = global::System.IDisposable;
using System.Linq;
using System;

namespace ScriptCoreLib.Shared.Query
{
    // todo: if all languages share this functionality
    // move it to Shared namespace

    [Script]
    internal abstract class OrderedEnumerable<TElement> : IOrderedEnumerable<TElement>, IEnumerable<TElement>, IEnumerable
    {
        internal IEnumerable<TElement> source;

        #region IOrderedEnumerable<TElement> Members

        IOrderedEnumerable<TElement> IOrderedEnumerable<TElement>.CreateOrderedEnumerable<TKey>(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            return new OrderedEnumerable<TElement, TKey>(this.source, keySelector, comparer, descending) { parent = this };
        }

 

 

        #endregion

        #region IEnumerable<TElement> Members

        public IEnumerator<TElement> GetEnumerator()
        {
            
            return source.GetEnumerator();

            //throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
    
    [Script]
    internal class OrderedEnumerable<TElement, TKey> : OrderedEnumerable<TElement>
    {
        // Fields
        internal IComparer<TKey> comparer;
        internal bool descending;
        internal Func<TElement, TKey> keySelector;
        internal OrderedEnumerable<TElement> parent;


        internal OrderedEnumerable(IEnumerable<TElement> source, Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            if (source == null)
            {
                throw new NullReferenceException("source");
            }
            if (keySelector == null)
            {
                throw new NullReferenceException("keySelector");
            }
            base.source = source;
            this.parent = null;
            this.keySelector = keySelector;
            //this.comparer = (comparer != null) ? comparer : ((IComparer<TKey>)Comparer<TKey>.Default);
            this.comparer = comparer;
            this.descending = descending;
        }

 

 



    }

}
