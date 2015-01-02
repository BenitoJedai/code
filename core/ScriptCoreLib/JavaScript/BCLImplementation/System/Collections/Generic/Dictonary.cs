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
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Collections\Generic\Dictionary.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Collections\Generic\Dictonary.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Collections\Generic\Dictionary.cs
    // http://referencesource.microsoft.com/#mscorlib/system/collections/generic/dictionary.cs
    // https://github.com/Microsoft/referencesource/blob/master/mscorlib/system/collections/generic/dictionary.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Collections.Generic/Dictionary.cs

    [Script(Implements = typeof(global::System.Collections.Generic.Dictionary<,>))]
    internal class __Dictionary<TKey, TValue> :
        __IDictionary<TKey, TValue>
    //, IEnumerable
    {
        // X:\jsc.svn\examples\javascript\Test\Test453IndexInitializer\Test453IndexInitializer\Application.cs
        // http://msdn.microsoft.com/en-us/library/bb531208.aspx
        // http://gigi.nullneuron.net/gigilabs/c-6-preview-index-initializers/

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
                return InternalItems.GetEnumerator();
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
                return InternalItems.GetEnumerator();
            }
        }

        readonly __ValueCollection InternalValues = new __ValueCollection();


        public Dictionary<TKey, TValue>.ValueCollection Values
        {
            get
            {
                return (Dictionary<TKey, TValue>.ValueCollection)(object)this.InternalValues;
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




        #region GetEnumerator


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
        #endregion




        //Error	22	'ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary<TKey,TValue>' 
        //        does not implement interface member 'ScriptCoreLib.Shared.BCLImplementation.System.Collections.__IEnumerable.GetEnumerator()'. 
        //            'ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary<TKey,TValue>.GetEnumerator()' 
        //            cannot implement 'ScriptCoreLib.Shared.BCLImplementation.System.Collections.__IEnumerable.GetEnumerator()' because it does not have the matching return type of 'System.Collections.IEnumerator'.	
        //            X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Collections\Generic\Dictonary.cs	15	20	ScriptCoreLib

        //Error	23	'ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary<TKey,TValue>'
        //            does not implement interface member 'ScriptCoreLib.Shared.BCLImplementation.System.Collections.Generic.__IDictionary<TKey,TValue>.Values'. 
        //                'ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic.__Dictionary<TKey,TValue>.Values' 
        //                cannot implement 'ScriptCoreLib.Shared.BCLImplementation.System.Collections.Generic.__IDictionary<TKey,TValue>.Values' 
        //                because it does not have the matching return type of 'System.Collections.Generic.ICollection<TValue>'.	
        //                X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Collections\Generic\Dictonary.cs	15	20	ScriptCoreLib


        IEnumerator __IEnumerable.GetEnumerator()
        {
            return (Dictionary<TKey, TValue>.Enumerator)(object)new __Enumerator(this);
        }

        public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
        {
            // jsc needs to find the original sig!
            return (Dictionary<TKey, TValue>.Enumerator)(object)new __Enumerator(this);
        }

        #region IEnumerable<KeyValuePair<TKey,TValue>> Members

        IEnumerator<KeyValuePair<TKey, TValue>> __IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return (IEnumerator<KeyValuePair<TKey, TValue>>)(object)new __Enumerator(this);
        }

        #endregion








        ICollection<TValue> __IDictionary<TKey, TValue>.Values
        {
            get { return this.Values; }
        }
    }

}
