using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.CompilerServices
{
	public class GLSLMacroFragment
	{
		public StringBuilder NameStringBuilder;

		public List<StringBuilder> Arguments;

		public override string ToString()
		{
			if (this.Arguments != null)
				return "#define " + NameStringBuilder + "()";

			return "#define " + NameStringBuilder;
		}
	}
}
