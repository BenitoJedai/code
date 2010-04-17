using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.Ultra.Documentation
{
	public class CompilationMethod : CompilationMethodBase
	{
		internal const string __Element = "Method";
		internal const string __Name = "Name";

		public string Name { get; set; }


		public CompilationMethod(CompilationType Context, XElement Data) : base(Context, Data)
		{

			this.Name = Data.Element(__Name).Value;

		}


	}
}
