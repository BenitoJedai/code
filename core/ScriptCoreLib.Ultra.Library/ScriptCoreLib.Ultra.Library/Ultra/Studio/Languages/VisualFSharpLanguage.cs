using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Studio.Languages
{
	public partial class VisualFSharpLanguage : SolutionProjectLanguage
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

		public override void WriteLinkCommentLine(SolutionFile File, Uri Link)
		{
			throw new NotImplementedException();
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

		public override void WriteMethod(SolutionFile File, SolutionProjectLanguageMethod Method, SolutionBuilder Context)
		{
			throw new NotImplementedException();
		}

		public override void WriteMethodBody(SolutionFile File, SolutionProjectLanguageCode Code, SolutionBuilder Context)
		{
			throw new NotImplementedException();
		}

		public override void WriteTypeName(SolutionFile File, SolutionProjectLanguageType Type)
		{
			throw new NotImplementedException();
		}

		public override void WriteType(SolutionFile File, SolutionProjectLanguageType Type, SolutionBuilder Context)
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

		public override void WritePseudoExpression(SolutionFile File, object Parameter)
		{
			throw new NotImplementedException();
		}

		public override void WritePseudoCallExpression(SolutionFile File, ScriptCoreLib.Ultra.Studio.PseudoExpressions.PseudoCallExpression Lambda)
		{
			throw new NotImplementedException();
		}
	}
}
