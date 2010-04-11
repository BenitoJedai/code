using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CreatingXElements
{
	class DocumentBuilder
	{
		public static XElement Create()
		{
			return new XElement("Document",
				new XAttribute("foo", "bar"),
				new XAttribute("foo2", "bar"),
				new XElement("Child") { Value = "Hello world" },
				new XElement("Child", "hello"),
				new XElement("Child",
					"hello", "world", 
					new XElement("Kenny",
						new XAttribute("foo", "bar"),
						new XAttribute("foo2", "bar"),
						"they killed kenny"
					)
				)
			);
		}
	}
}
