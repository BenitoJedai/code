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

		public bool IsAutoProperty
		{
			get
			{
				if (GetMethod == null)
					return false;

				if (GetMethod.Code != null)
					return false;

				if (SetMethod == null)
					return false;

				if (SetMethod.Code != null)
					return false;

				return true;
			}
		}
	}
}
