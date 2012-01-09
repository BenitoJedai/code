using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Library;
using System.Diagnostics;
using System.Reflection;

namespace jsc.Library
{
    [Obfuscation(Exclude = true)]
    public abstract class VirtualDictionaryBase
    {
        public abstract IDisposable ToTransientTransaction();

        public static VirtualDictionary<TKey, TValue> Create<TKey, TValue>(Func<TKey, TValue> handler)
        {
            var v = new VirtualDictionary<TKey, TValue>();

            v.Resolve +=
                e =>
                {
                    v[e] = handler(e);
                };

            return v;
        }
    }

    //class InternalTypeGUIDComparer : IEqualityComparer<Type>
    //{
    //    public bool Equals(Type x, Type y)
    //    {
    //        return GetHashCode(x) == GetHashCode(y);
    //    }

    //    public int GetHashCode(Type SourceType)
    //    {
    //        if (SourceType.IsGenericType)
    //            if (!SourceType.IsGenericTypeDefinition)
    //                return SourceType.GetHashCode();

    //        return SourceType.GUID.GetHashCode();
    //    }
    //}

    [Obfuscation(Exclude = true)]
    public class VirtualDictionary<TKey, TValue> : VirtualDictionaryBase
    {
        public Dictionary<TKey, TValue> BaseDictionary = new Dictionary<TKey, TValue>();

        // refactor to upper level type?
        public Dictionary<TKey, object> Flags = new Dictionary<TKey, object>();


        public event Action<TKey> Resolve;

        public object Tag;

        public VirtualDictionary()
        {
            //if (typeof(TKey) == typeof(Type))
            //{
            //    this.BaseDictionary = new Dictionary<TKey, TValue>((IEqualityComparer<TKey>)(object)new InternalTypeGUIDComparer());
            //}
        }

        public TValue[] this[TKey[] k]
        {
            [method: DebuggerStepThrough]
            get
            {
                return k.Select(kk => this[kk]).ToArray();
            }
        }


        public TValue this[TKey k]
        {
            //[method: DebuggerStepThrough]
            get
            {
                if (!BaseDictionary.ContainsKey(k))
                {
                    if (Resolve != null)
                        Resolve(k);
                }

                return BaseDictionary[k];
            }
            set
            {
                BaseDictionary[k] = value;
            }
        }

        public static implicit operator Func<TKey, TValue>(VirtualDictionary<TKey, TValue> e)
        {
            return k => e[k];
        }

        public override IDisposable ToTransientTransaction()
        {
            var BaseDictionary = this.BaseDictionary;
            var Flags = this.Flags;

            this.BaseDictionary = new Dictionary<TKey, TValue>(BaseDictionary);
            this.Flags = new Dictionary<TKey, object>(Flags);

            return (Disposable)
                delegate
                {
                    this.BaseDictionary = BaseDictionary;
                    this.Flags = Flags;
                };
        }
    }
}
