using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Studio
{
	public sealed class SolutionProjectLanguageArgument
	{
		public string Name;

		public string Summary;

		public SolutionProjectLanguageType Type;

        public static implicit operator SolutionProjectLanguageArgument(string Name)
        {
            return new SolutionProjectLanguageArgument { Name = Name };
        }
	}
}
