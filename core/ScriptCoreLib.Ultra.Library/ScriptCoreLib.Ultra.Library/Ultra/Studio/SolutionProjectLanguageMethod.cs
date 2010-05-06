using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Studio
{
	public sealed class SolutionProjectLanguageMethod
	{
		public string Name;

		public string Summary;

		public readonly List<SolutionProjectLanguageArgument> Parameters = new List<SolutionProjectLanguageArgument>();

		public SolutionProjectLanguageCode Code;
	}
}
