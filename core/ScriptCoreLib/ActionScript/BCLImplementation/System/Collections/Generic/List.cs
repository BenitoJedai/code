using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using ScriptCoreLib.ActionScript.BCLImplementation.Query;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(global::System.Collections.Generic.List<>))]
    public class __List<T> : IList<T>, ICollection<T>
    {
        public Array _items = new Array();

        public __List() : this(null)
        {

        }

        public __List(IEnumerable<T> collection)
        {
            // cannot have this check as the default ctor will pass null anyway
            //if (collection == null)
            //    throw new global::System.Exception("collection is null");

            if (collection != null)
                this.AddRange(collection);
        }


        public void Add(T item)
        {
            _items.push(item);
        }

        public void AddRange(IEnumerable<T> collection)
        {
            foreach (T v in collection)
            {
                this.Add(v);
            }
        }

        public void Clear()
        {
            _items.splice(0, (uint)Count);
        }

        public int Count
        {
            get { return (int)_items.length; }
        }

        #region IList<T> Members

        public int IndexOf(T item)
        {
            throw new NotImplementedException("");
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException("");
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException("");
        }

        public T this[int index]
        {
            get
            {
                throw new NotImplementedException("");
            }
            set
            {
                throw new NotImplementedException("");
            }
        }

        #endregion

        #region ICollection<T> Members


        public bool Contains(T item)
        {
            throw new NotImplementedException("");
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException("");
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(""); }
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException("");
        }

        #endregion

        #region IEnumerable<T> Members

        public __Enumerator GetEnumerator()
        {
            return new __Enumerator(this);
        }

        #endregion

        #region IEnumerable Members

        global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public T[] ToArray()
        {
            return (T[])(object)this._items;
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



        #region IEnumerable<T> Members

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}
