using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.Documentation
{
	public abstract class CompilationMethodBase
	{
		protected XElement Data { get; set; }

		public IEnumerable<CompilationMethodParameter> GetParameters()
		{
			return this.Data.Elements(CompilationMethodParameter.__Element).Select(k => new CompilationMethodParameter(this, k));
		}
	}
}
