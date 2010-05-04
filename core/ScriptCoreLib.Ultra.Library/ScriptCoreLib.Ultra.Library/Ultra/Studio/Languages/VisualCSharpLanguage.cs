using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Studio.Languages
{
	public class VisualCSharpLanguage : SolutionProjectLanguage
	{
		public override string ProjectFileExtension { get { return ".csproj"; } }
		public override string CodeFileExtension { get { return ".cs"; } }

		public override string Kind
		{
			get { return "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"; }
		}

		public override void WriteCommentLine(SolutionFile File, string Text)
		{
			File.WriteLine(SolutionFileTextFragment.Comment, "//" + Text);
		}

		public override void WriteXMLCommentLine(SolutionFile File, string Text)
		{
			// http://msdn.microsoft.com/en-us/library/aa288481(VS.71).aspx

			File.WriteLine(SolutionFileTextFragment.Comment, "/// " + Text);
		}

		public override void WriteIndent(SolutionFile File)
		{
			File.Write(SolutionFileTextFragment.None,
				"".PadLeft(
					File.CurrentIndent,
				// F# will use 4x spaces
					'\t'
				)
			);
		}
	}
}
