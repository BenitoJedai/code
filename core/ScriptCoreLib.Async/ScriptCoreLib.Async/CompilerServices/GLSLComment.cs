using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.CompilerServices
{
	public class GLSLComment
	{
		public bool IsLineComment;
		public bool IsBlockComment;

		public GLSLElement Parent;

		public StringBuilder ContentStringBuilder;


		public override string ToString()
		{
			return "// " + ContentStringBuilder;
		}
	}
}
