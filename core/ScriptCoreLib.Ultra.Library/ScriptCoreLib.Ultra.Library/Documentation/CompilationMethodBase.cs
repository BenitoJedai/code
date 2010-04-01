using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.Documentation
{
	public abstract class CompilationMethodBase : CompilationMember
	{
		public int MetadataToken { get; set; }

		public IEnumerable<CompilationMethodParameter> GetParameters()
		{
			return this.Data.Elements(CompilationMethodParameter.__Element).Select(k => new CompilationMethodParameter(this, k));
		}



		public CompilationMethodBase(CompilationType Context, XElement Data)
			: base(Context, Data)
		{
			this.MetadataToken = Convert.ToInt32(Data.Element(CompilationXNames.MetadataToken).Value);
		}
	}
}
