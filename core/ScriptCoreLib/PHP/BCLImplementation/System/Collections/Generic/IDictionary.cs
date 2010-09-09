using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(IDictionary<,>))]
    internal interface __IDictionary<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
    {
        ICollection<TKey> Keys { get; }

        ICollection<TValue> Values { get; }

        TValue this[TKey key] { get; set; }

        void Add(TKey key, TValue value);

        bool ContainsKey(TKey key);

        bool Remove(TKey key);

        bool TryGetValue(TKey key, out TValue value);
    }
}
