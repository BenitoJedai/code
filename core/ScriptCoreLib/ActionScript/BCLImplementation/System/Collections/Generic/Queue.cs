using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(Queue<>))]
    internal class __Queue<T> : IEnumerable<T>, ICollection, IEnumerable
    {
        readonly Array InternalList = new Array();

        public __Queue() : this(null) { }

        public __Queue(IEnumerable<T> collection)
        {
            if (collection != null)
            {
                foreach (var v in collection)
                {
                    Enqueue(v);
                }
            }
        }

        public int Count { get { return (int)InternalList.length; } }

        public void Clear()
        {
            InternalList.splice(0, (uint)Count);
        }

        public bool Contains(T item)
        {
            return InternalList.indexOf(item) != -1;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public T Dequeue()
        {
            return (T)InternalList.shift();
        }

        public void Enqueue(T item)
        {
            InternalList.push(item);
        }

        public __Enumerator GetEnumerator()
        {
            return new __Enumerator(this);
        }

        private T[] ArrayReference
        {
            get
            {
                return (T[])(object)this.InternalList;
            }
        }

        public T Peek()
        {
            return ArrayReference[0];
        }

        private T[] ArrayReferenceCloned
        {
            get
            {
                return (T[])(object)this.InternalList.slice();
            }
        }


        public T[] ToArray()
        {
            // testme: should return a new array

            return ArrayReferenceCloned;
        }

        public void TrimExcess()
        {
            throw new NotImplementedException();
        }

        // Summary:
        //     Enumerates the elements of a System.Collections.Generic.Queue<T>.
        [Script(Implements = typeof(Queue<>.Enumerator))]
        internal class __Enumerator : IEnumerator<T>, IDisposable, IEnumerator
        {

            IEnumerator<T> value;

			internal __Enumerator() : this(null)
			{

			}

            internal __Enumerator(__Queue<T> e)
            {
				if (e != null)
				{
					//value = InternalSequenceImplementation.AsEnumerable(e.ToArray()).GetEnumerator();
					value = ((IEnumerable<T>)e.ToArray()).GetEnumerator();

				}

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

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion



        #region ICollection Members

        void ICollection.CopyTo(global::System.Array array, int index)
        {
            throw new NotImplementedException();
        }

        int ICollection.Count
        {
            get { return this.Count; }
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

        #region IEnumerable<T> Members

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}
