using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;

namespace ScriptCoreLib.Ultra.Studio
{
	public abstract class SolutionProjectLanguage
	{
		abstract public string ProjectFileExtension { get; }
		abstract public string CodeFileExtension { get; }

		// http://msdn.microsoft.com/en-us/library/envdte.project.kind(VS.80).aspx
		// http://www.mztools.com/articles/2008/MZ2008017.aspx

		abstract public string Kind { get; }

		abstract public void WriteLinkCommentLine(SolutionFile File, Uri Link);
		abstract public void WriteCommentLine(SolutionFile File, string Text);
		abstract public void WriteXMLCommentLine(SolutionFile File, string Text);

		abstract public void WriteIndent(SolutionFile File);

		abstract public void WriteMethod(SolutionFile File, SolutionProjectLanguageMethod Method);
		abstract public void WriteMethodBody(SolutionFile File, SolutionProjectLanguageCode Code);
		abstract public void WriteTypeName(SolutionFile File, SolutionProjectLanguageType Type);
		abstract public void WriteType(SolutionFile File, SolutionProjectLanguageType Type);

		abstract public void WriteAssemblyAttribute(SolutionFile File, SolutionProjectLanguageAttribute Attribute);
		abstract public void WriteUsingNamespace(SolutionFile File, string item);

		abstract public void WritePseudoExpression(SolutionFile File, object Parameter);
		abstract public void WritePseudoCallExpression(SolutionFile File, PseudoCallExpression Lambda);


		public class Keyword : SolutionFileWriteArguments
		{
			public static implicit operator Keyword(string Text)
			{
				return new Keyword
				{
					Fragment = SolutionFileTextFragment.Keyword,
					Text = Text
				};
			}
		}
	}
}
