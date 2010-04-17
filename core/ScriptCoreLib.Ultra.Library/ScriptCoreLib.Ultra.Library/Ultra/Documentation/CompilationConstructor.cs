using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.Ultra.Documentation
{
	public class CompilationConstructor : CompilationMethodBase
	{
		internal const string __Element = "Constructor";
		internal const string __Name = "Name";



		public CompilationConstructor(CompilationType Context, XElement Data)
			: base(Context, Data)
		{

		}

		public IEnumerable<CompilationMethodParameter> GetParameters()
		{
			return this.Data.Elements(CompilationMethodParameter.__Element).Select(k => new CompilationMethodParameter(this, k));
		}
	}
}
