using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Studio
{
	public class SolutionProjectLanguageProperty
	{
		public bool IsStatic;
		public SolutionProjectLanguageType PropertyType;
		public string Name;

		public SolutionProjectLanguageMethod GetMethod;
		public SolutionProjectLanguageMethod SetMethod;
	}
}
