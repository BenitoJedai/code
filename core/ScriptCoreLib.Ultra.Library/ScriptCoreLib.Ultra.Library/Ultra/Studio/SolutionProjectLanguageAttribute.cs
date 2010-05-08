using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Studio
{
	public class SolutionProjectLanguageAttribute
	{
		public SolutionProjectLanguageType Type;

		public object[] Parameters;

		public Dictionary<SolutionProjectLanguageMethod, object> Properties;
	}
}
