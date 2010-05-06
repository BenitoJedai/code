using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Studio
{
	public abstract class SolutionProjectLanguage
	{
		abstract public string ProjectFileExtension { get; }
		abstract public string CodeFileExtension { get; }

		abstract public string Kind { get; }

		abstract public void WriteCommentLine(SolutionFile File, string Text);
		abstract public void WriteXMLCommentLine(SolutionFile File, string Text);

		abstract public void WriteIndent(SolutionFile File);

		abstract public void WriteMethod(SolutionFile File, SolutionProjectLanguageMethod Method);
		abstract public void WriteCode(SolutionFile File, SolutionProjectLanguageCode Code);
		abstract public void WriteTypeName(SolutionFile File, SolutionProjectLanguageType Type);
		abstract public void WriteType(SolutionFile File, SolutionProjectLanguageType Type);

	}
}
