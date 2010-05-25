using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Collections;
using System.Collections.Specialized;

namespace ScriptCoreLibJava.BCLImplementation.System.Collections.Specialized
{
    [Script(Implements = typeof(global::System.Collections.Specialized.StringDictionary))]
    internal class __StringDictionary : IEnumerable
    {
        readonly NameValueCollection InternalValue = new NameValueCollection();


        public __StringDictionary()
        {

        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public virtual bool ContainsKey(string key)
        {
            var r = false;

            foreach (var item in this.InternalValue.AllKeys)
            {
                if (item == key)
                {
                    r = true;
                    break;
                }
            }

            return r;
        }

        public virtual ICollection Keys
        {
            get
            {
                var c = new ArrayList();

                foreach (var item in this.InternalValue.AllKeys)
                {
                    c.Add(item);
                }
                
                return c;
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
