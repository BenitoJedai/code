using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Studio
{
	public class SolutionProjectLanguageMethod
	{
		public const string ConstructorName = ".ctor";

		public bool IsConstructor
		{
			get
			{
				return this.Name == ConstructorName;
			}
		}

		public bool IsStatic;

		public string Name;

		public string Summary;

		public readonly List<SolutionProjectLanguageArgument> Parameters = new List<SolutionProjectLanguageArgument>();

		public SolutionProjectLanguageCode Code;

		public SolutionProjectLanguageType DeclaringType;

		public bool IsProperty;
	}
}
