﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Collections;
using ScriptCoreLib.Shared.BCLImplementation.System.Linq;

namespace ScriptCoreLibJava.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(global::System.Collections.Generic.List<>))]
    internal class __List<T> : IList<T>, IEnumerable
    {
        readonly global::java.util.ArrayList<T> InternalList = new global::java.util.ArrayList<T>();

        public __List()
        {

        }

        public __List(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            this.AddRange(collection);
        }

        public void AddRange(IEnumerable<T> collection)
        {
            foreach (T v in collection)
            {
                this.Add(v);
            }
        }

        public int IndexOf(T item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public T this[int index]
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

        public void Add(T item)
        {
            this.InternalList.add(item);
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get
            {
                return InternalList.size();
            }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public List<T>.Enumerator GetEnumerator()
        //public __Enumerator GetEnumerator()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20121101/20121127

            return (List<T>.Enumerator)(object)new __Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new __Enumerator(this);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new __Enumerator(this);
        }


        public void Reverse()
        {
            var clone = this.ToArray();

            for (int i = 0; i < clone.Length; i++)
            {
                this[clone.Length - 1 - i] = clone[i];
            }


        }
        public T[] ToArray()
        {
            return this.InternalList.toArray(new T[0]);
        }




        [Script(Implements = typeof(global::System.Collections.Generic.List<>.Enumerator))]
        public class __Enumerator : IEnumerator<T>, IDisposable, IEnumerator
        {
            IEnumerator<T> value;

            internal __Enumerator()
                : this(null)
            {

            }

            internal __Enumerator(__List<T> list)
            {
                if (list == null)
                    return;

                value = __Enumerable_AsEnumerable.AsEnumerable(list.ToArray()).GetEnumerator();


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

        int IList<T>.IndexOf(T item)
        {
            throw new NotImplementedException();
        }

        void IList<T>.Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        void IList<T>.RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        T IList<T>.this[int index]
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

        void ICollection<T>.Add(T item)
        {
            throw new NotImplementedException();
        }

        void ICollection<T>.Clear()
        {
            throw new NotImplementedException();
        }

        bool ICollection<T>.Contains(T item)
        {
            throw new NotImplementedException();
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        int ICollection<T>.Count
        {
            get { throw new NotImplementedException(); }
        }

        bool ICollection<T>.IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        bool ICollection<T>.Remove(T item)
        {
            throw new NotImplementedException();
        }

     
    }
}


