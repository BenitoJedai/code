using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Collections;

namespace ScriptCoreLibJava.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(global::System.Collections.Generic.List<>))]
    internal class __List<T> : IList<T>, IEnumerable
    {
        readonly global::java.util.ArrayList<T> InternalList = new global::java.util.ArrayList<T>();

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


       

        public T[] ToArray()
        {
            __List_ToArray_ldtoken<T> __ldtoken;

            var __typeof_T_wrapper = typeof(__List_ToArray_ldtoken<T>);
            var __typeof_T = __typeof_T_wrapper.GetFields()[0].FieldType;

            //var e = Array.CreateInstance(typeof(T), 0);
            var e = Array.CreateInstance(__typeof_T, 0);

            return this.InternalList.toArray((T[])e);
        }
    }


    class __List_ToArray_ldtoken<_T>
    {
        public _T value;
    }
}
