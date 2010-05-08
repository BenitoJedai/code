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

		public SolutionFileComment Header;

		public SolutionProjectLanguageType ElementType;
		public SolutionProjectLanguageType DeclaringType;

		public readonly List<string> UsingNamespaces = new List<string>();

		public readonly List<SolutionProjectLanguageArgument> Arguments = new List<SolutionProjectLanguageArgument>();

		public readonly List<SolutionProjectLanguageMethod> Methods = new List<SolutionProjectLanguageMethod>();

		public string FullName
		{
			get
			{
				var w = new StringBuilder();

				if (!string.IsNullOrEmpty(Namespace))
				{
					w.Append(Namespace);
					w.Append(".");
				}

				w.Append(Name);

				return w.ToString();
			}
		}

		public static implicit operator SolutionFileWriteArguments(SolutionProjectLanguageType Type)
		{
			return new SolutionFileWriteArguments
			{
				Fragment = SolutionFileTextFragment.Type,
				Tag = Type,
				Text = Type.Name
			};
		}
	}
}
