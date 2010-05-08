using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Studio
{
	public class SolutionFileComment
	{
		public string Comment;
		public Uri Link;

		public void WriteTo(SolutionFile File, SolutionProjectLanguage Language)
		{
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

		}

		
	}
}
