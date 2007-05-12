using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic
{
    using ScriptCoreLib.JavaScript.Runtime;

    [Script(Implements = typeof(global::System.Collections.Generic.Dictionary<,>))]
    internal class __Dictionary<TKey, TValue> : IDictionary<TKey, TValue>, IEnumerable
    {
        Expando list = new Expando();

        public __Dictionary()
        {

        }

        public __Dictionary(IEqualityComparer<TKey> comparer)
        {

        }

        #region IDictionary<TKey,TValue> Members

        public void Add(TKey key, TValue value)
        {
            if (list.Contains(key))
                throw new global::System.Exception("Argument_AddingDuplicate");

            list.SetMember(key, value);
        }

        public bool ContainsKey(TKey key)
        {
            return list.Contains(key);
        }

        public ICollection<TKey> Keys
        {
            get { throw new global::System.Exception("The method or operation is not implemented."); }
        }

        public bool Remove(TKey key)
        {
            if (!list.Contains(key))
                return false;

            
            list.Remove(key);
            
            return true;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            throw new global::System.Exception("The method or operation is not implemented.");
        }

        public ICollection<TValue> Values
        {
            get { throw new global::System.Exception("The method or operation is not implemented."); }
        }

        public TValue this[TKey key]
        {
            get
            {
                if (this.ContainsKey(key))
                    return list.GetMember<TValue>(key);

                throw new Exception("Not found.");
            }
            set
            {
                list.SetMember(key, value);
            }
        }

        #endregion

        #region ICollection<KeyValuePair<TKey,TValue>> Members

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            this.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            list.RemoveAll();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new global::System.Exception("The method or operation is not implemented.");
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new global::System.Exception("The method or operation is not implemented.");
        }

        public int Count
        {
            get { throw new global::System.Exception("The method or operation is not implemented."); }
        }

        public bool IsReadOnly
        {
            get { throw new global::System.Exception("The method or operation is not implemented."); }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new global::System.Exception("The method or operation is not implemented.");
        }

        #endregion

        #region IEnumerable<KeyValuePair<TKey,TValue>> Members

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            throw new global::System.Exception("The method or operation is not implemented.");
        }

        #endregion

        #region IEnumerable Members


        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new global::System.Exception("The method or operation is not implemented.");
        }

        #endregion
    }

}
