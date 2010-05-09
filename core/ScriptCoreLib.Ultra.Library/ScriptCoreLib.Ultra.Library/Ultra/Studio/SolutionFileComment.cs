using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;

namespace ScriptCoreLib.Ultra.Studio
{
	public class SolutionFileComment
	{
		public string Comment;
		public Uri Link;

		public void WriteTo(SolutionFile File, SolutionProjectLanguage Language, SolutionBuilder Context)
		{
			if (this.IsActiveFilter != null)
				if (!this.IsActiveFilter(Context))
					return;

			if (Comment != null)
			{
				Language.WriteIndent(File);
				Language.WriteCommentLine(File, Comment);
			}
			if (Link != null)
			{
				Language.WriteIndent(File);
				Language.WriteLinkCommentLine(File, Link);
			}

			this.MarginBottom.Times(File.WriteLine);
		}

		public int MarginBottom;

		public Func<SolutionBuilder, bool> IsActiveFilter;
	}
}
