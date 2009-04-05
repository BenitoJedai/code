using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.Extensions
{
    [Script]
    public class DynamicContainer
    {
        public object Subject;

        [Script(OptimizedCode = "return subject[key];")]
        public static object GetValue(object subject, object key)
        {
            return default(object);
        }

        [Script(OptimizedCode = "subject[key] = value;")]
        public static void SetValue(object subject, object key, object value)
        {
        }

        public object this[object Key]
        {
            get
            {
                return GetValue(Subject, Key);
            }
            set
            {
                SetValue(Subject, Key, value);
            }
        }

		[Script]
		public class Delegates : DynamicContainer, IEnumerable<object>
		{
			public Delegates()
			{
				this.Subject = new object();

				// remember in actionscript the object is extendable aka dynamic
			}

			public void Add<T>(string name, Action<T> handler)
			{
				Add(name, (Delegate)handler);
			}

			public void Add<T, R>(string name, Converter<T, R> handler)
			{
				Add(name, (Delegate)handler);

			}
			public void Add(string name, Delegate handler)
			{
				this[name] = handler.ToFunction();
			}

			#region IEnumerable<object> Members

			public IEnumerator<object> GetEnumerator()
			{
				throw new NotImplementedException();
			}

			#endregion

			#region IEnumerable Members

			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			#endregion
		}
    }
}
