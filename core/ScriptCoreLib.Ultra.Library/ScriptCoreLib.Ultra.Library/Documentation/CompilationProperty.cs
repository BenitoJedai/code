using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.Documentation
{
	public class CompilationProperty
	{
		internal const string __Element = "Property";
		internal const string __Name = "Name";

		public string Name { get; set; }

		public CompilationProperty(CompilationType Context, XElement Data)
		{
			this.Name = Data.Element(__Name).Value;
		}
	}
}
