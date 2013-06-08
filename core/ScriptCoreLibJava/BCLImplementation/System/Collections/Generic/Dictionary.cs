using ScriptCoreLib;
using ScriptCoreLib.Shared.BCLImplementation.System.Collections;
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
        // Implementation not found for type import :
        // System.Collections.Generic.Dictionary`2[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] :: Enumerator GetEnumerator()
        // Did you forget to add the [Script] attribute?
        // Please double check the signature!
        // type: ScriptCoreLib.Shared.BCLImplementation.System.Collections.Specialized.__StringDictionary offset: 0x0007  
        // method:System.Collections.IEnumerator GetEnumerator()
        //jsc.meta.Loader UnhandledException:


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
            public java.util.HashMap InternalDictionary;

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
                // tested by X:\jsc.svn\examples\javascript\android\WithClickOnceLANLauncher\WithClickOnceLANLauncher\Application.cs
                get { return this.InternalDictionary.size(); }
            }

            public bool IsReadOnly
            {
                get { throw new NotImplementedException(); }
            }

            public bool Remove(TKey item)
            {
                throw new NotImplementedException();
            }


            #region GetEnumerator
            [Script]
            class __iterator : IEnumerator<TKey>
            {
                public java.util.Iterator InternalIterator;

                public TKey Current
                {
                    get;
                    set;
                }

                public void Dispose()
                {
                    this.InternalIterator = null;
                }

                object IEnumerator.Current
                {
                    get { return this.Current; }
                }

                public bool MoveNext()
                {
                    if (this.InternalIterator.hasNext())
                    {
                        this.Current = (TKey)this.InternalIterator.next();

                        return true;
                    }

                    return false;
                }

                public void Reset()
                {
                    throw new NotImplementedException();
                }
            }

            public IEnumerator<TKey> GetEnumerator()
            {
                // tested by X:\jsc.svn\examples\java\Test\TestNameValueCollectionEnumerator\TestNameValueCollectionEnumerator\Program.cs

                return new __iterator { InternalIterator = this.InternalDictionary.keySet().iterator() };
            }

            global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
            #endregion

            void ICollection.CopyTo(Array array, int index)
            {
                throw new NotImplementedException();
            }

            int ICollection.Count
            {
                // tested by X:\jsc.svn\examples\javascript\android\WithClickOnceLANLauncher\WithClickOnceLANLauncher\Application.cs
                get { return this.Count; }
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
                return new __KeyCollection { InternalDictionary = this.InternalDictionary };
            }
        }


        ICollection<TKey> __IDictionary<TKey, TValue>.Keys
        {
            get { return Keys; }
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
            return this.InternalDictionary.containsKey((object)key);
        }



        public int Count
        {
            get { return this.InternalDictionary.size(); }
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

        global::System.Collections.IEnumerator ScriptCoreLib.Shared.BCLImplementation.System.Collections.__IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }


        public bool Remove(TKey key)
        {
            throw new NotImplementedException();
        }


        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new global::System.Exception("The method or operation is not implemented.");
        }

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20121001-solutionbuilderv1/20121024-linq


        //Error	6	'ScriptCoreLibJava.BCLImplementation.System.Collections.Generic.__Dictionary<TKey,TValue>' 
        // does not implement interface member 'ScriptCoreLib.Shared.BCLImplementation.System.Collections.Generic.__ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.Remove(System.Collections.Generic.KeyValuePair<TKey,TValue>)'
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Collections\Generic\Dictionary.cs	13	20	ScriptCoreLibJava




        public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
        {
            // jsc needs to find the original sig!
            return (Dictionary<TKey, TValue>.Enumerator)(object)new __Enumerator(this);
        }

        IEnumerator<KeyValuePair<TKey, TValue>> __IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return (IEnumerator<KeyValuePair<TKey, TValue>>)(object)new __Enumerator(this);
        }



        //IEnumerator __IEnumerable.GetEnumerator()
        //{
        //    return (IEnumerator<KeyValuePair<TKey, TValue>>)(object)new __Enumerator(this);
        //}


    }
}
