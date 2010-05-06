using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Studio
{
	public class SolutionProjectLanguageType
	{
		public bool IsStatic;
		public bool IsSealed;

		public string Namespace;

		public string Name;

		public string Summary;

		public string Comment;

		public SolutionProjectLanguageType ElementType;

		public readonly List<string> UsingNamespaces = new List<string>();

		public readonly List<SolutionProjectLanguageArgument> Arguments = new List<SolutionProjectLanguageArgument>();

		public readonly List<SolutionProjectLanguageMethod> Methods = new List<SolutionProjectLanguageMethod>();
	}
}
