using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLibAppJet.JavaScript.DOM;
using ScriptCoreLib;
using ScriptCoreLibAppJet.JavaScript.Runtime;
using ScriptCoreLibAppJet.JavaScript.Query;

namespace ScriptCoreLibAppJet.JavaScript.BCLImplementation.System.Collections.Generic
{


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

            this.AddRange(collection);
        }


		private T[] ArrayReferenceCloned
		{
			get
			{
				return (T[])(object)this._items.slice(0);
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
            this._items.splice(index, 0, item);
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

        public void ForEach(Action<T> action)
        {
            if (action == null)
            {
                throw new Exception("ArgumentOutOfRangeException");
            }
            for (int i = 0; i < this.Count; i++)
            {
                action(this._items[i]);
            }
        }

 


        #region ICollection<T> Members

        public void Add(T item)
        {
            _items.push(item);
        }

        public void AddRange(IEnumerable<T> collection)
        {
            foreach (T v in InternalSequenceImplementation.AsEnumerable(collection))
            {
                this.Add(v);
            }
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


        public int RemoveAll(global::System.Predicate<T> a)
        {
            var x = 0;

            for (int i = 0; i < Count; i++)
            {
                if (a(this[i]))
                {
                    RemoveAt(x);

                    x--;
                }

                x++;
            }

            return x;
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
