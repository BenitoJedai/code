using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Documentation;

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

		public override void WriteMethod(SolutionFile File, SolutionProjectLanguageMethod m)
		{
			var @params = m.Parameters.ToDictionary(
				k => k.Name,
				k => k.Summary
			);

			this.WriteComment(File, m.Summary, @params);

			this.WriteIndent(File);

			File.Write(SolutionFileTextFragment.Keyword, "public");
			File.Write(SolutionFileTextFragment.None, " ");
			File.Write(SolutionFileTextFragment.Keyword, "void");
			File.Write(SolutionFileTextFragment.None, " ");
			File.Write(SolutionFileTextFragment.None, m.Name);
			File.Write(SolutionFileTextFragment.None, "(");

			var Parameters = m.Parameters.ToArray();

			for (int i = 0; i < Parameters.Length; i++)
			{
				if (i > 0)
				{
					File.Write(SolutionFileTextFragment.None, ", ");
				}

				this.WriteType(File, Parameters[i].Type);

				File.Write(SolutionFileTextFragment.None, " ");
				File.Write(SolutionFileTextFragment.None, Parameters[i].Name);
			}

			File.Write(SolutionFileTextFragment.None, ")");
			File.WriteLine();

			this.WriteCode(File, m.Code);
		}

		public override void WriteCode(SolutionFile File, SolutionProjectLanguageCode Code)
		{
			// should this be an extension method to all languages?

			this.WriteIndent(File);
			File.WriteLine(SolutionFileTextFragment.None, "{");
			File.CurrentIndent++;

			// ? :)
			foreach (var item in Code.History.ToArray())
			{
				var Comment = item as string;

				if (Comment != null)
				{
					this.WriteIndent(File);
					this.WriteCommentLine(File, Comment);
				}
			}

			File.CurrentIndent--;
			this.WriteIndent(File);
			File.WriteLine(SolutionFileTextFragment.None, "}");
		}

		public override void WriteType(SolutionFile File, SolutionProjectLanguageType Type)
		{
			File.Write(SolutionFileTextFragment.Type, Type.Name);

			if (Type.Arguments.Count > 0)
			{
				File.Write(SolutionFileTextFragment.None, "<");

				var Arguments = Type.Arguments.ToArray();

				for (int i = 0; i < Arguments.Length; i++)
				{
					if (i > 0)
					{
						File.Write(SolutionFileTextFragment.None, ", ");
					}

					this.WriteType(File, Arguments[i].Type);
				}

				File.Write(SolutionFileTextFragment.None, ">");

			}
		}
	}
}
