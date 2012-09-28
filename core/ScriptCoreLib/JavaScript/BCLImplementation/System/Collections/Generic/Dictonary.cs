using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic
{
    using ScriptCoreLib.JavaScript.Runtime;
    using ScriptCoreLib.Shared.BCLImplementation.System;
    using ScriptCoreLib.Shared.BCLImplementation.System.Collections;
    using ScriptCoreLib.Shared.BCLImplementation.System.Collections.Generic;

    [Script(Implements = typeof(global::System.Collections.Generic.Dictionary<,>))]
    internal class __Dictionary<TKey, TValue> : IDictionary<TKey, TValue>, IEnumerable
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


            _keys.Add(key);
            _values.Add(value);
        }



        public bool ContainsKey(TKey key)
        {
            return _keys.Contains(key);
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

        readonly __KeyCollection _keys = new __KeyCollection();

        public __KeyCollection Keys
        {
            get
            {
                return _keys;
            }
        }

  
        #endregion


        public bool Remove(TKey key)
        {
            if (!ContainsKey(key))
                return false;

            var i = _keys.InternalItems.IndexOf(key);

            _keys.InternalItems.RemoveAt(i);
            _values.InternalItems.RemoveAt(i);

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

        readonly __ValueCollection _values = new __ValueCollection();


        public __ValueCollection Values
        {
            get
            {
                return this._values;
            }
        }

 
        #endregion


        public TValue this[TKey key]
        {
            get
            {
                var i = _keys.InternalItems.IndexOf(key);

                if (i == -1)
                    throw new Exception("Not found.");

                return _values.InternalItems[i];


            }
            set
            {
                var i = _keys.InternalItems.IndexOf(key);

                if (i == -1)
                {
                    _keys.Add(key);
                    _values.Add(value);
                }
                else
                {
                    _values.InternalItems[i] = value;
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
            _keys.Clear();
            _values.Clear();
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
            get { return _keys.Count; }
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




        ICollection<TKey> IDictionary<TKey, TValue>.Keys
        {
            get { throw new NotImplementedException(); }
        }

        ICollection<TValue> IDictionary<TKey, TValue>.Values
        {
            get { throw new NotImplementedException(); }
        }
    }

}
