using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.Ultra.Documentation
{
	public class CompilationMethodParameter
	{
		internal const string __Element = "Parameter";
		internal const string __Name = "Name";

		public string Name { get; set; }

		public CompilationMethodParameter(CompilationMethodBase Context, XElement Data)
		{
			this.Name = Data.Element(__Name).Value;

		}
	}
}
