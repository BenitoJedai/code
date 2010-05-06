using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Studio
{
	public class SolutionProjectLanguageType
	{
		public string Name;

		public string Summary;

		public readonly List<SolutionProjectLanguageArgument> Arguments = new List<SolutionProjectLanguageArgument>();
	}
}
