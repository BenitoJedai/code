using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Collections.Generic
{
    // http://referencesource.microsoft.com/#mscorlib/system/collections/generic/keyvaluepair.cs

    [Script(Implements = typeof(global::System.Collections.Generic.KeyValuePair<,>))]
    internal class __KeyValuePair<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public __KeyValuePair()
            : this(default(TKey), default(TValue))
        {

        }

        // does this work for PHP?
        public __KeyValuePair(TKey Key, TValue Value)
        {
            this.Key = Key;
            this.Value = Value;
        }


    }
}
