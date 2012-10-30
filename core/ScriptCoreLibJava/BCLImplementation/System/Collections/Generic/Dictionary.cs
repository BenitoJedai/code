using ScriptCoreLib;
using ScriptCoreLib.Shared.BCLImplementation.System.Collections.Generic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLibJava.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(global::System.Collections.Generic.Dictionary<,>))]
    internal class __Dictionary<TKey, TValue> : 
        __IDictionary<TKey, TValue>
    {
        public java.util.HashMap InternalDictionary = new java.util.HashMap();

        public __Dictionary()
            : this(null)
        {

        }

        public __Dictionary(IEqualityComparer<TKey> comparer)
        {

        }

        #region Keys
        [Script(Implements = typeof(global::System.Collections.Generic.Dictionary<,>.KeyCollection))]
        public class __KeyCollection : ICollection<TKey>, ICollection
        {

            public void Add(TKey item)
            {
                throw new NotImplementedException();
            }

            public void Clear()
            {
                throw new NotImplementedException();
            }

            public bool Contains(TKey item)
            {
                throw new NotImplementedException();
            }

            public void CopyTo(TKey[] array, int arrayIndex)
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

            public bool Remove(TKey item)
            {
                throw new NotImplementedException();
            }

            public IEnumerator<TKey> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }

            void ICollection.CopyTo(Array array, int index)
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
        }

        public __KeyCollection Keys
        {
            get
            {
                return new __KeyCollection { };
            }
        }


        ICollection<TKey> __IDictionary<TKey, TValue>.Keys
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        public ICollection<TValue> Values
        {
            get { throw new NotImplementedException(); }
        }

        public TValue this[TKey key]
        {
            get
            {
                return (TValue)this.InternalDictionary.get(key);
            }
            set
            {
                this.InternalDictionary.put(key, value);
            }
        }

        public void Add(TKey key, TValue value)
        {
            this[key] = value;
        }

        public bool ContainsKey(TKey key)
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

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }



        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        global::System.Collections.IEnumerator ScriptCoreLib.Shared.BCLImplementation.System.Collections.__IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }


        public bool Remove(TKey key)
        {
            throw new NotImplementedException();
        }

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20121001-solutionbuilderv1/20121024-linq



        ICollection<TValue> __IDictionary<TKey, TValue>.Values
        {
            get { throw new NotImplementedException(); }
        }

        TValue __IDictionary<TKey, TValue>.this[TKey key]
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

        void __IDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            throw new NotImplementedException();
        }

        bool __IDictionary<TKey, TValue>.ContainsKey(TKey key)
        {
            throw new NotImplementedException();
        }

        bool __IDictionary<TKey, TValue>.Remove(TKey key)
        {
            throw new NotImplementedException();
        }

        int __ICollection<KeyValuePair<TKey, TValue>>.Count
        {
            get { throw new NotImplementedException(); }
        }

        bool __ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        void __ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        void __ICollection<KeyValuePair<TKey, TValue>>.Clear()
        {
            throw new NotImplementedException();
        }

        bool __ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        void __ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        bool __ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        IEnumerator<KeyValuePair<TKey, TValue>> __IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
