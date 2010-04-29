using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace XElementAsSerializer.Data
{
	public delegate void Class1Action(Class1 c);

	public class Class1
	{
		public string Foo;
		public string Bar;

		public static implicit operator XElement(Class1 that)
		{
			return new XElement("Class1",
				new XElement("Foo", that.Foo),
				new XElement("Bar", that.Bar)
			);
		}

		public static implicit operator Class1(XElement e)
		{
			return new Class1
			{
				Foo = e.Element("Foo").Value,
				Bar = e.Element("Bar").Value,
			};
		}
	}
}
