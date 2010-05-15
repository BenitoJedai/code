using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Studio
{
	public class SolutionProjectLanguagePartialType
	{
		/// <summary>
		/// If the primary type is Control then the partial name here
		/// could be Control.Designer.
		/// </summary>
		public string Name;

		public SolutionProjectLanguageType Type;
	}
}
