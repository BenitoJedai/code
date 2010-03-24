using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.Documentation
{
	public class CompilationMethod
	{
		internal const string __Method = "Method";
		internal const string __Name = "Name";

		public string Name { get; set; }

		public CompilationMethod(CompilationType Context, XElement Data)
		{
			this.Name = Data.Element(__Name).Value;

		}
	}
}
