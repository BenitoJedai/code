using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Collections.Specialized
{
    [Script(Implements = typeof(global::System.Collections.Specialized.StringDictionary))]
    internal class __StringDictionary : IEnumerable
    {
        readonly Dictionary<string, string> InternalValue = new Dictionary<string, string>();


        public __StringDictionary()
        {

        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException("");
        }

        public virtual bool ContainsKey(string key)
        {
            return this.InternalValue.ContainsKey(key);
        }

        public virtual ICollection Keys
        {
            get
            {
                return InternalValue.Keys;
            }
        }

        public virtual string this[string key]
        {
            get
            {
                return InternalValue[key];
            }
            set
            {
                InternalValue[key] = value;
            }
        }

        public virtual int Count
        {
            get
            {
                return this.InternalValue.Count;
            }
        }

        public virtual void Add(string key, string value)
        {
            this.InternalValue.Add(key, value);
        }
    }

}
