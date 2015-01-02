using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using ScriptCoreLib.Shared.BCLImplementation.System.Collections.Generic;
using ScriptCoreLib.Shared.BCLImplementation.System.Collections;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Collections.Generic
{
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Collections\Generic\Dictionary.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Collections\Generic\Dictonary.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Collections\Generic\Dictionary.cs
    // http://referencesource.microsoft.com/#mscorlib/system/collections/generic/dictionary.cs
    // https://github.com/Microsoft/referencesource/blob/master/mscorlib/system/collections/generic/dictionary.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Collections.Generic/Dictionary.cs

    [Script(Implements = typeof(Dictionary<,>))]
    internal class __Dictionary<TKey, TValue> : __IDictionary<TKey, TValue> //, __IEnumerable, __ICollection
    {
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

            // http://twistedoakstudios.com/blog/Post925_unfathomable-bugs-4-keys-that-arent
            _keys.Add(key);
            _values.Add(value);
        }



        public bool ContainsKey(TKey key)
        {
            return _keys.Contains(key);
        }

        public bool ContainsValue(TValue value)
        {
            return _values.Contains(value);
        }

        #region Keys
        [Script(Implements = typeof(global::System.Collections.Generic.Dictionary<,>.KeyCollection)
            //, IsDebugCode = true
            )]
        public class __KeyCollection : List<TKey>
        {
        }

        readonly __KeyCollection _keys = new __KeyCollection();

        public __KeyCollection Keys
        {
            get
            {
                return _keys;
            }
        }

        ICollection<TKey> __IDictionary<TKey, TValue>.Keys
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

            var i = _keys.IndexOf(key);

            _keys.RemoveAt(i);
            _values.RemoveAt(i);

            return true;
        }


        public bool TryGetValue(TKey key, out TValue value)
        {
            throw new global::System.Exception("The method or operation is not implemented.");
        }


        #region Values
        [Script(Implements = typeof(global::System.Collections.Generic.Dictionary<,>.ValueCollection)
            //, IsDebugCode = true
            )]
        public class __ValueCollection : List<TValue>
        {

        }

        readonly __ValueCollection _values = new __ValueCollection();


        public __ValueCollection Values
        {
            get
            {
                return _values;
            }
        }

        ICollection<TValue> __IDictionary<TKey, TValue>.Values
        {
            get
            {
                return _values;
            }
        }
        #endregion

        public TValue this[TKey key]
        {
            get
            {
                var i = _keys.IndexOf(key);

                if (i == -1)
                    throw new Exception("Not found.");

                return _values[i];


            }
            set
            {
                var i = _keys.IndexOf(key);

                if (i == -1)
                {
                    _keys.Add(key);
                    _values.Add(value);
                }
                else
                {
                    _values[i] = value;
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


        #region IEnumerable Members


        //IEnumerator __IEnumerable.GetEnumerator()
        //{
        //    return (IEnumerator)(object)this.GetEnumerator();
        //}

        #endregion

        #region __Enumerator
        //public __Dictionary<TKey, TValue>.__Enumerator GetEnumerator()
        //{
        //    return new __Enumerator(this);
        //}

        [Script(Implements = typeof(global::System.Collections.Generic.Dictionary<,>.Enumerator)
            //,IsDebugCode = true
            )]
        public class __Enumerator : __IEnumerator<KeyValuePair<TKey, TValue>>, __IEnumerator, IDisposable
        {
            IEnumerator<KeyValuePair<TKey, TValue>> list;

            public __Enumerator() : this(null) { }

            public __Enumerator(__Dictionary<TKey, TValue> e)
            {
                if (e == null)
                    return;

                var a = new global::System.Collections.Generic.List<KeyValuePair<TKey, TValue>>();

                var Keys = (ICollection<TKey>)e.Keys;

                // tested by 
                // X:\jsc.svn\examples\actionscript\Test\TestDictionaryOfTypeAndFunc\TestDictionaryOfTypeAndFunc\ApplicationCanvas.cs
                Func<TKey, KeyValuePair<TKey, TValue>> initobj =
                    xKey => new KeyValuePair<TKey, TValue>(xKey, e[xKey]);


                foreach (var Key in Keys)
                {
                    // Tested by X:\jsc.svn\examples\actionscript\Test\TestDictionaryKeys\TestDictionaryKeys\ApplicationCanvas.cs

                    var kv = initobj(Key);

                    // structs moving out out of scope must make a copy of themselves!
                    a.Add(kv);
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

            object __IEnumerator.Current
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



        #region ICollection Members

        public void CopyTo(global::System.Array array, int index)
        {
            throw new NotImplementedException();
        }

        public bool IsSynchronized
        {
            get { throw new NotImplementedException(); }
        }

        public object SyncRoot
        {
            get { throw new NotImplementedException(); }
        }

        #endregion


        //Error	13	'ScriptCoreLib.ActionScript.BCLImplementation.System.Collections.Generic.__Dictionary<TKey,TValue>' 
        // does not implement interface member 
        // 'ScriptCoreLib.Shared.BCLImplementation.System.Collections.__IEnumerable.GetEnumerator()'. '
        // ScriptCoreLib.ActionScript.BCLImplementation.System.Collections.Generic.__Dictionary<TKey,TValue>.GetEnumerator()' 
        // cannot implement 'ScriptCoreLib.Shared.BCLImplementation.System.Collections.__IEnumerable.GetEnumerator()' because 
        // it does not have the matching return type of 'System.Collections.IEnumerator'.	
        // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Collections\Generic\Dictionary.cs	12	20	ScriptCoreLib



        public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
        {
            // jsc needs to find the original sig!
            return (Dictionary<TKey, TValue>.Enumerator)(object)new __Enumerator(this);
        }

        IEnumerator<KeyValuePair<TKey, TValue>> __IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return (IEnumerator<KeyValuePair<TKey, TValue>>)(object)new __Enumerator(this);
        }



        IEnumerator __IEnumerable.GetEnumerator()
        {
            return (IEnumerator<KeyValuePair<TKey, TValue>>)(object)new __Enumerator(this);
        }
    }
}
