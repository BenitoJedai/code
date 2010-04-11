using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.XML.XLinq
{

	[Script(Implements = typeof(XName))]
	internal class __XName
	{
		internal string InternalValue;

		public string LocalName
		{
			get
			{
				return InternalValue;
			}
		}
		public override string ToString()
		{
			return LocalName;
		}

		public static implicit operator __XName(string e)
		{
			return new __XName { InternalValue = e };
		}

		public static XName Get(string localName, string namespaceName)
		{
			return (XName)(object)new __XName { InternalValue = localName };
		}



	}
}
