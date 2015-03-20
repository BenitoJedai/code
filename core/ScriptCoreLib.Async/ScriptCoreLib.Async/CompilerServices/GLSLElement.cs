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

			this.AppendComment = (StringBuilder CommentStringBuilder, bool IsLineComment, bool IsBlockComment) =>
			{
				// we are null tolerant.
				if (CommentStringBuilder == null)
					return null;

				// should we add to this?

				// need a type?

				var value = new GLSLComment
				{
					IsLineComment = IsLineComment,
					IsBlockComment = IsBlockComment,

					Parent = this,

					ContentStringBuilder = CommentStringBuilder
				};

				this.Elements.Add(value);

				return value;
			};
		}

		public readonly Func<StringBuilder, bool, bool, GLSLComment> AppendComment;



		public override string ToString()
		{
			// we should be able to render the parsed code back by now.

			return "";
		}
	}
}
