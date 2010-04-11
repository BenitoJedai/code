﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.JavaScript.DOM.XML;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.XML.XLinq
{
	[Script(Implements = typeof(XElement))]
	internal class __XElement : __XContainer
	{

		public __XName Name
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public __XElement()
			: this(null)
		{
		}

		public __XElement(XName name)
		{
			InternalElementName = (__XName)(object)name;
		}

		public __XElement(XName name, object item)
		{
			InternalElementName = (__XName)(object)name;
			this.Add(item);
		}

		public __XElement(XName name, params object[] c)
		{
			InternalElementName = (__XName)(object)name;
			foreach (var item in c)
			{
				this.Add(item);
			}
		}


		public string Value
		{
			get
			{
				return this.InternalElement.text;
			}
			set
			{
				this.RemoveAll();
				this.Add(value);
			}
		}

		public void RemoveAll()
		{
			if (this.InternalElement == null)
				return;

			var p = this.InternalElement.firstChild;

			while (p != null)
			{
				this.InternalElement.removeChild(p);

				p = this.InternalElement.firstChild;
			}
		}

		public XAttribute Attribute(XName name)
		{
			var e = InternalElement;

			if (e.hasAttribute(name.LocalName))
			{
				return (XAttribute)(object)new __XAttribute(name, null) { InternalElement = this };

			}

			return null;
		}



	}
}
