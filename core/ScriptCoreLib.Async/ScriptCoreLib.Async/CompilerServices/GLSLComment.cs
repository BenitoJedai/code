using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.CompilerServices
{
	public class GLSLComment
	{
        // https://www.khronos.org/registry/spir-v/papers/WhitePaper.html

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
