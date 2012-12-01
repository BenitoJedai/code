using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(IDictionary<,>))]
    public interface __IDictionary<TKey, TValue> : __ICollection<KeyValuePair<TKey, TValue>>, __IEnumerable<KeyValuePair<TKey, TValue>>, __IEnumerable
    {
        ICollection<TKey> Keys { get; }

        ICollection<TValue> Values { get; }

        TValue this[TKey key] { get; set; }

        void Add(TKey key, TValue value);

        bool ContainsKey(TKey key);

        bool Remove(TKey key);

        // ref-out not supported yet
        //bool TryGetValue(TKey key, out TValue value);


        // java cannot base type parameter type?
        //void __TypeReference(KeyValuePair<TKey, TValue> e);
    }


}
