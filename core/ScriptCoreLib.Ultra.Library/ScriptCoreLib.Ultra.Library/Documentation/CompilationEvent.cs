using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.Documentation
{
	public class CompilationEvent
	{
		internal const string __Element = "Event";
		internal const string __Name = "Name";

		public string Name { get; set; }

		public CompilationEvent(CompilationType Context, XElement Data)
		{
			this.Name = Data.Element(__Name).Value;
		}
	}
}
