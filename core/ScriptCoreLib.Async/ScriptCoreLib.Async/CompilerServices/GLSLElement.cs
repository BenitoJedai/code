using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.CompilerServices
{
	public class GLSLElement
	{
		// Observable ?
		// like XElement, XDocument


		public string SourcePath;

		public GLSLElement()
		{

			this.AppendLineComment = (StringBuilder xLineCommentStringBuilder) =>
			{
				// we are null tolerant.
				if (xLineCommentStringBuilder == null)
					return null;

				// should we add to this?

				// need a type?

				return new GLSLLineComment
				{
					Parent = this,

					ContentStringBuilder = xLineCommentStringBuilder
				};
			};
		}

		public readonly Func<StringBuilder, GLSLLineComment> AppendLineComment;



		public override string ToString()
		{
			// we should be able to render the parsed code back by now.

			return "";
		}
	}
}
