using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Studio.Languages
{
	public partial class VisualBasicLanguage : SolutionProjectLanguage
	{
		public override string ProjectFileExtension
		{
			get { throw new NotImplementedException(); }
		}

		public override string CodeFileExtension
		{
			get { throw new NotImplementedException(); }
		}

		public override string Kind
		{
			get { throw new NotImplementedException(); }
		}

		public override void WriteCommentLine(SolutionFile File, string Text)
		{
			throw new NotImplementedException();
		}

		public override void WriteXMLCommentLine(SolutionFile File, string Text)
		{
			throw new NotImplementedException();
		}

		public override void WriteIndent(SolutionFile File)
		{
			throw new NotImplementedException();
		}

		public override void WriteMethod(SolutionFile File, SolutionProjectLanguageMethod Method)
		{
			throw new NotImplementedException();
		}

		public override void WriteCode(SolutionFile File, SolutionProjectLanguageCode Code)
		{
			throw new NotImplementedException();
		}

		public override void WriteTypeName(SolutionFile File, SolutionProjectLanguageType Type)
		{
			throw new NotImplementedException();
		}

		public override void WriteType(SolutionFile File, SolutionProjectLanguageType Type)
		{
			throw new NotImplementedException();
		}

		public override void WriteAssemblyAttribute(SolutionFile File, SolutionProjectLanguageAttribute Attribute)
		{
			throw new NotImplementedException();
		}

		public override void WriteUsingNamespace(SolutionFile File, string item)
		{
			throw new NotImplementedException();
		}
	}
}
