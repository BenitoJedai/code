using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.BCLImplementation.System;
using ScriptCoreLib.Shared.BCLImplementation.System.Collections;
using ScriptCoreLib.Shared.BCLImplementation.System.Collections.Generic;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic
{


    [Script(Implements = typeof(global::System.Collections.Generic.Dictionary<,>))]
    internal class __Dictionary<TKey, TValue> :
        __IDictionary<TKey, TValue>
    //, IEnumerable
    {

        //Expando list = new Expando();

        public __Dictionary()
            : this(null)
        {

        }

        public __Dictionary(IEqualityComparer<TKey> comparer)
        {

        }

        #region IDictionary<TKey,TValue> Members

        public void Add(TKey key, TValue value)
        {
            //if (list.Contains(key))
            if (this.ContainsKey(key))
                throw new global::System.Exception("Argument_AddingDuplicate");


            InternalKeys.Add(key);
            InternalValues.Add(value);
        }



        public bool ContainsKey(TKey key)
        {
            return InternalKeys.Contains(key);
        }

        #region Keys
        [Script(Implements = typeof(global::System.Collections.Generic.Dictionary<,>.KeyCollection))]
        public class __KeyCollection : ICollection<TKey>
        {
            public List<TKey> InternalItems = new List<TKey>();

            public void Add(TKey item)
            {
                InternalItems.Add(item);
            }

            public void Clear()
            {
                InternalItems.Clear();
            }

            public bool Contains(TKey item)
            {
                return InternalItems.Contains(item);
            }

            public void CopyTo(TKey[] array, int arrayIndex)
            {
                InternalItems.CopyTo(array, arrayIndex);
            }

            public int Count
            {
                get
                {
                    return InternalItems.Count;
                }
            }

            public bool IsReadOnly
            {
                get
                {
                    return false;
                }
            }

            public bool Remove(TKey item)
            {
                return InternalItems.Remove(item);
            }

            public IEnumerator<TKey> GetEnumerator()
            {
                return InternalItems.GetEnumerator();
            }



            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }

        readonly __KeyCollection InternalKeys = new __KeyCollection();

        public __KeyCollection Keys
        {
            get
            {
                return InternalKeys;
            }
        }

        ICollection<TKey> __IDictionary<TKey, TValue>.Keys
        {
            get { return Keys; }
        }
        #endregion


        public bool Remove(TKey key)
        {
            if (!ContainsKey(key))
                return false;

            var i = InternalKeys.InternalItems.IndexOf(key);

            InternalKeys.InternalItems.RemoveAt(i);
            InternalValues.InternalItems.RemoveAt(i);

            return true;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            throw new global::System.Exception("The method or operation is not implemented.");
        }

        #region Values
        [Script(Implements = typeof(global::System.Collections.Generic.Dictionary<,>.ValueCollection))]
        public class __ValueCollection : ICollection<TValue>
        {
            public List<TValue> InternalItems = new List<TValue>();

            public void Add(TValue item)
            {
                InternalItems.Add(item);
            }

            public void Clear()
            {
                InternalItems.Clear();
            }

            public bool Contains(TValue item)
            {
                return InternalItems.Contains(item);
            }

            public void CopyTo(TValue[] array, int arrayIndex)
            {
                InternalItems.CopyTo(array, arrayIndex);
            }

            public int Count
            {
                get
                {
                    return InternalItems.Count;
                }
            }

            public bool IsReadOnly
            {
                get
                {
                    return false;
                }
            }

            public bool Remove(TValue item)
            {
                return InternalItems.Remove(item);
            }

            public IEnumerator<TValue> GetEnumerator()
            {
                return InternalItems.GetEnumerator();
            }



            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }

        readonly __ValueCollection InternalValues = new __ValueCollection();


        public __ValueCollection Values
        {
            get
            {
                return this.InternalValues;
            }
        }


        #endregion


        public TValue this[TKey key]
        {
            get
            {
                var i = InternalKeys.InternalItems.IndexOf(key);

                if (i == -1)
                    throw new Exception("Not found.");

                return InternalValues.InternalItems[i];


            }
            set
            {
                var i = InternalKeys.InternalItems.IndexOf(key);

                if (i == -1)
                {
                    InternalKeys.Add(key);
                    InternalValues.Add(value);
                }
                else
                {
                    InternalValues.InternalItems[i] = value;
                }
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
            InternalKeys.Clear();
            InternalValues.Clear();
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
            get { return InternalKeys.Count; }
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

     

     

        public __Dictionary<TKey, TValue>.__Enumerator GetEnumerator()
        {
            return new __Enumerator(this);
        }

        [Script(Implements = typeof(global::System.Collections.Generic.Dictionary<,>.Enumerator))]
        public class __Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator
        {
            IEnumerator<KeyValuePair<TKey, TValue>> list;

            public __Enumerator() : this(null) { }

            public __Enumerator(__Dictionary<TKey, TValue> e)
            {
                if (e == null)
                    return;

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

        IEnumerator __IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

}
