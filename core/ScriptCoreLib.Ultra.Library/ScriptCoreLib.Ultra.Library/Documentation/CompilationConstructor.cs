using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.Documentation
{
	public class CompilationConstructor : CompilationMethodBase
	{
		internal const string __Element = "Constructor";
		internal const string __Name = "Name";


		public CompilationType DeclaringType { get; private set; }

		public CompilationConstructor(CompilationType Context, XElement Data)
		{
			this.Data = Data;

			this.DeclaringType = Context;
		}

		public IEnumerable<CompilationMethodParameter> GetParameters()
		{
			return this.Data.Elements(CompilationMethodParameter.__Element).Select(k => new CompilationMethodParameter(this, k));
		}
	}
}
