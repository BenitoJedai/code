using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Collections;
using ScriptCoreLib.Shared.BCLImplementation.System.Linq;
using ScriptCoreLib.Shared.BCLImplementation.System.Collections.Generic;
using ScriptCoreLib.Shared.BCLImplementation.System.Collections;

namespace ScriptCoreLibJava.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(global::System.Collections.Generic.List<>))]
    internal class __List<T> :
        __IList<T>,
        __IEnumerable
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
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/04-monese/2014/201402/20140205
            // haha. lets add this. need it for data fill.

            return this.InternalList.indexOf(item);
        }

        public void Insert(int index, T item)
        {
            this.InternalList.add(index, item);
        }

        public void RemoveAt(int index)
        {
            this.InternalList.remove(index);
        }

        public T this[int index]
        {
            get
            {
                return (T)this.InternalList.get(index);
            }
            set
            {
                this.InternalList.set(index, value);
            }
        }

        public void Add(T item)
        {
            this.InternalList.add(item);
        }

        public void Clear()
        {
            this.InternalList.clear();
        }

        public bool Contains(T item)
        {
            return this.InternalList.contains(item);
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
            return this.InternalList.remove(item);
        }

        public List<T>.Enumerator GetEnumerator()
        //public __Enumerator GetEnumerator()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20121101/20121127

            return (List<T>.Enumerator)(object)new __Enumerator(this);
        }

        IEnumerator __IEnumerable.GetEnumerator()
        {
            return new __Enumerator(this);
        }

        IEnumerator<T> __IEnumerable<T>.GetEnumerator()
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
            // http://stackoverflow.com/questions/5061640/make-arraylist-toarray-return-more-specific-types
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

        int __IList<T>.IndexOf(T item)
        {
            throw new NotImplementedException();
        }

        void __IList<T>.Insert(int index, T item)
        {
            this.Insert(0, item);
        }

        void __IList<T>.RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        T __IList<T>.this[int index]
        {
            get
            {
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140111-iquery
                return this[index];
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        void __ICollection<T>.Add(T item)
        {
            this.Add(item);
        }

        void __ICollection<T>.Clear()
        {
            throw new NotImplementedException();
        }

        bool __ICollection<T>.Contains(T item)
        {
            throw new NotImplementedException();
        }

        void __ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        int __ICollection<T>.Count
        {
            get { return this.Count; }
        }

        bool __ICollection<T>.IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        bool __ICollection<T>.Remove(T item)
        {
            return this.Remove(item);
        }


    }
}


