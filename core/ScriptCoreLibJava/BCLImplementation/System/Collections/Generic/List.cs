using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Collections;
using ScriptCoreLib.Shared.Query;

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
            foreach (T v in InternalSequence.AsEnumerable(collection))
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
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
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
    }
}


