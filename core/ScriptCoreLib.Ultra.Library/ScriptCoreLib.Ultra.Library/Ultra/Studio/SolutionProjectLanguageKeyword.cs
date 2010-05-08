using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Studio
{
	public abstract class SolutionProjectLanguageKeyword
	{
		public readonly string Text;
		public SolutionProjectLanguageKeyword(string Text)
		{
			this.Text = Text;
		}
	}
}
