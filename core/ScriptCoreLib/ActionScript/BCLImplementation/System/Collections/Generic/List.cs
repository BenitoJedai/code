using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(global::System.Collections.Generic.List<>))]
    internal class __List<T> : IList<T>, ICollection<T>, IEnumerable<T>, IList, ICollection, IEnumerable
    {
        public Array _items = new Array();

        public __List()
            : this(null)
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

        public void ForEach(Action<T> action)
        {
            foreach (var e in this)
            {
                action(e);
            }
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
            var j = -1;

            for (int i = 0; i < Count; i++)
            {
                if (Object.ReferenceEquals(this[i], item))
                {
                    j = i;
                    break;
                }
            }

            return j;
        }

        public void Insert(int index, T item)
        {
			this._items.splice(index, 0, item);
        }

        public void RemoveAt(int index)
        {
            _items.splice(index, 1);
        }

        public T this[int index]
        {
            get
            {
                return ArrayReference[index];
            }
            set
            {
                ArrayReference[index] = value;
            }
        }

        #endregion

        #region ICollection<T> Members


        public bool Contains(T item)
        {
            return this._items.indexOf(item) != -1;
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
            var i = IndexOf(item);

            if (i < 0)
                return false;

            RemoveAt(i);

            return true;
        }

        public int RemoveAll(Predicate<T> filter)
        {
            var c = 0;

            for (int i = Count - 1; i >= 0; i--)
            {
                if (filter(this[i]))
                    this._items.splice(i, 1);
                else
                    c++;
            }


            return c;
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

        private T[] ArrayReference
        {
            get
            {
                return (T[])(object)this._items;
            }
        }

        private T[] ArrayReferenceCloned
        {
            get
            {
                return (T[])(object)this._items.slice();
            }
        }


        public T[] ToArray()
        {
            // testme: should return a new array

            return ArrayReferenceCloned;
        }


        [Script(Implements = typeof(global::System.Collections.Generic.List<>.Enumerator))]
        public class __Enumerator : IEnumerator<T>, IDisposable, IEnumerator
        {
            IEnumerator<T> value;

            internal __Enumerator() : this(null) { }
            internal __Enumerator(__List<T> list)
            {
                if (list == null)
                    return;

                //value = InternalSequenceImplementation.AsEnumerable(list.ToArray()).GetEnumerator();


                value = ((IEnumerable<T>)list.ToArray()).GetEnumerator();

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

        #region IList Members

        int IList.Add(object value)
        {
            throw new NotImplementedException();
        }

        void IList.Clear()
        {
            throw new NotImplementedException();
        }

        bool IList.Contains(object value)
        {
            throw new NotImplementedException();
        }

        int IList.IndexOf(object value)
        {
            throw new NotImplementedException();
        }

        void IList.Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        bool IList.IsFixedSize
        {
            get { throw new NotImplementedException(); }
        }

        bool IList.IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        void IList.Remove(object value)
        {
            throw new NotImplementedException();
        }

        void IList.RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        object IList.this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region ICollection Members

        void ICollection.CopyTo(global::System.Array array, int index)
        {
            throw new NotImplementedException();
        }

        int ICollection.Count
        {
            get { throw new NotImplementedException(); }
        }

        bool ICollection.IsSynchronized
        {
            get { throw new NotImplementedException(); }
        }

        object ICollection.SyncRoot
        {
            get { throw new NotImplementedException(); }
        }

        #endregion


		public void Reverse()
		{
			var clone = this.ToArray();

			for (int i = 0; i < clone.Length; i++)
			{
				this[clone.Length - 1 - i] = clone[i];
			}

			
		}
	}
}
