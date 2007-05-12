using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(global::System.Collections.Generic.KeyValuePair<,>))]
    internal class __KeyValuePair<TKey, TValue>
    {
        private TKey _Key;

        public TKey Key
        {
            get { return _Key; }
            set { _Key = value; }
        }

        private TValue _Value;

        public TValue Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        public __KeyValuePair()
        {

        }

        public __KeyValuePair(TKey Key, TValue Value)
        {
            this._Key = Key;
            this._Value = Value;
        }
    }
}
