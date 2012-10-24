using ScriptCoreLib;
using ScriptCoreLib.Shared.BCLImplementation.System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLibJava.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(global::System.Collections.Generic.Dictionary<,>))]
    internal class __Dictionary<TKey, TValue> : __IDictionary<TKey, TValue>
    {
        public java.util.HashMap InternalDictionary = new java.util.HashMap();

        public __Dictionary()
            : this(null)
        {

        }

        public __Dictionary(IEqualityComparer<TKey> comparer)
        {

        }


        public ICollection<TKey> Keys
        {
            get { throw new NotImplementedException(); }
        }

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
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }
    }
}
