using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic
{
    using ScriptCoreLib;
    using ScriptCoreLib.JavaScript.DOM;
    using ScriptCoreLib.JavaScript.Runtime;
    using ScriptCoreLib.JavaScript.Query;

    [Script(Implements = typeof(global::System.Collections.Generic.List<>))]
    internal class __List<T> : IList<T>, IEnumerable
    {
        IArray<T> _items = new IArray<T>();

        public __List()
        {

        }

        public __List(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new global::System.Exception("collection is null");

            foreach (T v in InternalSequenceImplementation.AsEnumerable(collection))
            {
                this.Add(v);
            }
        }



        public T[] ToArray()
        {
            return _items.ToArray();
        }



        [Script(Implements = typeof(global::System.Collections.Generic.List<>.Enumerator))]
        public class __Enumerator : IEnumerator<T>, IDisposable, IEnumerator
        {
            IEnumerator<T> value;

            internal __Enumerator(__List<T> list)
            {
                value = InternalSequenceImplementation.AsEnumerable(list.ToArray()).GetEnumerator();


            }


            #region IEnumerator<T> Members

            public T Current
            {
                get { return value.Current; }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                value.Dispose();
            }

            #endregion

            #region IEnumerator Members

            object IEnumerator.Current
            {
                get { return value.Current; }
            }

            public bool MoveNext()
            {
                return value.MoveNext();
            }

            public void Reset()
            {
                value.Reset();
            }

            #endregion
        }






        #region IList<T> Members

        public int IndexOf(T item)
        {
            var j = -1;

            for (int i = 0; i < Count; i++)
            {
                if (Expando.ReferenceEquals(this[i], item))
                {
                    j = i;
                    break;
                }
            }

            return j;
        }

        public void Insert(int index, T item)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void RemoveAt(int index)
        {
            if (index >= this.Count)
            {
                throw new Exception("ArgumentOutOfRangeException");
            }

            _items.splice(index, 1);
        }


        public T this[int index]
        {
            get
            {
                if (index >= this.Count)
                {
                    throw new Exception("ArgumentOutOfRangeException");
                }
                return this._items[index];
            }
            set
            {
                if (index >= this.Count)
                {
                    throw new Exception("ArgumentOutOfRangeException");
                }
                this._items[index] = value;
            }
        }

        #endregion

        #region ICollection<T> Members

        public void Add(T item)
        {
            _items.push(item);
        }

        public void Clear()
        {
            _items.splice(0, Count);
        }

        public bool Contains(T item)
        {
            bool j = false;

            for (int i = 0; i < Count; i++)
            {
                if (Expando.ReferenceEquals(this[i], item))
                {
                    j = true;
                    break;
                }
            }

            return j;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Count
        {
            get { return _items.length; }
        }

        public bool IsReadOnly
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public bool Remove(T item)
        {
            var i = IndexOf(item);

            if (i == -1)
                return false;
            RemoveAt(i);

            return true;
        }

        #endregion

        public __Enumerator GetEnumerator()
        {
            return new __Enumerator(this);
        }






        #region IEnumerable<T> Members

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members


        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }

}
