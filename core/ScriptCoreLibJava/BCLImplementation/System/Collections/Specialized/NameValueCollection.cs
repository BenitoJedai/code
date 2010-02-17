using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.Collections.Specialized
{
	[Script(Implements = typeof(global::System.Collections.Specialized.NameValueCollection))]
	internal class __NameValueCollection : __NameObjectCollectionBase
	{
		// http://www.jguru.com/faq/view.jsp?EID=430247
		readonly java.util.HashMap InternalCollection = new java.util.HashMap();

		public virtual void Add(string name, string value)
		{
			InternalCollection.put(name, value);
		}

		public virtual string[] AllKeys
		{
			get
			{
				var x = InternalCollection.keySet();

				return (string[])x.toArray(new string[x.size()]);
			}
		}
		public string this[string name]
		{
			get
			{
				return (string)InternalCollection.get(name);
			}
			set
			{
				InternalCollection.put(name, value);
			}
		}

		public virtual void Clear()
		{
			InternalCollection.clear();
		}
	}
}
