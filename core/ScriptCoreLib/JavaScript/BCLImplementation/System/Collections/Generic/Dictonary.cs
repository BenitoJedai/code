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
            get { 
            
                 global::System.Collections.Generic.List<TKey> a = new  global::System.Collections.Generic.List<TKey>();

                foreach (var v in list.GetMemberNames())
                {
                    a.Add((TKey)v);
                } 
                
                return a;
            }
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
            get
            {

                throw new global::System.Exception("The method or operation is not implemented.");
            }
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
            return this.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members


        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        public __Dictionary<TKey, TValue>.__Enumerator GetEnumerator()
        {
            return new __Enumerator(this);
        }

        [Script(ImplementationType = typeof(global::System.Collections.Generic.Dictionary<,>.Enumerator))]
        public class __Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator
        {
            IEnumerator<KeyValuePair<TKey, TValue>> list;

            public __Enumerator(__Dictionary<TKey, TValue> e)
            {
                global::System.Collections.Generic.List<KeyValuePair<TKey, TValue>> a = new global::System.Collections.Generic.List<KeyValuePair<TKey, TValue>>();

                foreach (var v in e.Keys)
                {
                    a.Add(new KeyValuePair<TKey, TValue>(v, e[v]));
                }

                this.list = a.GetEnumerator();
            }

            public KeyValuePair<TKey, TValue> Current { get { return list.Current; } }

            public void Dispose()
            {
                list.Dispose();
            }

            public bool MoveNext()
            {
                return list.MoveNext();
            }



            #region IEnumerator Members

            object IEnumerator.Current
            {
                get { return this.Current; }
            }

            public void Reset()
            {
                throw new Exception("The method or operation is not implemented.");
            }

            #endregion
        }
    }

}
