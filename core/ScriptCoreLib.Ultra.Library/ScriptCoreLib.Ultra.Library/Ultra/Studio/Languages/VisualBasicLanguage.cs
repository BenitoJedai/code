using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Studio.Languages
{
	public partial class VisualBasicLanguage : SolutionProjectLanguage
	{
		public override string ProjectFileExtension { get { return ".vbproj"; } }
		public override string CodeFileExtension { get { return ".vb"; } }

		public override string Kind
		{
			get { return "{F184B08F-C81C-45F6-A57F-5ABD9991F28F}"; }
		}

		public override void WriteCommentLine(SolutionFile File, string Text)
		{
			File.WriteLine(SolutionFileTextFragment.Comment, "' " + Text);
		}

		public override void WriteXMLCommentLine(SolutionFile File, string Text)
		{
			File.WriteLine(SolutionFileTextFragment.Comment, "''' " + Text);
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

		public override void WriteMethod(SolutionFile File, SolutionProjectLanguageMethod Method)
		{
		}

		public override void WriteCode(SolutionFile File, SolutionProjectLanguageCode Code)
		{
		}

		public override void WriteTypeName(SolutionFile File, SolutionProjectLanguageType Type)
		{
		}

		public override void WriteType(SolutionFile File, SolutionProjectLanguageType Type)
		{
		}

		public override void WriteAssemblyAttribute(SolutionFile File, SolutionProjectLanguageAttribute Attribute)
		{
		}

		public override void WriteUsingNamespace(SolutionFile File, string item)
		{
		}
	}
}
