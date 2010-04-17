using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.Ultra.Documentation
{
	public class CompilationField
	{
		internal const string _Field = "Field";
		internal const string __Name = "Name";

		public string Name { get; set; }

		public CompilationField(CompilationType Context, XElement Data)
		{
			this.Name = Data.Element(__Name).Value;

		}
	}
}
