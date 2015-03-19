using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScriptCoreLib.CompilerServices
{
	public class GLSLElement
	{
		// Observable ?
		// like XElement, XDocument


		public string SourcePath;

		public readonly List<object> Elements = new List<object>();

		public GLSLElement()
		{
			// are we multithreaded?
			var scope = new { Thread.CurrentThread.ManagedThreadId };

			this.AppendLineComment = (StringBuilder xLineCommentStringBuilder) =>
			{
				// we are null tolerant.
				if (xLineCommentStringBuilder == null)
					return null;

				// should we add to this?

				// need a type?

				var value = new GLSLLineComment
				{
					Parent = this,

					ContentStringBuilder = xLineCommentStringBuilder
				};

				this.Elements.Add(value);

				return value;
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
